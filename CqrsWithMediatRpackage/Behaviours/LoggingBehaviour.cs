using MediatR;
using System.Diagnostics;

namespace CqrsWithMediatRpackage.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger _logger;
        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            this._logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //pre logic 
            this._logger.LogInformation($"{request.GetType().Name} is starting");

            var timer = Stopwatch.StartNew();
            var response = next();

            //post logic
            this._logger.LogInformation($"{request.GetType().Name} has finished in {timer.ElapsedMilliseconds}ms");

            return await response;
        }
    }
}
