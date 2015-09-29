using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Eisk.Helpers;

namespace Eisk.Models
{
    public class EmployeeEditorModel : Employee
    {
        public EmployeeEditorModel()
        {
            PageTitle = "New Employee";
        }

        public EmployeeEditorModel(Employee employee)
        {
            PageTitle = StringHelper.ConnectStrings(" ", employee.TitleOfCourtesy, employee.FirstName, employee.LastName);
            Mapper.CreateMap<Employee, EmployeeEditorModel>();
            Mapper.Map(employee, this);
        }

        public string PageTitle { get; private set; }
        public string EditorAction => Id == 0 ? "Create" : "Edit";
    }
}