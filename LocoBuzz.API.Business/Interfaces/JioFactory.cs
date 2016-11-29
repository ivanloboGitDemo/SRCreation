using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocoBuzz.API.BusinessModels;
using LocoBuzz.API.BusinessModels.Models;
using LocoBuzz.API.Common.Constants;

namespace LocoBuzz.API.Business.Interfaces
{
    public interface IJioFactory
    {
        List<JioCategory> GetJioCategories();
        List<JioSubCategory> GetJioSubCategories(JioCategory Category);
        List<JioSubSubCategory> GetJioSubSubCategories(JioSubCategory SubCategory);
        bool ValidateJioUser(string Number, string Jtype);
        JioListServiceResponse GetSRListByJioNumber(string jioNumber);
    }

    public interface ISimService
    {
        T GetSimOrderStatus<T>(string data, OrderType OrderType, T t) where T : class,new();
        T GetBalanceDetails<T>(string data, OrderType OrderType, T t) where T : class,new();
        T SearchKnowledgeBase<T>(string data, T t) where T : class,new();
        T GetJioIdFromMobile<T>(string jioNumber, T t) where T : class,new();
        T ChangeEmail<T>(string queryParameter, string email, OrderType OrderType,T t) where T : class, new();
    }

    public interface ICustomerService
    {
        List<JioProduct> GetJioProductsForJioCustomer(JioCustomer Customer);
        JioCustomer GetJioCustomerByJioNumber(String JioNumber);
    }

    public interface ISRService : IJioFactory
    {
        JioRetrieveServiceResponse RetrieveServiceRequest(string srReferenceNo);

        IList<JioListServiceResponse> ListServiceRequest(string partyID);

        JioServiceResponse GenerateServiceRequest(String JioNumber,
             JioCustomer Customer,
             JioProduct Product,
             JioCategory Category,
             JioSubCategory SubCategory,
             JioSubSubCategory SubSubCategory,
             String Description,
             String Reason);

        JioServiceResponse CreateServiceRequest(JioServiceRequestViewModel jioServiceRequestModel);
    }

    public interface IJioManagerFactory
    {
       IJioFactory JioFactory { get; set; }
       ISRService SrService { get; set; }
       ICustomerService CustService { get; set; }
       ISimService SimService { get; set; }
    }

    
}
