using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using OfficeOpenXml;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace SearchString
{
    internal class SearchTextExecute
    {
        ///// <summary>
        ///// 1.利用正则表达式匹配   跟字符串截取比较，性能怎么样，安全行怎么样  to do ...
        ///// 2.多线程查找文件，利用类型安全的ConcurrentBag对象保存结果，再去重    
        ///// 考虑数据量大的情况，ConcurrentBag会不会内存占用过多怎么办，对后面的去重性有何影响  to do ...
        ///// 3.StreamReader 流式处理文件，避免把文件整个加载到内存   
        ///// 4.查找.cs,.js,.vue后缀的文件，忽略大小写，过滤掉大文件node_modules，dist，bin    to do ...
        ///// </summary>
        ///// <returns></returns>
        //public async Task SearchText(string directoryPath)
        //{
        //    Stopwatch stopwatch = Stopwatch.StartNew();           

        //    ////获取目录中的所有.cs文件
        //    // string[] files = Directory.GetFiles(directoryPath, "*.cs", SearchOption.AllDirectories);

        //    string[] files = SearchTextExecute.SearchFiles(directoryPath, ".cs", ".js", ".vue");
        //    Console.WriteLine("files count:" + files.Count());
        //    stopwatch.Stop();
        //    Console.WriteLine("get files spend:" + stopwatch.ElapsedMilliseconds);
        //    stopwatch.Restart();
        //    //限制并行线程数量
        //    var options = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        //    //使用 Parallel.ForEach 多线程处理文件
        //    await Task.Run(() =>
        //    {
        //        Parallel.ForEach(files, options, file =>
        //        {
        //            try
        //            {                        
        //                // 逐行读取文件内容，避免一次性加载大文件到内存中             
        //                using (var reader = new StreamReader(file))
        //                {
        //                    int lineNumber = 0;
        //                    string line;
        //                    while ((line = reader.ReadLine()) != null)
        //                    {
        //                        lineNumber++;
        //                        // 检查每一行是否包含指定的字符串
        //                        var list = SearchTextExecute.Pattern(line, file);
        //                        if (list.Any())
        //                        {
        //                            foreach (var val in list)
        //                            {
        //                                results.Add((file, lineNumber, val));
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine($"Error reading file {file}: {ex.Message}");
        //            }
        //        });
        //    });

        //    stopwatch.Stop();
        //    Console.WriteLine("task run spend:" + stopwatch.ElapsedMilliseconds);
        //    stopwatch.Restart();

        //    string fileName = @".\SearchResults.xlsx";
        //    string fileName2 = @".\SearchUniqueResults.xlsx";

        //    //string fileName = @".\SearchResults" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".xlsx";
        //    //string fileName2 = @".\SearchUniqueResults" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".xlsx";

        //    SearchTextExecute.SaveResultsToExcel(results, fileName);
        //    stopwatch.Stop();
        //    Console.WriteLine("SaveResultsToExcel spend:" + stopwatch.ElapsedMilliseconds);
        //    stopwatch.Restart();
        //    var keys = results.Select(x => x.text)
        //      .Distinct();
        //    stopwatch.Stop();
        //    Console.WriteLine("Distinct spend:" + stopwatch.ElapsedMilliseconds);
        //    stopwatch.Restart();
        //    SearchTextExecute.SaveUniqueResultsToExcel(keys, fileName2);
        //    stopwatch.Stop();
        //    Console.WriteLine("SaveUniqueResultsToExcel spend:" + stopwatch.ElapsedMilliseconds);
        //    Console.WriteLine("查找完成，结果已保存到 Excel 文件。");
        //}


        // ".cs", ".js", ".vue"



        /// <summary>
        /// 1.利用正则表达式匹配   跟字符串截取比较，性能怎么样，安全行怎么样  to do ...
        /// 2.多线程查找文件，利用类型安全的ConcurrentBag对象保存结果，再去重    
        /// 考虑数据量大的情况，ConcurrentBag会不会内存占用过多怎么办，对后面的去重性有何影响  to do ...
        /// 3.StreamReader 流式处理文件，避免把文件整个加载到内存   
        /// 4.查找.cs,.js,.vue后缀的文件，忽略大小写，过滤掉大文件node_modules，dist，bin    to do ...
        /// </summary>
        /// <returns></returns>
        public async Task SearchText(string directoryPath,string ExcelName, params string[] exts)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            
            string[] files = SearchTextExecute.SearchFiles(directoryPath,exts);
            Console.WriteLine("files count:" + files.Count());

            stopwatch.Stop();
            Console.WriteLine("get files spend:" + stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            //var results = await MatchTask(files);

            //用于存储结果，最后去重
            ConcurrentBag<(string file, int lineNumber, LineValue text)> results = new ConcurrentBag<(string, int, LineValue)>();
            //限制并行线程数量
            var options = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

            //使用 Parallel.ForEach 多线程处理文件
            await Task.Run(() =>
            {
                Parallel.ForEach(files, options, file =>
                {
                    try
                    {
                        // 逐行读取文件内容，避免一次性加载大文件到内存中             
                        using (var reader = new StreamReader(file))
                        {
                            int lineNumber = 0;
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                lineNumber++;
                                // 检查每一行是否包含指定的字符串
                                var list = SearchTextExecute.Pattern(line, file);
                                if (list.Any())
                                {
                                    foreach (var val in list)
                                    {
                                        results.Add((file, lineNumber, val));
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error reading file {file}: {ex.Message}");
                    }
                });
            });



            stopwatch.Stop();
            Console.WriteLine("task run spend:" + stopwatch.ElapsedMilliseconds);

            string fileName = @".\" + ExcelName + ".xlsx";
            string fileNameUnique = @".\" + ExcelName + "Unique.xlsx";

            stopwatch.Restart();          
            SearchTextExecute.SaveResultsToExcel(results, fileName);
            stopwatch.Stop();
            Console.WriteLine("SaveResultsToExcel spend:" + stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            var keys = results.Select(x => x.text.NewValue) .Distinct();
            stopwatch.Stop();
            Console.WriteLine("Distinct spend:" + stopwatch.ElapsedMilliseconds);

            stopwatch.Restart();
            SearchTextExecute.SaveUniqueResultsToExcel(keys, fileNameUnique);
            stopwatch.Stop();
            Console.WriteLine("SaveUniqueToExcel spend:" + stopwatch.ElapsedMilliseconds);
            Console.WriteLine("查找完成，结果已保存到 Excel 文件。");
        }

        public async Task<ConcurrentBag<(string file, int lineNumber, LineValue text)>> MatchTask(string[] files)
        {
            //用于存储结果，最后去重
            ConcurrentBag<(string file, int lineNumber, LineValue text)> results = new ConcurrentBag<(string, int, LineValue)>();
            //限制并行线程数量
            var options = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

            //使用 Parallel.ForEach 多线程处理文件
            await Task.Run(() =>
            {
                Parallel.ForEach(files, options, file =>
                {
                    try
                    {
                        // 逐行读取文件内容，避免一次性加载大文件到内存中             
                        using (var reader = new StreamReader(file))
                        {
                            int lineNumber = 0;
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                lineNumber++;
                                // 检查每一行是否包含指定的字符串
                                var list = SearchTextExecute.Pattern(line, file);
                                if (list.Any())
                                {
                                    foreach (var val in list)
                                    {
                                        results.Add((file, lineNumber, val));
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error reading file {file}: {ex.Message}");
                    }
                });
            });

            return results;
        }

        ///// <summary>
        ///// 1.利用正则表达式匹配   跟字符串截取比较，性能怎么样，安全行怎么样  to do ...
        ///// 2.多线程查找文件，利用类型安全的ConcurrentBag对象保存结果，再去重    
        ///// 考虑数据量大的情况，ConcurrentBag会不会内存占用过多怎么办，对后面的去重性有何影响  to do ...
        ///// 3.StreamReader 流式处理文件，避免把文件整个加载到内存   
        ///// 4.查找.cs,.js,.vue后缀的文件，忽略大小写，过滤掉大文件node_modules，dist，bin    to do ...
        ///// </summary>
        ///// <returns></returns>
        //public async Task SearchFRText(string directoryPath)
        //{
        //    Stopwatch stopwatch = Stopwatch.StartNew();
        //    //用于存储结果，最后去重
        //    ConcurrentBag<(string file, int lineNumber, LineValue text)> results = new ConcurrentBag<(string, int, LineValue)>();
           
        //    string[] files = SearchTextExecute.SearchFiles(directoryPath, ".cpt");
        //    Console.WriteLine("files count:" + files.Count());
        //    stopwatch.Stop();
        //    Console.WriteLine("get files spend:" + stopwatch.ElapsedMilliseconds);
        //    stopwatch.Restart();
        //    //限制并行线程数量
        //    var options = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        //    //使用 Parallel.ForEach 多线程处理文件
        //    await Task.Run(() =>
        //    {
        //        Parallel.ForEach(files, options, file =>
        //        {
        //            try
        //            {
        //                // 逐行读取文件内容，避免一次性加载大文件到内存中             
        //                using (var reader = new StreamReader(file))
        //                {
        //                    int lineNumber = 0;
        //                    string line;
        //                    while ((line = reader.ReadLine()) != null)
        //                    {
        //                        lineNumber++;
        //                        // 检查每一行是否包含指定的字符串
        //                        var list = SearchTextExecute.PatternFR(line, file);
        //                        if (list.Any())
        //                        {
        //                            foreach (var val in list)
        //                            {
        //                                results.Add((file, lineNumber, val));
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine($"Error reading file {file}: {ex.Message}");
        //            }
        //        });
        //    });

        //    stopwatch.Stop();
        //    Console.WriteLine("task run spend:" + stopwatch.ElapsedMilliseconds);
        //    stopwatch.Restart();

        //    string fileName = @".\SearchFR.xlsx";
        //    string fileName2 = @".\SearchFRUnique.xlsx";

        //    //string fileName = @".\SearchResults" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".xlsx";
        //    //string fileName2 = @".\SearchUniqueResults" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".xlsx";

        //    SearchTextExecute.SaveResultsFRToExcel(results, fileName);
        //    stopwatch.Stop();
        //    Console.WriteLine("SearchFR spend:" + stopwatch.ElapsedMilliseconds);
        //    stopwatch.Restart();
        //    var keys = results.Select(x => x.text.NewValue)
        //      .Distinct();
        //    stopwatch.Stop();
        //    Console.WriteLine("Distinct spend:" + stopwatch.ElapsedMilliseconds);
        //    stopwatch.Restart();
        //    SearchTextExecute.SaveUniqueResultsToExcel(keys, fileName2);
        //    stopwatch.Stop();
        //    Console.WriteLine("SearchFRUnique spend:" + stopwatch.ElapsedMilliseconds);
        //    Console.WriteLine("查找完成，结果已保存到 Excel 文件。");
        //}

     

        /// <summary>
        /// 获取指定目录下所有文件，排除无用文件夹
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="extensions"></param>
        /// <returns></returns>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public static string[] SearchFiles(string directoryPath, params string[] extensions)
        {
            // 确保目录存在
            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException($"目录不存在: {directoryPath}");
            }
            var files = extensions
            .SelectMany(ext => Directory.GetFiles(directoryPath, $"*{ext}", SearchOption.AllDirectories))               
            .Where(file => !file.Contains("node_modules") && !file.Contains("bin")  && !file.Contains("dist") && !file.Contains("build")  ) // 跳过 node_modules 文件夹                     
            .ToArray();           
            return files;
        }

        public static void SaveResultsToExcel(ConcurrentBag<(string file,int lineNumber, LineValue key)>? results, string outputPath)
        {
            // 设置 EPPlus 的 LicenseContext
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // 创建 Excel 文件
            using (var package = new ExcelPackage())
            {
                // 添加一个工作表
                var worksheet = package.Workbook.Worksheets.Add("Search Results");

                // 设置表头
                worksheet.Cells[1, 1].Value = "文件路径";
                worksheet.Cells[1, 2].Value = "行号";              
                worksheet.Cells[1, 3].Value = "原键";
                worksheet.Cells[1, 4].Value = "新键";


                // 填充数据
                int row = 2;
                foreach (var result in results)
                {
                    worksheet.Cells[row, 1].Value = result.file;
                    worksheet.Cells[row, 2].Value = result.lineNumber;
                    worksheet.Cells[row, 3].Value = result.key.OldValue;
                    worksheet.Cells[row, 4].Value = result.key.NewValue;
                    row++;
                }
                // 自动调整列宽
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                // 保存 Excel 文件
                package.SaveAs(new FileInfo(outputPath));
            }
        }
     
        public static void SaveUniqueResultsToExcel(IEnumerable<string> results, string outputPath)
        {
            // 设置 EPPlus 的 LicenseContext
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // 创建 Excel 文件
            using (var package = new ExcelPackage())
            {
                // 添加一个工作表
                var worksheet = package.Workbook.Worksheets.Add("Search Results");
                // 设置表头               
                worksheet.Cells[1, 1].Value = "键";
                // 填充数据
                int row = 2;
                foreach (var result in results)
                {
                    worksheet.Cells[row, 1].Value = result;                   
                    row++;
                }
                // 自动调整列宽
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // 保存 Excel 文件
                package.SaveAs(new FileInfo(outputPath));
            }
        }


        /// <summary>
        /// 使用正则表达式匹配所有项
        /// </summary>
        /// <param name="input"></param>
        /// <param name="filetype"></param>
        /// <returns></returns>
        public static List<LineValue> Pattern(string input, string filetype)
        {
            string Pattern = @"(?:LangManager\.GetText|Error|OK|ErrorAutoTranslate|OKAutoTranslate)\(\""([^\""]+)\""(?:,\s*([^)]+))?\)";
            string patternGetLangText = @"\""([^\""]+)\""\.GetLangText\((?:([^)]+))?\)";
            string patternExcelHeader = @"\.Cells\[\d+,\s*\d+\]\.Value\s*=\s*""([^""]+)"";";
            //string PatternVue = @"\$t\(\s*([""'])(.*?)\1\s*(?:,\s*(.*))?\)";
            string PatternVue = @"\$t\(\s*[""'](.*?)[""']";
            //string PatternVue = @"\$t\(\s*((['""])(.*?)\2|[^)]+?)\s*(?:,|\))";

            string UniversalPattern = @"i18n\((?:(?:&quot;)|(?:\\*[""']))(.*?)(?:(?:&quot;)|(?:\\*[""']))\)";
            List<LineValue> values = new List<LineValue>();

            if (filetype.Contains(".cs"))
            {
                MatchString(input, Pattern, ref values);
                MatchString(input, patternGetLangText, ref values);
                MatchString(input, patternExcelHeader, ref values);
            }
            else  if (filetype.Contains(".vue") || filetype.Contains(".js"))
            {
                MatchString(input, PatternVue, ref values);
            }
            else if (filetype.Contains(".cpt"))
            {
                MatchString(input, UniversalPattern, ref values);
            }
            return values;
        }

        public static void MatchString(string input, string pattern,ref List<LineValue> values)
        {
            try
            {
                MatchCollection matches = Regex.Matches(input, pattern);
                for (int i = 0; i < matches.Count; i++)
                {
                    string extracted = matches[i].Groups[1].Value;
                    extracted = extracted.Replace("\\\\", "\\").Replace("\\\"", "\"");
                    values.Add(new LineValue(input, extracted));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(input + ";" + ex.Message, ex.StackTrace);
            }
        }

        public record LineValue(string OldValue, string NewValue);
          
    }
}