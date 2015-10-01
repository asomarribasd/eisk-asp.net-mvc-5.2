using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Eisk.DataAccess;
using Eisk.Helpers;
using Eisk.Models;

namespace Eisk.BusinessRules
{
    public class SupervisorCountryMustBeSame
    {
        private const string ERROR_MESSAGE = "Supervisor country must be same as subordinate.";

        public static ValidationResult Validate(Employee employee)
        {
            if (employee.ReportsTo != null)
            {
                if (employee.Supervisor == null)
                    employee.Supervisor =
                        DependencyHelper.GetInstance<DatabaseContext>().
                            EmployeeRepository.
                            Find((int) employee.ReportsTo);

                if (employee.Address.Country != employee.Supervisor.Address.Country)
                    return new ValidationResult(ERROR_MESSAGE, new[] {string.Empty, nameof(Address.Country)});
            }

            return ValidationResult.Success;
        }

        public static bool IsErrorAvalilableIn(Controller controller)
        {
            return controller.IsErrorAvalilableIn(ERROR_MESSAGE);
        }
    }
}