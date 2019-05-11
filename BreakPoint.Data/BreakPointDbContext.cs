namespace BreakPoint.Data
{
    using BreakPoint.Data.EntityModels;
    using BreakPoint.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class BreakPointDbContext : DbContext, IBreakPointDbContext
    {
        public BreakPointDbContext(DbContextOptions<BreakPointDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<TokenManager> Tokens { get; set; }
    }
}
