using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Eisk.DataAccess;
using Eisk.Helpers;
using Eisk.Models;

namespace Eisk.BusinessRules
{
    public class EmployeeAddressMustBeUnique
    {
        private const string ERROR_MESSAGE = "Error for unique address occured. EmployeeId: {0}, EmployeeAddress: {1}";

        public static ValidationResult Validate(Employee employee)
        {
            var otherEmployeesHavingSameAddress =
                DependencyHelper.GetInstance<DatabaseContext>().
                    EmployeeRepository.
                    Where(e => (e.Address.AddressLine == employee.Address.AddressLine) && (e.Id != employee.Id)).
                    ToList();

            if (otherEmployeesHavingSameAddress.Count > 0)
                return new ValidationResult(GetFormattedErrorMessage(employee),
                    new[] {string.Empty, nameof(Address.AddressLine)});

            return ValidationResult.Success;
        }

        private static string GetFormattedErrorMessage(Employee employee)
        {
            return string.Format(ERROR_MESSAGE, employee.Id, employee.Address.AddressLine);
        }

        public static bool IsErrorAvalilableIn(Controller controller, Employee employee)
        {
            return controller.IsErrorAvalilableIn(GetFormattedErrorMessage(employee));
        }
    }
}