using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace YellowstonePathology.Store
{
    public class RedisStore
    {        
        protected List<RedisDB> m_Databases;

        public RedisStore()
        {
            this.m_Databases = new List<RedisDB>();
        }

        public RedisDB GetDB(AppDBNameEnum dbName)
        {
            return this.m_Databases.Find(x => x.Name == dbName.ToString());            
        }

        public IServer GetServer(AppDBNameEnum dbName)
        {
            return null;
        }

        public IRedisServer GetRedisServer(AppDBNameEnum dbName)
        {
            RedisDB redisDb = this.m_Databases.Find(x => x.Name == dbName.ToString());
            return redisDb.Server;
        }
    }
}
