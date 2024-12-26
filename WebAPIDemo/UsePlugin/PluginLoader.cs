using MyPlugin.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPIDemo.UsePlugin
{
    internal class PluginLoader
    {
        private AssemblyLoadContext _loadContext { get; set; }
        private readonly string _pluginName;
        private readonly string _pluginDir;
        private readonly string _rootPath;
        private readonly string _binPath;

        public string Name => _pluginName;

        private IPlugin? _plugin;
        public IPlugin? Plugin => _plugin;

        internal const string PLUGIN_SETTING_FILE = "settings.json";
        internal const string BIN_PATH = "bin";

        public PluginLoader(string mainAssemblyPath)
        {
            if (string.IsNullOrEmpty(mainAssemblyPath))
            {
                throw new ArgumentException("Value must be null or not empty", nameof(mainAssemblyPath));
            }

            if (!Path.IsPathRooted(mainAssemblyPath))
            {
                throw new ArgumentException("Value must be an absolute file path", nameof(mainAssemblyPath));
            }

            _pluginDir = Path.GetDirectoryName(mainAssemblyPath);
            _rootPath = Path.GetDirectoryName(_pluginDir);
            _binPath = Path.Combine(_rootPath, BIN_PATH);
            _pluginName = Path.GetFileNameWithoutExtension(mainAssemblyPath);

            if (!Directory.Exists(_binPath)) Directory.CreateDirectory(_binPath);

            Init();
        }

        private void Init()
        {
            // Read  
            var fileBytes = File.ReadAllBytes(Path.Combine(_rootPath, _pluginName, PLUGIN_SETTING_FILE));
            var setting = JsonSerializer.Deserialize<PluginOptions>(fileBytes);
            if (setting == null) throw new Exception($"{PLUGIN_SETTING_FILE} Deserialize Failed.");
            if (setting.Namespace == _pluginName) throw new Exception("Namespace not match.");

            var mainPath = Path.Combine(_binPath, _pluginName, _pluginName + ".dll");
            CopyToRunPath();
            using var fs = new FileStream(mainPath, FileMode.Open, FileAccess.Read);

            _loadContext ??= new AssemblyLoadContext(_pluginName, true);
            var assembly = _loadContext.LoadFromStream(fs);
            var pluginType = assembly.GetTypes()
                .FirstOrDefault(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsAbstract);
            if (pluginType == null) throw new NullReferenceException("IPlugin is Not Found");
            _plugin = Activator.CreateInstance(pluginType) as IPlugin ??
                      throw new NullReferenceException("IPlugin is Not Found");
            // Initialize with configuration from settings.json  
            _plugin.Init(setting);
        }

        private void CopyToRunPath()
        {
            var assemblyPath = Path.Combine(_binPath, _pluginName);
            if (Directory.Exists(assemblyPath)) Directory.Delete(assemblyPath, true);
            Directory.CreateDirectory(assemblyPath);
            var files = Directory.GetFiles(_pluginDir);
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                File.Copy(file, Path.Combine(assemblyPath, fileName));
            }
        }

        public bool Load()
        {
            if (_plugin != null) return false;
            try
            {
                Init();
                Console.WriteLine($"Load Plugin [{_pluginName}]");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Load Plugin Error [{_pluginName}]:{ex.Message}");
            }
            return true;
        }

        public bool Unload()
        {
            if (_plugin == null) return false;
            _loadContext.Unload();
            _loadContext = null;
            _plugin = null;
            return true;
        }

    }
}
