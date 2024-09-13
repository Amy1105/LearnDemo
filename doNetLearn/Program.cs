
using System;
using System.Globalization;

////Convert.ChangeType 会根据当前区域获取数据
//// Convert a Continent to a Double.
//Continent cont = Continent.NorthAmerica;
//        Console.WriteLine("{0:N2}",Convert.ChangeType(cont, typeof(Double)));

//        // Convert a Double to a Continent.
//        Double number = 6.0;
//        try
//        {
//            Console.WriteLine("{0}",Convert.ChangeType(number, typeof(Continent)));
//        }
//        catch (InvalidCastException)
//        {
//            Console.WriteLine("Cannot convert a Double to a Continent");
//        }
//        Console.WriteLine("{0}", (Continent)number);

//var d=Convert.ChangeType("2024-09-02", typeof(DateTime));

//Console.WriteLine(d);

// 设置当前线程的文化信息为西班牙语（西班牙）
CultureInfo spanishCulture = new CultureInfo("es-ES");
Thread.CurrentThread.CurrentCulture = spanishCulture;
Thread.CurrentThread.CurrentUICulture = spanishCulture;

// 创建一个DateTime对象
DateTime now = DateTime.Now;

// 输出格式化后的DateTime对象
Console.WriteLine(now.ToString()); // 使用当前线程文化信息进行格式化

