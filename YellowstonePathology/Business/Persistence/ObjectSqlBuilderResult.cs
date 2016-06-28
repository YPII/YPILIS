using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public class ObjectSqlBuilderResult
    {
        private string m_SqlCommandText;
        private Queue<Type> m_ObjectTypes;

        public ObjectSqlBuilderResult()
        {
            this.m_ObjectTypes = new Queue<Type>();
        }

        public string SqlCommandText
        {
            get { return this.m_SqlCommandText; }
            set { this.m_SqlCommandText = value; }
        }

        public Queue<Type> ObjectTypes
        {
            get { return this.m_ObjectTypes; }
            set { this.m_ObjectTypes = value; }
        }
    }
}
