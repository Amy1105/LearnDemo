using OfficeOpenXml;
using System.Collections.Concurrent;
using VOL.Entity.DomainModels;

namespace SearchString
{
    public class dbService
    {
        /// <summary>
        /// 1.多线程读取大excel，分块去读，放在线程安全的ConcurrentBag类型中
        /// 2. ExcelPackage保存   如果数据量超大，怎么顺利保存到excel中  1.数据分块，2.多线程写入，3.NPOI支持流式写入
        /// 3.大型报表可以后台线程执行
        /// </summary>
        /// <param name="dbList"></param>
        /// <returns></returns>
        public async Task ExcelAnlysis(List<Sys_Text_Main> dbList)
        {
            // 设置EPPlus的LicenseContext
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            string filePath = "./SearchUniqueResults2025-03-14-11-27-46.xlsx";
            string outputFilePath = "./Languages.xlsx";

            var data = new ConcurrentBag<VModel>(); // 线程安全的集合，用于存储读取的数据

            // 读取Excel文件
            await ReadExcelInParallelAsync(filePath, dbList,data);

            await WriteLargeListToExcelAsync(data, outputFilePath);

            Console.WriteLine("语言excel生成完毕");
        }

        static async Task ReadExcelInParallelAsync(string filePath, List<Sys_Text_Main> list,ConcurrentBag<VModel> data)
        {
            await Task.Run(() =>
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var workbook = package.Workbook;
                    var worksheet = workbook.Worksheets[0];

                    int totalRows = worksheet.Dimension.Rows;
                    int totalColumns = 1;// worksheet.Dimension.Columns;

                    // 将文件分成多个块，每个线程处理一个块
                    int chunkSize = 1000; // 每个块的行数
                    int numberOfChunks = (int)Math.Ceiling((double)totalRows / chunkSize);

                    Parallel.For(0, numberOfChunks, chunkIndex =>
                    {
                        var chunkData = new List<VModel>();

                        int startRow = chunkIndex * chunkSize + 1; // Excel行号从1开始
                        int endRow = Math.Min(startRow + chunkSize - 1, totalRows);

                        for (int row = startRow; row <= endRow; row++)
                        {                           
                            var rowData = worksheet.Cells[row, totalColumns].Text;
                            var item= list.Where(x => x.Key == rowData).FirstOrDefault();
                            if(item==null)
                            {
                                chunkData.Add(new VModel(rowData, "", "", "", ""));                              
                            }
                            else
                            {
                               var cntext= item.Sys_Texts.Where(x => x.Lang == "zh-CN").Select(x => x.Value).FirstOrDefault();
                              var enText=  item.Sys_Texts.Where(x => x.Lang == "en-US").Select(x => x.Value).FirstOrDefault();
                              var jpText=  item.Sys_Texts.Where(x => x.Lang == "日本語").Select(x => x.Value).FirstOrDefault();
                              var frText=  item.Sys_Texts.Where(x => x.Lang == "fr-FR").Select(x => x.Value).FirstOrDefault();
                                chunkData.Add(new VModel(item.Key, cntext, enText, jpText, frText));
                            }                            
                        }
                        // 将块数据添加到线程安全的集合中
                        foreach (var row in chunkData)
                        {
                            data.Add(row);
                        }
                    });
                }
            });
        }


        static async Task WriteLargeListToExcelAsync(ConcurrentBag<VModel> largeList, string filePath)
        {
            await Task.Run(() =>
            {               
                // 将所有块的结果写入Excel
                using (var package = new ExcelPackage())
                {
                    var workbook = package.Workbook;
                    var worksheet = workbook.Worksheets.Add("Sheet1");
                    // 设置表头
                    worksheet.Cells[1, 1].Value = "键";
                    worksheet.Cells[1, 2].Value = "中文";
                    worksheet.Cells[1, 3].Value = "英文";
                    worksheet.Cells[1, 4].Value = "日文";
                    worksheet.Cells[1, 5].Value = "法文";
                    int row = 2;
                    foreach (var item in largeList)
                    {
                        worksheet.Cells[row, 1].Value = item.key;
                        worksheet.Cells[row, 2].Value = item.ChineseText;
                        worksheet.Cells[row, 3].Value = item.EnglishText;
                        worksheet.Cells[row, 4].Value = item.JPText;
                        worksheet.Cells[row, 5].Value = item.FRText;
                        row++;
                    }

                    // 保存Excel文件
                    package.SaveAs(new FileInfo(filePath));
                }
            });
        }
    }

    public class VModel
    {       
        public VModel(string key, string? chineseText, string? englishText, string? jPText, string? fRText)
        {
            this.key = key;
            ChineseText = chineseText;
            EnglishText = englishText;
            JPText = jPText;
            FRText = fRText;
        }

        public string key { get; set; }

        public string? ChineseText { get; set; }

        public string? EnglishText { get; set; }

        public string? JPText { get; set; }

        public string? FRText { get; set; }
    }
}
