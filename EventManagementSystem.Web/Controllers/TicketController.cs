using AutoMapper;
using EventManagementSystem.Commons;
using EventManagementSystem.Commons.Behavior;
using EventManagementSystem.Commons.Services;
using EventManagementSystem.Web.Dto.Request;
using EventManagementSystem.Web.Dto.Response;
using EventManagementSystem.Web.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventManagementSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(LoggingActionFilter))]
    public class TicketController : ControllerBase
    {
        private readonly EmContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IDateTimeService _dateTimeService;
        private readonly ILogger<TicketController> _logger;

        public TicketController(
            EmContext context,
            IMapper mapper,
            IMediator mediator,
            IDateTimeService dateTimeService,
            ILogger<TicketController> logger
        )
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _dateTimeService = dateTimeService;
            _logger = logger;
        }

        [HttpGet("/by-event/{id}")]
        public async Task<ActionResult<TicketResponse[]>> GetAllTicketsByEvent(int id)
        {
            var tickets = await _context.Tickets.Where(a => a.EventId == id).ToArrayAsync();
            return _mapper.Map<TicketResponse[]>(tickets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketResponse>> GetTicketById(int id)
        {
            var ticketwithPID = await _context.Tickets.FirstOrDefaultAsync(a => a.Id == id);
            var ticket = await _context.Tickets
                .Where(a => a.Id == id)
                .Include(a => a.Event)
                .FirstOrDefaultAsync();
            if (ticket == null)
                throw new ItemNotFoundException(
                    ErrorCode.ITEM_NOT_FOUND,
                    typeof(Ticket).ToString(),
                    id
                );

            return Ok(_mapper.Map<TicketResponse>(ticket));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TicketRequest request)
        {
            if (!_context.Events.Any(a => a.Id == request.EventId))
                throw new ItemNotFoundException(
                    ErrorCode.ITEM_NOT_FOUND,
                    typeof(Event).ToString(),
                    request.EventId
                );

            _context.Tickets.Add(
                new Ticket
                {
                    EventId = request.EventId,
                    Price = request.Price,
                    TicketType = request.TicketType
                }
            );
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, request);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(
                a => a.Id == id && a.ParticipantId == null
            );

            if (ticket == null)
                throw new ItemNotFoundException(
                    ErrorCode.ITEM_NOT_FOUND,
                    $"Can't find ticket with {id} that also don't belong to the participant"
                );

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Successfuly deleted {typeof(Ticket).ToString()} of id {id}");

            return Ok();
        }
    }
}
