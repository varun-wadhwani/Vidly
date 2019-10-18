using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        //name was nullable before so to make it required we have overridden its default behaviour.
        //This is called Data Annotations.
        [Required(ErrorMessage = "Please enter customer's name.")]

        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        //MembershipType will be called a navigation property because it allows us to navigate from one type to another, 
        //in this case from Customer to its MembershipType. These navigation properties are useful when we want to load the 
        //object and its related object from the database. For eg., we can load customer and its MembershipType together.
        public MembershipType MembershipType { get; set; }

        //Sometimes for optimization we don't want to load the entire membership object , we may only need the foriegn key
        //By convention we are going to call it MembershipTypeId. So entity framework recognizes this convention and treats this property as a foriegn key.
        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }

        [DataType(DataType.Date)]
        [Min18YearsIfAMember]
        //To allow null DOBs(as the customer may or may not provide DoB), put question mark after the datatype DateTime?
        public DateTime? Birthdate { get; set; }
    }
}