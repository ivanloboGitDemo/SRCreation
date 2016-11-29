//using RelianceSRCreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocoBuzz.API.BusinessModels;
using LocoBuzz.API.BusinessModels.Models;
using LocoBuzz.API.Business.Interfaces;
using LocoBuzz.API.Business;
using LocoBuzz.API.Common.Helpers;
using LocoBuzz.API.Services.Controllers;
using System.Net.Http;
namespace SRTester
{
    class Program
    {
        static string MatrixDim = string.Empty;
        static double MatrixRowDim;
        static double MatrixColDim;
        static string centerOfMatrix;
        static void Main(string[] args)
        {
            MatrixDim = "3,3";
            
            //ISimWrapper simwrapper = new SimWrapper();
            //var re = simwrapper.GetOrderStatus<CustomerOrderDetails>("12345656");
            // TestRecieveUser();
            // string jioNumber = "+917021136401";

            //IList<JioCategory> jioCategory = JioFactorySingleton.Instance.JioFactory.GetJioCategories();
            //JioCustomerOrderDetails jioCustOrderDetails=default(JioCustomerOrderDetails);
            //jioCustOrderDetails = JioFactorySingleton.Instance.JioServiceFacade.SearchKnowledgeBase<Jio>(jioNumber,LocoBuzz.API.Common.Constants.OrderType.MOBILENUMBER,jioCustOrderDetails);

            //JioChangeEmail jioChangeEmail = default(JioChangeEmail);
            //jioChangeEmail = JioFactorySingleton.Instance.JioFactoryMgr.SimService.ChangeEmail<JioChangeEmail>("+919763466464","Muttu@ril.com", LocoBuzz.API.Common.Constants.OrderType.MOBILENUMBER, jioChangeEmail as JioChangeEmail);
            //  JioRetrieveServiceResponse jioServieResp = default(JioRetrieveServiceResponse);
            //string apiPath = "http:/localhost:53195/api/JioController/RetrieveServiceRequest/SR000003KVUK";
            //  JioContr
            //  var jioserviceresponse = NewTesteter<JioRetrieveServiceResponse>(jioServieResp, apiPath);

            // var bln = ValidateJioUser()

            //var wrapper = new SRWrapper();

            //var cust = wrapper.GetJioCustomerByJioNumber(jioNumber);
            //var products = wrapper.GetJioProductsForJioCustomer(cust);

            //var cats = wrapper.GetJioCategories();
            //cats.ForEach(w => Console.WriteLine(w.Name));

            //var subCats = wrapper.GetJioSubCategories(cats[0]);
            //subCats.ForEach(w => Console.WriteLine(w.Name));

            //var subsubCats = wrapper.GetJioSubSubCategories(subCats[0]);
            //subsubCats.ForEach(w => Console.WriteLine(w.Name));

            //var serviceRequest = wrapper.GenerateServiceRequest(jioNumber, cust, products[0],
            //    cats[0], subCats[0], subsubCats[0], "testing desc", "testing reason");

            for (int i = 0; i < MatrixDim.Length; i++)
            {
                if (MatrixRowDim == 0 && !MatrixDim[i].Equals(","))
                    MatrixRowDim = int.Parse(MatrixDim[i].ToString());
                else if(! MatrixDim[i].ToString().Equals(","))
                    MatrixColDim = int.Parse(MatrixDim[i].ToString());
            }

            double rowCenter = Math.Round(MatrixRowDim / 2);
            double colCenter = Math.Round(MatrixColDim / 2);
            

            string path = "1,1 1,2 1,3 2,3 3,3 3,2 3,1 2,1 1,1";
            string paintposition = "3,3";
            TestRobberPath(path, paintposition,int.Parse(rowCenter.ToString()),int.Parse(colCenter.ToString()));
        } 

        protected static bool TestRobberPath(string robberspath, string paintposition,int rCenter, int cCenter)
        {
            string[] rPath = robberspath.Split(new char[0]);
            for (int camrounds = 0; camrounds < 4; camrounds++)
            {
                for (int rtraverse = 0; rtraverse < rCenter; rtraverse++)
                {
                    for (int ctraverse = 0; ctraverse < cCenter; ctraverse++)
                    {
                        if ((camrounds / 2 == 0 && camrounds % 2 <= 1)) 
                        {

                        }
                    }
                }
            }
            return true;
        }

        private async Task DoWork(int a)
        {
            await Task.Yield();
            int i = a * a;
            await GetFile(i);
        }

        private async Task GetFile(int a)
        {
            await Task.Yield();
            //var folder = 
            ///await .GetFileAsync(a.ToString() + ".txt");
        }

        protected static void TestRecieveUser()
        {
            // var wrapper = new SRWrapper();
            //wrapper.ValidateJioUser("SR000003KVTW", "SR");


        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="apiPath"></param>
        /// <param name=""></param>
        /// <returns></returns>
        //public static T NewTesteter<T>(T t,string apiPath) where T : class,new()
        //{
        //    JioController jioController = new JioController();
        //   // var reservc = jioController.RetrieveServiceRequest("8655394156");
        //}
    }

}
