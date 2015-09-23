using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eisk.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eisk.Models
{
    [Table("Employees")]
    public partial class Employee : Person
    {
        [StringLength(30)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Required")]
        [DateGreaterThan("BirthDate")]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Supervisor")]
        public int? ReportsTo { get; set; }

        [ForeignKey("ReportsTo")]
        public virtual Employee Supervisor { get; set; }

        public virtual List<Employee> Subordinates { get; set; }

    }
}