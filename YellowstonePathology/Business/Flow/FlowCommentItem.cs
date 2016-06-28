using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Data;
using System.Data.SqlClient;

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
				
