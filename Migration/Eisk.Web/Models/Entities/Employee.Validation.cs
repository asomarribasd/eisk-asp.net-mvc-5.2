using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eisk.BusinessRules;

namespace Eisk.Models
{
    public partial class Employee : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return EmployeeAddressMustBeUnique.Validate(this);
            yield return SupervisorCountryMustBeSame.Validate(this);
        }
    }
}