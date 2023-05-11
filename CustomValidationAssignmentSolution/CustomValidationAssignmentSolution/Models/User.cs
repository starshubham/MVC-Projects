using CustomValidationAssignmentSolution.CustomValidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomValidationAssignmentSolution.Models
{
    [HobbiesValidation(ErrorMessage = "At least one hobby and maximum of four hobbies should be selected")]
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public bool Photography { get; set; }
        public bool Gardening { get; set; }
        public bool Dance { get; set; }
        public bool Hiking { get; set; }
        public bool Painting { get; set; }
    }
}


