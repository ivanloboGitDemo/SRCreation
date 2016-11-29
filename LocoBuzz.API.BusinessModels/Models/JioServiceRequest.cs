using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocoBuzz.API.BusinessModels.Models
{
    public class JioServiceResponse
    {
        public string SRID { get; set; }
        public string PartyID { get; set; }
        public string JioNumber { get; set; }
        public string ChannelID { get; set; }
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public string SubSubCategoryID { get; set; }
        public string SubSubCategoryName { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
    }
}
