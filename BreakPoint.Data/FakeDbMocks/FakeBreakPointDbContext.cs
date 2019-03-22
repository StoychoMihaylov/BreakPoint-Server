namespace BreakPoint.Data.FakeDbMocks
{
    using BreakPoint.Data.EntityModels;
    using BreakPoint.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class FakeBreakPointDbContext : IBreakPointDbContext
    {
        public FakeBreakPointDbContext()
        {
            this.Users = new FakeDbSet<User>();
        }

        public DbSet<User> Users { get; set; }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
