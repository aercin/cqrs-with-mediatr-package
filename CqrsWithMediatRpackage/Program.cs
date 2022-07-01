using CqrsWithMediatRpackage.Behaviours;
using CqrsWithMediatRpackage.Commands;
using CqrsWithMediatRpackage.Exceptions;
using CqrsWithMediatRpackage.Persistence;
using CqrsWithMediatRpackage.Validation;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddProblemDetails(setup =>
{
    setup.IncludeExceptionDetails = (ctx, env) => false;

    setup.Map<ValidationException>(exception => new ValidationProblemDetails
    {
        Title = exception.Title,
        Detail = exception.Detail,
        Status = exception.Status,
        Type = exception.Type,
        Instance = exception.Instance,
        AdditionalInfo = exception.AdditionalInfo,
    });
});

builder.Services.AddTransient<IValidationHandler<UserCreate.Command>, UserCreate.Validator>();

builder.Services.AddMediatR(typeof(UserCreate.CommandHandler).Assembly);

//Behaviour order is important!!!
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

builder.Services.AddSingleton<Repository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseProblemDetails();

app.MapControllers();

app.Run();
