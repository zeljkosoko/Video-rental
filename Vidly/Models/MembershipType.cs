﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }

        //magic numbers for Min18Years Custom Validation
        public static readonly byte Unknown = 0;
        public static readonly byte PayAsYouGo = 1;
    }
}