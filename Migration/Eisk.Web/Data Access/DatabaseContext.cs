using System.Data.Entity;
using Eisk.Models;

namespace Eisk.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            // Required to prevent bug - http://stackoverflow.com/questions/5737733
            Configuration.ValidateOnSaveEnabled = false;
            Configuration.LazyLoadingEnabled = true;
        }

        public virtual IDbSet<Employee> EmployeeRepository { get; set; }
    }
}