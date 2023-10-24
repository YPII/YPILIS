using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class MedicareCodeCollection : ObservableCollection<CptCode>
    {
        public bool IsMedicareCode(string cptCode)
        {
            bool result = false;
            foreach (CptCode item in this)
            {
                if (item.Code == cptCode)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public static MedicareCodeCollection GetAll()
        {
            MedicareCodeCollection result = new MedicareCodeCollection();
            result.Add(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("G0123", null));
            result.Add(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("G0124", null));
            result.Add(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("G0145", null));
            result.Add(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("G0461", null));
            result.Add(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("G0462", null));
            

            return result;
        }
    }
}
