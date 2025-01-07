using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceGeneratorDemo
{
    public record Member(string MemberId, string Name);


    public partial class OrderLogger
    {
        private readonly ILogger _logger;

        public OrderLogger(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 使用字符串插值记录
        /// </summary>
        public void LogByStringInterpolation(Member member, DateTime now) =>
            _logger.LogInformation($"会员[{member}]在[{now:yyyy-MM-dd HH:mm:ss}]充值了一个小目标");

        /// <summary>
        /// 使用参数化记录
        /// </summary>
        public void LogByStructure(Member member, DateTime now) =>
            _logger.LogInformation("会员[{Member}]在[{Now:yyyy-MM-dd HH:mm:ss}]充值了一个小目标", member, now);

        /// <summary>
        /// 使用源代码生成
        /// </summary>
        [LoggerMessage(
            EventId = 0,
            Level = LogLevel.Information,
            Message = "会员[{member}]在[{Now:yyyy-MM-dd HH:mm:ss}]充值了一个小目标")]
        public static partial void LogBySourceGenerator(ILogger logger, Member member, DateTime now);
    }
}
