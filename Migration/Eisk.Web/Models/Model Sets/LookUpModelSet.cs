using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Eisk.DataAccess;
using Eisk.Helpers;

namespace Eisk.Models
{
    public class LookUpModelSet
    {
        public static IEnumerable<SelectListItem> SupervisorSelectList
        {
            get
            {
                IEnumerable<Employee> supervisors =
                    DependencyHelper.GetInstance<DatabaseContext>().EmployeeRepository.AsEnumerable();

                IEnumerable<SelectListItem> supervisorSelectList =
                    supervisors.Select(option => new SelectListItem
                    {
                        Text = option.FirstName + " " + option.LastName,
                        Value = option.Id.ToString()
                    });

                return supervisorSelectList;
            }
        }

        static SelectList _countrySelectList;
        public static SelectList CountrySelectList => _countrySelectList ?? (_countrySelectList = new SelectList(CountryList.Countries));
    }
}