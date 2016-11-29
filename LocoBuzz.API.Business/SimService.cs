using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using LocoBuzz.API.Common.Constants;
using LocoBuzz.API.Business.Interfaces;
using LocoBuzz.API.Common.Helpers;
using RestSharp;
using LocoBuzz.API.BusinessModels.Models;

namespace LocoBuzz.API.Business
{
    public class SimService : ISimService
    {
        private string apiPath;
        public SimService()
        {
            apiPath = ConfigurationManager.AppSettings["SimUrl"].Trim();
        }
        private string CreateApiPath(string querystring,string JioConstant)
        {
            IEnumerable<string> list = new List<string>() { JioConstant, querystring };
            string uri = string.Join<string>("/", list);
            return uri;
        }

        private string CreateApiPath(List<string> querystring, string JioConstant)
        {
            List<string> list = new List<string>() { JioConstant };
            string uri = string.Empty;

            list.AddRange(querystring);
            
            uri = string.Join<string>("/", list);
            return uri;
        }

        public T GetJioIdFromMobile<T>(string jioNumber,T t = default(T)) where T : class, new()
        {
            apiPath = CreateApiPath(jioNumber, JioConstants.USERINFO);
            if(t == null) t = new T();
            return t = new RestClient().RestClient<T>(apiPath, Method.GET, "application/json", t);
        }


        public T SearchKnowledgeBase<T>(string data, T t= default(T)) where T : class, new()
        {
            apiPath = CreateApiPath(data, JioConstants.KMSEARCH);
            if(t == null) t = new T();
            return t = new RestClient().RestClient<T>(apiPath, Method.GET, "application/json", t);    
        }

        public T GetSimOrderStatus<T>(string data, OrderType OrderType, T t) where T : class, new()
        {
            string jioQueryString = string.Empty;
            if (OrderType == OrderType.JiOID || OrderType == OrderType.MOBILENUMBER)
                jioQueryString = JioConstants.ORDERSTATUSMOBNUMBER;
            else
                jioQueryString = JioConstants.QUERYCUSTOMERORDER;

            apiPath = string.Concat(apiPath,CreateApiPath(data, jioQueryString));
            if(t==null)t = new T();
            return t = new RestClient().RestClient<T>(apiPath, Method.GET, "application/json", t);
        }

        public virtual T ChangeEmail<T>(string queryParameter,string email,OrderType OrderType,T t=default(T)) where T : class, new()
        {
            List<string> emailList = new List<string>() { queryParameter,email,"/" };
            apiPath = string.Concat(apiPath,CreateApiPath(emailList, JioConstants.EMAILCONSTANT));
            if (t == null) t = new T();
            return t = new RestClient().RestClient<T>(apiPath, Method.GET, "application/json", t);
        }


        public T GetBalanceDetails<T>(string data, OrderType OrderType, T t=default(T)) where T : class, new()
        {
            apiPath = CreateApiPath(data, JioConstants.BALANCESUMMARY);
            if (t == null) t = new T();
            return t = new RestClient().RestClient<T>(apiPath, Method.GET, "application/json", t);
        }
    }
}
