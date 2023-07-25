using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Ticketmaster.Options
{
    public class GatewayOptions
    {
        [Required]
        [Url]
        public string Url { get; set; }

        [Required]
        public string Version { get; set; }

        [Required]
        public string Package { get; set; }

        public string ApiKey { get; set; }
    }
}
