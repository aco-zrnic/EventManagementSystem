using EventManagementSystem.Commons;

namespace EventManagementSystem.Ticketmaster.Exceptions
{
    public class TicketmasterBadResponseException : BadGatewayException
    {
        public TicketmasterBadResponseException(string statusCode, string responseBody)
            : base(ErrorCode.GATEWAY_ERROR, $"Status code:{statusCode}  Message = {responseBody}")
        { }
    }
}
