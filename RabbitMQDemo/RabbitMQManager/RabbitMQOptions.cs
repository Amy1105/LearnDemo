using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VOL.Core.RabbitMQManager
{
    public class RabbitMQOptions
    {
        [Required]
        public string HostName { get; set; }
        [Required]
        public int Port { get; set; }
        [Required]
        public string VirtualHost { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string RouteKey { get; set; }     
    }
}
