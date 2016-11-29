using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LocoBuzz.API.Business.Interfaces;
using LocoBuzz.API.BusinessModels.Models;
using LocoBuzz.API.Business;
using LocoBuzz.API.Services.Filters;

namespace LocoBuzz.API.Services.Controllers
{
    [ControllerExceptionFilterAttribute]
    public class JioController : ApiController
    {
        public IHttpActionResult GetJioCategories()
        {
            IList<JioCategory> jioCategory = JioFactorySingleton.Instance.JioServiceFacade.GetJioCategories();
                if (jioCategory == null)
                      return NotFound();
                
                return Ok<IList<JioCategory>>(jioCategory);
        }

        public IHttpActionResult GetJioSubCategories([FromBody]JioCategory jioCategory)
        {
           
                IList<JioSubCategory> jioSubCategory = JioFactorySingleton.Instance.JioServiceFacade.GetJioSubCategories(jioCategory);
                if (jioSubCategory == null)
                    return NotFound();

                return Ok<IList<JioSubCategory>>(jioSubCategory);
            
        }
        public IHttpActionResult GetSimOrderStatusByMobileNumber([FromUri]string jioMobileNumber)
        {
           
                IList<JioCustomerOrderDetails> jioCustomerOrderDetails = default(List<JioCustomerOrderDetails>);
                jioCustomerOrderDetails = JioFactorySingleton.Instance.JioServiceFacade.GetSimOrderStatus<List<JioCustomerOrderDetails>>(jioMobileNumber, Common.Constants.OrderType.MOBILENUMBER, jioCustomerOrderDetails as List<JioCustomerOrderDetails>);
                if (jioCustomerOrderDetails == null)
                    return NotFound();

                return Ok<IList<JioCustomerOrderDetails>>(jioCustomerOrderDetails);
           
        }
        public IHttpActionResult GetSimOrderStatusByRefNo([FromUri]string refNo)
        {
           
                IList<JioCustomerOrderDetails> jioCustomerOrderDetails = default(List<JioCustomerOrderDetails>);
                jioCustomerOrderDetails = JioFactorySingleton.Instance.JioServiceFacade.GetSimOrderStatus<List<JioCustomerOrderDetails>>(refNo, Common.Constants.OrderType.REFNUMBER, jioCustomerOrderDetails as List<JioCustomerOrderDetails>);
                if (jioCustomerOrderDetails == null)
                    return NotFound();

                return Ok<IList<JioCustomerOrderDetails>>(jioCustomerOrderDetails);
           
        }
        public IHttpActionResult GetSimOrderStatusByJioId([FromUri]string jioID)
        {
           
                IList<JioCustomerOrderDetails> jioCustomerOrderDetails = default(List<JioCustomerOrderDetails>);
                jioCustomerOrderDetails = JioFactorySingleton.Instance.JioServiceFacade.GetSimOrderStatus<List<JioCustomerOrderDetails>>(jioID, Common.Constants.OrderType.MOBILENUMBER, jioCustomerOrderDetails as List<JioCustomerOrderDetails>);
                if (jioCustomerOrderDetails == null)
                    return NotFound();

                return Ok<IList<JioCustomerOrderDetails>>(jioCustomerOrderDetails);
         
        }

        public IHttpActionResult GetEmailByJioId([FromUri]string jioID,[FromUri]string email)
        {
            try
            {
                JioChangeEmail jioChangeEmail = default(JioChangeEmail);
                jioChangeEmail = JioFactorySingleton.Instance.JioServiceFacade.ChangeEmail<JioChangeEmail>(jioID, email, Common.Constants.OrderType.JiOID, jioChangeEmail as JioChangeEmail);
                if (jioChangeEmail == null)
                    return NotFound();

                return Ok<JioChangeEmail>(jioChangeEmail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IHttpActionResult GetEmailByMobileNo([FromUri]string jioMobileNo, [FromUri]string email)
        {
            
                JioChangeEmail jioChangeEmail = default(JioChangeEmail);
                jioChangeEmail = JioFactorySingleton.Instance.JioServiceFacade.ChangeEmail<JioChangeEmail>(jioMobileNo, email, Common.Constants.OrderType.JiOID, jioChangeEmail as JioChangeEmail);
                if (jioChangeEmail == null)
                    return NotFound();

                return Ok<JioChangeEmail>(jioChangeEmail);
            
        }
        public IHttpActionResult GetBalanceSummaryByJioId([FromUri]string jioID)
        {
           
                JioBalanceList jioBalanceSummary = default(JioBalanceList);
                jioBalanceSummary = JioFactorySingleton.Instance.JioServiceFacade.GetBalanceDetails<JioBalanceList>(jioID, Common.Constants.OrderType.JiOID, jioBalanceSummary as JioBalanceList);
                if (jioBalanceSummary == null)
                    return NotFound();

                return Ok<JioBalanceList>(jioBalanceSummary);
            
        }

        [HttpGet]
        public IHttpActionResult ListServiceRequest([FromUri]string partyID)
        {
           
                IList<JioListServiceResponse> jioServiceResponseList = default(List<JioListServiceResponse>);
                jioServiceResponseList = JioFactorySingleton.Instance.JioServiceFacade.ListServiceRequest(partyID);
                if (jioServiceResponseList == null)
                    return NotFound();

                return Ok<IList<JioListServiceResponse>>(jioServiceResponseList);
           
        }

        [HttpGet]
        public IHttpActionResult RetrieveServiceRequest([FromUri]string srReferenceNo)
        {
          
                JioRetrieveServiceResponse jioRetrieveServiceResponse = default(JioRetrieveServiceResponse);
                jioRetrieveServiceResponse = JioFactorySingleton.Instance.JioServiceFacade.RetrieveServiceRequest(srReferenceNo);
                if (jioRetrieveServiceResponse == null)
                    return NotFound();

                return Ok<JioRetrieveServiceResponse>(jioRetrieveServiceResponse);
          
        }


        public IHttpActionResult GetSRListByJioNumber([FromUri]string jioNumber)
        {
            JioListServiceResponse jioListServiceResponse = default(JioListServiceResponse);
            jioListServiceResponse = JioFactorySingleton.Instance.JioServiceFacade.GetSRListByJioNumber(jioNumber);
            if (jioListServiceResponse == null)
                return NotFound();

            return Ok<JioListServiceResponse>(jioListServiceResponse);
        }

        public IHttpActionResult SearchKnowledgeBase([FromUri]string parameter)
        {
            JioKnowledgeBase jioKnowledgeBase = default(JioKnowledgeBase);
            jioKnowledgeBase = JioFactorySingleton.Instance.JioServiceFacade.SearchKnowledgeBase<JioKnowledgeBase>(parameter, jioKnowledgeBase);
            if (jioKnowledgeBase == null)
                return NotFound();

            return Ok<JioKnowledgeBase>(jioKnowledgeBase);
        }

        public IHttpActionResult CreateServiceRequest([FromBody]JioServiceRequestViewModel jioServiceRequestModel)
        {
            JioServiceResponse jioServiceResponse = default(JioServiceResponse);
            jioServiceResponse = JioFactorySingleton.Instance.JioServiceFacade.CreateServiceRequest(jioServiceRequestModel);
            if (jioServiceResponse == null)
                return NotFound();

            return Ok<JioServiceResponse>(jioServiceResponse);
        }

        public IHttpActionResult ValidateJioUser(string Number, string Jtype)
        {
            bool IsValid = JioFactorySingleton.Instance.JioServiceFacade.ValidateJioUser(Number, Jtype);

            return Ok<bool>(IsValid);
        }   

    }
}
 