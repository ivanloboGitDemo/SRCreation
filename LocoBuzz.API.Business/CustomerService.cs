using LocoBuzz.API.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocoBuzz.API.BusinessModels.Models;
using LocoBuzz.API.Common.Constants;

namespace LocoBuzz.API.Business
{
    public class CustomerService : ICustomerService
    {
        public List<JioProduct> GetJioProductsForJioCustomer(JioCustomer Customer)
        {
            try
            {
                //if (Constants.Constants.DEBUG)
                //{
                //    string text = File.ReadAllText("products.json");
                //    return RestSharp.SimpleJson.DeserializeObject<List<JioProduct>>(text);
                //}
                //else
                //{
                var products = new List<JioProduct>();

                var configurationClient = new CustomerConfigurationInquiry.CustomerConfigurationInquiryV0dot6Client();
                var individual = new CustomerConfigurationInquiry.Individual();
                var salesChannel = new CustomerConfigurationInquiry.SalesChannel();
                var configRequest = new CustomerConfigurationInquiry.RetrieveCustomerServiceConfigurationRequest();
                individual.partyId = Customer.PartyID;
                salesChannel.id = JioConstants.SALESCHANNEL;

                configRequest.customer = individual;
                configRequest.salesChannel = salesChannel;

                var configResponse = configurationClient.retrieveCustomerServiceConfiguration(configRequest);
                if (configResponse != null && configResponse.customer != null && configResponse.customer.CustomerAccount != null)

                    foreach (var item in configResponse.customer.Product)
                    {
                        products.Add(new JioProduct()
                        {
                            ProductName = item.name,
                            ProductCode = item.id,
                            ProductType = item.Identifier[0].value
                        });
                    }
                return products;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JioCustomer GetJioCustomerByJioNumber(string JioNumber)
        {
            try
            {
                //if (Constants.Constants.DEBUG)
                //{
                //    string text = File.ReadAllText("customer.json");
                //    return RestSharp.SimpleJson.DeserializeObject<JioCustomer>(text);
                //}
                //else
                //{
                JioCustomer jCustomer = null;

                var customerRequest = new CustomerProfileManagement.FindCustomerProfileRequest();
                var customer = new CustomerProfileManagement.Individual();
                customer.mobileNumber = JioNumber; //"+912244758845";
                customerRequest.customer = new CustomerProfileManagement.Individual[1];
                customerRequest.customer[0] = customer;

                var customerClient = new CustomerProfileManagement.CustomerProfileManagementV3dot8Client();
                var customerResult = customerClient.findCustomerProfile(customerRequest);

                var result = customerResult.customer.LastOrDefault();

                jCustomer = new JioCustomer();
                jCustomer.AadhaarCard = string.Empty;
                jCustomer.Address = (result.permanentAddress != null) ? result.permanentAddress.ToString() : "";
                jCustomer.AlternateNumber = result.alternateNumber;
                jCustomer.EmailID = result.emailId;

                jCustomer.FirstName = result.name.firstName;
                jCustomer.LastName = result.name.lastName;
                jCustomer.MiddleName = result.name.middleName;
                jCustomer.PANCardNo = (result.PanIdentification != null) ? result.PanIdentification.number : "";
                jCustomer.PartyID = result.partyId;
                jCustomer.PassportNo = string.Empty;
                jCustomer.Title = result.jobTitle;

                //var sCustomer = RestSharp.SimpleJson.SerializeObject(jCustomer);
                //File.WriteAllText("customer.json", sCustomer);

                return jCustomer;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
