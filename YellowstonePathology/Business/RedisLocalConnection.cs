﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business
{
    public class RedisLocalConnection : RedisConnection
    {
        public RedisLocalConnection(RedisDatabaseEnum redisDb) : base("localhost", "6379", redisDb)
        { }
    }
}
