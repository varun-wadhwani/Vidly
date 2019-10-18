using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MembershipType
    {
        //In an entity framework, every entity must have a key which would be mapped with the primary key of the corresponding table in the database
        //byte because we have only few membership types, Id is membership type
        public byte Id { get; set; }
        //short because the fee would not be more than 32000 dollars
        public short SignUpFee { get; set; }
        //byte because the largest no. we would store is 12, for 12 months
        public byte DurationInMonths { get; set; }
        //byte because it would be in percentage from 0 to 100
        public byte DiscountRate { get; set; }
        public string Name { get; set; }

        public static readonly byte Unknown = 0;
        public static readonly byte PayAsYouGo = 1;

    }
}