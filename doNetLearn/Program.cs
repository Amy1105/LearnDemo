
using doNetLearn;

////Convert.ChangeType 会根据当前区域获取数据
//// Convert a Continent to a Double.
//Continent cont = Continent.NorthAmerica;
//        Console.WriteLine("{0:N2}",Convert.ChangeType(cont, typeof(Double)));

Console.WriteLine("返回指定类型的对象，其值等于指定对象");
{

    Continent cont = Continent.NorthAmerica;
    Console.WriteLine("{0:N2}", Convert.ChangeType(cont, typeof(Double)));

    Double number = 6.0;
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



Console.WriteLine("DateTime kind 属性");
{
    DatetimeAttribute.show();
}


