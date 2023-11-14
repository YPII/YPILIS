using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Slide.Model
{
    public class IHCSlide : Slide
    {
        public IHCSlide()
        {
            this.m_LabelType = SlideLabelTypeEnum.PaperLabel;
        }
    }
}
