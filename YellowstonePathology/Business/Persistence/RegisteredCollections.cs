using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public class RegisteredCollections : Collection<RegisteredCollection>
    {                
        public RegisteredCollections()
        {
            
        }        

        public RegisteredCollection GetRegisteredCollection(object originalCollection)
        {
            RegisteredCollection result = null;
			foreach (RegisteredCollection registeredCollection in this)
			{
				if (originalCollection == registeredCollection.Collection)
				{
					result = registeredCollection;
					break;
				}
			}
            return result;
        }
    }
}
