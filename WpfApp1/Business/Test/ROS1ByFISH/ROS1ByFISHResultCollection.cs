using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ROS1ByFISH
{
    public class ROS1ByFISHResultCollection : ObservableCollection<ROS1ByFISHResult>
    {
        public ROS1ByFISHResultCollection()
        {
            this.Add(new ROS1ByFISHNegativeResult());
            this.Add(new ROS1ByFISHNegativeWithPolysomyResult());
            this.Add(new ROS1ByFISHNegativeWithMonosomyResult());
            this.Add(new ROS1ByFISHPositiveResult());
            this.Add(new ROS1ByFISHAbnormalResult());
            this.Add(new ROS1ByFISHInconclusiveResult());
            this.Add(new ROS1ByFISHQNSResult());
        }

        public ROS1ByFISHResult GetByResultCode(string resultCode)
        {
            ROS1ByFISHResult result = null;
            foreach (ROS1ByFISHResult ros1ByFISHResult in this)
            {
                if(ros1ByFISHResult.ResultCode == resultCode)
                {
                    result = ros1ByFISHResult;
                    break;
                }
            }
            return result;
        }

    }
}
