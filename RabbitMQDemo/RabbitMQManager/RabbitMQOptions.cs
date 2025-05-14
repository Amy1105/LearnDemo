using System.ComponentModel.DataAnnotations;

namespace VOL.Core.RabbitMQManager
{
    public class RabbitMQOptions
    {
        [Required]
        public required string HostName { get; set; }
        [Required]
        public required int Port { get; set; }
        [Required]
        public required string VirtualHost { get; set; }
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required string RouteKey { get; set; }     
    }
}
