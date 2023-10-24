using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public class DocumentWriter
    {
        private object m_Writer;
        private WriterScopeEnum m_Scope;

        public DocumentWriter(object writer, WriterScopeEnum scope)
        {
            this.m_Writer = writer;
            this.m_Scope = scope;
        }

        public object Writer
        {
            get { return this.m_Writer; }
        }

        public WriterScopeEnum Scope
        {
            get { return this.m_Scope; }
        }
    }
}
