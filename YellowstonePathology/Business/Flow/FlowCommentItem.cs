using System;

namespace YellowstonePathology.Business.Flow
{
    public partial class FlowCommentItem
    {
        public FlowCommentItem()
        {

        }

		public void Save(bool releaseLock)
		{
			base.SaveOld(this);
		}
	}
}
				
