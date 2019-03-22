namespace BreakPoint.Data.Interfaces
{
    using BreakPoint.Data.EntityModels;
    using Microsoft.EntityFrameworkCore;

    public interface IBreakPointDbContext
    {
        DbSet<User> Users { get; set; }

        int SaveChanges();
    }
}
