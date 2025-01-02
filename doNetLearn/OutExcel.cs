using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn
{
    public class OutExcel
    {
        //在添加或读取元素之前，会阻塞进程一直等待
        BlockingCollection<DataModel> blockingCollection = null;
        int count = 0;
        ISheet sheet1 = null;
        int sheetNum = 0;
        XSSFWorkbook _workbook = null;
        object lockObj = new object();

        public void GetData()
        {
            Task.Run(() =>
            {
                using (SqlConnection connection = new SqlConnection(""))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("select * from table1");
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        DataModel dataModel = new DataModel();
                        bool res = false;
                        while (!res)
                        {
                            res = blockingCollection.TryAdd(dataModel, 100);
                            if (blockingCollection.Count > 1000)
                            {
                                Console.WriteLine($"队列已满！");
                            }
                        }
                    }
                    blockingCollection.CompleteAdding();  //指示不再添加项                    
                }
            });
        }

        public void CreateExcel()
        {
            _workbook = new XSSFWorkbook();
            sheet1 = _workbook.CreateSheet($"Sheet{sheetNum}");

            List<Task> list = new List<Task>();

            list.Add(Task.Run(() =>
            {
                Insert();
            }));

            list.Add(Task.Run(() =>
            {
                Insert();
            }));
            list.Add(Task.Run(() =>
            {
                Insert();
            }));

            Task.WhenAll(list).ContinueWith(t =>
            {
                Console.WriteLine("导出结束");
                using (FileStream file = new FileStream(
                    System.DateTime.Now.Ticks.ToString() + ".xls", FileMode.Create))
                {
                    _workbook.Write(file);
                }
            });
        }

        private void Insert()
        {
            while (!blockingCollection.IsCompleted) //集合何时为空且不再添加项
            {
                bool lockTaken = false;
                while (!lockTaken)
                {
                    Monitor.TryEnter(lockObj, 100, ref lockTaken);
                }
                try
                {
                    if (count > 0 && (count % 1000000 == 0))
                    {
                        sheetNum++;
                        count = 0;
                        sheet1 = _workbook.CreateSheet($"开始创建第{sheetNum}个工作簿");
                    }
                    if (count > 0 && (count % 100000 == 0))
                    {
                        Console.WriteLine($"导入{count}条！");
                    }

                    DataModel? item = null;
                    blockingCollection?.TryTake(out item);
                    if (item != null)
                    {
                        IRow row = sheet1.CreateRow(count);

                        row.CreateCell(0).SetCellValue(item.ID);
                        row.CreateCell(1).SetCellValue(item.ProjectCode);
                        row.CreateCell(2).SetCellValue(item.ProjectLeader);
                        row.CreateCell(3).SetCellValue(item.ProjectFund);
                        count++;
                    }
                }
                finally
                {
                    Monitor.Exit(lockObj);
                }
            }
        }


    }

    public class DataModel
    {
        public string ID { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectLeader { get; set; }
        public string ProjectFund { get; set; }
    }
}
