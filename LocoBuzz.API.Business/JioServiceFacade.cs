using LocoBuzz.API.BusinessModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocoBuzz.API.Business.Interfaces;
using LocoBuzz.API.Common.Helpers;
using LocoBuzz.API.Common.Constants;

namespace LocoBuzz.API.Business
{
    public class JioServiceFacade : JioFactoryBase
    {
        //private ISRService _srservice;
        //private ICustomerService _custservice;
        private IJioFactory _jioFactory;
        //private ISRService _simService;
        private IJioManagerFactory _jioManagerFactory;

        public JioServiceFacade()
        {}
        public JioServiceFacade(IJioFactory iJioFactory)
        {
            _jioFactory = iJioFactory;
        }

        public JioServiceFacade(IJioManagerFactory ijioManagerFactory) : base(ijioManagerFactory)
        {
            _jioManagerFactory = ijioManagerFactory;
        }
        

        public override List<JioCategory> GetJioCategories()
        {
            return base.GetJioCategories();
        }

        public override List<JioSubCategory> GetJioSubCategories(BusinessModels.Models.JioCategory Category)
        {
            return base.GetJioSubCategories(Category);
        }

        public override List<JioSubSubCategory> GetJioSubSubCategories(BusinessModels.Models.JioSubCategory SubCategory)
        {
            return base.GetJioSubSubCategories(SubCategory);
        }

        public override bool ValidateJioUser(string Number, string Jtype)
        {
            try
            {
                var strType = Jtype.ToEnum<JioType>();

                if (strType == JioType.JIO)
                {
                    if (string.IsNullOrEmpty(_jioManagerFactory.CustService.GetJioCustomerByJioNumber(Number).PartyID))
                        return false;
                }
                else if (strType == JioType.SR)
                {
                    if (string.IsNullOrEmpty(_jioManagerFactory.SrService.RetrieveServiceRequest(Number).PartyId))
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override JioListServiceResponse GetSRListByJioNumber(string jioNumber)
        {
            try
            {
                IList<JioListServiceResponse> jioListSrvResponse = default(List<JioListServiceResponse>);
                JioListServiceResponse jioSrvcResponse = default(JioListServiceResponse);
                JioCustomer jioCustomer = _jioManagerFactory.CustService.GetJioCustomerByJioNumber(jioNumber);
                if (jioCustomer != null && !string.IsNullOrEmpty(jioCustomer.PartyID))
                    jioListSrvResponse = _jioManagerFactory.SrService.ListServiceRequest(jioCustomer.PartyID);
                if (jioListSrvResponse != null)
                    jioSrvcResponse = jioListSrvResponse.OrderByDescending(x => x.CreationDate).FirstOrDefault();
               
                return jioSrvcResponse;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public override T GetSimOrderStatus<T>(string data, OrderType OrderType, T t=default(T))
        {
            return base.GetSimOrderStatus<T>(data,OrderType,t);
        }

        public new T SearchKnowledgeBase<T>(string data, T t) where T : class, new()
        {
            return _jioManagerFactory.SimService.SearchKnowledgeBase<T>(data, t);
        }

        public override T GetJioIdFromMobile<T>(string jioNumber, T t)
        {
            return base.GetJioIdFromMobile<T>(jioNumber, t);
        }

        public override JioRetrieveServiceResponse RetrieveServiceRequest(string srReferenceNo)
        {
            return base.RetrieveServiceRequest(srReferenceNo);
        }

        public override IList<JioListServiceResponse> ListServiceRequest(string partyID)
        {
            return base.ListServiceRequest(partyID);
        }

        public override JioServiceResponse GenerateServiceRequest(string JioNumber, JioCustomer Customer, JioProduct Product, JioCategory Category, JioSubCategory SubCategory, JioSubSubCategory SubSubCategory, string Description, string Reason)
        {
            return base.GenerateServiceRequest(JioNumber, Customer, Product, Category, SubCategory, SubSubCategory, Description, Reason);
        }

        public override List<JioProduct> GetJioProductsForJioCustomer(JioCustomer Customer)
        {
            return base.GetJioProductsForJioCustomer(Customer);
        }

        public override JioCustomer GetJioCustomerByJioNumber(string JioNumber)
        {
            return base.GetJioCustomerByJioNumber(JioNumber);
        }

        public override T ChangeEmail<T>(string queryParameter, string email, OrderType OrderType, T t)
        {
            return base.ChangeEmail<T>(queryParameter,email,OrderType,t);
        }


        public override T GetBalanceDetails<T>(string data, OrderType OrderType, T t) 
        {
            return base.GetBalanceDetails<T>(data, OrderType, t);
        }

        public override JioServiceResponse CreateServiceRequest(JioServiceRequestViewModel jioServiceRequestModel)
        {
            JioCustomer custInfo = this.GetJioCustomerByJioNumber(jioServiceRequestModel.JioNumber);
            IList<JioProduct> products = this.GetJioProductsForJioCustomer(custInfo);
            JioServiceResponse serviceRequest = this.GenerateServiceRequest(jioServiceRequestModel.JioNumber, custInfo, products.FirstOrDefault(),
                jioServiceRequestModel.Category, jioServiceRequestModel.SubCategory, jioServiceRequestModel.SubSubCategory, jioServiceRequestModel.Description,jioServiceRequestModel.Reason);
            return serviceRequest;
        }
    }
}
