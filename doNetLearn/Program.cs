
using doNetLearn;
using System.Globalization;


Console.WriteLine("返回指定类型的对象，其值等于指定对象");
{

    Continent cont = Continent.NorthAmerica;
    Console.WriteLine("{0:N2}", Convert.ChangeType(cont, typeof(Double)));
    int number = 6;
    try
    {
        Console.WriteLine("{0}", Convert.ChangeType(number, typeof(Continent)));
    }
    catch (InvalidCastException)
    {
        Console.WriteLine("Cannot convert a Double to a Continent");
    }
    Console.WriteLine("{0}", (Continent)number);

    var d = Convert.ChangeType("2024-09-02", typeof(DateTime));
    Console.WriteLine(d);
}

{
    // 设置当前线程的文化信息为西班牙语（西班牙）
    CultureInfo spanishCulture = new CultureInfo("es-ES");
    Thread.CurrentThread.CurrentCulture = spanishCulture;
    Thread.CurrentThread.CurrentUICulture = spanishCulture;

    // 创建一个DateTime对象
    DateTime now = DateTime.Now;

    // 输出格式化后的DateTime对象
    Console.WriteLine(now.ToString()); // 使用当前线程文化信息进行格式化
}

Console.WriteLine("DateTime kind 属性");
{
    DatetimeAttribute.show();
}


