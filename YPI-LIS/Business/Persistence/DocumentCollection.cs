using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Persistence
{
    public class DocumentCollection : ObservableCollection<Document>
    {
        public bool DoIHaveTheLock(string masterAccessionNo)
        {
            bool result = false;
            foreach(Document document in this)
            {
                if(document.Value is YellowstonePathology.Business.Test.AccessionOrder)
                {                    
                    YellowstonePathology.Business.Test.AccessionOrder accessionOrder = (YellowstonePathology.Business.Test.AccessionOrder)document.Value;
                    if(accessionOrder.MasterAccessionNo == masterAccessionNo)
                    {
                        result = accessionOrder.AccessionLock.IsLockAquiredByMe;
                        break;
                    }                    
                }
            }

            return result;
        }

        public void Remove(DocumentId documentId)
        {
            for(int i=0; i< this.Count; i++)
            {
                Document document = this[i];
                if(documentId.Type.FullName == document.Type.FullName &&
                    documentId.Key.ToString() == document.Key.ToString())
                {
                    this.Remove(document);
                    break;
                }
            }
        }

        public bool HasMultipleSameAO(string masterAccessionNo)
        {
            bool result = false;
            int count = 0;
            foreach(Document document in this)
            {
                if(document.Key.ToString() == masterAccessionNo)
                {
                    count += 1;
                }
            }
            if (count > 1) result = true;
            return result;
        }

        public bool Exists(string key, object writer)
        {
            bool result = false;
            foreach(Document doc in this)
            {
                if(doc.Key.ToString() == key && doc.WriterExists(writer) == true)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool Exists(string key)
        {
            bool result = false;
            foreach (Document doc in this)
            {
                if (doc.Key.ToString() == key)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public Document Get(string key)
        {
            Document result = null;
            foreach (Document doc in this)
            {
                if (doc.Key.ToString() == key)
                {
                    result = doc;
                    break;
                }
            }
            return result;
        }
    }
}
