using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocoBuzz.API.Business
{
    public class Result
    {
        public string description { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string url { get; set; }
    }

    public class JioKnowledgeBase
    {
        public JioKnowledgeBase() { }
        public List<Result> results { get; set; }
    }
}
