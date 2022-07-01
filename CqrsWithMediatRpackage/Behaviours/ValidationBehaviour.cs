using CqrsWithMediatRpackage.Exceptions;
using CqrsWithMediatRpackage.Validation;
using MediatR;

namespace CqrsWithMediatRpackage.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ValidationBehaviour<TRequest, TResponse>> _logger;
        private readonly IValidationHandler<TRequest> _validationHandler;

        // Have 2 constructors incase the validator does not exist
        public ValidationBehaviour(ILogger<ValidationBehaviour<TRequest, TResponse>> logger)
        {
            this._logger = logger;
        }

        public ValidationBehaviour(ILogger<ValidationBehaviour<TRequest, TResponse>> logger,
                                   IValidationHandler<TRequest> validationHandler,
                                   IHttpContextAccessor httpContextAccessor) : this(logger)
        { 
            this._validationHandler = validationHandler;
            this._httpContextAccessor = httpContextAccessor;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (this._validationHandler == null)
            {
                this._logger.LogInformation($"{typeof(TRequest).Name} does not have a validation handler configured.");

                return await next();
            }

            var validationResult = await this._validationHandler.Validate(request);

            if (!validationResult.IsSuccessful)
            {
                this._logger.LogWarning($"Validation failed for { typeof(TRequest).Name}. Error: {validationResult.Error}");

                throw ValidationException.Create("about.html", "validation error title", "validation error detail", this._httpContextAccessor.HttpContext.Request.Path, "additionalInfo");
            }

            this._logger.LogInformation($"Validation successful for {typeof(TRequest).Name}.");

            return await next();
        }
    }
}
