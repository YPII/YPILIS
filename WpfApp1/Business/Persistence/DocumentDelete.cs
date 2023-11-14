using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace YellowstonePathology.Business.Persistence
{
    public class DocumentDelete : Document
    {
        public DocumentDelete(DocumentId documentId) 
            : base (documentId)
        {
            
        }

        public override YellowstonePathology.Business.Persistence.SubmissionResult Submit()
        {            
            PersistentClass persistentClassAttribute = (PersistentClass)this.m_Type.GetCustomAttributes(typeof(PersistentClass), false).Single();
            SqlCommandSubmitter objectSubmitter = new SqlCommandSubmitter(persistentClassAttribute.Database);
            PropertyInfo keyProperty = this.m_Type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            object keyPropertyValue = keyProperty.GetValue(this.m_Value, null);

            DeleteCommandBuilder deleteCommandBuilder = new DeleteCommandBuilder();
            deleteCommandBuilder.Build(this.m_Value, objectSubmitter.SqlDeleteFirstCommands, objectSubmitter.SqlDeleteCommands);
            return objectSubmitter.SubmitChanges();            
        }

        public override bool IsDirty()
        {
            return false;
        }
    }
}
