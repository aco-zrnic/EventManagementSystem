using EventManagementSystem.Commons;
using EventManagementSystem.Web.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventManagementSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly EmContext _context;
        private ILogger<EventController> _logger;

        public EventController(EmContext context, ILogger<EventController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<EventController>/5
        [HttpGet("{id}")]
        public Task<ActionResult<Event>> GetEvent(int id)
        {
            var Event = _context.Events.FirstOrDefault(e => e.Id == id);
            if (Event == null)
                throw new ItemNotFoundException(
                    ErrorCode.ITEM_NOT_FOUND,
                    typeof(Event).ToString(),
                    id
                );
            return Event;
        }

        // POST api/<EventController>
        [HttpPost]
        public void Post([FromBody] string value) { }

        // PUT api/<EventController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        // DELETE api/<EventController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEvent(int id)
        {
            var Event = _context.Events.FirstOrDefault(e => e.Id == id);

            if (Event == null)
                throw new ItemNotFoundException(
                    ErrorCode.ITEM_NOT_FOUND,
                    typeof(Event).ToString(),
                    id
                );

            _context.Events.Remove(Event);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Successfuly deleted {typeof(Event).ToString()} of id {id}");

            return Ok();
        }
    }
}
