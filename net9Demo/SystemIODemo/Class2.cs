using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net9Demo.SystemIODemo
{
    internal class Class2
    {

        public async Task<List<SearchResult>> SearchKeywordAsync(string keyword)
        {
            var results = new List<SearchResult>();
            var fileInfo = new FileInfo(_filePath);
            long fileLength = fileInfo.Length;

            int maxKeywordLength = keyword.Length;
            int effectiveOverlap = Math.Max(_overlapSize, maxKeywordLength);

            using (var mmf = MemoryMappedFile.CreateFromFile(_filePath, FileMode.Open))
            {
                long position = 0;
                int chunkIndex = 0;

                while (position < fileLength)
                {
                    // 计算包含重叠区域的读取范围
                    long chunkStart = Math.Max(0, position - (chunkIndex > 0 ? effectiveOverlap : 0));
                    long chunkEnd = Math.Min(position + _chunkSize + effectiveOverlap, fileLength);
                    long chunkSize = chunkEnd - chunkStart;

                    using (var stream = mmf.CreateViewStream(chunkStart, chunkSize, MemoryMappedFileAccess.Read))
                    using (var reader = new StreamReader(stream, _encoding, detectEncodingFromByteOrderMarks: false))
                    {
                        string chunkText = await reader.ReadToEndAsync();

                        // 直接搜索，不需要手动拼接
                        var chunkResults = SearchInChunk(chunkText, keyword, chunkStart, chunkIndex, effectiveOverlap);
                        results.AddRange(chunkResults);
                    }

                    position += _chunkSize;
                    chunkIndex++;
                }
            }

            return results;
        }

        private List<SearchResult> SearchInChunk(string chunkText, string keyword, long basePosition, int chunkIndex, int overlapSize)
        {
            var results = new List<SearchResult>();
            int startIndex = 0;

            // 如果不是第一个分片，跳过重叠区域（避免重复计数）
            if (chunkIndex > 0)
            {
                startIndex = overlapSize;
            }

            int foundIndex = chunkText.IndexOf(keyword, startIndex, StringComparison.Ordinal);

            while (foundIndex >= 0)
            {
                long absolutePosition = basePosition + foundIndex;
                results.Add(new SearchResult
                {
                    Position = absolutePosition,
                    ChunkIndex = chunkIndex,
                    Keyword = keyword
                });

                foundIndex = chunkText.IndexOf(keyword, foundIndex + 1, StringComparison.Ordinal);
            }

            return results;
        }


        public async Task<List<SearchResult>> SearchKeyword1Async(string keyword)
        {
            var results = new List<SearchResult>();
            var fileInfo = new FileInfo(_filePath);
            long fileLength = fileInfo.Length;

            int effectiveOverlap = Math.Max(_overlapSize, keyword.Length);

            using (var mmf = MemoryMappedFile.CreateFromFile(_filePath, FileMode.Open))
            {
                long position = 0;
                int chunkIndex = 0;
                string previousTail = string.Empty;

                while (position < fileLength)
                {
                    // 不包含重叠区域的正常分片
                    long chunkSize = Math.Min(_chunkSize, fileLength - position);

                    using (var stream = mmf.CreateViewStream(position, chunkSize, MemoryMappedFileAccess.Read))
                    using (var reader = new StreamReader(stream, _encoding, false))
                    {
                        string chunkText = await reader.ReadToEndAsync();

                        // 手动拼接
                        string searchText = chunkIndex > 0 ? previousTail + chunkText : chunkText;

                        // 搜索时调整位置
                        var chunkResults = SearchInText1(searchText, keyword, position, chunkIndex, previousTail.Length);
                        results.AddRange(chunkResults);

                        // 保存尾部用于下一个分片
                        previousTail = chunkText.Length > effectiveOverlap
                            ? chunkText.Substring(chunkText.Length - effectiveOverlap)
                            : chunkText;
                    }

                    position += _chunkSize;
                    chunkIndex++;
                }
            }

            return results;
        }

        private List<SearchResult> SearchInText1(string searchText, string keyword, long basePosition, int chunkIndex, int tailLength)
        {
            var results = new List<SearchResult>();
            int startIndex = chunkIndex > 0 ? tailLength : 0; // 跳过拼接部分的重叠区域
            int foundIndex = searchText.IndexOf(keyword, startIndex, StringComparison.Ordinal);

            while (foundIndex >= 0)
            {
                long absolutePosition = basePosition + foundIndex - (chunkIndex > 0 ? tailLength : 0);
                results.Add(new SearchResult
                {
                    Position = absolutePosition,
                    ChunkIndex = chunkIndex,
                    Keyword = keyword
                });

                foundIndex = searchText.IndexOf(keyword, foundIndex + 1, StringComparison.Ordinal);
            }

            return results;
        }


    }
}
