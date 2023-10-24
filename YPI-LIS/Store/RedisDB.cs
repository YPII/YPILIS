using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace YellowstonePathology.Store
{
    public class RedisDB
    {
        protected string m_Name;
        protected int m_Index;
        protected IRedisServer m_Server;
        protected IDatabase m_DataBase;

        public RedisDB(AppDBNameEnum dbNameEnum, int index, IRedisServer server)
        {
            this.m_Name = dbNameEnum.ToString();
            this.m_Index = index;
            this.m_Server = server;
            this.m_DataBase = server.GetDB(index);
        }

        public string Name
        {
            get { return this.m_Name; }
        }

        public int Index
        {
            get { return this.m_Index; }
        }

        public IRedisServer Server
        {
            get { return this.m_Server; }
        }    
        
        public IDatabase DataBase
        {
            get { return this.m_DataBase; }
        }    

        public string[] GetAllJSONKeys()
        {
            string script = "local data = redis.call('keys', '*') " +
                            "local result = {} " +
                            "for i, item in ipairs(data) do " +
                            "result[i] = redis.call('json.get', data[i]) " +
                            "end " +
                            "return result ";
            LuaScript prepared = LuaScript.Prepare(script);
            return (string[])this.m_DataBase.ScriptEvaluate(prepared);
        }

        public string[] GetAllJSONKeys(string arg)
        {
            string script = "local data = redis.call('keys', '" + arg + "*') " +
                            "local result = {} " +
                            "for i, item in ipairs(data) do " +
                            "result[i] = redis.call('json.get', data[i]) " +
                            "end " +
                            "return result ";
            LuaScript prepared = LuaScript.Prepare(script);
            return (string[])this.m_DataBase.ScriptEvaluate(prepared);
        }

        public string[] GetAllJSONKeysBySlideId(string value)
        {
            string script = "local data = redis.call('keys', '*" + value + "') " +
                            "local result = {} " +
                            "for i, item in ipairs(data) do " +
                            "result[i] = redis.call('json.get', data[i])  end " +
                            "return result ";
            LuaScript prepared = LuaScript.Prepare(script);
            return (string[])this.m_DataBase.ScriptEvaluate(prepared);
        }

        public RedisResult[] GetAllHashes()
        {
            string script = "local data = redis.call('keys', '*') " +
                            "local result = {} " +
                            "for i, item in ipairs(data) do " +
                            "result[i] = redis.call('HGetAll', data[i]) " +
                            "end " +
                            "return result ";
            LuaScript prepared = LuaScript.Prepare(script);
            return (RedisResult[])this.m_DataBase.ScriptEvaluate(prepared);            
        }

        public string[] GetAllStrings()
        {
            string script = "local data = redis.call('keys', '*') " +
                            "local result = {} " +
                            "for i, item in ipairs(data) do " +
                            "result[i] = data[i] .. ' ' .. redis.call('Get', data[i]) " +
                            "end " +
                            "return result ";
            LuaScript prepared = LuaScript.Prepare(script);
            return (string[])this.m_DataBase.ScriptEvaluate(prepared);
        }
    }
}
