using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace YellowstonePathology.Store
{
    public class RedisStoreProd : RedisStore
    {               
        public RedisStoreProd()
        {
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.Lock, 0, RedisServerProd1.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.CPTCode, 1, RedisServerProd1.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.DictationTemplate, 2, RedisServerProd1.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.Stain, 3, RedisServerProd1.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.ICDCode, 4, RedisServerProd1.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.Specimen, 5, RedisServerProd1.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.VantageSlide, 6, RedisServerProd1.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.BozemanBlockCount, 7, RedisServerProd1.Instance));
        }
    }
}
