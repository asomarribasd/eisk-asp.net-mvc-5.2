using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Eisk.DataAccess;
using Eisk.Helpers;

namespace Eisk.Models
{
    public class LookUpModelSet
    {
        public static IEnumerable<SelectListItem> SupervisorSelectList(int? reportsToId)
        {
            var supervisors =
                DependencyHelper.GetInstance<DatabaseContext>().EmployeeRepository.AsEnumerable();

            var supervisorSelectList =
                supervisors.Select(option => new SelectListItem
                {
                    Text = option.FirstName + " " + option.LastName,
                    Value = option.Id.ToString(),
                    Selected = reportsToId != null && option.Id == reportsToId
                });

            return supervisorSelectList;
        }
    }
}