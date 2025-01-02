
using BenchmarkDotNet.Running;
using doNetLearn;
using doNetLearn.DataTypes;
using doNetLearn.TypeConversion;
using nietras.SeparatedValues;
using System.Globalization;
using System.Text.Json;


var timeZones = TimeZoneInfo.GetSystemTimeZones();

// 打印每个时区的显示名称
foreach (var timeZone in timeZones)
{
Console.WriteLine(timeZone.Id);
}

//Console.WriteLine("返回指定类型的对象，其值等于指定对象");
//{

//    Continent cont = Continent.NorthAmerica;
//    Console.WriteLine("{0:N2}", Convert.ChangeType(cont, typeof(Double)));
//    int number = 6;
//    try
//    {
//        Console.WriteLine("{0}", Convert.ChangeType(number, typeof(Continent)));
//    }
//    catch (InvalidCastException)
//    {
//        Console.WriteLine("Cannot convert a Double to a Continent");
//    }
//    Console.WriteLine("{0}", (Continent)number);

//    var d = Convert.ChangeType("2024-09-02", typeof(DateTime));
//    Console.WriteLine(d);
//}

//{
//// 设置当前线程的文化信息为西班牙语（西班牙）
//CultureInfo spanishCulture = new CultureInfo("es-ES");
//Thread.CurrentThread.CurrentCulture = spanishCulture;
//Thread.CurrentThread.CurrentUICulture = spanishCulture;

//// 创建一个DateTime对象
//DateTime now = DateTime.Now;

//// 输出格式化后的DateTime对象
//Console.WriteLine(now.ToString()); // 使用当前线程文化信息进行格式化
//}

//Console.WriteLine("DateTime kind 属性");
//{
//    DatetimeAttribute.show();
//}

//未成功
//Console.WriteLine("人脸识别接口试用");
{
    //// 加载已知的人脸图片
    //var knownImage = Image.Load("known_face.jpg");   //这？

    //// 将图片中的人脸特征提取出来
    //var knownFaceEncoding = FaceRecognition.FaceEncodings(knownImage)[0].ToArray();

    //// 加载待检测的人脸图片
    //var image = Image.FromFile("unknown_face.jpg");

    //// 也提取待检测图片中的人脸特征
    //var faceEncodings = FaceRecognition.FaceEncodings(image);

    //// 遍历所有检测到的人脸
    //foreach (var faceEncoding in faceEncodings)
    //{
    //    // 使用FaceDistance来比较两个人脸特征的相似度
    //    var faceDistance = FaceRecognition.FaceDistance(new[] { knownFaceEncoding }, faceEncoding.ToArray());

    //    // 如果faceDistance小于一个阈值，我们可以认为这是同一个人脸
    //    if (faceDistance < 0.6)
    //    {
    //        Console.WriteLine($"Matched {faceDistance}!");
    //    }
    //    else
    //    {
    //        Console.WriteLine($"Unmatched {faceDistance}!");
    //    }
    //}
}

{
    //Json序列化如何实现的

    ////为什么不一样呢？
    //ParentClass parentClass = new ParentClass();

    //ChildClass child = new ChildClass();
    //string json = JsonSerializer.Serialize(child);
    //Console.WriteLine(json);


    //var s=parentClass.GetformValue(child);
    //Console.WriteLine(s);


    //DatetimeAttribute.TimeZoneExample();
}

{
    //ValueTypeDemo.SpanLearn();

    //ArraySegmentLearn.Method();

    //ArraySegmentLearn arraySegmentLearn = new ArraySegmentLearn();
    //await arraySegmentLearn.Method2();

    //ArraySegmentLearn.Method3();

    //SpanBenchmark spanBenchmark = new SpanBenchmark();
    //spanBenchmark.ReserveUseSpan();
    //spanBenchmark.ReserveMethod();

   // var summary = BenchmarkRunner.Run<SpanBenchmark>();

    //BenchmarkRunner.Run<VerifyDecimalPlaces>();
}

//{
//    TypeConversionDemo typeConversionDemo = new TypeConversionDemo();

//    typeConversionDemo.Method();
//    typeConversionDemo.Method2();
//    typeConversionDemo.Method3();
//    typeConversionDemo.Method4();
//}

{
    //DateTime? CalibrationDate = null;
    //if (DateTime.TryParseExact("2019-12-20", "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime date))
    //{
    //    CalibrationDate = date;
    //}
    //if (DateTime.TryParseExact("2019-12-20", "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture, DateTimeStyles.AllowLeadingWhite, out DateTime date1))
    //{
    //    CalibrationDate = date1;
    //}
    //if (DateTime.TryParseExact("2019-12-20", "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture, DateTimeStyles.AdjustToUniversal, out DateTime date2))
    //{
    //    CalibrationDate = date2;
    //}
    
}


//sep demo
{

    //{
    //    // 指定CSV文件的路径
    //    string filePath = "file.csv";

    //    // 使用Sep库创建一个CSV读取器，从文件中读取数据
    //    using var reader = Sep.Reader().FromFile(filePath);

    //    // 遍历CSV文件中的每一行
    //    foreach (var readRow in reader)
    //    {
    //        // 假设我们知道CSV文件的列结构，可以直接通过列名访问数据
    //        string columnA = readRow["A"].ToString();
    //        string columnB = readRow["B"].ToString();
    //        int columnC = readRow["C"].Parse<int>();
    //        double columnD = readRow["D"].Parse<double>();

    //        // 处理每一行的数据
    //        Console.WriteLine($"A: {columnA}, B: {columnB}, C: {columnC}, D: {columnD}");
    //    }
    //}
    //{
    //    ReadCSV readCSV = new ReadCSV();

    //    readCSV.ProcessProducts(@"C:\Users\yingying.zhu\Downloads\11.csv", "样品生产条码");
    //}


    //{
    //    // 定义一个多行字符串，表示一个CSV格式的数据。
    //    var text = """
    //       A;B;C;D;E;F
    //       Sep;🚀;1;1.2;0.1;0.5
    //       CSV;✅;2;2.2;0.2;1.5
    //       """;

    //    // 使用Sep库创建一个CSV读取器，自动从标题行推断分隔符。
    //    using var reader = Sep.Reader().FromText(text);

    //    // 根据读取器的规格创建一个写入器，准备将数据写入文本。
    //    using var writer = reader.Spec.Writer().ToText();

    //    // 获取列"B"在标题中的索引位置。
    //    var idx = reader.Header.IndexOf("B");
    //    // 定义一个包含列名的数组。
    //    var nms = new[] { "E", "F" };

    //    // 遍历读取器中的每一行数据。
    //    foreach (var readRow in reader)
    //    {
    //        // 将列"A"读取为只读的字符跨度。
    //        var a = readRow["A"].Span;
    //        // 将列"B"的值转换为字符串。
    //        var b = readRow[idx].ToString();
    //        // 将列"C"的值解析为整数。
    //        var c = readRow["C"].Parse<int>();
    //        // 将列"D"的值解析为浮点数，使用csFastFloat库进行快速解析。
    //        var d = readRow["D"].Parse<float>();
    //        // 将列"E"和"F"的值解析为双精度浮点数的跨度。
    //        var s = readRow[nms].Parse<double>();
    //        // 遍历解析后的数值，并将每个值乘以10。
    //        foreach (ref var v in s) { v *= 10; }

    //        // 开始写入新一行数据，行数据在Dispose时写入。
    //        using var writeRow = writer.NewRow();
    //        // 通过只读的字符跨度设置列"A"的值。
    //        writeRow["A"].Set(a);
    //        // 通过字符串设置列"B"的值。
    //        writeRow["B"].Set(b);
    //        // 通过插值字符串处理器设置列"C"的值，不会产生新的内存分配。
    //        writeRow["C"].Set($"{c * 2}");
    //        // 格式化列"D"的值，将数值除以2。
    //        writeRow["D"].Format(d / 2);
    //        // 直接格式化多个列的值。
    //        writeRow[nms].Format(s);
    //    }

    //    Console.WriteLine(writer.ToString());
    //}



    {

        //DataTypeDemo dataTypeDemo = new DataTypeDemo();
        //dataTypeDemo.MethodDictionary();

    }


    {
        CopyExample copyExample = new CopyExample();
        copyExample.Method();
    }
}