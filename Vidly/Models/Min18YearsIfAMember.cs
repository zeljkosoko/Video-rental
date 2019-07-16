using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Models
{
    public class Min18YearsIfAMember: ValidationAttribute
    {
        //this custom val class checks birthdate prop and based on it user can select membershipTypeId(2,3,4)
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            //select membershipTypeId(0(not selected),1) => Birthdate property will not be validated
            //if (customer.MembershipTypeId == 0 || customer.MembershipTypeId == 1)
            if(customer.MembershipTypeId == MembershipType.Unknown || 
                customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            //select 2,3,4
            if (customer.Birthdate == null)
                return new ValidationResult("Birthdate is required");

            var birthdate = DateTime.Now.Year - customer.Birthdate.Value.Year;
            return (birthdate >= 18) 
                ? ValidationResult.Success 
                : new ValidationResult("Customer must be 18 year old or greater.");
        }
    }
}