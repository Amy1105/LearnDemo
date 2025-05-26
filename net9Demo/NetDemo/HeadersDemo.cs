using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace net9Demo.NetDemo
{
    internal class HeadersDemo
    {
        public void Method(HttpRequestMessage httpRequest)
        {
            /*
                对应的 HTTP 标头：Authorization 或 WWW-Authenticate
                用途：处理 HTTP 认证信息（如 Bearer Token、Basic Auth）。
                关键属性/方法：Scheme：认证方案（如 "Bearer"、"Basic"）。
                Parameter：认证参数（如 Token 值）。
             */


            // 设置 Authorization 标头
            var authHeader = new AuthenticationHeaderValue("Bearer", "xyz123");
            httpRequest.Headers.Authorization = authHeader;

            // 解析标头：Bearer xyz123

           
        }

        public void Method2(HttpResponseMessage httpResponse )
        {
            /*
                对应的 HTTP 标头：Cache-Control
                用途：控制缓存行为（如 no-cache、max-age）。
                关键属性：
                NoCache：是否禁用缓存。
                MaxAge：资源缓存的最大时间（TimeSpan）。
                Public/Private：缓存是否可被共享
             */

            // 设置 Cache-Control: max-age=3600, public
            var cacheControl = new CacheControlHeaderValue
            {
                MaxAge = TimeSpan.FromHours(1),
                Public = true
            };
            httpResponse.Headers.CacheControl = cacheControl;

            /*
            对应的 HTTP 标头：Content-Disposition
            用途：定义响应内容的处理方式（如文件下载的附件名）。
            关键属性：DispositionType："inline"（直接显示）或 "attachment"（下载）。FileName：下载时建议的文件名。
             */

            // 设置 Content-Disposition: attachment; filename="example.pdf"
            var contentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "example.pdf"
            };
            httpResponse.Content.Headers.ContentDisposition = contentDisposition;


            //对应的 HTTP 标头：Content-Range
            //用途：表示部分响应（Partial Content）的范围（用于断点续传或分块下载）。
            //关键属性：From / To：字节范围的起始 / 结束位置。
            //Length：资源总大小（如 "bytes 0-999/2000"）。

            // 设置 Content-Range: bytes 0-999/2000
            var rangeHeader = new ContentRangeHeaderValue(0, 999, 2000);
            httpResponse.Content.Headers.ContentRange = rangeHeader;

        }


        public async Task Test1()
        {
            using (var httpClient = new HttpClient())
            {
                // 调试流处理的好方法：
                var request = new HttpRequestMessage(HttpMethod.Get, "https://www.baidu.com");
                var response = await httpClient.SendAsync(request,
                    HttpCompletionOption.ResponseHeadersRead);
                var stream = await response.Content.ReadAsStreamAsync();


                await httpClient.GetAsync("https://www.baidu.com"); // 应复用连接

                // 此时stream就是具体的HttpBaseStream子类实例

                //一、思考：这些 API 背后隐藏了哪些组件？这就是源码的入口

                // 1. 最常见的用法
                var response2 = await httpClient.GetAsync("https://example.com");
                var text = await response2.Content.ReadAsStringAsync();

                // 2. 稍微进阶的用法
                var request2 = new HttpRequestMessage(HttpMethod.Post, "...");
                request.Content = new StringContent("{}", Encoding.UTF8, "application/json");
                await httpClient.SendAsync(request2);
            }
        }

        /*
        二、携带具体问题去阅读（示例问题清单）
            带着这些问题看代码会更有焦点：

            数据流动问题：

            当我调用 GetAsync() 时，字节流是如何从网卡到达我的字符串变量的？

            HttpContent 如何适配不同类型的数据（JSON/二进制/表单）？

            设计抽象问题：

            为什么要有 HttpBaseStream 这个抽象层？

            HTTP/1.1 和 HTTP/3 的流实现差异在哪？

            性能问题：

            大文件上传时如何避免内存爆炸？

            连接池是如何管理的？



        三、实操：用调试器"可视化"调用栈
            步骤：

            在代码中设置断点：

            csharp
            var response = await httpClient.GetAsync("https://example.com"); // 这里断点
            调试时查看调用栈（Call Stack），你会看到：

            HttpClient.GetAsync()
            → HttpClient.SendAsync()
              → SocketsHttpHandler.SendAsync() 
                → HttpConnectionPool.SendAsync()
                  → Http3Connection.SendAsync() 
                    → Http3Stream.WriteAsync()
            顺着调用栈点击进入每个层级，观察：

            参数传递：数据如何被层层加工

            类型转换：抽象如何逐步具象化

        四、重点突破：画一个最小类图
只抓最核心的 5-7 个类，用白板或纸笔画它们的关系：

         */



    }
}
