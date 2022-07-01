using Microsoft.AspNetCore.Mvc;

namespace CqrsWithMediatRpackage.Exceptions
{
    public class ValidationProblemDetails: ProblemDetails
    {
        public string AdditionalInfo { get; set; }
    }
}
