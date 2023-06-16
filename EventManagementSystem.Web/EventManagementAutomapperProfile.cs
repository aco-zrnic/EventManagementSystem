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
            CreateMap<Event, EventResponse>();
        }
    }
}
