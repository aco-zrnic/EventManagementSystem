using AutoMapper;
using ConductorSharp.Engine.Interface;
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
        public int Id { get; set; }
    }
    public class RequestedEvent
    {
        public EventResponse Event { get; set; }
    }
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
            var Event = await _context.Events.FirstOrDefaultAsync(a => a.Id == request.Id);

            if (Event == null)
                throw new ItemNotFoundException(ErrorCode.ITEM_NOT_FOUND);

            return new RequestedEvent { Event = _mapper.Map<EventResponse>(Event) };
        }
    }
}
