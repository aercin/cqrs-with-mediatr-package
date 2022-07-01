using CqrsWithMediatRpackage.Persistence;
using CqrsWithMediatRpackage.Validation;
using MediatR;

namespace CqrsWithMediatRpackage.Commands
{
    public static class UserCreate
    {
        //Command
        public record Command(string name, string surname, int age) : IRequest<Response>;

        //Command Validation
        public class Validator : IValidationHandler<Command>
        {
            private readonly Repository _repo;
            public Validator(Repository repo)
            {
                this._repo = repo;
            }

            public async Task<ValidationResult> Validate(Command request)
            {
                if (this._repo.Users.Any(x => x.Name.Equals(request.name, StringComparison.OrdinalIgnoreCase)))
                {
                    return ValidationResult.Fail("User already exists");
                }
                return ValidationResult.Success;
            }
        }

        //Command Handler
        public class CommandHandler : IRequestHandler<Command, Response>
        {
            private readonly Repository _repo;
            public CommandHandler(Repository repo)
            {
                this._repo = repo;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var newUser = new User
                {
                    Id = this._repo.NewId,
                    Name = request.name,
                    SurName = request.surname,
                    Age = request.age
                };

                this._repo.Users.Add(newUser);

                return await Task.FromResult(new Response(newUser.Id));
            }
        }

        //Response
        public record Response(int id);

        //Notification Event
        public record Event(int id) : INotification;

        //Notification Event Handler
        public class EvenHandler : INotificationHandler<Event>
        {
            public async Task Handle(Event notification, CancellationToken cancellationToken)
            {
                Console.WriteLine($"raising event id is {notification.id}");
                await Task.CompletedTask;
            }
        }
    }
}
