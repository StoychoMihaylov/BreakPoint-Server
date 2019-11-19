namespace BreakPoint.UnitTests.Services
{
    using System;
    using Data;
    using Xunit;
    using Microsoft.EntityFrameworkCore;
    using BreakPoint.Services.Services;
    using BreakPoint.Models.BindingModels.Account;
    using BreakPoint.Data.EntityModels;
    using System.Linq;
    using BreakPoint.Models.ViewModels.Account;

    public class AccountServiceTest
    {
        [Fact]
        public void CreateNewUserAccount_CalledWithCorrectInputData_ShouldReturnTrueAndUserCreated()
        {
            // Arrange
            var createdUser = new User();
            var db = this.GetDatabase();
            var service = new AccountService(db);

            RegisterUserBindingModel bm = new RegisterUserBindingModel()
            {
                Name = "Gosho",
                Email = "gosho@abv.bg",
                Password = "P@ssl0rd",
                ConfirmPassword = "P@ssl0rd"
            };

            // Act
            var tokenResponse = service.CreateNewUserAccount(bm);
            if (tokenResponse != null)
            {
                // Check if user is created.
                createdUser = db.Users.Single();
            }

            // Assert
            Assert.NotNull(createdUser);
        }

        [Fact]
        public void LoginUser_CalledWithRelevantInputData_ShouldReturnTokenBearer()
        {
            // Arrange
            var userCredentials = new AccountLoginViewModel();
            var db = this.GetDatabase();
            var service = new AccountService(db);

            RegisterUserBindingModel bm = new RegisterUserBindingModel()
            {
                Name = "Gosho",
                Email = "gosho@abv.bg",
                Password = "P@ssl0rd",
                ConfirmPassword = "P@sslord"
            };

            LoginUserBindingModel loginForm = new LoginUserBindingModel()
            {
                Email = "gosho@abv.bg",
                Password = "P@ssl0rd"
            };

            // Act
            // Case with Correct Password Input
            var isUserCreated = service.CreateNewUserAccount(bm);
            if (isUserCreated != null)
            {
                userCredentials = service.LoginUser(loginForm);
            }

            // Assert
            Assert.NotNull(userCredentials);
            var userId = userCredentials.Token;
            Assert.NotEmpty(userId);
            var token = userCredentials.Token;
            Assert.NotEqual(0, token.Length);
        }

        private BreakPointDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<BreakPointDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Microsoft.EntityFrameworkCore.InMemory
                .Options;

            return new BreakPointDbContext(dbOptions);
        }
    }
}
