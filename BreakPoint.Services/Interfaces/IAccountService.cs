namespace BreakPoint.Services.Interfaces
{
    using BreakPoint.Models.BindingModels.Account;

    public interface IAccountService
    {
        string CreateNewUserAccount(RegisterUserBindingModel bm);
        string LoginUser(LoginUserBindingModel bm);
    }
}
