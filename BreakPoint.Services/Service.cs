namespace BreakPoint.Services
{
    using BreakPoint.Data.Interfaces;

    public abstract class Service
    {
        public Service(IBreakPointDbContext context)
        {
            this.Context = context;
        }

        protected IBreakPointDbContext Context { get; set; }
    }
}
