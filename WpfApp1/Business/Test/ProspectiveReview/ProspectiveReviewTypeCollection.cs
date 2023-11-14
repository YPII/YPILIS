using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ProspectiveReview
{
    public class ProspectiveReviewTypeCollection : ObservableCollection<string>
    {
        public ProspectiveReviewTypeCollection()
        {
            foreach (ProspectiveReviewTypeEnum prospectiveReviewType in Enum.GetValues(typeof(ProspectiveReviewTypeEnum)))
            {
                this.Add(prospectiveReviewType.ToString());
            }
        }
    }
}
