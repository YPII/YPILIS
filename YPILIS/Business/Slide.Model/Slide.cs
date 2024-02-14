using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Slide.Model
{
    public class Slide
    {

        protected SlideLabelTypeEnum m_LabelType;

        public Slide()
        {

        }

        public SlideLabelTypeEnum LabelType
        {
            get { return this.m_LabelType; }
        }
    }
}
