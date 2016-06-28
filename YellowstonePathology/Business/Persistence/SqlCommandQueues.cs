using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class SqlCommandQueues
    {
        private Queue<SqlCommand> m_InsertCmdQueue;
        private Dictionary<string, SqlCommand> m_InsertCmdLastQueue;

        public SqlCommandQueues()
        {
            this.m_InsertCmdQueue = new Queue<SqlCommand>();
            this.m_InsertCmdLastQueue = new Dictionary<string, SqlCommand>();
        }

        public void MoveLastInsertsIntoInsertCmdQueue()
        {
            foreach(KeyValuePair<string, SqlCommand> value in this.m_InsertCmdLastQueue)
            {                
                this.InsertedCmdQueue.Enqueue(value.Value);
            }
            this.m_InsertCmdLastQueue.Clear();
        }

        public Queue<SqlCommand> InsertedCmdQueue
        {
            get { return this.m_InsertCmdQueue; }
        }

        public Dictionary<string, SqlCommand> InsertedCmdLastQueue
        {
            get { return this.m_InsertCmdLastQueue; }
        }
    }
}
