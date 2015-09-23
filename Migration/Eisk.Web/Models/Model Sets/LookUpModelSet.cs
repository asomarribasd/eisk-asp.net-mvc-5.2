using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Eisk.DataAccess;
using Eisk.Helpers;

namespace Eisk.Models
{
    public class LookUpModelSet
    {
        private static SelectList _countrySelectList;

        public static IEnumerable<SelectListItem> SupervisorSelectList
        {
            get
            {
                var supervisors =
                    DependencyHelper.GetInstance<DatabaseContext>().EmployeeRepository.AsEnumerable();

                var supervisorSelectList =
                    supervisors.Select(option => new SelectListItem
                    {
                        Text = option.FirstName + " " + option.LastName,
                        Value = option.Id.ToString()
                    });

                return supervisorSelectList;
            }
        }

        public static SelectList CountrySelectList
            => _countrySelectList ?? (_countrySelectList = new SelectList(CountryList.Countries));
    }
}