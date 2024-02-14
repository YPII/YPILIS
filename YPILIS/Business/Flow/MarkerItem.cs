using System;

namespace YellowstonePathology.Business.Flow
{
    public partial class MarkerItem
    {
        public MarkerItem()
        {

        }

		public void Save(bool releaseLock)
		{
			base.SaveOld(this);
		}
	}
}
				
