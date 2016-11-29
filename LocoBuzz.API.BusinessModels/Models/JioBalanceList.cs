using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocoBuzz.API.BusinessModels.Models
{
    public class BalnceList
    {
        public string data { get; set; }

        public string voiceCalls { get; set; }

        public string sms { get; set; }
    }

    public class JioBalanceList
    {
        public JioBalanceList()
        { }

        public string dataDetails { get; set; }

        public List<BalnceList> balnceList { get; set; }
    }

}
