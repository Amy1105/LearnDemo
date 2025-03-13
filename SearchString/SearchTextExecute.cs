using Microsoft.Extensions.FileSystemGlobbing.Internal;
using OfficeOpenXml;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchString
{
    internal class SearchTextExecute
    {

        public async Task SearchText()
        {

            Stopwatch stopwatch = Stopwatch.StartNew();
            string directoryPath = @"D:\Projects\LiMS";//  @"D:\Projects\LiMS\TVC.Server\TVC.ApplicationForm\Services\ApplicationForm\Partial";

            //用于存储去重后的结果
            ConcurrentBag<(string file, int lineNumber, string text)> results = new ConcurrentBag<(string, int, string)>();

            ////获取目录中的所有.cs文件
            // string[] files = Directory.GetFiles(directoryPath, "*.cs", SearchOption.AllDirectories);


            string[] files = SearchTextExecute.SearchFiles(directoryPath, ".cs", ".js", ".vue");
            Console.WriteLine("files count:" + files.Count());
            stopwatch.Stop();
            Console.WriteLine("get files spend:" + stopwatch.ElapsedMilliseconds);
            stopwatch.Restart();
            //限制并行线程数量
            var options = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

            //使用 Parallel.ForEach 多线程处理文件
            await Task.Run(() =>
            {
                Parallel.ForEach(files, options, file =>
                {
                    try
                    {
                        ////读取文件内容
                        //string[] lines = File.ReadAllLines(file);
                        //for (int i = 0; i < lines.Length; i++)
                        //{
                        //    //检查每一行是否包含指定的字符串
                        //   var list = Class1.Pattern(lines[i], file);
                        //    if (list.Any())
                        //    {
                        //        foreach (var line in list)
                        //        {
                        //            results.Add((file, i + 1, line));
                        //        }
                        //    }
                        //}


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

                        /*
                         * 
        files count:5886
        get files spend:1257
        task run spend:31583
        SaveResultsToExcel spend:13224
        Distinct spend:3
        SaveUniqueResultsToExcel spend:280
        查找完成，结果已保存到 Excel 文件。
                         * 
                         */
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error reading file {file}: {ex.Message}");
                    }
                });
            });

            stopwatch.Stop();
            Console.WriteLine("task run spend:" + stopwatch.ElapsedMilliseconds);
            stopwatch.Restart();




            //// 输出结果
            //foreach (var result in uniqueResults)
            //{
            //    Console.WriteLine($"File: {result.file}");
            //    Console.WriteLine($"Line {result.text}");
            //    Console.WriteLine();
            //}

            string fileName = @".\SearchResults" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".xlsx";
            string fileName2 = @".\SearchUniqueResults" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".xlsx";


            //搜索原值
            SearchTextExecute.SaveResultsToExcel(results, fileName);

            stopwatch.Stop();
            Console.WriteLine("SaveResultsToExcel spend:" + stopwatch.ElapsedMilliseconds);
            stopwatch.Restart();

            var keys = results.Select(x => x.text)
              .Distinct();
            stopwatch.Stop();
            Console.WriteLine("Distinct spend:" + stopwatch.ElapsedMilliseconds);
            stopwatch.Restart();

            SearchTextExecute.SaveUniqueResultsToExcel(keys, fileName2);

            stopwatch.Stop();
            Console.WriteLine("SaveUniqueResultsToExcel spend:" + stopwatch.ElapsedMilliseconds);
            Console.WriteLine("查找完成，结果已保存到 Excel 文件。");

        }


        public static string[] SearchFiles(string directoryPath, params string[] extensions)
        {

            // 确保目录存在
            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException($"目录不存在: {directoryPath}");
            }

            var files = extensions
            .SelectMany(ext => Directory.GetFiles(directoryPath, $"*{ext}", SearchOption.AllDirectories))               
            .Where(file => !file.Contains("node_modules") && !file.Contains("bin")) // 跳过 node_modules 文件夹           
            .Distinct() // 去重
            .ToArray();

            // var files = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories)
            //.Where(file => file.EndsWith(".cs", StringComparison.OrdinalIgnoreCase)
            //            || file.EndsWith(".js", StringComparison.OrdinalIgnoreCase)
            //            || file.EndsWith(".vue", StringComparison.OrdinalIgnoreCase))
            //.ToArray();

            //var allFiles = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories);

            //// 过滤出指定扩展名的文件（不区分大小写）
            //var files = allFiles
            //    .Where(file =>  file!= "node_modules" &&  extensions.Contains(Path.GetExtension(file), StringComparer.OrdinalIgnoreCase))
            //    .ToArray();

            return files;
        }

        public static void SaveResultsToExcel(ConcurrentBag<(string file,int lineNumber, string key)> results, string outputPath)
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
                worksheet.Cells[1, 3].Value = "键";


                // 填充数据
                int row = 2;
                foreach (var result in results)
                {
                    worksheet.Cells[row, 1].Value = result.file;
                    worksheet.Cells[row, 2].Value = result.lineNumber;
                    worksheet.Cells[row, 3].Value = result.key;
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
        /// 
        /// </summary>
        public static List<string> Pattern(string input,string filetype)
        {
            List<string> values = new List<string>();
            if (filetype.Contains(".cs"))
            {
                string Pattern = @"(?:LangManager\.GetText|ErrorAutoTranslate|OKAutoTranslate)\(\""([^\""]+)\""(?:,\s*([^)]+))?\)";
                string pattern2 = @"\""([^\""]+)\""\.GetLangText\((?:([^)]+))?\)";
                //string pattern3 = @"\""([^\""]+)\""\.GetLangText\((?:([^)]+))?\)";
                // 使用正则表达式匹配所有项

                GetMatch(input, Pattern, ref values, "LangManager");
                GetMatch(input, pattern2, ref values);
            }
            else if(filetype.Contains(".vue") || filetype.Contains(".js"))
            {
                //_this.$t("请输入") + _this.$t("特殊样品SOC和物流说明"),
                //$t('项目总预算:{0}', { 0: generalBudget })
                GetMatch2(input, ref values);
            }                     
            return values;
        }

        public static void GetMatch(string input,string pattern,ref List<string> values,string flag= "GetLangText")
        {
            MatchCollection matches = Regex.Matches(input, pattern, RegexOptions.IgnorePatternWhitespace);
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    // 提取参数部分
                    string parameter = match.Groups[1].Value;
                    values.Add(parameter);
                    //Console.WriteLine("提取的参数: " + parameter);
                }
            }
        }

        public static void GetMatch2(string input,  ref List<string> values)
        {
            string Pattern = @"\$t\(\s*([""'])(.*?)\1\s*(?:,\s*(.*))?\)";

            MatchCollection matches = Regex.Matches(input, Pattern, RegexOptions.IgnorePatternWhitespace);
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    // 提取参数部分
                    string parameter = match.Groups[2].Value;
                    values.Add(parameter);
                    //Console.WriteLine("提取的参数: " + parameter);
                }
            }
        }


        public static void PatternVue()
        {
            //_this.$t("请输入") + _this.$t("特殊样品SOC和物流说明"),
            //$t('项目总预算:{0}', { 0: generalBudget })
            //string input = "_this.$t(\"请输入\") + _this.$t(\"特殊样品SOC和物流说明\");.$t('项目总预算:{0}', { 0: generalBudget })";

            string input = @"$t('样品类型');$t('基本信息') +  $t('项目总预算:{0}', { 0: generalBudget })";

            //string Pattern = @"(?\.$t)\(\""([^\""]+)\""(?:,\s*([^)]+))?\)";
            
            
           // string Pattern = @"\$t\(\s*([""'])(.*?)\1\s*\))";

            string Pattern = @"\$t\(\s*([""'])(.*?)\1\s*(?:,\s*(.*))?\)";


            //string pattern = @"\$t\(\s*([""'])(.*?)\1\s*(?:,\s*(.*))?\)";

            //string Pattern = @"(?:\.)?\$t\(\s*([""'])(.*?)\1\s*(?:,\s*(.*))?\)";

            // 定义正则表达式
            //string pattern2 = @"\.$t\(\s*([""'])(.*?)\1\s*(?:,\s*(.*))?\)";
            // string pattern = @"\$t\(\""([^\""]+)\""\)";

            // 使用正则表达式匹配所有项
            MatchCollection matches = Regex.Matches(input, Pattern);
            // 遍历所有匹配项
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    // 提取字符串参数
                    string stringParameter = match.Groups[2].Value;
                    Console.WriteLine("字符串参数: " + stringParameter);

                    // 提取动态参数（如果有）
                    if (match.Groups[3].Success)
                    {
                        string dynamicParameter = match.Groups[3].Value;
                        Console.WriteLine("动态参数: " + dynamicParameter);
                    }
                    else
                    {
                        Console.WriteLine("动态参数: 无");
                    }
                    Console.WriteLine();
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public static void Pattern()
        {
            string input = @"LangManager.GetText(""只有待提交或者草稿或者修改申请单或者已驳回的状态才能修改"") + LangManager.GetText(""原因:{0},{1},修改时间：{2}"", p.Remark, p.Description, p.CreateDate)+ErrorAutoTranslate(""BOP类别必填"")+ ErrorAutoTranslate(""该测试单状态[{0}]无法再次审核！"", orderHeader.OrderStatus)
OKAutoTranslate(""提交成功"") + 
OKAutoTranslate(""计算成功,预估费用:{0}"", totalCost.ToString(""F2""), totalCost.ToString(""F2""))+""请求OA失败"".GetLangText()+""【{0}】的验证工程师不存在，不能进行审核操作;"".GetLangText(orderSample.BarCode)
";
          
            
            string Pattern = @"(?:LangManager\.GetText|ErrorAutoTranslate|OKAutoTranslate)\(\""([^\""]+)\""(?:,\s*([^)]+))?\)|\""([^\""]+)\""\.GetLangText\((?:([^)]+))?\)";
          
            // 使用正则表达式匹配所有项
            MatchCollection matches = Regex.Matches(input, Pattern, RegexOptions.IgnorePatternWhitespace);
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    // 提取参数部分
                    string parameter = match.Groups[1].Value;
                    Console.WriteLine("提取的参数: " + parameter);
                }
            }
        } 

        /// <summary>
        /// LangManager.GetText("其它申请单已有条码：{0}", string.Join(",", duplicateBarcodes));
        /// </summary>
        public static void Pattern2()
        {           
            string input = "LangManager.GetText(\"只有待提交或者草稿或者修改申请单或者已驳回的状态才能修改\") + LangManager.GetText(\"原因:{0},{1},修改时间：{2}\", p.Remark, p.Description, p.CreateDate)";

            // 定义正则表达式
           //string pattern = @"LangManager\.GetText\(\""([^\""]+)\"",\s*([^)]+)\)";
            string LangManagerGetTextPattern = @"LangManager\.GetText\(\""([^\""]+)\""(?:,\s*([^)]+))?\)";

            // 使用正则表达式匹配所有项
            MatchCollection matches = Regex.Matches(input, LangManagerGetTextPattern);
            // 遍历所有匹配项
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    // 提取参数部分
                    string parameter = match.Groups[1].Value;
                    Console.WriteLine("提取的参数: " + parameter);
                }
            }
        }

        /// <summary>
        /// ErrorAutoTranslate("BOP类别必填")
        /// ErrorAutoTranslate("该测试单状态[{0}]无法再次审核！", orderHeader.OrderStatus)
        /// </summary>
        public static void Pattern3()
        {
            // 输入字符串
            string input = "ErrorAutoTranslate(\"BOP类别必填\")+ ErrorAutoTranslate(\"该测试单状态[{0}]无法再次审核！\", orderHeader.OrderStatus)";


            string pattern = @"ErrorAutoTranslate\(\""([^\""]+)\""(?:,\s*([^)]+))?\)";

           // string pattern = @"ErrorAutoTranslate\(\""([^\""]+)\"",\s*([^)]+)\)";

            // 定义正则表达式
            //string pattern = @"ErrorAutoTranslate\(\""([^\""]+)\""\)";

            // 使用正则表达式匹配所有项
            MatchCollection matches = Regex.Matches(input, pattern);
            // 遍历所有匹配项
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    // 提取参数部分
                    string parameter = match.Groups[1].Value;
                    Console.WriteLine("提取的参数: " + parameter);
                }
            }
        }


        /// <summary>
        /// OKAutoTranslate("提交成功")
        /// OKAutoTranslate("计算成功,预估费用:{0}", totalCost.ToString("F2"), totalCost.ToString("F2"))
        /// </summary>
        public static void Pattern4()
        {
            // 输入字符串
            string input = "OKAutoTranslate(\"提交成功\") + OKAutoTranslate(\"计算成功,预估费用:{0}\", totalCost.ToString(\"F2\"), totalCost.ToString(\"F2\"))";

            // 定义正则表达式
           // string pattern = @"OKAutoTranslate\(\""([^\""]+)\""\)";


            string pattern = @"OKAutoTranslate\(\""([^\""]+)\""(?:,\s*([^)]+))?\)";


            // 使用正则表达式匹配所有项
            MatchCollection matches = Regex.Matches(input, pattern);
            // 遍历所有匹配项
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    // 提取参数部分
                    string parameter = match.Groups[1].Value;
                    Console.WriteLine("提取的参数: " + parameter);
                }
            }
        }


        /// <summary>
        /// "请求OA失败".GetLangText()
        ///  "【{0}】的验证工程师不存在，不能进行审核操作;".GetLangText(orderSample.BarCode)
        /// </summary>
        public static void Pattern5()
        {
            string input = @"""请求OA失败"".GetLangText()+""【{0}】的验证工程师不存在，不能进行审核操作;"".GetLangText(orderSample.BarCode)";


            string input2 = @"return WebResponseContent.Instance.OK(""操作成功"".GetLangText());";
                  string p = @"\""([^\""]+)\""\.GetLangText\((?:([^)]+))?\)";
            string pattern = @"\""([^\""]+)\""\.GetLangText\((?:([^)]+))?\)";

           // string pattern = @"""([^""]+)""\.GetLangText\(\)";          
            // 使用正则表达式匹配所有项
            MatchCollection matches = Regex.Matches(input2, p);
            // 遍历所有匹配项
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    // 提取参数部分
                    string parameter = match.Groups[1].Value;
                    Console.WriteLine("提取的参数: " + parameter);
                }
            }
        }
    }
}