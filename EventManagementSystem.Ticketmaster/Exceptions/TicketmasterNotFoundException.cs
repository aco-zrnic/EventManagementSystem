using EventManagementSystem.Commons;

namespace EventManagementSystem.Ticketmaster.Exceptions
{
    public class TicketmasterNotFoundException : UserFriendlyException
    {
        public TicketmasterNotFoundException(ErrorCode code, string uri)
            : base(code, $"For requested {uri} response not found") { }
    }
}
