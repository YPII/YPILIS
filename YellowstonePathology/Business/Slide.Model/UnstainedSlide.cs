using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Slide.Model
{
    public class UnstainedSlide : Slide
    {
        public UnstainedSlide()
        {
            this.m_LabelType = SlideLabelTypeEnum.PaperLabel;
        }
    }
}
