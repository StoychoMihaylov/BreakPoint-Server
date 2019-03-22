namespace BreakPoint.Services.Services
{
    using BreakPoint.Data.Interfaces;
    using BreakPoint.Services.Interfaces;

    public class AccountService : Service, IAccountService
    {
        public AccountService(IBreakPointDbContext context)
            : base(context)
        {
        }
    }
}
