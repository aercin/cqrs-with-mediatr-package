using System.Net;

namespace CqrsWithMediatRpackage.Exceptions
{
    public class ValidationException : Exception
    {
        private ValidationException()
        {
        }

        public static ValidationException Create(string type, string detail, string title, string instance, string additionalInfo)
        {
            return new ValidationException
            {
                Type = type,
                Title = title,
                Detail = detail,
                AdditionalInfo = additionalInfo,
                Instance = instance
            };
        }

        public string AdditionalInfo { get; init; }
        public string Type { get; init; }
        public string Detail { get; init; }
        public string Title { get; init; }
        public string Instance { get; init; }
        public int Status { get; init; } = (int)HttpStatusCode.BadRequest;
    }
}
