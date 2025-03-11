using System.Globalization;
using System.Resources;

namespace OrderService
{
    public class MessageService
    {
        private ResourceManager resourceManager;
        public MessageService()
        {
            resourceManager= new ResourceManager("OrderService", GetType().Assembly);
        }
        public string GetMsg()
        {

            return GetLocalizedString("success");
        }

        public string GetLocalizedString(string name)
        {
            return resourceManager.GetString(name, CultureInfo.CurrentUICulture);
        }
    }
}
