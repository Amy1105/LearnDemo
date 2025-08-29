using System.Text.RegularExpressions;

namespace SearchString
{
    public class MatchTest
    {


         


         public static void Get()
        {
            string[] testCases = [
               @" *代码由框架生成,任何更改都可能导致被代码生成器覆盖",
                @"LangManager.GetText(""只有待提交或者草稿或者修改申请单或者已驳回的状态才能修改"") + LangManager.GetText(""原因:{0},{1},修改时间：{2}"", p.Remark, p.Description, p.CreateDate)+ErrorAutoTranslate(""BOP类别必填"")+ ErrorAutoTranslate(""该测试单状态[{0}]无法再次审核！"", orderHeader.OrderStatus)
OKAutoTranslate(""提交成功"") + 
OKAutoTranslate(""计算成功,预估费用:{0}"", totalCost.ToString(""F2""), totalCost.ToString(""F2""))+""请求OA失败"".GetLangText()+""【{0}】的验证工程师不存在，不能进行审核操作;"".GetLangText(orderSample.BarCode)
",
 @"""请求OA失败"".GetLangText()+""【{0}】的验证工程师不存在，不能进行审核操作;"".GetLangText(orderSample.BarCode)",
                @"return WebResponseContent.Instance.OK(""操作成功"".GetLangText());",
                "OKAutoTranslate(\"提交成功\")",
                "OKAutoTranslate(\"提交成功\") + OKAutoTranslate(\"计算成功,预估费用:{0}\", totalCost.ToString(\"F2\"), totalCost.ToString(\"F2\"))",
                "ErrorAutoTranslate(\"BOP类别必填\")",
                "ErrorAutoTranslate(\"BOP类别必填\")+ ErrorAutoTranslate(\"该测试单状态[{0}]无法再次审核！\", orderHeader.OrderStatus)",
                "LangManager.GetText(\"只有待提交或者草稿或者修改申请单或者已驳回的状态才能修改\") ",
                "LangManager.GetText(\"只有待提交或者草稿或者修改申请单或者已驳回的状态才能修改\") + LangManager.GetText(\"原因:{0},{1},修改时间：{2}\", p.Remark, p.Description, p.CreateDate)"
];
            string pattern = @"(?:LangManager\.GetText|Error|OK)\(\""([^\""]+)\""(?:,\s*([^)]+))?\)";
            MatchString(pattern, testCases);
        }


        public static void GetVue()
        {           
            string[] testCases = [ ":placeholder=\"$t('请选择') + $t('状态')\"", "$t('电芯停测审核')","$t(\"请输入\")","$t('项目总预算:{0}', { 0: generalBudget }",
                @"{{ $t('大小') + ( file.size / 1024).toFixed(2) + 'KB' }}",
               "$t(\"样品类型\");","$t(\"项目总预算:{0}\", { 0: generalBudget })"
               ];
            //string pattern = @"\$t\(\s*([""'])(.*?)\1\s*[),]";
            string pattern = @"\$t\(\s*[""'](.*?)[""']";
            MatchString(pattern, testCases);
        }


   
      
        public static void GetLangManagerText()
        {
            string[] testCases = ["LangManager.GetText(\"只有待提交或者草稿或者修改申请单或者已驳回的状态才能修改\") ",
                "LangManager.GetText(\"只有待提交或者草稿或者修改申请单或者已驳回的状态才能修改\") + LangManager.GetText(\"原因:{0},{1},修改时间：{2}\", p.Remark, p.Description, p.CreateDate)"];           
            string pattern = @"LangManager\.GetText\(\""([^\""]+)\""(?:,\s*([^)]+))?\)";
            MatchString(pattern, testCases);
        }

    
        public static void GetErrorAutoTranslate()
        {
            // 输入字符串
            string [] testCases = ["ErrorAutoTranslate(\"BOP类别必填\")", 
                "ErrorAutoTranslate(\"BOP类别必填\")+ ErrorAutoTranslate(\"该测试单状态[{0}]无法再次审核！\", orderHeader.OrderStatus)"];
            string pattern = @"ErrorAutoTranslate\(\""([^\""]+)\""(?:,\s*([^)]+))?\)";
            MatchString(pattern, testCases);
        }


        /// <summary>
        /// OKAutoTranslate("提交成功")
        /// OKAutoTranslate("计算成功,预估费用:{0}", totalCost.ToString("F2"), totalCost.ToString("F2")),
        /// </summary>
        public static void GetOKAutoTranslate()
        {
            // 输入字符串
            string [] testCases = ["OKAutoTranslate(\"提交成功\")",
                "OKAutoTranslate(\"提交成功\") + OKAutoTranslate(\"计算成功,预估费用:{0}\", totalCost.ToString(\"F2\"), totalCost.ToString(\"F2\"))"];           
            string pattern = @"OKAutoTranslate\(\""([^\""]+)\""(?:,\s*([^)]+))?\)";
            MatchString(pattern, testCases);
        }

    
        public static void GetLangTextMatch()
        {
            string[] testCases = [ 
                @"""请求OA失败"".GetLangText()+""【{0}】的验证工程师不存在，不能进行审核操作;"".GetLangText(orderSample.BarCode)",
                @"return WebResponseContent.Instance.OK(""操作成功"".GetLangText());"];

            string p = @"\""([^\""]+)\""\.GetLangText\((?:([^)]+))?\)";
            string pattern = @"\""([^\""]+)\""\.GetLangText\((?:([^)]+))?\)";
         
            MatchString(pattern, testCases);
        }



     
        public static void Pattern()
        {
            string[] testCases = [
                @" *代码由框架生成,任何更改都可能导致被代码生成器覆盖",
                @"LangManager.GetText(""只有待提交或者草稿或者修改申请单或者已驳回的状态才能修改"") + LangManager.GetText(""原因:{0},{1},修改时间：{2}"", p.Remark, p.Description, p.CreateDate)+ErrorAutoTranslate(""BOP类别必填"")+ ErrorAutoTranslate(""该测试单状态[{0}]无法再次审核！"", orderHeader.OrderStatus)
OKAutoTranslate(""提交成功"") + 
OKAutoTranslate(""计算成功,预估费用:{0}"", totalCost.ToString(""F2""), totalCost.ToString(""F2""))+""请求OA失败"".GetLangText()+""【{0}】的验证工程师不存在，不能进行审核操作;"".GetLangText(orderSample.BarCode)
",
 @"""请求OA失败"".GetLangText()+""【{0}】的验证工程师不存在，不能进行审核操作;"".GetLangText(orderSample.BarCode)",
                @"return WebResponseContent.Instance.OK(""操作成功"".GetLangText());",
                "OKAutoTranslate(\"提交成功\")",
                "OKAutoTranslate(\"提交成功\") + OKAutoTranslate(\"计算成功,预估费用:{0}\", totalCost.ToString(\"F2\"), totalCost.ToString(\"F2\"))",
                "ErrorAutoTranslate(\"BOP类别必填\")",
                "ErrorAutoTranslate(\"BOP类别必填\")+ ErrorAutoTranslate(\"该测试单状态[{0}]无法再次审核！\", orderHeader.OrderStatus)",
                "LangManager.GetText(\"只有待提交或者草稿或者修改申请单或者已驳回的状态才能修改\") ",
                "LangManager.GetText(\"只有待提交或者草稿或者修改申请单或者已驳回的状态才能修改\") + LangManager.GetText(\"原因:{0},{1},修改时间：{2}\", p.Remark, p.Description, p.CreateDate)"
];


            string pattern = @"(?:LangManager\.GetText|ErrorAutoTranslate|OKAutoTranslate)\(\""([^\""]+)\""(?:,\s*([^)]+))?\)|\""([^\""]+)\""\.GetLangText\((?:([^)]+))?\)";

            MatchString(pattern, testCases);
        }


        /// <summary>
        /// 帆软.cpt模板
        /// </summary>
        public static void GetMatchFR()
        {
            string[] testCases = {
            "CONCATENATE(&quot;&quot;, i18n(&quot;测试简码&quot;)",
            "CONCATENATE(&quot;*&quot;, i18n(&quot;申请单号&quot;)",
            "<![CDATA[=CONCATENATE(\\\"\\\", i18n(\\\"新建图表标题\\\"), \\\"\\\")]]></O>",
            "i18n(\"申请单号\")",
            "i18n(&quot;电芯编码&quot;)",
            "i18n('测试内容')",
            "CONCATENATE('', i18n('另一个测试'))",
            "CONCATENATE(\"\", i18n(\"工步序号\"), \"\\n\", i18n(\"时长\"), \"ms\")]]",
            "CONCATENATE(&quot;&quot;, i18n(&quot;测试简码&quot;), i18n(&quot;另一个测试&quot;))",
            "<![CDATA[=CONCATENATE(\\\"\\\", i18n(\\\"标题1\\\"), i18n(\\\"标题2\\\"), \\\"\\\")]]>",
            "i18n(\"单个调用\")",
            "没有i18n调用的文本",
            "CONCATENATE(i18n('第一个'), i18n(\"第二个\"), i18n(&quot;第三个&quot;))"
        };

            string pattern = @"i18n\((?:(?:&quot;)|(?:\\*[""']))(.*?)(?:(?:&quot;)|(?:\\*[""']))\)";
            MatchString(pattern, testCases);
        }





        public static void MatchString(string pattern,string[] strings)
        {
            foreach (string input in strings)
            {
                MatchCollection matches = Regex.Matches(input, pattern,RegexOptions.IgnorePatternWhitespace);

                Console.WriteLine($"\n输入: {input}");
                Console.WriteLine($"找到 {matches.Count} 个匹配项:");

                for (int i = 0; i < matches.Count; i++)
                {                   
                    string extracted = matches[i].Groups[1].Value;
                    Console.WriteLine($"  {i + 1}:{extracted}");
                }
                if (matches.Count == 0)
                {
                    Console.WriteLine("  (无匹配项)");
                }
            }
        }
    }
}
