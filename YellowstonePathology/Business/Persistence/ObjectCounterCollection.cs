using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Persistence
{
    public class ObjectCounterCollection : ObservableCollection<ObjectCounter>
    {
        public ObjectCounterCollection()
        { }

        public void Update(object objectToCount)
        {
            if (this.Exists(objectToCount) == true)
            {
                ObjectCounter objectCounter = this.Get(objectToCount);
                objectCounter.Update(objectToCount);
            }
            else
            {
                ObjectCounter objectCounter = new ObjectCounter(objectToCount.GetType());
                this.Add(objectCounter);
            }
        }

        private bool Exists(object objectToFind)
        {
            bool result = false;
            foreach (ObjectCounter objectCounter in this)
            {
                if (objectCounter.ObjectType == objectToFind.GetType())
                {
                    result = true;
                    break;
                }
            }
            return result;
       }

        private ObjectCounter Get(object objectToFind)
        {
            ObjectCounter result = null;
            foreach (ObjectCounter objectCounter in this)
            {
                if (objectCounter.ObjectType == objectToFind.GetType())
                {
                    result = objectCounter;
                    break;
                }
            }
            return result;
        }
    }
}
