namespace BreakPoint.App.Controllers
{
    using BreakPoint.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private IAccountService service;

        public AccountController(IAccountService service)
        {
            this.service = service;
        }

        // api/account/register
        [HttpPost]
        [Route("register")]
        public IActionResult Index()
        {
            return View();
        }
    }
}