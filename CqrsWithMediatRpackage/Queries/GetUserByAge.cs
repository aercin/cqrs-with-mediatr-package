using CqrsWithMediatRpackage.Persistence;
using MediatR;

namespace CqrsWithMediatRpackage.Queries
{
    public static class GetUserByAge
    {
        //Query
        public record Query(int age) : IRequest<Result>;

        //Handler
        public class QueryHandler : IRequestHandler<Query, Result>
        {
            private readonly Repository _repo;
            public QueryHandler(Repository repo)
            {
                _repo = repo;
            }

            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                var searchResult = this._repo.Users.Where(x => x.Age == request.age).ToList();

                var resultItem = new List<Item>();
                searchResult.ForEach(x =>
                {
                    resultItem.Add(new Item
                    {
                        Id = x.Id,
                        Name = x.Name,
                        SurName = x.SurName,
                        Age = x.Age
                    });
                });
                var result = new Result { Items = resultItem };

                return await Task.FromResult(result);
            }
        }

        //Response
        public record Result
        {
            public List<Item> Items { get; init; }
        }

        public record Item
        {
            public int Id { get; init; }
            public string Name { get; init; }
            public string SurName { get; init; }
            public int Age { get; init; }
        }
    }
}
