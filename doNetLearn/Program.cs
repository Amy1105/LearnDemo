
using System;


        // Convert a Continent to a Double.
        Continent cont = Continent.NorthAmerica;
        Console.WriteLine("{0:N2}",Convert.ChangeType(cont, typeof(Double)));

        // Convert a Double to a Continent.
        Double number = 6.0;
        try
        {
            Console.WriteLine("{0}",Convert.ChangeType(number, typeof(Continent)));
        }
        catch (InvalidCastException)
        {
            Console.WriteLine("Cannot convert a Double to a Continent");
        }
        Console.WriteLine("{0}", (Continent)number);

var d=Convert.ChangeType("2024-09-02", typeof(DateTime));

Console.WriteLine(d);
