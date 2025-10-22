using System.IO.MemoryMappedFiles;
using System.Text;

namespace net9Demo.SystemIODemo
{
    /// <summary>
    /// 以下是“大文件分片读取（提取日志关键词）”的核心代码框架，
    /// 基于.NET 9 的 System.IO.MemoryMappedFiles 命名空间，已标注关键细节和需补充的部分：
    /// 
    ///  需你补充验证的关键细节（也是容易踩坑的点）：
    ///  1. 关键词截断问题：若分片边界刚好拆分了关键词（如“Er”在第一片，“ror”在第二片），会漏检。
    ///  可尝试在分片间保留“重叠区域”（如每片末尾多读取10个字符，与下一片开头拼接）。
    ///  2. 超大文件分片循环：当文件超过 viewSize 时，需计算总分片数，循环修改 offset （如 offset += viewSize - 重叠长度 ），
    ///  直至覆盖整个文件。
    ///  3. 编码一致性：确保 StreamReader 的编码（如 UTF8、GBK）与日志文件实际编码一致，否则会出现乱码，导致关键词匹配失败。
    ///  运行后可对比“传统 FileStream 逐行读取”和“内存映射分片读取”的速度差异（尤其文件越大，内存映射的优势越明显）。
    ///  如果遇到具体报错或逻辑问题，随时告诉我，我们可以一起排查。
    /// </summary>

    class MemoryMappedFileDemo
    {
        static void Main(string[] args)
        {
            // 1. 准备目标大文件（建议用几百MB的日志文件，路径自行替换）
            string largeLogPath = @"D:\test\large_log.txt";
            string targetKeyword = "Error"; // 要提取的关键词，可自定义

            // 2. 用FileStream打开文件（需指定FileOptions.RandomAccess，适配内存映射的随机读取特性）
            using (var fileStream = new FileStream(
                path: largeLogPath,
                mode: FileMode.Open,
                access: FileAccess.Read,
                share: FileShare.Read,
                bufferSize: 4096,
                options: FileOptions.RandomAccess))
            {

                // 3. 创建内存映射文件（映射整个文件，也可指定部分大小，适合超大型文件分片）
                using (var mmf = MemoryMappedFile.CreateFromFile(
                    fileStream: fileStream,
                    mapName: null, // 非跨进程共享时设为null，避免命名冲突
                    capacity: 0, // 0表示使用文件实际大小，若需分片可设固定值（如1024*1024=1MB）
                    access: MemoryMappedFileAccess.Read,
                    inheritability: HandleInheritability.None,
                    leaveOpen: false))
                {
                    // 4. 创建映射视图（核心：指定读取的起始位置和长度，实现“分片”）
                    // 示例：读取文件前1MB内容，可循环修改offset实现全文件分片读取
                    long offset = 0;
                    long viewSize = 1024 * 1024; // 1MB分片，可根据内存调整

                    using (var viewStream = mmf.CreateViewStream(offset, viewSize, MemoryMappedFileAccess.Read))
                    {
                        // 5. 读取并匹配关键词（注意：流读取需处理编码，避免关键词被“分片截断”）
                        using (var reader = new StreamReader(viewStream, Encoding.UTF8))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                if (line.Contains(targetKeyword))
                                {
                                    Console.WriteLine($"找到关键词行：{line}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}