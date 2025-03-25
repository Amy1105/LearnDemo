namespace doNetLearn.CSharpGrammer
{
    public class CSharp12
    {
//主构造函数

        public static void Method1()
        {


        }
        //集合表达式
        public static void Method2()
        {
            // Create an array:
            int[] a = [1, 2, 3, 4, 5, 6, 7, 8];

            // Create a list:
            List<string> b = ["one", "two", "three"];

            // Create a span
            Span<char> c = ['a', 'b', 'c', 'd', 'e', 'f', 'h', 'i'];

            // Create a jagged 2D array:
            int[][] twoD = [[1, 2, 3], [4, 5, 6], [7, 8, 9]];

            // 从变量创建锯齿状二维数组：
            int[] row0 = [1, 2, 3];
            int[] row1 = [4, 5, 6];
            int[] row2 = [7, 8, 9];
            int[][] twoDFromVariables = [row0, row1, row2];
          
            int[] single = [.. row0, .. row1, .. row2];
            foreach (var element in single)
            {
                Console.Write($"{element}, ");
            }
            // output:
            // 1, 2, 3, 4, 5, 6, 7, 8, 9,
        }

        //ref readonly 个参数
        public static void Method3()
        {


        }

        //默认 lambda 参数
        public static void Method4()
        {


        }

        //任何类型的别名
        public static void Method5()
        {


        }

        //内联数组
        public static void Method6()
        {


        }
        //Experimental 属性
        public static void Method7()
        {


        }

        //拦截 器
        public static void Method8()
        {


        }
    }
}
