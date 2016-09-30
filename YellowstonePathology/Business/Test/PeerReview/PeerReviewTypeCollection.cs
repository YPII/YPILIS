using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PeerReview
{
    public class PeerReviewTypeCollection : ObservableCollection<string>
    {
        public PeerReviewTypeCollection()
        {
            foreach (PeerReviewTypeEnum peerReviewType in Enum.GetValues(typeof(PeerReviewTypeEnum)))
            {
                this.Add(peerReviewType.ToString());
            }
        }
    }
}
