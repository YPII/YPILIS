using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.MaterialTracking.Model
{
	public class MaterialTrackingLogCollection : ObservableCollection<MaterialTrackingLog>
	{
		public MaterialTrackingLogCollection()
		{

		}

        public void UpdateClientAccessioned(YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection specimenOrderCollection)
        {
            foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in specimenOrderCollection)
            {
                if (this.MaterialExists(specimenOrder.SpecimenOrderId) == true)
                {
                    List<YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog> materialTrackingLogList = this.GetByMaterialId(specimenOrder.SpecimenOrderId);
                    foreach (YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog in materialTrackingLogList)
                    {                        
                        materialTrackingLog.ClientAccessioned = specimenOrder.ClientAccessioned;                        
                    }
                }

                foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    if (this.MaterialExists(aliquotOrder.AliquotOrderId) == true)
                    {
                        List<YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog> materialTrackingLogList = this.GetByMaterialId(aliquotOrder.AliquotOrderId);
                        foreach (YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog in materialTrackingLogList)
                        {
                            materialTrackingLog.ClientAccessioned = aliquotOrder.ClientAccessioned;
                        }
                    }

                    foreach (YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder in aliquotOrder.SlideOrderCollection)
                    {
                        if (this.MaterialExists(slideOrder.SlideOrderId) == true)
                        {
                            List<YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog> materialTrackingLogList = this.GetByMaterialId(slideOrder.SlideOrderId);
                            foreach (YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog in materialTrackingLogList)
                            {
                                materialTrackingLog.ClientAccessioned = slideOrder.ClientAccessioned;
                            }
                        }
                    }
                }
            }
        }

		public bool MaterialExists(string materialId)
		{
			bool result = false;
			foreach (MaterialTrackingLog materialTrackingLog in this)
			{
				if (materialTrackingLog.MaterialId == materialId)
				{
					result = true;
					break;
				}
			}
			return result;
		}

        public MaterialTrackingLog Get(string materialTrackingLogId)
        {
            MaterialTrackingLog result = null;
            foreach (MaterialTrackingLog materialTrackingLog in this)
            {
                if (materialTrackingLog.MaterialTrackingLogId == materialTrackingLogId)
                {
                    result = materialTrackingLog;
                }
            }
            return result;
        }

        public List<YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog> GetByMaterialId(string materialId)
        {
            List<YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog> result = new List<MaterialTrackingLog>();
            foreach (MaterialTrackingLog materialTrackingLog in this)
            {
                if (materialTrackingLog.MaterialId == materialId)
                {
                    result.Add(materialTrackingLog);
                }
            }
            return result;
        }

        public List<YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog> GetList(List<string> materialIdList)
        {
            List<YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog> result = new List<MaterialTrackingLog>();
            foreach (string materialId in materialIdList)
            {
                if (this.MaterialExists(materialId) == true)
                {
                    List<YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog> list = this.GetByMaterialId(materialId);
                    foreach (YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog in list)
                    {
                        result.Add(materialTrackingLog);
                    }                    
                }
            }
            return result;
        }

		public void Remove(string materialId)
		{
			for (int idx = 0; idx < this.Count; idx++)
			{
				if (this[idx].MaterialId == materialId)
				{
					this.RemoveAt(idx);
					break;
				}
			}
		}
	}
}
