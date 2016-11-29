using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocoBuzz.API.BusinessModels.Models
{
    public class JioCustomer
    {
        public string PartyID { get; set; }
        public string JioNumber { get; set; }
        public string AlternateNumber { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PANCardNo { get; set; }
        public string PassportNo { get; set; }
        public string AadhaarCard { get; set; }
        public string EmailID { get; set; }
        public string PhotoURL { get; set; }
        public string Address { get; set; }

        public string BirthDate { get; set; }
    }
}
