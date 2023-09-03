using AutoMapper;
using EventManagementSystem.Commons;
using EventManagementSystem.Commons.Behavior;
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
    [ServiceFilter(typeof(LoggingActionFilter))]
    public class SponsorController : ControllerBase
    {
        private readonly EmContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IDateTimeService _dateTimeService;
        private readonly ILogger<StaffController> _logger;

        public SponsorController(
            EmContext context,
            IMapper mapper,
            IMediator mediator,
            IDateTimeService dateTimeService,
            ILogger<StaffController> logger
        )
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _dateTimeService = dateTimeService;
            _logger = logger;
        }

        [HttpGet("by-event/{id}")]
        [Authorize(Policy = PermissionResource.View)]
        public async Task<ActionResult<IEnumerable<SponsoreResponse>>> GetSponsorsOfEvent(int id)
        {
            var response = await _context.Sponsors.Where(a => a.EventId == id).ToListAsync();
            return Ok(_mapper.Map<SponsoreResponse[]>(response));
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PermissionResource.View)]
        public async Task<ActionResult<SponsoreResponse>> Get(int id)
        {
            var response = await _context.Sponsors.SingleOrDefaultAsync(a => a.Id == id);
            if (response == null)
                throw new ItemNotFoundException(
                    ErrorCode.ITEM_NOT_FOUND,
                    typeof(Sponsor).ToString(),
                    id
                );
            return Ok(_mapper.Map<SponsoreResponse>(response));
        }

        [HttpPost]
        [Authorize(Policy = PermissionResource.Create)]
        public async Task<ActionResult> Post([FromBody] SponsoreRequest request)
        {
            if (!_context.Events.Any(a => a.Id == request.EventId))
                throw new ItemNotFoundException(
                    ErrorCode.ITEM_NOT_FOUND,
                    typeof(Event).ToString(),
                    request.EventId
                );

            _context.Sponsors.Add(
                new Sponsor
                {
                    EventId = request.EventId,
                    Name = request.Name,
                    SponsorshipLevel = request.SponsorshipLevel,
                }
            );
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, request);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = PermissionResource.Update)]
        public async Task<ActionResult<SponsoreResponse>> UpdateSponsoreshipLevel(
            int id,
            [FromBody] SponsoreRequest request
        )
        {
            var sponsore = await _context.Sponsors.FirstOrDefaultAsync(a => a.Id == id);

            ItemNotFoundException.ThrowIfNull(sponsore, typeof(Sponsor).ToString());

            sponsore.SponsorshipLevel = request.SponsorshipLevel;

            await _context.SaveChangesAsync();

            return Ok(request);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = PermissionResource.Delete)]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var sponsor = await _context.Sponsors.SingleOrDefaultAsync(a => a.Id == id);

            ItemNotFoundException.ThrowIfNull(sponsor, typeof(Sponsor).ToString());

            _context.Sponsors.Remove(sponsor);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Successfuly deleted {@sponsor}", sponsor);

            return Ok();
        }
    }
}
