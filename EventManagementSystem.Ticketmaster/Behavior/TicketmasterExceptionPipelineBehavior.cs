using EventManagementSystem.Commons.Behavior;
using MediatR;

namespace EventManagementSystem.Ticketmaster.Behavior
{
    public class TicketmasterExceptionPipelineBehavior<TRequest, TResponse> : BaseExceptionPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        protected override void ExceptionHandling(Exception exception)
        {
            Console.WriteLine(exception.ToString());
            switch (exception)
            {
               
            }
        }
    }
}
