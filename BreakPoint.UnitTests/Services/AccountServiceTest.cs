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
            if (tokenResponse != string.Empty)
            {
                // Check if user is created.
                createdUser = db.Users.Single();
            }

            // Assert
            Assert.NotEmpty(tokenResponse);
            Assert.NotNull(createdUser);
        }

        [Fact]
        public void LoginUser_CalledWithRelevantInputData_ShouldReturnTokenBearer()
        {
            // Arrange
            var responseToken = "";
            var responseEmptyString = "";
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
            if (isUserCreated != string.Empty)
            {
                responseToken = service.LoginUser(loginForm);
            }

            // Case with Incorrect Password Input
            if (isUserCreated != string.Empty)
            {
                loginForm.Password = "fdsfdfd";
                responseEmptyString = service.LoginUser(loginForm);
            }

            //Assert
            Assert.NotEmpty(isUserCreated);
            Assert.NotEmpty(responseToken);
            Assert.Empty(responseEmptyString);
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
