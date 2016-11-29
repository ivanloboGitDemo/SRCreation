using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocoBuzz.API.Business.Interfaces;

namespace LocoBuzz.API.Business
{
    public class JioFactoryManager : IJioManagerFactory
    {
        public JioFactoryManager(IJioFactory iJioFactory)
        {
            JioFactory = iJioFactory;
        }

        public JioFactoryManager(ISRService srService,ICustomerService custService,ISimService simService)
        {
            this.SrService = srService;
            this.CustService = custService;
            this.SimService = simService;
        }
        public IJioFactory JioFactory
        {
            get;
            set;
        }

        public ISRService SrService
        {
            get;
            set;
        }

        public ICustomerService CustService
        {
            get; 
            set;
        }

        public ISimService SimService
        {
            get;
            set;
        }
    }
}
