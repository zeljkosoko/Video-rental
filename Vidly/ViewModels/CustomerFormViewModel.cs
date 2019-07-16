using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerFormViewModel
    {
        public Customer Customer { get; set; }
        //we define property type of IEnumerable
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
    }
}