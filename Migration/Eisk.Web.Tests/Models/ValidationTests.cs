using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Eisk.Models;
using Xunit;
/****************** Copyright Notice *****************
 
This code is licensed under Microsoft Public License (Ms-PL). 
You are free to use, modify and distribute any portion of this code. 
The only requirement to do that, you need to keep the developer name, as provided below to recognize and encourage original work:

=======================================================
   
Architecture Designed and Implemented By:
Mohammad Ashraful Alam
Microsoft Most Valuable Professional, ASP.NET 2007 – 2013
Twitter: http://twitter.com/AshrafulAlam | Blog: http://weblogs.asp.net/ashraful | Github: https://github.com/ashrafalam
   
*******************************************************/

namespace Eisk.Tests
{
    public class ValidationTests
    {
        [Fact]
        public void Employee_model_set_with_Ivalidable_objet()
        {
            // Arrange
            var propertyInfo = typeof(Employee).GetProperty("FirstName");

            // Act
            var attribute = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), false);

            // Assert
            Assert.NotNull(attribute);
        }
        [Fact]
        public void Test_Copy()
        {
            Employee emp = TestDataHelper.CreateEmployeeWithValidData();
            Type type = emp.GetType();
            PropertyInfo[] myObjectFields = type.GetProperties(
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo fi in myObjectFields)
            {
                Console.WriteLine(fi.ToString());
            }
        }

        private static void UpdateForType(Type type, Object source, Object destination)
        {
            FieldInfo[] myObjectFields = type.GetFields(
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (FieldInfo fi in myObjectFields)
            {
                fi.SetValue(destination, fi.GetValue(source));
            }
        }

    }
}
