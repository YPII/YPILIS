﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Slide.Model
{
    public class ParaffinCurls : Slide
    {
        public ParaffinCurls()
        {
            this.m_LabelType = SlideLabelTypeEnum.DoNotPrint;
        }
    }
}
