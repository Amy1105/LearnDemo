using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net9Demo.SystemIODemo
{
    /// <summary>
    /// 更完善的处理边界情况
    /// </summary>
    public class AdvancedMemoryMappedFileSearcher
    {
        private readonly string _filePath;
        private readonly int _chunkSize;
        private readonly int _overlapSize;
        private readonly Encoding _encoding;

        public AdvancedMemoryMappedFileSearcher(string filePath, int chunkSize = 1024 * 1024,
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

            // 计算最大关键词长度用于重叠区域
            int maxKeywordLength = keyword.Length;
            int effectiveOverlap = Math.Max(_overlapSize, maxKeywordLength);

            using (var mmf = MemoryMappedFile.CreateFromFile(_filePath, FileMode.Open))
            {
                long position = 0;
                int chunkIndex = 0;
                string previousChunkTail = string.Empty;

                while (position < fileLength) //开始位置小于文件总长度就可以一直循环
                {                  
                    // 分片起始位置：第一次position是0，第二次就需要 上次的起始位置 减去 effectiveOverlap
                    long chunkStart = Math.Max(0, position - (chunkIndex > 0 ? effectiveOverlap : 0));

                    //分片结束地址：起始位置+固定分片尺寸+重复区域；或最后一节是文件的总长度
                    long chunkEnd = Math.Min(position + _chunkSize + effectiveOverlap, fileLength);
                    long chunkSize = chunkEnd - chunkStart;

                    using (var accessor = mmf.CreateViewAccessor(chunkStart, chunkSize, MemoryMappedFileAccess.Read))
                    {
                        byte[] buffer = new byte[chunkSize];
                        accessor.ReadArray(0, buffer, 0, buffer.Length);

                        string searchText = _encoding.GetString(buffer);                       

                        // 搜索关键词
                        var chunkResults = SearchInText2(searchText, keyword, chunkStart, chunkIndex, _overlapSize);
                        results.AddRange(chunkResults);                        
                    }
                    position += _chunkSize;
                    chunkIndex++;
                }
            }
            return results;
        }

        private List<SearchResult> SearchInText(string text, string keyword, long basePosition, int chunkIndex)
        {
            var results = new List<SearchResult>();
            int foundIndex = 0;
            while (foundIndex >= 0)
            {
                foundIndex = text.IndexOf(keyword, foundIndex, StringComparison.Ordinal);
                if (foundIndex >= 0)
                {
                    long absolutePosition = basePosition + foundIndex;
                    results.Add(new SearchResult
                    {
                        Position = absolutePosition,
                        ChunkIndex = chunkIndex,
                        Keyword = keyword
                    });
                    foundIndex += keyword.Length;
                }
            }
            return results;
        }

        private List<SearchResult> SearchInText2(string text, string keyword, long basePosition, int chunkIndex, int overlapSize)
        {
            var results = new List<SearchResult>();
            int foundIndex = 0;

            // 如果不是第一个分片，需要跳过重叠区域（避免重复搜索）
            int startSearchFrom = chunkIndex > 0 ? overlapSize : 0;

            while (foundIndex >= 0)
            {
                foundIndex = text.IndexOf(keyword, startSearchFrom, StringComparison.Ordinal);
                if (foundIndex >= 0)
                {
                    long absolutePosition = basePosition + foundIndex;
                    results.Add(new SearchResult
                    {
                        Position = absolutePosition,
                        ChunkIndex = chunkIndex,
                        Keyword = keyword
                    });
                    foundIndex += keyword.Length;
                    startSearchFrom = foundIndex; // 更新搜索起始位置
                }
            }

            return results;
        }

        /// <summary>
        /// 调用方法
        /// </summary>
        /// <returns></returns>
        public async Task Mehod()
        {
            string filePath = @"C:\largefile.txt";
            string keyword = "Error";

            var searcher = new AdvancedMemoryMappedFileSearcher(
                filePath,
                chunkSize: 1024 * 1024, // 1MB chunks
                overlapSize: 100        // 100字符重叠区域
            );

            var results = await searcher.SearchKeywordAsync(keyword);

            Console.WriteLine($"找到 {results.Count} 个匹配项:");
            foreach (var result in results)
            {
                Console.WriteLine($"位置: {result.Position}, 分片: {result.ChunkIndex}");
            }
        }

    }


    public class SearchResult
    {
        public long Position { get; set; }
        public int ChunkIndex { get; set; }
        public string Keyword { get; set; }
    }
}
