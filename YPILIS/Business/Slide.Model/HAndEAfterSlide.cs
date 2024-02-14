using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Slide.Model
{
    public class HAndEAfterSlide : Slide
    {
        public HAndEAfterSlide()
        {
            this.m_LabelType = SlideLabelTypeEnum.DirectPrint;
        }
    }
}
