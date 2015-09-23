using Eisk.DataAccess;
using Eisk.Models;
using System.Collections.Generic;
using System.Data.Entity;
namespace Eisk
{
    public class DatabaseContextInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        readonly string _testDataFilePath;
        public DatabaseContextInitializer(string testDataFilePath)
        {
            _testDataFilePath = testDataFilePath;
        }

        protected override void Seed(DatabaseContext context)
        {
            List<Employee> employees = TestDataHelper.GetEmployeeDataFromXml(_testDataFilePath);
            employees.ForEach(d => context.EmployeeRepository.Add(d));
        }
    }
}