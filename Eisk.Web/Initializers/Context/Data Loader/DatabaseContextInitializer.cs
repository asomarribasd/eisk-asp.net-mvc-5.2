using System.Data.Entity;
using Eisk.DataAccess;

namespace Eisk
{
    public class DatabaseContextInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        private readonly string _testDataFilePath;

        public DatabaseContextInitializer(string testDataFilePath)
        {
            _testDataFilePath = testDataFilePath;
        }

        protected override void Seed(DatabaseContext context)
        {
            var employees = TestDataHelper.GetEmployeeDataFromXml(_testDataFilePath);
            employees.ForEach(d => context.EmployeeRepository.Add(d));
        }
    }
}