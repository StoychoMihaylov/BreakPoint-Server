namespace BreakPoint.UnitTests.Controllers
{
    using BreakPoint.App.Controllers;
    using FluentAssertions;
    using Xunit;

    public class ValuesControllerTest
    {

        ValuesController controller;

        public ValuesControllerTest()
        {
            this.controller = new ValuesController();
        }

        [Fact]
        public void Get_WhenIsCalled_ReturnsNumber()
        {
            // Act
            var result = this.controller.Get().Value;

            // Assert
            Assert.Equal(1, result);
        }
    }
}
