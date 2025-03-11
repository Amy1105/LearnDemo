using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace doNetLearn.DataStructures
{
    /// <summary>
    /// span 结构学习
    /// </summary>
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class SpanBenchmark
    {

        //比如：字符串拆分和数组类型转换
        //const string StringForSplit = "666,747,200,468,471,395,942,589,87,353,456,536,772,599,552,338,553,925,532,383,668,96,61,125,621,917,774,146,54,885";

        //[Benchmark]
        //public int[] TestSplitString()
        //{
        //    return StringForSplit.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
        //}

        //[Benchmark]
        //public int[] TestSplitStringWithSpan()
        //{
        //    var span = StringForSplit.AsSpan();
        //    var separator = ",".AsSpan();
        //    var sepLen = separator.Length;
        //    var index = -1;
        //    var result = new List<int>();
        //    do
        //    {
        //        //搜索指定的序列并返回其第一次出现的索引。如果未找到，则返回-1。使用IEquatable{T}比较值。等于（T）。
        //        index = span.IndexOf(separator);
        //        if (index == -1)
        //        {
        //            result.Add(int.Parse(span));
        //        }
        //        else
        //        {
        //            var value = span.Slice(0, index);
        //            result.Add(int.Parse(value));
        //            span = span.Slice(index + sepLen);
        //        }
        //    }
        //    while (index != -1);
        //    return result.ToArray();
        //}



        //从 HTML 代码中提取文本内容              
        //        const string HtmlCode = @"<html>
        //<head>
        //    <meta charset=""utf-8"">
        //    <title>Test Page</title>
        //</head>
        //<body>
        //    <header>...</header>
        //    <main>
        //        <article>
        //            <h3>Country list</h3>
        //            <ul>
        //                <li><span>Australia</span></li>
        //                <li><span>Brazil</span></li>
        //                <li><span>Canada</span></li>
        //                <li><span>China</span></li>
        //                <li><span>France</span></li>
        //                <li><span>Germany</span></li>
        //                <li><span>Japan</span></li>
        //                <li><span>South Korea</span></li>
        //                <li><span>United States</span></li>
        //                <li><span>United Kingdom</span></li>
        //            </ul>
        //        </article>
        //    </main>
        //    <footer>...</footer>
        //</body>
        //</html>";

        //[Benchmark]
        //public string[] TestFilterWithRegularExpression()
        //{
        //    const string REGX_PTN = @"<li><span>(.*)<\/span><\/li>";
        //    var matches = Regex.Matches(HtmlCode, REGX_PTN);
        //    var result = new List<string>();
        //    foreach (Match match in matches)
        //    {
        //        result.Add(match.Groups[1].Value);
        //    }
        //    return result.ToArray();
        //}

        //[Benchmark]
        //public string[] TestFilterWithSpan()
        //{
        //    const string countryBegin = "<h3>Country list</h3>";
        //    const string countryEnd = "</ul>";
        //    const string startTag = "<li><span>";
        //    const string endTag = "</span>";

        //    var span = HtmlCode.AsSpan();
        //    var countrySpan = countryBegin.AsSpan();
        //    int index = span.IndexOf(countrySpan);

        //    span = span.Slice(index + countrySpan.Length);
        //    index = span.IndexOf(countryEnd.AsSpan());
        //    span = span.Slice(0, index);

        //    var startTagSpan = startTag.AsSpan();
        //    var endTagSpan = endTag.AsSpan();
        //    var startTagLen = startTagSpan.Length;
        //    var endTagLen = endTagSpan.Length;
        //    var result = new List<string>();
        //    while (true)
        //    {
        //        index = span.IndexOf(startTagSpan);
        //        var endIndex = span.IndexOf(endTagSpan);
        //        if (index == -1 || endIndex == -1) break;
        //        var value = span.Slice(index + startTagLen, endIndex - index - startTagLen);
        //        result.Add(value.ToString());
        //        span = span.Slice(endIndex + endTagLen);
        //    }
        //    return result.ToArray();
        //}

        [Benchmark]
        public void ReserveUseSpan()
        {
            string text = "Hello, World!";
            var span = text.AsSpan();
            // 假设我们要反转字符串
            char[] reversed = new char[span.Length];
            for (int i = 0; i < span.Length; i++)
            {
                reversed[i] = span[span.Length - 1 - i];
            }
            string reversedText = new string(reversed);
            Console.WriteLine(reversedText); // 输出 "!dlroW ,olleH"
        }

        [Benchmark]
        public void ReserveMethod()
        {
            string text = "Hello, World!";
            Console.WriteLine(text.Reverse()); // 输出 "!dlroW ,olleH"
        }
    }
}