using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocoBuzz.API.Common.Constants
{
    //public class Constants
    //{
    //    
    //    public const bool DEBUG = false;
        
    //}
    public enum JioType 
    { 
        JIO,
        SR
    }

    public enum OrderType
    { 
        MOBILENUMBER,
        REFNUMBER,
        JiOID
    }
    public static class JioConstants
    {
        public const string ORDERSTATUSMOBNUMBER = @"orderStatus";
        public const string QUERYCUSTOMERORDER = @"queryCustomerOrder";
        public const string EMAILCONSTANT = @"updateEmail";
        public const string USERINFO = @"getUserInfo";
        public const string BALANCESUMMARY = @"userData";
        public const string KMSEARCH = @"userSearch";
        public const string SALESCHANNEL = "88";
        public const string CATEGORYCONSTANT = "CUSTOMERPROBLEMCATG";
        public const string SUBCATEGORYCONSTANT = "CUSTOMERPROBLEMSUBCATG";
        public const string SUBSUBCATEGORYCONSTANT = "CUSTOMERPROBLEMSUB_SUBCATG";
    }

}
