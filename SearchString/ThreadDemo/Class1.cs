using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchString.ThreadDemo
{
    public class Class1
    {
        /// <summary>
        /// 尝试自带方法
        /// > Parallel : for、foreach、Invoke
        /// > PLINQ AsParallel、AsSequential 、AsOrdered
        /// 
        /// </summary>
        public void Test()
        {
            var inputs = Enumerable.Range(1, 20).ToArray();
            var outputs=new int[inputs.Length];

            var sw = Stopwatch.StartNew();

            //1.
            //for (int i = 0; i < inputs.Length; i++)
            //{
            //    outputs[i] = HeavyJob(inputs[i]);
            //}

            //2.
            Parallel.For(0, inputs.Length, i => outputs[i]=HeavyJob(inputs[i]));

            //3.
            outputs=inputs.AsParallel().AsOrdered().Select(x=> HeavyJob(x)).ToArray();

            Console.WriteLine($"Elapsed time:{sw.ElapsedMilliseconds}ms");

        }

        public int HeavyJob(int input)
        {
            Thread.Sleep(100);
            return input * input;
        }

        public void PrintArray(IEnumerable<int> arrs)
        {
            foreach (int i in arrs)
            {
                Console.WriteLine(i);
            }
        }
    }
}
