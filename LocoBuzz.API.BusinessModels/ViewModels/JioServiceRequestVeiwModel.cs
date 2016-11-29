using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocoBuzz.API.BusinessModels.Models
{
    public class JioServiceRequestViewModel
    {
        public string JioNumber {get;set;}
        //public JioCustomer Customer {get;set;}
        //public JioProduct Product {get;set;}
        public JioCategory Category { get; set; }
        public JioSubCategory SubCategory {get;set;}
        public JioSubSubCategory SubSubCategory { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
    }
}
