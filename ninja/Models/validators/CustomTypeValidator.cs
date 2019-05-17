using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ninja.Models.validators
{
    public class CustomTypeValidator : ValidationAttribute

    {



        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {

                System.Collections.ArrayList types = new ArrayList();
                types.Add("A");
                types.Add("B");
                types.Add("C");

                string typeInput = value.ToString().ToUpper();

                if (types.Contains(typeInput))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Please Enter a Valid Type.");
                }
            }
            else
            {
                return new ValidationResult("" + validationContext.DisplayName + " is required");
            }
        }
    }
}