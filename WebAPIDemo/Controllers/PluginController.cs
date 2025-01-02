using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebAPIDemo.UsePlugin;

//namespace WebAPIDemo.Controllers
//{
//    [Controller(BaseUrl = "/plugin/test")]
//    public class PluginController
//    {
//        private readonly PluginManager _pluginManager;
//        public PluginController()
//        {
//            _pluginManager = PluginManager.Instance;
//            _pluginManager.SetRootPath("../plugins");
//        }

//        [HttpGet(Route = "load")]
//        public ActionResult Load()
//        {
//            _pluginManager.LoadAll();
//            return GetResult("ok");
//        }

//        [HttpGet(Route = "execute")]
//        public async ActionResult Execute(string name)
//        {
//            var plugin = _pluginManager.GetPlugin(name);
//            await plugin?.Execute();
//            return GetResult("ok");
//        }

//        [HttpGet(Route = "unload")]
//        public ActionResult Unload(string name)
//        {
//            var res = _pluginManager.RemovePlugin(name);
//            return res ? GetResult("ok") : FailResult("failed");
//        }

//        [HttpGet(Route = "reload")]
//        public ActionResult Reload(string name)
//        {
//            var res = _pluginManager.ReloadPlugin(name);
//            return res ? OkResult() : ("failed");
//        }
//    }
//}
