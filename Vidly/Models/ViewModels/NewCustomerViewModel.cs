using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Why we need a separate view model is because if on the view we have to return things from more than one model we combine their properties in the 
//ViewModel.

namespace Vidly.Models.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer Customer { get; set; }
    }
}