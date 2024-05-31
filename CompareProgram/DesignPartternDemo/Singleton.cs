using DesignPartternDemo.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPartternDemo.Singleton
{
    public class SingleObject
    {

        //创建 SingleObject 的一个对象
        private static SingleObject instance = new SingleObject();

        //让构造函数为 private，这样该类就不会被实例化
        private SingleObject() { }

        //获取唯一可用的对象
        public static SingleObject getInstance()
        {
            return instance;
        }

        public void showMessage()
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class SingletonPatternDemo
    {
        public static void method()
        {

            //不合法的构造函数
            //编译时错误：构造函数 SingleObject() 是不可见的
            //SingleObject object = new SingleObject();

            //获取唯一可用的对象
            var object = SingleObject.getInstance();

            //显示消息
            object.showMessage();
        }
    }

    /// <summary>
    /// 实现方式：懒汉式，线程不安全
    /// </summary>
    public class Singleton
    {
        private static Singleton instance;
        private Singleton() { }

        public static Singleton getInstance()
        {
            if (instance == null)
            {
                instance = new Singleton();
            }
            return instance;
        }
    }

    /// <summary>
    /// 懒汉式，线程安全
    /// </summary>
    public class Singleton2
    {
        private static Singleton2 instance;
        private Singleton2() { }
        public static  Singleton2 getInstance()
        {
            if (instance == null)
            {
                instance = new Singleton2();
            }
            return instance;
        }
    }

    /// <summary>
    /// 饿汉式
    /// </summary>
    public class Singleton3
    {
        private static Singleton3 instance = new Singleton3();
        private Singleton3() { }
        public static Singleton3 getInstance()
        {
            return instance;
        }
    }

    /// <summary>
    /// 双检锁/双重校验锁（DCL，即 double-checked locking）
    /// </summary>
    public class Singleton4
    {
        private volatile static Singleton4 singleton;
        private Singleton4() { }
        public static Singleton4 getSingleton()
        {
            if (singleton == null)
            {
                lock (singleton) {
                    if (singleton == null) {
                        singleton = new Singleton4();
                    }
                }
            }
            return singleton;
        }

        /// <summary>
        /// 登记式/静态内部类
        /// </summary>
        public class Singleton5
        {
            private static class SingletonHolder
            {
                public static readonly Singleton5 INSTANCE = new Singleton5();
            }
            private Singleton5() { }
            public static Singleton5 getInstance()
            {
                return SingletonHolder.INSTANCE;
            }
        }
    }
    /// <summary>
    /// 枚举   c#中，枚举类型中，不能添加方法！！！
    /// </summary>
    public enum Singleton6
    {
        // INSTANCE;
        //    public void whateverMethod()
        //{
        //}  
    }
}
