using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net9Demo.SystemIODemo
{
    /// <summary>
    /// 处理大文件
    /// 1.跳着读
    /// 2.顺序读
    /// </summary>
    internal class Class1
    {
        /// <summary>
        /// 
        /// </summary>
        public void ReadBigFile()
        {
            string filePath = "test.dat";

            // 使用 MemoryMappedViewAccessor 写入和读取结构数据
            // 从磁盘上的文件创建具有指定访问模式、名称和容量的内存映射文件。
            using (var mmf = MemoryMappedFile.CreateFromFile(filePath, FileMode.Create, "MyMap", 1024))
            using (var accessor = mmf.CreateViewAccessor())
            {
                // 在位置0写入一个整数
                accessor.Write(0, 42);
                // 在位置4写入一个双精度浮点数
                accessor.Write(4, 3.14159);

                // 从位置0读取整数
                accessor.Read(0, out int readInt);
                // 从位置4读取双精度浮点数
                accessor.Read(4, out double readDouble);

                Console.WriteLine($"Read Int: {readInt}"); // 输出: Read Int: 42
                Console.WriteLine($"Read Double: {readDouble}"); // 输出: Read Double: 3.14159
            }

            // 使用 MemoryMappedViewStream 写入和读取流数据
            // 从磁盘上的文件创建具有指定访问模式和名称的内存映射文件。
            using (var mmf = MemoryMappedFile.CreateFromFile(filePath, FileMode.Open, "MyMap"))
            using (var stream = mmf.CreateViewStream())
            {
                string message = "Hello, Memory Mapped File Stream!";
                byte[] buffer = Encoding.UTF8.GetBytes(message);

                // 将数据写入流
                stream.Write(buffer, 0, buffer.Length);

                // 将流的位置重置到开始，以便读取
                stream.Position = 0;

                // 从流中读取数据
                byte[] readBuffer = new byte[buffer.Length];
                stream.Read(readBuffer, 0, readBuffer.Length);
                string readMessage = Encoding.UTF8.GetString(readBuffer);

                Console.WriteLine($"Read from stream: {readMessage}");
            }
        }

        // 示例1：处理大型数据库文件的索引部分
        public void ReadDatabaseIndex(string dbFile)
        {
            using (var mmf = MemoryMappedFile.CreateFromFile(dbFile))
            using (var accessor = mmf.CreateViewAccessor(offset: 0, size: 65536)) // 只映射前64KB索引区
            {             
                // 在索引区内随机跳转读取
                accessor.Read(1024, out IndexEntry entry1);
                accessor.Read(4096, out IndexEntry entry2);
            }
        }

        public struct IndexEntry { }


        // 示例2：顺序分析整个日志文件
        public void AnalyzeLogFile(string logFile)
        {
            using (var mmf = MemoryMappedFile.CreateFromFile(logFile))
            using (var stream = mmf.CreateViewStream()) // 映射整个文件
            using (var reader = new StreamReader(stream))
            {
                // 顺序读取所有内容
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    AnalyzeLine(line);
                }
            }
        }

        /// <summary>
        /// 伪代码
        /// </summary>
        /// <param name="line"></param>
        public void AnalyzeLine(string line)
        {
        }
    }
}