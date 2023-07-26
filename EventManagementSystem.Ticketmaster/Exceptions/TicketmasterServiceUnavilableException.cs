using EventManagementSystem.Commons;

namespace EventManagementSystem.Ticketmaster.Exceptions
{
    public class TicketmasterServiceUnavilableException : UserFriendlyException
    {
        public TicketmasterServiceUnavilableException()
            : base(ErrorCode.GATEWAY_ERROR, "Service unavailable") { }
    }
}
