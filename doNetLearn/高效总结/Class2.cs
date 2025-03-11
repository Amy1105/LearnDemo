using System.Globalization;

namespace doNetLearn.高效总结
{
    public class Class2
    {
        public class StockTrade
        {
            public int Id { get; set; }
            public string Symbol { get; set; }          // 股票代码
            public decimal Price { get; set; }          // 价格
            public int Quantity { get; set; }           // 数量
            public TradeType Type { get; set; }          // 交易类型
            public string UserId { get; set; }           // 用户ID
            public decimal TotalAmount { get; set; }     // 总金额
            public byte[] UserCredentials { get; set; }  // 用户凭证（敏感数据！）
            public List<TradeHistory> History { get; set; } // 交易历史
            public DateTime ExecutionTime { get; set; }  // 执行时间
        }

        //// 我们曾直接将此模型暴露给前端！
        //[HttpPost("execute-trade")]
        //publicasync Task<ActionResult<StockTrade>> ExecuteTrade(TradeRequest request)
        //{
        //    var trade = await _tradeService.ExecuteTrade(request);
        //    return Ok(trade);  // 直接返回，泄露敏感数据和内部细节
        //}

        public class TradeDto
        {
            public TradeDto(StockTrade trade)
            {
                if (trade == null) throw new ArgumentNullException(nameof(trade));

                Id = trade.Id;
                Symbol = trade.Symbol;
                FormattedPrice = FormatCurrency(trade.Price); // 格式化价格
                Quantity = trade.Quantity;
                Type = trade.Type;
                TotalValue = FormatCurrency(trade.TotalAmount); // 格式化总金额
                ExecutionTime = trade.ExecutionTime.ToUniversalTime(); // 统一时间格式
                Status = DetermineTradeStatus(trade); // 计算交易状态
            }

            private string FormatCurrency(decimal amount)
            {
                return amount.ToString("C", CultureInfo.GetCultureInfo("en-US")); // 美式货币格式
            }

            private TradeStatus DetermineTradeStatus(StockTrade trade)
            {
                if (trade.ExecutionTime.AddSeconds(30) < DateTime.UtcNow)
                    return TradeStatus.Settled; // 交易已结算

                return trade.Type == TradeType.Buy
                    ? TradeStatus.Buying  // 买入中
                    : TradeStatus.Selling; // 卖出中
            }

            // 只读属性确保数据不可变
            public int Id { get; }
            public string Symbol { get; }
            public string FormattedPrice { get; }
            public int Quantity { get; }
            public TradeType Type { get; }
            public string TotalValue { get; }
            public DateTime ExecutionTime { get; }
            public TradeStatus Status { get; }
        }

        public enum TradeType
        {
            Buy
        }

        public enum TradeStatus
        {
            Settled,
            Buying,
            Selling
        }

        public class TradeHistory
        {

        }
    }
}