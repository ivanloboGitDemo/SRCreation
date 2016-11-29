//using RelianceSRCreation.Constants;
using RelianceSRCreation.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocoBuzz.API.Common.Helpers;
using LocoBuzz.API.Common.Constants;

namespace RelianceSRCreation
{
    public class SRWrapper
    {
        public List<JioCategory> GetJioCategories()
        {
            if (Constants.Constants.DEBUG)
            {
                string text = File.ReadAllText("categories.json");
                return RestSharp.SimpleJson.DeserializeObject<List<JioCategory>>(text);
            }
            else
            {
                List<JioCategory> cats = new List<JioCategory>();

                var lookupVal = new MDMReferenceData.LookUpValue();
                lookupVal.lovType = "CUSTOMERPROBLEMCATG";

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

                //var sCats = RestSharp.SimpleJson.SerializeObject(cats);
                //File.WriteAllText("categories.json", sCats);

                return cats;
            }
            
            
        }

        public List<JioSubCategory> GetJioSubCategories(JioCategory Category)
        {
            if (Constants.Constants.DEBUG)
            {
                string text = File.ReadAllText("subcategories.json");
                return RestSharp.SimpleJson.DeserializeObject<List<JioSubCategory>>(text);
            }
            else
            {
                var subCats = new List<JioSubCategory>();

                var lookupHierarchy = new MDMReferenceData.LookupHierarchicalValueRequest();
                lookupHierarchy.parentLookupValue = new MDMReferenceData.LookUpValue();
                lookupHierarchy.parentLookupValue.lovType = "CUSTOMERPROBLEMCATG";
                lookupHierarchy.parentLookupValue.lovCode = Category.CategoryCode;
                lookupHierarchy.lookupValue = new MDMReferenceData.LookUpValue();
                lookupHierarchy.lookupValue.lovType = "CUSTOMERPROBLEMSUBCATG";

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

                //var ssubCats = RestSharp.SimpleJson.SerializeObject(subCats);
                //File.WriteAllText("subcategories.json", ssubCats);

                return subCats;
            }
        }

        public List<JioSubSubCategory> GetJioSubSubCategories(JioSubCategory SubCategory)
        {
            if (Constants.Constants.DEBUG)
            {
                string text = File.ReadAllText("subsubcategories.json");
                return RestSharp.SimpleJson.DeserializeObject<List<JioSubSubCategory>>(text);
            }
            else
            {
                var subCats = new List<JioSubSubCategory>();

                var lookupHierarchy = new MDMReferenceData.LookupHierarchicalValueRequest();
                lookupHierarchy.parentLookupValue = new MDMReferenceData.LookUpValue();
                lookupHierarchy.parentLookupValue.lovType = "CUSTOMERPROBLEMSUBCATG";
                lookupHierarchy.parentLookupValue.lovCode = SubCategory.SubCategoryCode;
                lookupHierarchy.lookupValue = new MDMReferenceData.LookUpValue();
                lookupHierarchy.lookupValue.lovType = "CUSTOMERPROBLEMSUB_SUBCATG";

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


                //var ssubCats = RestSharp.SimpleJson.SerializeObject(subCats);
                //File.WriteAllText("subsubcategories.json", ssubCats);

                return subCats;
            }
        }

        public JioCustomer GetJioCustomerByJioNumber(String JioNumber)
        {
            try
            {
                if (Constants.Constants.DEBUG)
                {
                    string text = File.ReadAllText("customer.json");
                    return RestSharp.SimpleJson.DeserializeObject<JioCustomer>(text);
                }
                else
                {
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
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
       }

        public List<JioProduct> GetJioProductsForJioCustomer(JioCustomer Customer)
        {
            try {
                    if (Constants.Constants.DEBUG)
                    {
                        string text = File.ReadAllText("products.json");
                        return RestSharp.SimpleJson.DeserializeObject<List<JioProduct>>(text);
                    }
                    else
                    {
                        var products = new List<JioProduct>();

                        var configurationClient = new CustomerConfigurationInquiry.CustomerConfigurationInquiryV0dot6Client();
                        var individual = new CustomerConfigurationInquiry.Individual();
                        var salesChannel = new CustomerConfigurationInquiry.SalesChannel();
                        var configRequest = new CustomerConfigurationInquiry.RetrieveCustomerServiceConfigurationRequest();
                        individual.partyId = Customer.PartyID;
                        salesChannel.id = Constants.Constants.SALESCHANNEL;

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
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public JioServiceRequest GenerateServiceRequest(String JioNumber,
            JioCustomer Customer, 
            JioProduct Product, 
            JioCategory Category,
            JioSubCategory SubCategory,
            JioSubSubCategory SubSubCategory,
            String Description,
            String Reason)
        {
            try
            {
                if (Constants.Constants.DEBUG)
                {
                    string text = File.ReadAllText("sr.json");
                    return RestSharp.SimpleJson.DeserializeObject<JioServiceRequest>(text);
                }
                else
                {
                    var serviceRequest = new CustomerProblemManagement.CreateCustomerProblemRequest();

                    serviceRequest.customer = new CustomerProblemManagement.Individual() { partyId = Customer.PartyID };
                    serviceRequest.identifier = new CustomerProblemManagement.Identifier() { value = Product.ProductType };
                    serviceRequest.salesChannel = new CustomerProblemManagement.SalesChannel() { id = Constants.Constants.SALESCHANNEL };
                    serviceRequest.product = new CustomerProblemManagement.Product() { id = Product.ProductCode, name = Product.ProductName };
                    serviceRequest.customerProblem = new CustomerProblemManagement.CustomerProblem()
                    {
                        description = Description, 
                        category = Category.CategoryCode,
                        reason = Reason,
                        subCategory = SubCategory.SubCategoryCode,
                        subSubCategory = SubSubCategory.SubSubCategoryCode,
                    };

                    var client = new CustomerProblemManagement.CustomerProblemManagementV2dot5Client();
                    var result = client.createCustomerProblem(serviceRequest);
                    
                    var sr = new JioServiceRequest();

                    sr.SRID = result.customerProblem.referenceNumber;
                    sr.CategoryID = Category.CategoryCode;
                    sr.CategoryName = Category.Name;
                    sr.ChannelID = Constants.Constants.SALESCHANNEL;
                    sr.JioNumber = JioNumber;
                    sr.PartyID = Customer.PartyID;
                    sr.Remarks = Reason + "\n" + Description;
                    sr.Status = result.customerProblem.activityStatus;
                    sr.SubCategoryID = SubCategory.SubCategoryCode;
                    sr.SubCategoryName = SubCategory.Name;
                    sr.SubSubCategoryID = SubSubCategory.SubSubCategoryCode;
                    sr.SubSubCategoryName = SubSubCategory.Name;

                    return sr;
                }
            }
            catch(Exception ex)
            {
            throw ex;
            }
       }

        public JioRetrieveServiceResponse RetrieveServiceRequest(string srReferenceNo)
        {
            var clientRequest = new CustomerProblemManagement.retrieveCustomerProblemRequest();
            clientRequest.customerProblem = new CustomerProblemManagement.CustomerProblem()
            {
                referenceNumber = srReferenceNo
            };
            var client = new CustomerProblemManagement.CustomerProblemManagementV2dot5Client();
            var retrieveProblem = client.retrieveCustomerProblem(clientRequest);
            JioRetrieveServiceResponse response = default(JioRetrieveServiceResponse);
            if(retrieveProblem != null && retrieveProblem.customerProblem != null)
            {
                response = new JioRetrieveServiceResponse()
                {
                    PartyId = retrieveProblem.customerProblem.Customer != null ? retrieveProblem.customerProblem.Customer.partyId : string.Empty,    
                    Status = retrieveProblem.customerProblem.statusDescription,
                    ResolutionDate = retrieveProblem.customerProblem.TroubleTicket != null ? retrieveProblem.customerProblem.TroubleTicket.serviceRestoredDate : (DateTime?)null
                };
            }
            return response;
        }

        public JioListServiceResponse ListServiceRequest(string partyID)
        {
            //var clientRequest = new CustomerProblemManagement.listCustomerProblemRequest();
            //clientRequest.customer.partyId = partyID;
            //var client = new CustomerProblemManagement.CustomerProblemManagementV2dot5Client();
            //var listProblem = client.listCustomerProblem(clientRequest);
            //List<JioListServiceResponse> response = default(List<JioListServiceResponse>);
            //if (listProblem != null && listProblem.customerProblem != null)
            //{
            //    response.ForEach((x) =>   
                        
            //        )
            //}
            return null;
        }

        public bool ValidateJioUser(string Number, string Jtype)
        {
            try
            {
                var strType = Jtype.ToEnum<Constants.JioType>();
                
                if (strType == Constants.JioType.JIO)
                {
                    if (string.IsNullOrEmpty(GetJioCustomerByJioNumber(Number).PartyID))
                        return false;
                }
                else if (strType == Constants.JioType.SR)
                {
                    if (string.IsNullOrEmpty(RetrieveServiceRequest(Number).PartyId))
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
