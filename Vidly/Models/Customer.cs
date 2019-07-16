using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        //navigation property  Customer ----(*,1)-> MembershipType
        public MembershipType MembershipType { get; set; }

        //Foreign Key
        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }

        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Date of Birth")]
        [Min18YearsIfAMember] //MVC Doesnt support "Custom" Client Side Validation
        
        public DateTime? Birthdate { get; set; }
        
    }
}