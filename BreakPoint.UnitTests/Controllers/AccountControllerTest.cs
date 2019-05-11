namespace BreakPoint.UnitTests.Controllers
{
    using BreakPoint.App.Controllers;
    using BreakPoint.Models.BindingModels.Account;
    using BreakPoint.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class AccountControllerTest
    {
        [Fact]
        public void Post_RegisterAndLogin_ShouldReturnStatusCode201()
        {
            // Arrange
            var token = "tokenBearer-tokenBearer-tokenBearer";
            RegisterUserBindingModel bm = new RegisterUserBindingModel()
            {
                Name = "Gosho",
                Email = "gosho@abv.bg",
                Password = "P@ssl0rd",
                ConfirmPassword = "P@ssl0rd"
            };

            var serviceMock = new Mock<IAccountService>(); // Using Moq to mock the service
            serviceMock
                .Setup(s => s.CreateNewUserAccount(bm))
                .Returns(token);

            var controller = new AccountController(serviceMock.Object);

            // Act
            var response = controller.RegisterAndLogin(bm);

            // Assert
            Assert.IsType<OkObjectResult>(response);
        }

    }
}
