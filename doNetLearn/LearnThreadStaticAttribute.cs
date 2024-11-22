using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn
{
    /// <summary>
    /// 学习 ThreadStaticAttribute  类
    /// 表示静态字段的值对于每个线程都是唯一的。
    /// </summary>
    public class LearnThreadStaticAttribute
    {

       //以下示例实例化了一个随机数生成器，除了主线程外还创建了十个线程，然后在每个线程中生成200万个随机数。
       //它使用ThreadStaticAttribute属性来计算每个线程的随机数的总和和计数。
       //它还定义了两个额外的每个线程字段，即先前字段和异常字段，使其能够检测随机数生成器的损坏。

        [ThreadStatic] static double previous = 0.0;
        [ThreadStatic] static double sum = 0.0;
        [ThreadStatic] static int calls = 0;
        [ThreadStatic] static bool abnormal;
        static int totalNumbers = 0;
        static CountdownEvent countdown;
        private static Object lockObj;
        Random rand;

        public LearnThreadStaticAttribute()
        {
            rand = new Random();
            lockObj = new Object();
            countdown = new CountdownEvent(1);
        }

        public static void Method()
        {
            LearnThreadStaticAttribute ex = new LearnThreadStaticAttribute();
            Thread.CurrentThread.Name = "Main";
            ex.Execute();
            countdown.Wait();
            Console.WriteLine("{0:N0} random numbers were generated.", totalNumbers);
        }

        private void Execute()
        {
            for (int threads = 1; threads <= 10; threads++)
            {
                Thread newThread = new Thread(new ThreadStart(this.GetRandomNumbers));
                countdown.AddCount();
                newThread.Name = threads.ToString();
                newThread.Start();
            }
            this.GetRandomNumbers();
        }

        private void GetRandomNumbers()
        {
            double result = 0.0;

            for (int ctr = 0; ctr < 2000000; ctr++)
            {
                lock (lockObj)
                {
                    result = rand.NextDouble();
                    calls++;
                    Interlocked.Increment(ref totalNumbers);
                    // We should never get the same random number twice.
                    if (result == previous)
                    {
                        abnormal = true;
                        break;
                    }
                    else
                    {
                        previous = result;
                        sum += result;
                    }
                }
            }
            // get last result
            if (abnormal)
                Console.WriteLine("Result is {0} in {1}", previous, Thread.CurrentThread.Name);

            Console.WriteLine("Thread {0} finished random number generation.", Thread.CurrentThread.Name);
            Console.WriteLine("Sum = {0:N4}, Mean = {1:N4}, n = {2:N0}\n", sum, sum / calls, calls);
            countdown.Signal();
        }
    }
    // The example displays output similar to the following:
    //    Thread 1 finished random number generation.
    //    Sum = 1,000,556.7483, Mean = 0.5003, n = 2,000,000
    //    
    //    Thread 6 finished random number generation.
    //    Sum = 999,704.3865, Mean = 0.4999, n = 2,000,000
    //    
    //    Thread 2 finished random number generation.
    //    Sum = 999,680.8904, Mean = 0.4998, n = 2,000,000
    //    
    //    Thread 10 finished random number generation.
    //    Sum = 999,437.5132, Mean = 0.4997, n = 2,000,000
    //    
    //    Thread 8 finished random number generation.
    //    Sum = 1,000,663.7789, Mean = 0.5003, n = 2,000,000
    //    
    //    Thread 4 finished random number generation.
    //    Sum = 999,379.5978, Mean = 0.4997, n = 2,000,000
    //    
    //    Thread 5 finished random number generation.
    //    Sum = 1,000,011.0605, Mean = 0.5000, n = 2,000,000
    //    
    //    Thread 9 finished random number generation.
    //    Sum = 1,000,637.4556, Mean = 0.5003, n = 2,000,000
    //    
    //    Thread Main finished random number generation.
    //    Sum = 1,000,676.2381, Mean = 0.5003, n = 2,000,000
    //    
    //    Thread 3 finished random number generation.
    //    Sum = 999,951.1025, Mean = 0.5000, n = 2,000,000
    //    
    //    Thread 7 finished random number generation.
    //    Sum = 1,000,844.5217, Mean = 0.5004, n = 2,000,000
    //    
    //    22,000,000 random numbers were generated.
}