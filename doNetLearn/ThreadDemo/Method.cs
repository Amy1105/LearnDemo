using nietras.SeparatedValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.ThreadDemo
{
    internal class Method
    {

        public void SepDemo()
        {


            {
                // 指定CSV文件的路径
                string filePath = "file.csv";

                // 使用Sep库创建一个CSV读取器，从文件中读取数据
                using var reader = Sep.Reader().FromFile(filePath);

                // 遍历CSV文件中的每一行
                foreach (var readRow in reader)
                {
                    // 假设我们知道CSV文件的列结构，可以直接通过列名访问数据
                    string columnA = readRow["A"].ToString();
                    string columnB = readRow["B"].ToString();
                    int columnC = readRow["C"].Parse<int>();
                    double columnD = readRow["D"].Parse<double>();

                    // 处理每一行的数据
                    Console.WriteLine($"A: {columnA}, B: {columnB}, C: {columnC}, D: {columnD}");
                }
            }
            {
                ReadCSV readCSV = new ReadCSV();

                readCSV.ProcessProducts(@"C:\Users\yingying.zhu\Downloads\11.csv", "样品生产条码");
            }


            {
                // 定义一个多行字符串，表示一个CSV格式的数据。
                var text = """
                   A;B;C;D;E;F
                   Sep;🚀;1;1.2;0.1;0.5
                   CSV;✅;2;2.2;0.2;1.5
                   """;

                // 使用Sep库创建一个CSV读取器，自动从标题行推断分隔符。
                using var reader = Sep.Reader().FromText(text);

                // 根据读取器的规格创建一个写入器，准备将数据写入文本。
                using var writer = reader.Spec.Writer().ToText();

                // 获取列"B"在标题中的索引位置。
                var idx = reader.Header.IndexOf("B");
                // 定义一个包含列名的数组。
                var nms = new[] { "E", "F" };

                // 遍历读取器中的每一行数据。
                foreach (var readRow in reader)
                {
                    // 将列"A"读取为只读的字符跨度。
                    var a = readRow["A"].Span;
                    // 将列"B"的值转换为字符串。
                    var b = readRow[idx].ToString();
                    // 将列"C"的值解析为整数。
                    var c = readRow["C"].Parse<int>();
                    // 将列"D"的值解析为浮点数，使用csFastFloat库进行快速解析。
                    var d = readRow["D"].Parse<float>();
                    // 将列"E"和"F"的值解析为双精度浮点数的跨度。
                    var s = readRow[nms].Parse<double>();
                    // 遍历解析后的数值，并将每个值乘以10。
                    foreach (ref var v in s) { v *= 10; }

                    // 开始写入新一行数据，行数据在Dispose时写入。
                    using var writeRow = writer.NewRow();
                    // 通过只读的字符跨度设置列"A"的值。
                    writeRow["A"].Set(a);
                    // 通过字符串设置列"B"的值。
                    writeRow["B"].Set(b);
                    // 通过插值字符串处理器设置列"C"的值，不会产生新的内存分配。
                    writeRow["C"].Set($"{c * 2}");
                    // 格式化列"D"的值，将数值除以2。
                    writeRow["D"].Format(d / 2);
                    // 直接格式化多个列的值。
                    writeRow[nms].Format(s);
                }

                Console.WriteLine(writer.ToString());
            }


        }
    }
}