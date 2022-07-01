namespace CqrsWithMediatRpackage.Validation
{
    public interface IValidationHandler<T>
    {
        Task<ValidationResult> Validate(T request);
    }
}
