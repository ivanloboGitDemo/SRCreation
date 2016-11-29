using LocoBuzz.API.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocoBuzz.API.Common.Helpers;
using LocoBuzz.API.Common.Constants;
using LocoBuzz.API.BusinessModels.Models;

namespace LocoBuzz.API.Business
{
    public abstract class JioFactoryBase : IJioFactory,ISimService,ISRService,ICustomerService
    {
        private IJioManagerFactory _jioFactoryManager;
        public JioFactoryBase() { }

        public JioFactoryBase(IJioManagerFactory jioFactoryManager)
        {
            _jioFactoryManager = jioFactoryManager;
        }

        public virtual List<JioCategory> GetJioCategories()
        {
            List<JioCategory> cats = new List<JioCategory>();

            var lookupVal = new MDMReferenceData.LookUpValue();
            lookupVal.lovType = JioConstants.CATEGORYCONSTANT;

            var req = new MDMReferenceData.LookupValueRequest();
            req.lookupValue = lookupVal;

            var client = new MDMReferenceData.ReferenceDataInquiryV1dot2Client();
            var response = client.lookupValue(req);

            var total = response.dataRecord.totalRecords;
            var pageSize = response.dataRecord.pagingSize;


            foreach (var item in response.valueList)
            {
                cats.Add(new JioCategory()
                {
                    Name = item.lovName,
                    CategoryCode = item.lovCode,
                    Description = item.lovDesc
                });
            }

            return cats;
        }


        public virtual List<JioSubCategory> GetJioSubCategories(JioCategory Category)
        {
            var subCats = new List<JioSubCategory>();

            var lookupHierarchy = new MDMReferenceData.LookupHierarchicalValueRequest();
            lookupHierarchy.parentLookupValue = new MDMReferenceData.LookUpValue();
            lookupHierarchy.parentLookupValue.lovType = JioConstants.CATEGORYCONSTANT;
            lookupHierarchy.parentLookupValue.lovCode = Category.CategoryCode;
            lookupHierarchy.lookupValue = new MDMReferenceData.LookUpValue();
            lookupHierarchy.lookupValue.lovType = JioConstants.SUBCATEGORYCONSTANT;

            var serviceClient = new MDMReferenceData.ReferenceDataInquiryV1dot2Client();
            var result = serviceClient.lookupHierarchicalValue(lookupHierarchy);

            foreach (var item in result.valueList)
            {
                subCats.Add(new JioSubCategory()
                {
                    Name = item.lovName,
                    SubCategoryCode = item.lovCode,
                    Description = item.lovDesc
                });
            }

            return subCats;
        }


        public virtual List<JioSubSubCategory> GetJioSubSubCategories(JioSubCategory SubCategory)
        {
            var subCats = new List<JioSubSubCategory>();

            var lookupHierarchy = new MDMReferenceData.LookupHierarchicalValueRequest();
            lookupHierarchy.parentLookupValue = new MDMReferenceData.LookUpValue();
            lookupHierarchy.parentLookupValue.lovType = JioConstants.SUBCATEGORYCONSTANT;
            lookupHierarchy.parentLookupValue.lovCode = SubCategory.SubCategoryCode;
            lookupHierarchy.lookupValue = new MDMReferenceData.LookUpValue();
            lookupHierarchy.lookupValue.lovType = JioConstants.SUBSUBCATEGORYCONSTANT;

            var serviceClient = new MDMReferenceData.ReferenceDataInquiryV1dot2Client();
            var result = serviceClient.lookupHierarchicalValue(lookupHierarchy);

            foreach (var item in result.valueList)
            {
                subCats.Add(new JioSubSubCategory()
                {
                    Name = item.lovName,
                    SubSubCategoryCode = item.lovCode,
                    Description = item.lovDesc
                });
            }
            
            return subCats;
        }


        public abstract bool ValidateJioUser(string Number, string Jtype);


        public abstract JioListServiceResponse GetSRListByJioNumber(string jioNumber);


        public virtual T GetSimOrderStatus<T>(string data, OrderType OrderType, T t) where T : class, new()
        {
            return _jioFactoryManager.SimService.GetSimOrderStatus<T>(data, OrderType, t);
        }

        public virtual T GetBalanceDetails<T>(string data, OrderType OrderType, T t) where T : class, new()
        {
            return _jioFactoryManager.SimService.GetBalanceDetails<T>(data, OrderType, t);
        }

        public T SearchKnowledgeBase<T>(string data, T t) where T : class, new()
        {
            return _jioFactoryManager.SimService.SearchKnowledgeBase<T>(data, t);
        }

        public virtual T GetJioIdFromMobile<T>(string jioNumber, T t) where T : class, new()
        {
            return _jioFactoryManager.SimService.GetJioIdFromMobile<T>(jioNumber, t);
        }

        public virtual T ChangeEmail<T>(string queryParameter, string email, OrderType OrderType, T t) where T : class, new()
        {
            return _jioFactoryManager.SimService.ChangeEmail<T>(queryParameter, email, OrderType, t);
        }

        public virtual JioRetrieveServiceResponse RetrieveServiceRequest(string srReferenceNo)
        {
            return _jioFactoryManager.SrService.RetrieveServiceRequest(srReferenceNo);
        }

        public virtual IList<JioListServiceResponse> ListServiceRequest(string partyID)
        {
            return _jioFactoryManager.SrService.ListServiceRequest(partyID);
        }

        public virtual JioServiceResponse GenerateServiceRequest(string JioNumber, JioCustomer Customer, JioProduct Product, JioCategory Category, JioSubCategory SubCategory, JioSubSubCategory SubSubCategory, string Description, string Reason)
        {
            return _jioFactoryManager.SrService.GenerateServiceRequest(JioNumber, Customer, Product, Category, SubCategory, SubSubCategory, Description, Reason);
        }

        public virtual List<JioProduct> GetJioProductsForJioCustomer(JioCustomer Customer)
        {
            return _jioFactoryManager.CustService.GetJioProductsForJioCustomer(Customer);
        }

        public virtual JioCustomer GetJioCustomerByJioNumber(string JioNumber)
        {
            return _jioFactoryManager.CustService.GetJioCustomerByJioNumber(JioNumber);
        }

        public virtual JioServiceResponse CreateServiceRequest(JioServiceRequestViewModel jioServiceRequestModel)
        {
            return _jioFactoryManager.SrService.CreateServiceRequest(jioServiceRequestModel);
        }
    }
}
