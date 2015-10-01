using System.ComponentModel.DataAnnotations;
using Eisk.Helpers;
using Eisk.Models;
using Xunit;

namespace Eisk.Tests
{
    public class EmployeeClassValidationTests:TestBase
    {
        [Fact]
        public void Check_FirstName_IsRequired()
        {
            AssertValidationAttribute(typeof (Employee), nameof(Employee.FirstName), typeof (RequiredAttribute));
        }

        [Fact]
        public void Check_LastName_IsRequired()
        {
            AssertValidationAttribute(typeof(Employee), nameof(Employee.LastName), typeof(RequiredAttribute));
        }
    }
}