namespace BreakPoint.Data
{
    using BreakPoint.Data.EntityModels;
    using Microsoft.EntityFrameworkCore;

    public class BreakPointDbContext : DbContext
    {
        public BreakPointDbContext(DbContextOptions<BreakPointDbContext> options) 
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
    }
}
