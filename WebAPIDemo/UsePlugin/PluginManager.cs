using MyPlugin.Base;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemo.UsePlugin
{
    public class PluginManager
    {
        private static PluginManager _instance;
        public static PluginManager Instance => _instance ??= new PluginManager();

        private static readonly ConcurrentDictionary<string, PluginLoader> _loaderPool = new ConcurrentDictionary<string, PluginLoader>();

        private string _rootPath;
        PluginManager()
        {
            _rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins");
        }

        // Set the plugin directory path
        public void SetRootPath(string path)
        {
            if (Path.IsPathRooted(path))
            {
                _rootPath = path;
            }
            else
            {
                _rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            }
        }

        // Load and initialize all plugins in the plugin directory
        public void LoadAll()
        {
            if (!Directory.Exists(_rootPath)) return;
            var rootDir = new DirectoryInfo(_rootPath);
            foreach (var pluginDir in rootDir.GetDirectories())
            {
                if (pluginDir.Name == PluginLoader.BIN_PATH) continue;
                var files = pluginDir.GetFiles();
                var hasBin = files.Any(f => f.Name == pluginDir.Name + ".dll");
                var hasSettings = files.Any(f => f.Name == PluginLoader.PLUGIN_SETTING_FILE);
                if (hasBin && hasSettings)
                {
                    LoadPlugin(pluginDir.Name);
                }
            }
        }

        // Load and initialize a single plugin
        private void LoadPlugin(string name)
        {
            var srcPath = Path.Combine(_rootPath, name, name + ".dll");
            try
            {
                var loader = new PluginLoader(srcPath);
                _loaderPool.TryAdd(name, loader);
                Console.WriteLine($"Load Plugin [{name}]");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Load Plugin Error [{name}]:{ex.Message}");
            }
        }
        // Get a plugin
        public IPlugin? GetPlugin(string name)
        {
            _loaderPool.TryGetValue(name, out var loader);
            return loader?.Plugin;
        }
        // Remove and unload a plugin
        public bool RemovePlugin(string name)
        {
            if (!_loaderPool.TryRemove(name, out var loader)) return false;
            return loader.Unload();
        }
        // Reload a plugin
        public bool ReloadPlugin(string name)
        {
            if (!_loaderPool.TryGetValue(name, out var loader)) return false;
            loader.Unload();
            return loader.Load();
        }

    }
}
