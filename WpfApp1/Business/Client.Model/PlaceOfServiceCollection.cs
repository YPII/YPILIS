using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Client.Model
{
    public class PlaceOfServiceCollection : ObservableCollection<PlaceOfService>
    {
        public PlaceOfServiceCollection()
        {
            this.Add(new PlaceOfService("11", "Office"));
            this.Add(new PlaceOfService("10", "Unassigned"));
            this.Add(new PlaceOfService("19", "Off Campus - Outpatient Hospital"));
            this.Add(new PlaceOfService("21", "Inpatient Hospital"));
            this.Add(new PlaceOfService("22", "On Campus - Outpatient Hospital"));
            this.Add(new PlaceOfService("23", "Emergency Room - Hospital"));
            this.Add(new PlaceOfService("24", "Ambulatory Surgical Center"));            
        }
    }
}
