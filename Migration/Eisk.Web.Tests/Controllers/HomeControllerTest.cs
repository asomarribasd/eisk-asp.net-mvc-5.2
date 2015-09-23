using Eisk.Controllers;
using Xunit;

namespace Eisk.Tests
{
    public class HomeControllerTest
    {
        [Fact]
        public void About()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.About();

            // Assert
            Assert.NotNull(result);
        }
    }
}