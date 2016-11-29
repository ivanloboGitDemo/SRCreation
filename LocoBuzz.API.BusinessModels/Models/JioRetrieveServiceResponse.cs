using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocoBuzz.API.BusinessModels.Models
{
    public class JioRetrieveServiceResponse
    {
        public string Status {get;set;}

        public DateTime? ResolutionDate { get; set; }

        public string PartyId { get; set; }
    }
}
