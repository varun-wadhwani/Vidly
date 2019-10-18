using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        /*
         * We have to make Dto(Data transfer object) completely decoupled(not dependent in any way) with our domain objects so do not include anything 
         * from the domain object. Only include primitive types, such as, int , bool, etc or custom Dtos. So if we have to use hierarchical data structure 
         * such as, MembershipType we will another type called MembershipType Dto. That's why commenting the below line.
         */

        //public MembershipType MembershipType { get; set; }

        public byte MembershipTypeId { get; set; }

        public MembershipTypeDto MembershipType { get; set; }

        //[Min18YearsIfAMember]
        //To allow null DOBs(as the customer may or may not provide DoB), put question mark after the datatype DateTime?
        public DateTime? Birthdate { get; set; }
    }
}