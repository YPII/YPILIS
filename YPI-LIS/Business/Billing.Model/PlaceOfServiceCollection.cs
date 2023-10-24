using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Billing.Model
{
    public class PlaceOfServiceCollection : ObservableCollection<PlaceOfService>
    {
        //101 IP
        //102 OP
        //113 OP (seems like hospital normally IP but is OP)
        //104 OP (seems like hospital normally IP but is OP)

        public PlaceOfServiceCollection()
        {
            this.Add(new PlaceOfService("ABSRK", "11", "OP", "102"));
            this.Add(new PlaceOfService("BIGOB", "11", "OP", "102"));
            this.Add(new PlaceOfService("FCCMED", "11", "OP", "102"));
            this.Add(new PlaceOfService("FPWIIM", "1", "OP", "102"));
            this.Add(new PlaceOfService("FSBW", "11", "102", "102"));
            this.Add(new PlaceOfService("FSDERM", "11", "OP", "102"));
            this.Add(new PlaceOfService("FSHTS", "11", "OP", "102"));
            this.Add(new PlaceOfService("FSIM", "11", "OP", "102"));
            this.Add(new PlaceOfService("FSLRL", "11", "OP", "102"));
            this.Add(new PlaceOfService("FSNSHI", "11", "OP", "102"));
            this.Add(new PlaceOfService("FSURO", "11", "OP", "102"));
            this.Add(new PlaceOfService("FSWGFM", "11", "OP", "102"));
            this.Add(new PlaceOfService("HRDN", "11", "OP", "102"));
            this.Add(new PlaceOfService("US", "11", "OP", "102"));
            this.Add(new PlaceOfService("VBRST", "11", "OP", "102"));

            this.Add(new PlaceOfService("VAP", "21", "IP", "101"));
            this.Add(new PlaceOfService("VICS", "21", "IP", "101"));                        
            this.Add(new PlaceOfService("VMTY", "21", "IP", "101"));
            this.Add(new PlaceOfService("VPAC", "21", "IP", "101"));
            this.Add(new PlaceOfService("V6TO", "21", "IP", "101"));
            this.Add(new PlaceOfService("V3FO", "21", "IP", "101"));

            this.Add(new PlaceOfService("OR", "21", "IP", "101"));
            this.Add(new PlaceOfService("OR", "21", "IP", "104"));
            this.Add(new PlaceOfService("OR", "21", "IP", "113"));  //this most of the time.
            

            this.Add(new PlaceOfService("PERI", "21", "IP", "113"));
            this.Add(new PlaceOfService("VGI", "21", "IP", "113"));
            this.Add(new PlaceOfService("VPERI", "21", "IP", "113"));
            this.Add(new PlaceOfService("VPRE", "21", "IP", "113"));

            this.Add(new PlaceOfService("SPPHII", "21", "IP", "156"));
            this.Add(new PlaceOfService("SPPHII", "21", "IP", "113"));

            this.Add(new PlaceOfService("VLX", "11", "OP", "109"));                        
            this.Add(new PlaceOfService("VED", "23", "OP", "103"));

            this.Add(new PlaceOfService("VOR", "21", "IP", "101"));            
            this.Add(new PlaceOfService("VOR", "21", "IP", "108"));
            this.Add(new PlaceOfService("VOR", "21", "IP", "113"));
        }

        public PlaceOfService Get(string name)
        {
            PlaceOfService result = null;
            foreach(PlaceOfService placeOfService in this)
            {
                if(placeOfService.Name == name)
                {
                    result = placeOfService;
                    break;
                }
            }
            return result;
        }
    }
}
