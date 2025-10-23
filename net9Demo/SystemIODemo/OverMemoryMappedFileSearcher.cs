using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net9Demo.SystemIODemo
{
    /// <summary>
    /// 超过2g大型文件的分片
    /// </summary>
    internal class OverMemoryMappedFileSearcher
    {
        private readonly string _filePath;
        private readonly int _chunkSize;
        private readonly int _overlapSize;
        private readonly Encoding _encoding;

        public OverMemoryMappedFileSearcher(string filePath, int chunkSize = 1024 * 1024,
                                              int overlapSize = 100, Encoding encoding = null)
        {
            _filePath = filePath;
            _chunkSize = chunkSize;
            _overlapSize = Math.Min(overlapSize, chunkSize / 2);
            _encoding = encoding ?? Encoding.UTF8;
        }
        public async Task<List<SearchResult>> SearchKeywordAsync(string keyword)
        {
            var results = new List<SearchResult>();
            var fileInfo = new FileInfo(_filePath);
            long fileLength = fileInfo.Length;

            int maxKeywordLength = keyword.Length;
            int effectiveOverlap = Math.Max(_overlapSize, maxKeywordLength);

            // 关键：定义视图大小限制
            long maxViewSize = 1024L * 1024 * 1024; // 1GB，避免达到系统限制

            using (var mmf = MemoryMappedFile.CreateFromFile(_filePath, FileMode.Open))
            {
                long position = 0;
                int chunkIndex = 0;

                while (position < fileLength)
                {
                    // 计算当前视图的实际大小（考虑系统限制）
                    long viewStart = Math.Max(0, position - (chunkIndex > 0 ? effectiveOverlap : 0));
                    long desiredViewEnd = Math.Min(position + _chunkSize + effectiveOverlap, fileLength);

                    // 确保视图大小不超过系统限制
                    long actualViewSize = Math.Min(desiredViewEnd - viewStart, maxViewSize);
                    long actualViewEnd = viewStart + actualViewSize;

                    using (var accessor = mmf.CreateViewAccessor(viewStart, actualViewSize, MemoryMappedFileAccess.Read))
                    {
                        byte[] buffer = new byte[actualViewSize];
                        accessor.ReadArray(0, buffer, 0, buffer.Length);
                        string searchText = _encoding.GetString(buffer);

                        var chunkResults = SearchWithOverlapCounting(searchText, keyword, viewStart, chunkIndex, effectiveOverlap);
                        results.AddRange(chunkResults);
                    }

                    // 关键：计算下一个位置时，要考虑重叠区域
                    // 不是简单的 position += _chunkSize
                    if (actualViewEnd >= fileLength)
                    {
                        break; // 已经处理完文件
                    }

                    // 重要：下一个分片的起始位置 = 当前视图结束位置 - 重叠区域
                    // 这样可以确保连续性，同时避免重复
                    long nextChunkStart = actualViewEnd - effectiveOverlap;

                    // 确保不会回退
                    position = Math.Max(position + _chunkSize, nextChunkStart);
                    chunkIndex++;
                }
            }
            return results;
        }
    }
}
