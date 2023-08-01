using AutoMapper;
using EventManagementSystem.Commons;
using EventManagementSystem.Commons.Behavior;
using EventManagementSystem.Commons.Services;
using EventManagementSystem.Web.Dto.Request;
using EventManagementSystem.Web.Dto.Response;
using EventManagementSystem.Web.Entities;
using EventManagementSystem.Web.Handler;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(LoggingActionFilter))]
    public class EventController : ControllerBase
    {
        private readonly EmContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IDateTimeService _dateTimeService;
        private ILogger<EventController> _logger;

        public EventController(
            EmContext context,
            ILogger<EventController> logger,
            IMapper mapper,
            IDateTimeService dateTimeService,
            IMediator mediator
        )
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _dateTimeService = dateTimeService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<EventResponse[]>> GetAllEvents()
        {
            var events = await _context.Events.ToArrayAsync();
            return Ok(_mapper.Map<EventResponse[]>(events));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventResponse>> GetEvent(int id)
        {
            var Event = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);

            if (Event == null)
                throw new ItemNotFoundException(
                    ErrorCode.ITEM_NOT_FOUND,
                    typeof(Event).ToString(),
                    id
                );

            return Ok(_mapper.Map<EventResponse>(Event));
        }
        [HttpGet("test/{id}")]
        public async Task<ActionResult> GetEventHandlerTest(string eventName, string venue)
        {
            var response = await _mediator.Send(new GetEvent { Name = eventName, Venue = venue });
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EventRequest request)
        {
            if (request.Date.UtcDateTime < _dateTimeService.UtcTime)
                throw new UserFriendlyException(
                    ErrorCode.BAD_REQUEST,
                    $"Can't add {typeof(Event).ToString()} because date {request.Date.UtcDateTime} is not correct"
                );

            var Event = _mapper.Map<Event>(request);

            _context.Events.Add(Event);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Successfuly added {typeof(Event).ToString()}");

            return StatusCode(StatusCodes.Status201Created, request);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EventResponse>> UpdateEvent(
            int id,
            [FromBody] EventRequest request
        )
        {
            var Event = await _context.Events.FirstOrDefaultAsync(a => a.Id == id);

            if (Event == null)
                throw new ItemNotFoundException(
                    ErrorCode.ITEM_NOT_FOUND,
                    typeof(Event).ToString(),
                    request.Id
                );
            Event.Name = request.Name;
            Event.Date = request.Date;
            Event.Venue = request.Venue;

            await _context.SaveChangesAsync();

            return Ok(request);
        }

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
