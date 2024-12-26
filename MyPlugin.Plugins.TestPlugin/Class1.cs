using MyPlugin.Base;
using System.Text.Json;

namespace MyPlugin.Plugins.TestPlugin
{
    public sealed class MainPlugin : IPlugin
    {
        private IPluginOptions _options;
        public IPlugin Init(IPluginOptions? options)
        {
            _options = options;
            return this;
        }

        public async Task<string> Execute()
        {
            Console.WriteLine($"Start Executing {_options.Namespace}");
            Console.WriteLine($"Description {_options.Description}");
            Console.WriteLine($"Version {_options.Version}");
            await Task.Delay(1000);
            Console.WriteLine($"Done.");

            return JsonSerializer.Serialize(new { code = 0, message = "ok" });
        }
    }
}
