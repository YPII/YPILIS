using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Reflection;

namespace YellowstonePathology.Business.Persistence
{
    public class RegisteredObjectCollection : ObservableCollection<RegisteredObject>
    {
        public RegisteredObjectCollection()
        { }

        public RegisteredObject Get(object objectToFind)
        {
			Type objectType = objectToFind.GetType();
			PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
			object objectToFindKey = keyProperty.GetValue(objectToFind, null);
			
			RegisteredObject result = null;
            foreach (RegisteredObject registeredObject in this)
            {
                if(registeredObject.Key.Equals(objectToFindKey) && registeredObject.ValueType == objectType)
                {
                    result = registeredObject;
                    break;
                }
            }

            return result;
        }

        public bool Exists(object objectToFind)
        {
			Type objectType = objectToFind.GetType();
			PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
			object objectToFindKey = keyProperty.GetValue(objectToFind, null);
			
            bool result = false;
            foreach (RegisteredObject registeredObject in this)
            {
                if (registeredObject.Key.Equals(objectToFindKey) && registeredObject.ValueType.Name == objectType.Name)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
        
        public void Register(object objectToRegister, object registeredBy)
        {
        	if(this.Exists(objectToRegister) == true)
        	{
        		RegisteredObject existingRegisteredObject = this.Get(objectToRegister);
                existingRegisteredObject.Value = objectToRegister;

        		if(existingRegisteredObject.RegisteredBy.Contains(registeredBy) == false)
        		{
        			existingRegisteredObject.RegisteredBy.Add(registeredBy);
        		}                
        	}
        	else
        	{
	        	RegisteredObject registeredObject = new RegisteredObject(objectToRegister, registeredBy);
	        	this.Add(registeredObject);
        	}
        }

        public void Deregister(object objectToRegister, object registeredBy)
        {
            if (this.Exists(objectToRegister) == true)
            {
                RegisteredObject existingRegisteredObject = this.Get(objectToRegister);
                if (existingRegisteredObject.RegisteredBy.Contains(registeredBy) == true)
                {
                    existingRegisteredObject.RegisteredBy.Remove(registeredBy);
                }

                if (existingRegisteredObject.RegisteredBy.Count == 0)
                {
                    this.Remove(existingRegisteredObject);
                }
            }
        }

        public void CleanUp(object registeredBy)
        {
        	for (int idx = this.Count - 1; idx > -1; idx--)
            {
        		RegisteredObject registeredObject = this[idx];
        		if(registeredObject.RegisteredBy.Contains(registeredBy))
                {
                    registeredObject.RegisteredBy.Remove(registeredBy);
                }

                if (registeredObject.RegisteredBy.Count == 0)
                {
                    this.RemoveAt(idx);
                }
            }
        }

        public bool IsRegisteredBy(object objectToFind, object registeredBy)
        {
        	bool result = false;
        	if(this.Exists(objectToFind) == true)
        	{
        		RegisteredObject registeredObject = this.Get(objectToFind);
        		if(registeredObject.RegisteredBy.Contains(registeredBy) == true)
        		{
        			result = true;
        		}
        	}
        	return result;
        }
    }
}
