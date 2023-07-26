using EventManagementSystem.Ticketmaster.Client.Models.Request;
using EventManagementSystem.Ticketmaster.Util;
using Microsoft.AspNetCore.Mvc;
using Flurl;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Web;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventManagementSystem.Ticketmaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketmasterController : ControllerBase
    {
        private readonly TicketmasterClient _client;
        private static Url baseUrl = "events";
        public TicketmasterController(TicketmasterClient client)
        {
            _client = client;
        }

        [HttpGet("get-events-in-city")]
        public async Task<ActionResult<string>> GetEventsInCity(
            [FromQuery] GetEventsInCityRequest request
        )
        {
            var uri = new Url(baseUrl);

            foreach (var property in request.GetType().GetProperties())
            {
                var queryParam = property.Name;
                queryParam = char.ToLower(queryParam[0]) + queryParam.Substring(1);
                uri = uri.SetQueryParam(
                    queryParam,
                    property.GetValue(request)?.ToString(),
                    true,
                    Flurl.NullValueHandling.Remove
                );
            }

            var response = await _client.GetAsync<TicketMasterApiResponse>(uri.ToUri());
            return Ok(response);
        }

        // GET api/<TicketmasterController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TicketmasterController>
        [HttpPost]
        public void Post([FromBody] string value) { }

        // PUT api/<TicketmasterController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        // DELETE api/<TicketmasterController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) { }
    }
}
