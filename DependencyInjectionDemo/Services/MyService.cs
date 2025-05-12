using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Services
{
    /// <summary>
    /// 注册多个实现，并通过 IEnumerable<IRabbitMQConnection> 获取所有实例，适用于需要遍历所有实现或动态选择的情况
    /// 适用场景：
    /// 1.需要遍历所有实现（如插件系统）
    /// 2.运行时动态选择某个实现
    /// </summary>
    internal class MyService
    {
        private readonly IEnumerable<IPhone> phones;

        public MyService(IEnumerable<IPhone> phones)
        {
            this.phones = phones;
        }

        public void GetPhoneName()
        {
            var statisPhone = phones.FirstOrDefault(c => c is HonorPhone);

            Console.WriteLine(statisPhone.GetName());
        }
    }

    /// <summary>
    /// 使用命名/键控依赖注入（Keyed Services，.NET 8+ 内置支持） 适用于按名称/键选择不同实现的情况
    /// 1.需要显式指定不同的实现（如主备切换）
    /// 2..NET 8+ 推荐方式
    /// </summary>
    internal class MyService2
    {

        private readonly IPhone _defaultConnection;
        private readonly IPhone _secondaryConnection;

        public MyService2(
            [FromKeyedServices("default")] IPhone defaultConnection,
            [FromKeyedServices("secondary")] IPhone secondaryConnection)
        {
            _defaultConnection = defaultConnection;
            _secondaryConnection = secondaryConnection;
        }
        public void GetPhoneName()
        {
            Console.WriteLine(_defaultConnection.GetName());
            Console.WriteLine(_secondaryConnection.GetName());
        }
    }


    /// <summary>
    /// 使用工厂模式（自定义选择逻辑）  或策略模式，其实原理差不多根据配置或option去选择实现类
    /// 1.需要动态切换实现（如 A/B 测试、环境变量控制）
    /// 2.逻辑较复杂时使用
    /// </summary>
    internal class MyService3
    {

        private readonly IPhone _connection;

        public MyService3(IPhone connection)
        {
            _connection = connection; // 具体实现由工厂决定
        }
    }
}
