using System.Web.Mvc;
using Eisk.BusinessRules;
using Eisk.Controllers;
using Eisk.Helpers;
using Xunit;

namespace Eisk.IntegrationTests
{
    public class EmployeesControllerTest : IntegrationTestBase
    {
        [Fact]
        public void Edit_Positive_Get_Test()
        {
            // Arrange
            TestInitialize();
            var controller = DependencyHelper.GetInstance<EmployeesController>();

            // Act
            var viewResult = controller.Edit(1) as ViewResult;

            // Assert
            Assert.Equal(string.Empty, viewResult.ViewName);
        }

        [Fact]
        public void Edit_Positive_Post_Test()
        {
            //Arrange
            TestInitialize();
            var controller = DependencyHelper.GetInstance<EmployeesController>();

            var employee = TestDataHelper.CreateEmployeeWithValidData(1);

            controller.FireValidationForModel(employee);

            //Act
            var result = controller.Edit(employee);

            //Assert
            Assert.True(controller.ModelState.IsValid);

        }

        [Fact]
        public void Edit_Negative_Test_Post_Test()
        {
            //Arrange
            TestInitialize();
            var controller = DependencyHelper.GetInstance<EmployeesController>();
            var employee = TestDataHelper.CreateEmployeeWithValidData(1);
            employee.Address.AddressLine = "2, ABC Road";
            controller.FireValidationForModel(employee);
            //Act
            controller.Edit(employee);

            //Assert
            Assert.True(EmployeeAddressMustBeUnique.IsErrorAvalilableIn(controller, employee));
        }

        [Fact]
        public void Edit_Test()
        {
            //Arrange
            TestInitialize();

            var controller = DependencyHelper.GetInstance<EmployeesController>();

            var employee = TestDataHelper.CreateEmployeeWithValidData(1);

            employee.Address.AddressLine = string.Empty;

            controller.FireValidationForModel(employee.Address);

            //Act
            var result = controller.Edit(employee);

            //Assert
            Assert.False(controller.ModelState.IsValid);

        }

    }
}
