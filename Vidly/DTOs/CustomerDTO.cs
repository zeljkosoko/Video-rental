using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.DTOs
{
    public class CustomerDTO
    {               //model class without dependency models(MembershipType) and display attributes:

        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public byte MembershipTypeId { get; set; }
        //DTO should not have domain object like MembershipType than MembershipTypeDTO
        public MembershipTypeDTO MembershipType { get; set; }

        //[Min18YearsIfAMember] //MVC Doesnt support "Custom" Client Side Validation
        //comment attribute cause (Customer)cast operator will fail with CustomerDto casting in Min18years model 
        public DateTime? Birthdate { get; set; }
    }
}