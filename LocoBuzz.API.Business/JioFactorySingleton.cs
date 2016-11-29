using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocoBuzz.API.Business.Interfaces;
using System.Threading;

namespace LocoBuzz.API.Business
{
    public sealed class JioFactorySingleton
    {
         protected IJioManagerFactory JioFactoryMgr { get; set; }
        //public IJioFactory JioFactory { get; set; }

        //public ISRService SrService { get; set; }

        //public ICustomerService CustService { get; set; }

        //public ISimService SimService { get; set; }
        private ReaderWriterLockSlim synclock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

        public JioFactoryBase JioServiceFacade { get; set; }

        private static readonly Lazy<JioFactorySingleton> lazy =
            new Lazy<JioFactorySingleton>(() => new JioFactorySingleton());

        public static JioFactorySingleton Instance { get { return lazy.Value; } }

        private JioFactorySingleton()
        {
            if (JioServiceFacade == null)
            {
                JioFactoryMgr = new JioFactoryManager(new SRService(), new CustomerService(), new SimService());
                JioServiceFacade = new JioServiceFacade(JioFactoryMgr);
            }
        }
    }
}
