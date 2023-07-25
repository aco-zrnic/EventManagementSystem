using EventManagementSystem.Commons;

namespace EventManagementSystem.Ticketmaster.Exceptions
{
    public class TicketmasterBadRequestException : UserFriendlyException
    {
        public TicketmasterBadRequestException(
            ErrorCode code,
            string error,
            string ticketmasterErrorCode
        ) : base(code, $"Error={error},Error code={ticketmasterErrorCode}") =>
            (Error, ErrorCode) = (error, ticketmasterErrorCode);

        public string Error { get; }
        public new string ErrorCode { get; }
    }
}
