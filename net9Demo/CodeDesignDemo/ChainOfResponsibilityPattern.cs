using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net9Demo.CodeDesignDemo
{
    /// <summary>
    /// 允许将请求沿着处理者链传递，直到请求被处理为止;
    /// 当有多个对象可以处理请求，且具体由哪个对象处理由运行时决定时
    /// </summary>

    public abstract class AbstractLogger
    {
        public static int INFO = 1;
        public static int DEBUG = 2;
        public static int ERROR = 3;

        protected int level;

        //责任链中的下一个元素
        protected AbstractLogger nextLogger;

        public void setNextLogger(AbstractLogger nextLogger)
        {
            this.nextLogger = nextLogger;
        }

        public void logMessage(int level, String message)
        {
            if (this.level <= level)
            {
                write(message);
            }
            if (nextLogger != null)
            {
                nextLogger.logMessage(level, message);
            }
        }

        abstract protected void write(String message);

    }

    public class ConsoleLogger : AbstractLogger
    {
        public ConsoleLogger(int level)
        {
            this.level = level;
        }

        protected override void write(String message)
        {
            Console.WriteLine("Standard Console::Logger: " + message);
        }
    }

    public class ErrorLogger : AbstractLogger
    {
        public ErrorLogger(int level)
        {
            this.level = level;
        }

        protected override void write(String message)
        {
            Console.WriteLine("Error Console::Logger: " + message);
        }
    }

    public class FileLogger : AbstractLogger
    {


        public FileLogger(int level)
        {
            this.level = level;
        }

        protected override void write(String message)
        {
            Console.WriteLine("File::Logger: " + message);
        }
    }

    public class ChainPatternDemo
    {
        private static AbstractLogger getChainOfLoggers()
        {

            AbstractLogger errorLogger = new ErrorLogger(AbstractLogger.ERROR);
            AbstractLogger fileLogger = new FileLogger(AbstractLogger.DEBUG);
            AbstractLogger consoleLogger = new ConsoleLogger(AbstractLogger.INFO);

            errorLogger.setNextLogger(fileLogger);
            fileLogger.setNextLogger(consoleLogger);

            return errorLogger;
        }

        public static void Test()
        {
            AbstractLogger loggerChain = getChainOfLoggers();

            loggerChain.logMessage(AbstractLogger.INFO, "This is an information.");

            loggerChain.logMessage(AbstractLogger.DEBUG,
               "This is a debug level information.");

            loggerChain.logMessage(AbstractLogger.ERROR,
               "This is an error information.");
        }
    }
}