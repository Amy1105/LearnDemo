using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.LinqDemo
{
    internal class SelectService
    {
        public static void Demo()
        {
            //投影是指将对象转换为一种新形式的操作，该形式通常只包含那些将随后使用的属性。 通过使用投影，您可以构造从每个对象生成的新类型。 可以投影属性，并对该属性执行数学函数。 还可以在不更改原始对象的情况下投影该对象。
            //投影运算 Select  SelectMany  Zip

            Console.WriteLine("---Select---");
            {
                List<string> words = ["an", "apple", "a", "day"];
                var query = from word in words
                            select word.Substring(0, 1);

                foreach (string s in query)
                {
                    Console.WriteLine(s);
                }
            }

            {
                List<string> words = ["an", "apple", "a", "day"];

                var query = words.Select(word => word.Substring(0, 1));

                foreach (string s in query)
                {
                    Console.WriteLine(s);
                }
            }
            Console.WriteLine("---SelectMany---");
            {
                List<string> phrases = ["an apple a day", "the quick brown fox"];
                var query = from phrase in phrases
                            from word in phrase.Split(' ')
                            select word;

                foreach (string s in query)
                {
                    Console.WriteLine(s);
                }
            }

            //该方法 SelectMany 多个数据源
            {
                List<string> phrases = ["an apple a day", "the quick brown fox"];
                var query = phrases.SelectMany(phrases => phrases.Split(' '));
                foreach (string s in query)
                {
                    Console.WriteLine(s);
                }
            }

            // An int array with 7 elements.
            IEnumerable<int> numbers = [1, 2, 3, 4, 5, 6, 7];
            // A char array with 6 elements.
            IEnumerable<char> letters = ['A', 'B', 'C', 'D', 'E', 'F'];

            //该方法 SelectMany 还可以形成匹配第一个序列中的每个项与第二个序列中的每个项的组合,笛卡尔积的效果
            {
                var query = from number in numbers
                            from letter in letters
                            select (number, letter);

                foreach (var item in query)
                {
                    Console.WriteLine(item);
                }
            }

            {
                var method = numbers
                .SelectMany(number => letters,
                (number, letter) => (number, letter));

                foreach (var item in method)
                {
                    Console.WriteLine(item);
                }
            }

            Console.WriteLine("---Zip---");
            //Zip 投影运算符有多个重载。 所有 Zip 方法都处理两个或更多可能是异构类型的序列。 前两个重载返回元组，具有来自给定序列的相应位置类型。
            {
                foreach ((int number, char letter) in numbers.Zip(letters))
                {
                    Console.WriteLine($"Number: {number} zipped with letter: '{letter}'");
                }
            }
        }
    }
}
