using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.MaterialTracking.Model
{
	public class MaterialTrackingBatchCollection : ObservableCollection <MaterialTrackingBatch>
	{
		public MaterialTrackingBatchCollection()
		{

		}

        public void Add(string objectId, string description, DateTime openDate, YellowstonePathology.Business.Facility.Model.Facility fromFacility, YellowstonePathology.Business.Facility.Model.Location fromLocation,
            YellowstonePathology.Business.Facility.Model.Facility toFacility, YellowstonePathology.Business.Facility.Model.Location toLocation, string masterAccessionNo)
		{
			MaterialTrackingBatch materialTrackingBatch = new MaterialTrackingBatch(objectId, description, fromFacility, fromLocation, toFacility, toLocation, masterAccessionNo);
			this.Add(materialTrackingBatch);
		}
	}
}
