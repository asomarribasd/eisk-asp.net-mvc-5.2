using System.Data.Entity;
using Eisk.Models;

namespace Eisk.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public virtual IDbSet<Employee> EmployeeRepository { get; set; }

        public DatabaseContext()
        {
            // Required to prevent bug - http://stackoverflow.com/questions/5737733
            Configuration.ValidateOnSaveEnabled = false;
            Configuration.LazyLoadingEnabled = true;
        }

        public new virtual IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}