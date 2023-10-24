using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class SlideOrderReturnEventArgs : System.EventArgs
    {
        YellowstonePathology.Business.Slide.Model.SlideOrder m_SlideOrder;

        public SlideOrderReturnEventArgs(YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder)
        {
            this.m_SlideOrder = slideOrder;
        }

        public YellowstonePathology.Business.Slide.Model.SlideOrder SlideOrder
        {
            get { return this.m_SlideOrder; }
        }
    }
}
