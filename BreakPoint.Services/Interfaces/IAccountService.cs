namespace BreakPoint.Services.Interfaces
{
    using BreakPoint.Models.BindingModels.Account;
    using BreakPoint.Models.ViewModels.Account;

    public interface IAccountService
    {
        AccountLoginViewModel CreateNewUserAccount(RegisterUserBindingModel bm);
        AccountLoginViewModel LoginUser(LoginUserBindingModel bm);
    }
}
