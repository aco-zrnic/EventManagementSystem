using AutoMapper;
using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Util;
using EventManagementSystem.Commons;
using EventManagementSystem.Web.Dto.Response;
using EventManagementSystem.Web.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Web.Handler
{
    public class GetEvent : IRequest<RequestedEvent>
    {
        [Required]
        public string Name { get; set; }
        public string Venue { get; set; }
        public DateTimeOffset Date { get; set; }
    }
    public class RequestedEvent
    {
        public EventResponse Event { get; set; }
    }
    [OriginalName("DB_get_event")]
    public class DB_GetEvent : ITaskRequestHandler<GetEvent, RequestedEvent>
    {
        private readonly EmContext _context;
        private readonly IMapper _mapper;
        public DB_GetEvent(EmContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RequestedEvent> Handle(
            GetEvent request,
            CancellationToken cancellationToken
        )
        {
            var Event = await _context.Events.FirstOrDefaultAsync(
                a => a.Name == request.Name && a.Venue == request.Venue && a.Date == request.Date
            );

            if (Event != null)
                throw new BaseException(ErrorCode.GENERAL, "Event already exists in database");

            return new RequestedEvent { Event = _mapper.Map<EventResponse>(request) };
        }
    }
}
