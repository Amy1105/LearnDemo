namespace doNetLearn.DesignPatterns
{
    /// <summary>
    ///  什么是责任链模式？
    ///  责任链模式构建一个处理器对象链，每个对象决定是否处理请求或将其传递到下一环节。其核心优势在于：
    ///  • 解耦性：请求发送者无需知晓最终由哪个处理器处理。
    ///  • 扩展性：增删处理器不影响链中其他组件。
    ///  工作原理
    ///  1. 客户端发起请求
    ///  2. 处理器链中每个对象依次处理或传递请求
    ///  3. 当某个处理器处理请求或链结束时终止流程
    /// 
    /// </summary>
    public class CoRPattern
    {        
        /// <summary>
        /// 责任链 vs 多重if-else：谁更胜一筹？
        /// 步骤3：构建处理链并运行
        /// </summary>
       public static void execute()
        {
            var consoleHandler = new ConsoleLogHandler();
            var fileHandler = new FileLogHandler();
            var errorHandler = new ErrorLogHandler();

            // 构建责任链
            consoleHandler.SetNext(fileHandler);
            fileHandler.SetNext(errorHandler);

            // 触发处理
            consoleHandler.Handle("这是一条信息级日志", LogLevel.Info);
            consoleHandler.Handle("这是一条警告级日志", LogLevel.Warning);
            consoleHandler.Handle("这是一条错误级日志", LogLevel.Error);
        }

        /// <summary>
        /// 
        /// 
        /// 缺陷：
        /// • 紧耦合逻辑：增减条件需修改同一方法，违反开闭原则。
        /// • 代码僵化：条件分支难以扩展或调整顺序。
        /// • 职责混杂：条件判断与业务处理逻辑混杂。
        /// </summary>
        public class DiscountHandler0
        {
            public string ApplyDiscount(decimal amount)
            {
                if (amount > 1000)
                    return "10%折扣已应用";
                else if (amount > 500)
                    return "5%折扣已应用";
                else
                    return "无折扣";
            }
        }

        /// <summary>
        /// 将条件拆分为独立处理器，动态组合处理链
        /// 
        /// 
        /// 优势：
        /// • 灵活组合：处理器可动态增删或调整顺序。
        /// • 单一职责：每个处理器仅关注自身逻辑。
        /// • 易于测试：独立处理器可单独验证。
        /// </summary>
        public abstract class DiscountHandler
        {
            protected DiscountHandler _nextHandler;

            public void SetNext(DiscountHandler nextHandler)
                => _nextHandler = nextHandler;

            public abstract string Handle(decimal amount);
        }

        public class HighDiscountHandler : DiscountHandler
        {
            public override string Handle(decimal amount)
            {
                if (amount > 1000) return "10%折扣已应用";
                else return _nextHandler?.Handle(amount) ?? "无折扣";
            }
        }

        public class MediumDiscountHandler : DiscountHandler
        {
            public override string Handle(decimal amount)
            {
                if (amount > 500) return "5%折扣已应用";
                else return _nextHandler?.Handle(amount) ?? "无折扣";
            }
        }


        //责任链模式的黄金场景 CoR不仅替代if-else，更适用于复杂业务流：
        //        1. 请求处理管道
        //案例：日志框架中，日志消息依次经过控制台、文件、远程服务器处理器。

        //2. 验证链
        //案例：用户输入验证，如邮箱格式、手机号有效性、密码强度等独立校验。

        //3. 条件化工作流
        //案例：电商结账流程，依次处理折扣计算、税费核算、支付方式选择。



    }


    // 实战：.NET Core中的日志处理框架
    //场景：按日志级别（Info/Warning/Error）分发处理


    /// <summary>
    /// 步骤1：定义基础处理器
    /// </summary>
    public abstract class LogHandler
    {
        protected LogHandler _nextHandler;

        public void SetNext(LogHandler nextHandler)
            => _nextHandler = nextHandler;

        public abstract void Handle(string message, LogLevel level);
    }

    public enum LogLevel { Info, Warning, Error }

    /// <summary>
    /// 步骤2：实现具体处理器
    /// </summary>
    public class ConsoleLogHandler : LogHandler
    {
        public override void Handle(string message, LogLevel level)
        {
            if (level == LogLevel.Info)
                Console.WriteLine($"控制台日志：{message}");
            else
                _nextHandler?.Handle(message, level);
        }
    }

    public class FileLogHandler : LogHandler
    {
        public override void Handle(string message, LogLevel level)
        {
            if (level == LogLevel.Warning)
                Console.WriteLine($"文件日志：{message}"); // 模拟写入文件
            else
                _nextHandler?.Handle(message, level);
        }
    }

    public class ErrorLogHandler : LogHandler
    {
        public override void Handle(string message, LogLevel level)
        {
            if (level == LogLevel.Error)
                Console.WriteLine($"错误日志：{message}");
            else
                _nextHandler?.Handle(message, level);
        }
    }
    class program2
    {
        
    }
}