
using doNetLearn;
using System.Text.Json;


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
//    // 设置当前线程的文化信息为西班牙语（西班牙）
//    CultureInfo spanishCulture = new CultureInfo("es-ES");
//    Thread.CurrentThread.CurrentCulture = spanishCulture;
//    Thread.CurrentThread.CurrentUICulture = spanishCulture;

//    // 创建一个DateTime对象
//    DateTime now = DateTime.Now;

//    // 输出格式化后的DateTime对象
//    Console.WriteLine(now.ToString()); // 使用当前线程文化信息进行格式化
//}

//Console.WriteLine("DateTime kind 属性");
//{
//    DatetimeAttribute.show();
//}

//未成功
Console.WriteLine("人脸识别接口试用");
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

//为什么不一样呢？
ParentClass parentClass = new ParentClass();

ChildClass child = new ChildClass();
string json = JsonSerializer.Serialize(child);
Console.WriteLine(json);


var s=parentClass.GetformValue(child);
Console.WriteLine(s);
