using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class LocationCollection : ObservableCollection<Location>
    {
        public LocationCollection()
        {			
			
        }

        public static LocationCollection GetFromHostCollection(HostCollection hostCollection)
        {
            LocationCollection allLocations = LocationCollection.GetAllLocations();
            LocationCollection result = new LocationCollection();
            foreach (Location location in allLocations)
            {
                if (hostCollection.LocationIdExists(location.LocationId) == true)
                {
                    result.Add(location);
                }
            }
            return result;
        }

        private static LocationCollection Sort(LocationCollection locationCollection)
        {
            LocationCollection result = new LocationCollection();
            IOrderedEnumerable<Location> orderedResult = locationCollection.OrderBy(i => i.Description);
            foreach (Location location in orderedResult)
            {
                result.Add(location);
            }
            return result;
        }

        public static LocationCollection GetAllLocations()
        {
            LocationCollection result = new LocationCollection();
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.NullLocation());

            // YellowstonePathologistBillings
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.DrBrownOffice());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.DrDurdenOffice());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.DrEmerickOffice());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.DrNeroOffice());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.DrSchultzOffice());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.SvhPathologistOffice());

            // YellowstonePathologistCody
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.PamCleggOffice());

            // YellowstonePathologyInstituteBillings
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsCaseCompilationStation());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsCuttingStationCaptain());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsCuttingStationTenille());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsCytologyLoginStation());
			result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsCytologySlideStation());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsFlowAStation());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsFlowBStation());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsFlowCStation());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsGrossHobbitStation());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsGrossPathStation());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsGrossTechStation());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsHistologyAStation());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsHistologyBStation());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsHistologyIHCStation());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsICStation());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsIT3Station());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsMolecularAStation());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.BlgsMolecularBStation());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.TheDarkRoom());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.DonaCranstonOffice());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.EricRamseyOffice());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.JoiGarzaOffice());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.JulieBlaschakOffice());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.KevinBengeOffice());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.MelissaMelbyOffice());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.MikeBoydOffice());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.ChelsyOrtloffOffice());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.RobertaBeckersOffice());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.SidHarderOffice());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.TiffanyGoodsonOffice());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.WilliamCoplandOffice());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.YolandaHuttonOffice());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.Compile());

            //YellowstonePathologyInstituteCody
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.CodyAccessionStation());
            result.Add(new YellowstonePathology.Business.Facility.Model.LocationDefinitions.CodyGrossStation());

            return Sort(result);
        }

        public Location GetLocation(string locationId)
        {
            Location location = null;
            foreach (Location lctn in this)
            {
                if (lctn.LocationId == locationId)
                {
                    location = lctn;
                    break;
                }
            }
            return location;
        }

        public bool Exists(string locationId)
        {
            bool result = false;
            foreach (Location lctn in this)
            {
                if (lctn.LocationId == locationId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }				
	}
}
