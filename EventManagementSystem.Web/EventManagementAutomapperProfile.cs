using AutoMapper;
using EventManagementSystem.Web.Dto.Request;
using EventManagementSystem.Web.Dto.Response;
using EventManagementSystem.Web.Entities;

namespace EventManagementSystem.Web
{
    public class EventManagementAutomapperProfile : Profile
    {
        public EventManagementAutomapperProfile()
        {
            CreateMap<EventRequest, Event>();
            CreateMap<Event, EventOverride>();
            CreateMap<Event, EventResponse>();

            CreateMap<Ticket, TicketResponse>()
                .ForMember(a => a.Event, opt => opt.MapFrom(a => a.Event));

            CreateMap<Staff, StaffResponse>();
            CreateMap<StaffRequest, Staff>();
        }
    }
}
