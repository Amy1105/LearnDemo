using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.DataTypes
{
    /// <summary>
    /// 引用类型中的集合类型
    /// </summary>
    internal class DataTypeDemo
    {

        public void MethodDictionary()
        {
            //重复key会报错
            Dictionary<int, string> msgs = new Dictionary<int, string>()
{
    { 1, "Hello, "},
    { 2 , "World"},
    { 3, "!"}
};

            //重复key，后面的覆盖掉旧的，程序不报错
            Dictionary<int, string> MsgErrs = new Dictionary<int, string>()
            {
                [1] = "Hello, ",
                [2] = "World",
                [2] = "!",
            };

            Console.WriteLine("ok");
            /*            
            上述两者在初始化赋值时都差不多，但是两者还是有一些区别，前者在初始化时出现重复key值，程序会直接报错。
            而后者初始化时，key可以有重复值，系统会自动过滤掉重复的key值，程序也不会报错。            
             */

            ConcurrentDictionary<int, string> keys = new ConcurrentDictionary<int, string>();
            keys.TryAdd(1, "LL");
            keys.TryAdd(2, "LL");
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(1, "OJ");
            dic.TryAdd(2, "R");
            Stopwatch stopwatch = new Stopwatch();

            #region 写入
            stopwatch.Start();
            Parallel.For(0, 2000000, i =>
            {
                lock (dic)
                {
                    dic[i] = new Random().Next(100, 99999).ToString();
                }
            });
            stopwatch.Stop();
            Console.WriteLine("Dictionary加锁写入花费时间：{0}", stopwatch.Elapsed);

            stopwatch.Restart();

            Parallel.For(0, 2000000, i =>
            {
                keys[i] = new Random().Next(100, 99999).ToString();
            });
            stopwatch.Stop();
            Console.WriteLine("ConcurrentDictionary加锁写入花费时间：{0}", stopwatch.Elapsed);
            #endregion


            #region 读取
            string result = string.Empty;
            stopwatch.Restart();
            Parallel.For(0, 2000000, i =>
            {
                lock (dic)
                {
                    result = dic[i];
                }
            });
            stopwatch.Stop();
            Console.WriteLine("Dictionary加锁读取花费时间：{0}", stopwatch.Elapsed);

            stopwatch.Restart();
            Parallel.For(0, 2000000, i =>
            {
                result = keys[i];
            });
            stopwatch.Stop();
            Console.WriteLine("ConcurrentDictionary加锁读取花费时间：{0}", stopwatch.Elapsed);
            #endregion

            Console.ReadLine();

           // 经过对两者的比较，ConcurrentDictionary读取性能更好，Dictionary写入性能更好
        }
    }
}
