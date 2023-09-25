using EventManagementSystem.Commons.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem.Commons.Behavior
{
    public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;
        private readonly IDateTimeService _dateTimeService;

        public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger, IDateTimeService dateTimeService)
        {
            _logger = logger;
            _dateTimeService = dateTimeService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation("Starting request {@RequestName}, {@DateTimeUtc}",typeof(TRequest).Name,_dateTimeService.UtcTime);

            var result =  await next();

            _logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", typeof(TRequest).Name, _dateTimeService.UtcTime);

            return result;
        }
    }
}
