using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Store
{
    public class AppDataStore
    {
        private static AppDataStore instance = null;
        private static readonly object padlock = new object();

        //private static string MODE = "DEV";
        private static string MODE = "PROD";                

        private RedisStore m_RedisStore;
        private Business.Billing.Model.CptCodeCollection m_CPTCodeCollection;      

        AppDataStore()
        {
            if (MODE == "PROD")
            {
                this.m_RedisStore = new RedisStoreProd();
            }
            else
            {
                this.m_RedisStore = new RedisStoreDev();
            }
        }

        public static AppDataStore Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new AppDataStore();
                    }
                    return instance;
                }
            }
        }

        public void LoadData()
        {
            this.m_CPTCodeCollection = new Business.Billing.Model.CptCodeCollection();
            this.m_CPTCodeCollection.Load();
        }

        public RedisStore RedisStore
        {
            get { return this.m_RedisStore; }
        }        

        public Business.Billing.Model.CptCodeCollection CPTCodeCollection
        {
            get { return this.m_CPTCodeCollection; }
        }

    }
}
