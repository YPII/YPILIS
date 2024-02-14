using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
	public class MaterialTrackingBatchEventArgs
	{
		private YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch m_MaterialTrackingBatch;
		private YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection m_MaterialTrackingLogCollection;        
		private YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogViewCollection m_MaterialTrackingLogViewCollection;

		public MaterialTrackingBatchEventArgs(YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch)
        {
            this.m_MaterialTrackingBatch = materialTrackingBatch;
        }

		public MaterialTrackingBatchEventArgs(YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch,
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogViewCollection materialTrackingLogViewCollection)
        {
            this.m_MaterialTrackingBatch = materialTrackingBatch;
            this.m_MaterialTrackingLogViewCollection = materialTrackingLogViewCollection;
        }

		public MaterialTrackingBatchEventArgs(YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch,
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection)
        {
            this.m_MaterialTrackingBatch = materialTrackingBatch;
            this.m_MaterialTrackingLogCollection = materialTrackingLogCollection;            
        }

		public YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch MaterialTrackingBatch
        {
            get { return this.m_MaterialTrackingBatch; }
        }

		public YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection MaterialTrackingLogCollection
        {
            get { return this.m_MaterialTrackingLogCollection; }
        }

		public YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogViewCollection MaterialTrackingLogViewCollection
        {
            get { return this.m_MaterialTrackingLogViewCollection; }
        }        
	}
}
