using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
    public class UniversalServiceCollection : ObservableCollection<UniversalService>
    {
        public UniversalServiceCollection()
        {            					            
            
        }

        public static UniversalServiceCollection GetAll()
        {
            UniversalServiceCollection result = new UniversalServiceCollection();
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceJAK2());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceCTGC());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceHERAMP());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceHRHPVTEST());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceBRAFMANAL());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServicePNHHS());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceHPV16());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceKRASBRAF());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceFVLMUT());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServicePROMUT());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMTHFRM());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceAUTOPSY());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceBCELLCLON());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceKRAS());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceTHINPREP());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServicePLATEAB());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceRETICPLATE());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceTHROMPRO());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceSTEMCE());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceEPPR());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceEGFR());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceYPI());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceSurgicalPathology());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceTRCHMNAA());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceCYTO());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServicePathSummary());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceHPV1618GEN());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceFLOWYPI());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
            result.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceCFYPI());
            return result;
        }

        public bool Exists(string universalServiceId)
        {
            bool result = false;
            foreach (UniversalService item in this)
            {
                if (item.UniversalServiceId.ToUpper() == universalServiceId.ToUpper())
                {
                    result = true;
                    break;
                }
            }

            if (result == false) throw new Exception("The universal services id cannot be null");
            return result;
        }

        public UniversalService GetByUniversalServiceId(string universalServiceId)
        {
            UniversalService result = null;
            foreach(UniversalService item in this)
            {
                if (item.UniversalServiceId.ToUpper() == universalServiceId.ToUpper())
                {
                    result = item;
                    break;
                }
            }

            if (result == null) result = new UniversalServiceDefinitions.UniversalServiceNone();
            return result;
        }

        public UniversalService GetByApplicationName(UniversalServiceApplicationNameEnum applicationName)
        {
            UniversalService result = null;
            foreach (UniversalService item in this)
            {
                if (item.ApplicationName == applicationName)
                {
                    result = item;
                    break;
                }
            }

            if (result == null) result = new UniversalServiceDefinitions.UniversalServiceNone();

            return result;
        }
    }
}
