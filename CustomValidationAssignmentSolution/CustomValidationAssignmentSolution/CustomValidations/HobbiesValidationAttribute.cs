using CustomValidationAssignmentSolution.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomValidationAssignmentSolution.CustomValidations
{
    public class HobbiesValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            User u = (User)value;  // if checkbox is selected, it will be true; otherwise false
            List<bool> allHobbies = new List<bool>() { u.Dance, u.Gardening, u.Hiking, u.Painting, u.Photography };  // get all checkbox values
            List<bool> selectedHobbies = allHobbies.FindAll(hobby => hobby); // get all true values
            if (selectedHobbies.Count >= 1 && selectedHobbies.Count <= 4)  // if at least one true and maximum of 4 trues
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(this.ErrorMessage);
            }
        }
    }
}
