﻿using System;
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
        }
    }
}
