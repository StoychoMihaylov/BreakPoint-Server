namespace BreakPoint.Data.Interfaces
{
    using BreakPoint.Data.EntityModels;
    using Microsoft.EntityFrameworkCore;

    public interface IBreakPointDbContext
    {
        DbSet<User> Users { get; set; }

        DbSet<TokenManager> Tokens { get; set; }

        int SaveChanges();
    }
}
