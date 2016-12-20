using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class SqlCommandQueues
    {
        private Queue<MySqlCommand> m_InsertCmdQueue;
        private Dictionary<string, MySqlCommand> m_InsertCmdLastQueue;

        public SqlCommandQueues()
        {
            this.m_InsertCmdQueue = new Queue<MySqlCommand>();
            this.m_InsertCmdLastQueue = new Dictionary<string, MySqlCommand>();
        }

        public void MoveLastInsertsIntoInsertCmdQueue()
        {
            foreach(KeyValuePair<string, MySqlCommand> value in this.m_InsertCmdLastQueue)
            {                
                this.InsertedCmdQueue.Enqueue(value.Value);
            }
            this.m_InsertCmdLastQueue.Clear();
        }

        public Queue<MySqlCommand> InsertedCmdQueue
        {
            get { return this.m_InsertCmdQueue; }
        }

        public Dictionary<string, MySqlCommand> InsertedCmdLastQueue
        {
            get { return this.m_InsertCmdLastQueue; }
        }
    }
}
