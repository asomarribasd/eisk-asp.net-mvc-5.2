using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Xml.Linq;
using Eisk.DataAccess;
using Eisk.Helpers;
using Eisk.Models;

namespace Eisk
{
    public static class TestDataHelper
    {
        public static Employee CreateEmployeeWithValidData(int id = 0)
        {
            return new Employee
            {
                Id = id,
                LastName = "Alam",
                FirstName = "Ashraful",
                Title = "Chief Architect",
                TitleOfCourtesy = "Mr.",
                BirthDate = new DateTime(1983, 1, 23),
                HireDate = new DateTime(2007, 5, 16),
                Address = new Address
                {
                    AddressLine = "103 Banani",
                    City = "Dhaka",
                    Region = "N/A",
                    PostalCode = "1207",
                    Country = "USA"
                },
                Phone = "3901-2420-9334",
                Extension = "123",
                Photo = null,
                Notes = "Coding geek.",
                ReportsTo = null

            };
        }

        public static void InitializeSchemaAndData(string filePath)
        {
            Database.Delete(nameof(DatabaseContext));
            var initializer = new DatabaseContextInitializer(filePath);
            Database.SetInitializer(initializer);
            initializer.InitializeDatabase(DependencyHelper.GetInstance<DatabaseContext>());
        }

        public static List<Employee> GetEmployeeDataFromXml(string filePath)
        {
            XDocument xDoc = XDocument.Load(filePath);
            return (from xElement in xDoc.Descendants("Employees")
                let dateTimeValue = xElement.GetDateTimeValue(nameof(Employee.HireDate))
                where dateTimeValue != null
                select new Employee
                    {
                        //Id = int.Parse(e.Element("Id").Value),
                        LastName = xElement.GetStringValue(nameof(Employee.LastName)),
                        FirstName = xElement.GetStringValue(nameof(Employee.FirstName)),
                        Title = xElement.GetStringValue(nameof(Employee.Title)),
                        TitleOfCourtesy = xElement.GetStringValue(nameof(Employee.TitleOfCourtesy)),
                        BirthDate = xElement.GetDateTimeValue(nameof(Employee.BirthDate)),
                        HireDate = (DateTime)dateTimeValue,
                        Address = new Address
                        {
                            AddressLine = xElement.GetStringValue(nameof(Employee.Address)),
                            City = xElement.GetStringValue(nameof(Address.City)),
                            Region = xElement.GetStringValue(nameof(Address.Region)),
                            PostalCode = xElement.GetStringValue(nameof(Address.PostalCode)),
                            Country = xElement.GetStringValue(nameof(Address.Country))
                        },
                        Phone = xElement.GetStringValue(nameof(Employee.Phone)),
                        Extension = xElement.GetStringValue(nameof(Employee.Extension)),
                        Photo = xElement.GetByteArrayValue(nameof(Employee.Photo)),
                        Notes = xElement.GetStringValue(nameof(Employee.Notes)),
                        ReportsTo = xElement.GetIntValue(nameof(Employee.ReportsTo))
                    }).ToList();
        }
    }
}

