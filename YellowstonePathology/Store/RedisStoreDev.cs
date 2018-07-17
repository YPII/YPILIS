using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace YellowstonePathology.Store
{
    public class RedisStoreDev : RedisStore
    {               
        public RedisStoreDev()
        {
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.Lock, 0, RedisServerDev.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.CPTCode, 1, RedisServerDev.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.DictationTemplate, 2, RedisServerDev.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.Stain, 3, RedisServerDev.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.ICDCode, 4, RedisServerDev.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.Specimen, 5, RedisServerDev.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.VantageSlide, 6, RedisServerDev.Instance));
            this.m_Databases.Add(new RedisDB(AppDBNameEnum.BozemanBlockCount, 7, RedisServerDev.Instance));
            //this.m_Databases.Add(new RedisDB(AppDBNameEnum.EmbeddingScan, 8, RedisServerDev.Instance));
        }
    }
}
