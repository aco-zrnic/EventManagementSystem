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
    public class StaffController : ControllerBase
    {
        private readonly EmContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IDateTimeService _dateTimeService;
        private readonly ILogger<StaffController> _logger;

        public StaffController(
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffResponse>>> GetAllStaffOfEvent(int id)
        {
            var response = await _context.Staff.Where(a => a.EventId == id).ToListAsync();
            return Ok(_mapper.Map<StaffResponse[]>(response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StaffResponse>> Get(int id)
        {
            var response = await _context.Staff.SingleOrDefaultAsync(a => a.Id == id);
            if (response == null)
                throw new ItemNotFoundException(
                    ErrorCode.ITEM_NOT_FOUND,
                    typeof(Staff).ToString(),
                    id
                );
            return Ok(_mapper.Map<StaffResponse>(response));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] StaffRequest request)
        {
            var staff = _mapper.Map<Staff>(request);
            _context.Staff.Add(staff);

            await _context.SaveChangesAsync();
            _logger.LogInformation($"Successfuly added {typeof(Staff).ToString()}");

            return StatusCode(StatusCodes.Status201Created, request);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var staff = await _context.Staff.SingleOrDefaultAsync(a => a.Id == id);
            if (staff == null)
                throw new ItemNotFoundException(
                    ErrorCode.ITEM_NOT_FOUND,
                    typeof(Staff).ToString(),
                    id
                );

            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Successfuly deleted {@staff}", staff);

            return Ok();
        }
    }
}
