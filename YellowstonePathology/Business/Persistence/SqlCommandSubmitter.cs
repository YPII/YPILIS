using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{	
	public class SqlCommandSubmitter
    {

        private Queue<MySqlCommand> m_SqlInsertCommands;
        private Queue<MySqlCommand> m_SqlInsertLastCommands;
        private Stack<MySqlCommand> m_SqlDeleteFirstCommands;
        private Stack<MySqlCommand> m_SqlDeleteCommands;
        private Queue<MySqlCommand> m_SqlUpdateCommands;
        private string m_ConnectionString;

        public SqlCommandSubmitter(string databaseName)
        {           
            switch (databaseName)
            {
                case "YPIDATA":
                    //this.m_ConnectionString = @"Data Source=TestSQL;Initial Catalog=YPIData;Integrated Security=True";
                    this.m_ConnectionString = YellowstonePathology.Properties.Settings.Default.CurrentConnectionString;
                    break;
                //case "YPILocalData":
                //    this.m_ConnectionString = @"Data Source=.\LIS;Initial Catalog=YPILocalData;Integrated Security=True";
                //    break;
                default:
                    throw new Exception("Database name does not match existing.");
            }

            this.m_SqlInsertCommands = new Queue<MySqlCommand>();
            this.m_SqlInsertLastCommands = new Queue<MySqlCommand>();
            this.m_SqlDeleteFirstCommands = new Stack<MySqlCommand>();
            this.m_SqlDeleteCommands = new Stack<MySqlCommand>();
            this.m_SqlUpdateCommands = new Queue<MySqlCommand>();
        }
		
        public Queue<MySqlCommand> SqlInsertCommands
        {
            get { return this.m_SqlInsertCommands; }
			set { this.m_SqlInsertCommands = value; }
        }

		public Queue<MySqlCommand> SqlInsertLastCommands
        {
            get { return this.m_SqlInsertLastCommands; }
			set { this.m_SqlInsertLastCommands = value; }
        }
		
		public Stack<MySqlCommand> SqlDeleteFirstCommands
        {
            get { return this.m_SqlDeleteFirstCommands; }
			set { this.m_SqlDeleteFirstCommands = value; }
        }

        public Stack<MySqlCommand> SqlDeleteCommands
        {
            get { return this.m_SqlDeleteCommands; }
            set { this.m_SqlDeleteCommands = value; }
        }

		public Queue<MySqlCommand> SqlUpdateCommands
        {
            get { return this.m_SqlUpdateCommands; }
			set { this.m_SqlUpdateCommands = value; }
        }

        public SubmissionResult SubmitChanges()
        {                                                
            SubmissionResult result = new SubmissionResult();
            result.HasUpdateCommands = this.m_SqlUpdateCommands.Count > 0;
            result.HasDeleteFirstCommands = this.m_SqlDeleteFirstCommands.Count > 0;
            result.HasDeleteCommands = this.m_SqlDeleteCommands.Count > 0;
            result.HasInsertCommands = this.m_SqlInsertCommands.Count > 0;
            result.HasInsertLastCommands = this.m_SqlInsertLastCommands.Count > 0;

            if (result.HasUpdateCommands || result.HasDeleteFirstCommands || result.HasDeleteCommands || result.HasInsertCommands || result.HasInsertLastCommands)
            {
                using (MySqlConnection cn = new MySqlConnection(this.m_ConnectionString))
                {
                    cn.Open();

                    MySqlTransaction trans = cn.BeginTransaction();
                    try
                    {
                        this.RunSqlCommands(this.m_SqlUpdateCommands, cn, trans);
                        this.RunSqlCommands(this.m_SqlDeleteFirstCommands, cn, trans);
                        this.RunSqlCommands(this.m_SqlDeleteCommands, cn, trans);
                        this.RunSqlCommands(this.m_SqlInsertCommands, cn, trans);
                        this.RunSqlCommands(this.m_SqlInsertLastCommands, cn, trans);
                        trans.Commit();
                    }
                    catch (Exception ex) //error occurred
                    {
                        trans.Rollback();
                        cn.Close();
                        throw(ex);
                    }
                }
            }
            return result;
        }

        private void RunSqlCommands(Queue<MySqlCommand> sqlCommandQueue, MySqlConnection cn, MySqlTransaction trans)
        {            
            while (sqlCommandQueue.Count != 0)
            {
                MySqlCommand cmd = sqlCommandQueue.Dequeue();
                cmd.Connection = cn;
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
            }
        }

        private void RunSqlCommands(Stack<MySqlCommand> sqlCommandStack, MySqlConnection cn, MySqlTransaction trans)
        {            
            while (sqlCommandStack.Count != 0)
            {
                MySqlCommand cmd = sqlCommandStack.Pop();
                cmd.Connection = cn;
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
            }
        }

        public bool HasNonASCIICharacters()
        {
            bool result = false;
            foreach (MySqlCommand sqlCommand in this.m_SqlUpdateCommands)
            {
                for (int i = 0; i < sqlCommand.CommandText.Length; ++i)
                {
                    char c = sqlCommand.CommandText[i];
                    if (((int)c) > 127)
                    {
                        result = true;
                        break;
                    }
                }  
            }
            return result;
        }

        public bool HasChanges()
        {
            bool result = false;
            if (this.m_SqlUpdateCommands.Count != 0 || 
                this.m_SqlInsertCommands.Count != 0 || 
                this.m_SqlDeleteCommands.Count != 0 ||
                this.m_SqlDeleteFirstCommands.Count != 0 ||
                this.m_SqlInsertLastCommands.Count != 0)
            {
                result = true;                
            }
            return result;
        }

        public void LogCommands()
        {
            foreach(MySqlCommand cmd in this.m_SqlUpdateCommands)
            {
                Console.WriteLine(cmd.CommandText);
            }
        }
    }
}