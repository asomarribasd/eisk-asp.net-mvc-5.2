using System.Web.Mvc;
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
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About();

            // Assert
            Assert.NotNull(result);
        }

    }
}
