using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchString
{
    /// <summary>
    /// 处理多个大文件（每个1-2GB）中的URL并统计重复最多的前10个，可以采用 分块处理+哈希统计+堆排序 的策略
    /// </summary>
    //public class UrlCounter
    //{
    //    public static async Task<List<(string Url, int Count)>> FindTop10DuplicatesAsync(IEnumerable<string> filePaths)
    //    {
    //        // 分块读取文件并合并统计
    //        //2.字典统计（O(1)时间复杂度）
    //        //Dictionary<string, int> 快速统计URL出现次数。
    //        var urlCounts = new Dictionary<string, int>();

    //        foreach (var filePath in filePaths)
    //        {
    //            await foreach (var url in ReadLinesChunkedAsync(filePath))
    //            {
    //                urlCounts[url] = urlCounts.TryGetValue(url, out var count) ? count + 1 : 1;
    //            }
    //        }

    //        //3.最小堆筛选Top10（O(n log k)）
    //        //维护大小为10的最小堆，避免全量排序。

    //        // 使用最小堆找出Top10
    //        var minHeap = new SortedSet<(string Url, int Count)>(
    //            Comparer<(string Url, int Count)>.Create((x, y) =>
    //                x.Count == y.Count ? string.CompareOrdinal(x.Url, y.Url) : x.Count.CompareTo(y.Count))
    //        );

    //        foreach (var pair in urlCounts)
    //        {
    //            minHeap.Add((pair.Key, pair.Value));
    //            if (minHeap.Count > 10)
    //                minHeap.Remove(minHeap.Min);
    //        }

    //        return minHeap.OrderByDescending(x => x.Count).ToList();
    //    }

    //    private static async IAsyncEnumerable<string> ReadLinesChunkedAsync(string filePath)
    //    {
    //        //1.流式读取（内存高效）
    //        ////使用 StreamReader 逐行读取，避免一次性加载整个文件。
    //        ////异步迭代器(IAsyncEnumerable) 减少线程阻塞。
    //        const int bufferSize = 8192; // 8KB缓冲区
    //        using var reader = new StreamReader(filePath, bufferSize: bufferSize);
    //        //4.并行处理（可选扩展）
    //        //如需进一步加速，可并行处理多个文件：

    //        //var parallelResults = await Task.WhenAll(
    //        //    filePaths.Select(file => Task.Run(() => CountUrlsInFileAsync(file)))
    //        //);
    //        while (!reader.EndOfStream)
    //        {
    //            var line = await reader.ReadLineAsync();
    //            if (!string.IsNullOrWhiteSpace(line))
    //                yield return line.Trim();
    //        }
    //    }
    //}

    public class ParallelUrlCounter
    {
        public static async Task<List<(string Url, int Count)>> FindTop10DuplicatesAsync(IEnumerable<string> filePaths)
        {
            // 线程安全的并发字典
            var urlCounts = new ConcurrentDictionary<string, int>();

            // 并行处理所有文件（每个文件独立任务）
            await Task.WhenAll(
                filePaths.Select(filePath =>
                    Task.Run(async () =>
                    {
                        await foreach (var url in ReadLinesChunkedAsync(filePath))
                        {
                            urlCounts.AddOrUpdate(url, 1, (_, count) => count + 1);
                        }
                    })
                )
            );

            // 使用最小堆找出Top10
            var minHeap = new SortedSet<(string Url, int Count)>(
                Comparer<(string Url, int Count)>.Create((x, y) =>
                    x.Count == y.Count ? string.CompareOrdinal(x.Url, y.Url) : x.Count.CompareTo(y.Count))
            );

            foreach (var pair in urlCounts)
            {
                minHeap.Add((pair.Key, pair.Value));
                if (minHeap.Count > 10)
                    minHeap.Remove(minHeap.Min);
            }

            return minHeap.OrderByDescending(x => x.Count).ToList();
        }

        private static async IAsyncEnumerable<string> ReadLinesChunkedAsync(string filePath)
        {
            const int bufferSize = 8192; // 8KB缓冲区          
            using var reader = new StreamReader(filePath, System.Text.Encoding.ASCII, false, bufferSize: bufferSize);

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (!string.IsNullOrWhiteSpace(line))
                    yield return line.Trim();
            }
        }
    }

}