using Newtonsoft.Json;

namespace EventManagementSystem.Ticketmaster.Models.Response
{
    public class Error
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("httpCode")]
        public int HttpCode { get; set; }
    }

    public class ErrorResponse
    {
        [JsonProperty("error")]
        public Error Error { get; set; }
    }
}
