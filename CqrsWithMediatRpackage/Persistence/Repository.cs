namespace CqrsWithMediatRpackage.Persistence
{
    public class Repository
    {
        public List<User> Users { get; set; } = new List<User>();

        public int NewId
        {
            get
            {
                if (Users.Count == 0)
                    return 1;
                return Users.Max(x => x.Id) + 1;
            }
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public int Age { get; set; }
    }
}
