using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace YellowstonePathology.Store
{
    public interface IRedisServer
    {
        IDatabase GetDB(int databaseNumber);
    }
}
