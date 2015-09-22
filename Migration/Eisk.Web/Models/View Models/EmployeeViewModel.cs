/****************** Copyright Notice *****************
 
This code is licensed under Microsoft Public License (Ms-PL). 
You are free to use, modify and distribute any portion of this code. 
The only requirement to do that, you need to keep the developer name, as provided below to recognize and encourage original work:

=======================================================
   
Architecture Designed and Implemented By:
Mohammad Ashraful Alam
Microsoft Most Valuable Professional, ASP.NET 2007 – 2013
Twitter: http://twitter.com/AshrafulAlam | Blog: http://weblogs.asp.net/ashraful | Github: https://github.com/ashrafalam
   
*******************************************************/

using System.Web;
using System.Web.Mvc;
using Eisk.Helpers;

namespace Eisk.Models
{
    public class EmployeeViewModel
    {
        readonly Employee _employee;
        readonly ControllerHelper _controllerHelper;

        public EmployeeViewModel(Employee employee, Controller controller)
        {
            _employee = employee;
            _controllerHelper = new ControllerHelper(controller);

        }

        public int EmployeeId => _employee.Id;

        public string FullName => StringHelper.ConnectStrings(" ", _employee.TitleOfCourtesy, _employee.FirstName, _employee.LastName);

        public string Title => _employee.Title;

        public string HireDate => $"{_employee.HireDate:M/dd/yyyy}";

        public string BirthDate
        {
            get
            {
                if (_employee.BirthDate != null)
                    return $"{_employee.BirthDate:M/dd/yyyy}";
                return "No birthday provided.";
            }
        }

        public string FullAddress => StringHelper.ConnectStrings(", ", _employee.Address.AddressLine,
            _employee.Address.City, _employee.Address.Region, _employee.Address.PostalCode, _employee.Address.Country);

        public string PhoneWithExtension => StringHelper.ConnectStrings(" - ", _employee.Phone, _employee.Extension);

        public string EmployeeImageSource => _controllerHelper.Url.Action("EmployeeImageFile", new { id = _employee.Id });

        public IHtmlString SupervisorFullName
        {
            get
            {
                if (_employee.Supervisor != null)
                {
                    string supervisorDetailsUrl = _controllerHelper.Url.Action("Details", new { id = _employee.Supervisor.Id });
                    return MvcHtmlString.Create(HtmlConverter.LinkWrapper(StringHelper.ConnectStrings(" ", _employee.Supervisor.TitleOfCourtesy,
                        _employee.Supervisor.FirstName, _employee.Supervisor.LastName), 
                        supervisorDetailsUrl));
                }

                return MvcHtmlString.Create("Damn lucky guy!");
            }
        }
    
        public IHtmlString ShortNotes
        {
            get
            {
                if (_employee.Notes != null)
                {
                    if (_employee.Notes.Length > 1000)
                    {
                        _employee.Notes = _employee.Notes.Substring(0, 1000) + " ...(<b>notes shortened).</b>";
                    }

                    _employee.Notes = HtmlConverter.ConvertTextToPlainHtml(_employee.Notes);
                }

                return MvcHtmlString.Create(_employee.Notes);
            }
        }
    }
}