using AutoMapper;
using EventManagementSystem.Commons;
using EventManagementSystem.Commons.Security;
using EventManagementSystem.Commons.Services;
using EventManagementSystem.Web.Dto.Request;
using EventManagementSystem.Web.Dto.Response;
using EventManagementSystem.Web.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventManagementSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly EmContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IDateTimeService _dateTimeService;
        private readonly ILogger<ParticipantController> _logger;

        public ParticipantController(
            EmContext context,
            IMapper mapper,
            IMediator mediator,
            IDateTimeService dateTimeService,
            ILogger<ParticipantController> logger
        )
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _dateTimeService = dateTimeService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Policy = PermissionResource.View)]
        public async Task<ActionResult<ParticipantResponse[]>> GetAll(
            [FromQuery] ParticipantRequest request
        )
        {
            var query = _context.Participants.AsQueryable();

            if (!string.IsNullOrEmpty(request.ContactNumber))
                query = query.Where(a => a.ContactNumber.Contains(request.ContactNumber));

            if (!string.IsNullOrEmpty(request.Name))
                query = query.Where(a => a.Name.Contains(request.Name));

            if (!string.IsNullOrEmpty(request.Email))
                query = query.Where(a => a.Email.Contains(request.Email));

            var result = await query.ToArrayAsync();
            return _mapper.Map<ParticipantResponse[]>(result);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PermissionResource.View)]
        public async Task<ActionResult<ParticipantResponse>> Get(int id)
        {
            var participant = await _context.Participants.FirstOrDefaultAsync(e => e.Id == id);

            ItemNotFoundException.ThrowIfNull(participant, typeof(Participant).ToString());

            return Ok(_mapper.Map<ParticipantResponse>(participant));
        }

        [HttpPost]
        [Authorize(Policy = PermissionResource.Create)]
        public async Task<ActionResult> CreateParticipant([FromBody] ParticipantRequest request)
        {
            var Event = _mapper.Map<Participant>(request);

            _context.Participants.Add(Event);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Successfuly added {typeof(Participant).ToString()}");

            return StatusCode(StatusCodes.Status201Created, request);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = PermissionResource.Update)]
        public async Task<ActionResult> Put(int id, [FromBody] ParticipantRequest request)
        {
            var participant = await _context.Participants.FirstOrDefaultAsync(a => a.Id == id);

            ItemNotFoundException.ThrowIfNull(participant, typeof(Participant).ToString());

            participant.Email = request.Email;

            await _context.SaveChangesAsync();

            return Ok(request);
        }

        // DELETE api/<ParticipantController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = PermissionResource.Delete)]
        public void Delete(int id) { }
    }
}
