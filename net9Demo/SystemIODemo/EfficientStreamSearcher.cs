using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net9Demo.SystemIODemo
{
    public class EfficientStreamSearcher
    {
        private readonly string _filePath;
        private readonly int _chunkSize;
        private readonly int _overlapSize;
        private readonly Encoding _encoding;

        public EfficientStreamSearcher(string filePath, int chunkSize = 1024 * 1024,
                                     int overlapSize = 100, Encoding encoding = null)
        {
            _filePath = filePath;
            _chunkSize = chunkSize;
            _overlapSize = overlapSize;
            _encoding = encoding ?? Encoding.UTF8;
        }

        public IEnumerable<long> SearchKeyword(string keyword)
        {
            var fileInfo = new FileInfo(_filePath);
            long fileLength = fileInfo.Length;
            int keywordLength = keyword.Length;
            int effectiveOverlap = Math.Max(_overlapSize, keywordLength);

            using (var mmf = MemoryMappedFile.CreateFromFile(_filePath, FileMode.Open))
            {
                long position = 0;
                int chunkIndex = 0;
                char[] overlapBuffer = new char[0];

                while (position < fileLength)
                {
                    // 计算读取范围
                    long readStart = position;
                    long readLength = Math.Min(_chunkSize + effectiveOverlap, fileLength - position);

                    using (var stream = mmf.CreateViewStream(readStart, readLength, MemoryMappedFileAccess.Read))
                    using (var reader = new StreamReader(stream, _encoding, false, 1024, true))
                    {
                        // 读取整个分片
                        string currentChunk = reader.ReadToEnd();

                        // 拼接重叠部分
                        string searchText = chunkIndex == 0 ? currentChunk : new string(overlapBuffer) + currentChunk;

                        // 搜索关键词
                        foreach (var result in SearchInChunk(searchText, keyword, readStart, chunkIndex, overlapBuffer.Length))
                        {
                            yield return result;
                        }

                        // 更新重叠缓冲区
                        if (currentChunk.Length >= effectiveOverlap)
                        {
                            overlapBuffer = currentChunk.Substring(currentChunk.Length - effectiveOverlap).ToCharArray();
                        }
                        else
                        {
                            overlapBuffer = currentChunk.ToCharArray();
                        }
                    }

                    position += _chunkSize;
                    chunkIndex++;
                }
            }
        }

        private IEnumerable<long> SearchInChunk(string text, string keyword, long basePosition, int chunkIndex, int overlapLength)
        {
            int foundIndex = 0;

            while (foundIndex >= 0)
            {
                foundIndex = text.IndexOf(keyword, foundIndex, StringComparison.Ordinal);
                if (foundIndex >= 0)
                {
                    // 调整位置：减去重叠部分的偏移
                    long absolutePosition = basePosition + foundIndex - (chunkIndex > 0 ? overlapLength : 0);
                    yield return absolutePosition;
                    foundIndex += keyword.Length;
                }
            }
        }

        public async Task Method(string[] args)
        {
            string filePath = @"C:\largefile.txt";
            string keyword = "错误"; // 支持中文等多字节字符

            var searcher = new EfficientStreamSearcher(
                filePath,
                chunkSize: 1024 * 1024,
                overlapSize: 100,
                encoding: Encoding.UTF8
            );

            var results = searcher.SearchKeyword(keyword);

            foreach (var result in results)
            {
                //Console.WriteLine($"在位置 {result.Position} 找到关键词 '{result.Keyword}'");
            }
        }
    }
}
