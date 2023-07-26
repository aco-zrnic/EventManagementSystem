using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem.Ticketmaster.Client.Models.Request
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class GetEventsInCityRequest
    {
        [Required]
        [JsonProperty("city")]
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
    }
}
