namespace MyPlugin.Base
{
    public interface IPlugin
    {
        IPlugin Init(IPluginOptions? options = null);

        Task<string> Execute();
    }

    public class IPluginOptions
    { 
        /// <summary>  
      /// Namespace  
      /// </summary>  
        public string Namespace { get; set; }
        /// <summary>  
        /// Version information  
        /// </summary>  
        public string Version { get; set; }
        /// <summary>  
        /// Plugin description  
        /// </summary>  
        public string Description { get; set; }
    }


    public class PluginOptions : IPluginOptions
    {
        /// <summary>  
        /// Version code  
        /// </summary>  
        public int VersionCode { get; set; }
       
        /// <summary>  
        /// Plugin dependencies  
        /// </summary>  
        public string[] Dependencies { get; set; }
        /// <summary>  
        /// Other parameter options  
        /// </summary>  
        public Dictionary<string, string> Options { get; set; }

        public virtual bool TryGetOption(string key, out string value)
        {
            value = "";
            return Options?.TryGetValue(key, out value) ?? false;
        }
    }
}
