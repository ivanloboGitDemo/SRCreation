﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelianceSRCreation.Models
{
    public class JioListServiceResponse
    {
        public string Category { get; set; }

        public string SubCategory { get; set; }

        public string SubSubCategory { get; set; }

        public string Status { get; set; }

        public DateTime? ResolvedDate { get; set; }

        public string Remarks { get; set; }
    }
}
