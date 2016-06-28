using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Slide.Model
{
    public class SlideFactory
    {        
        public static Slide Get(int testId)
        {
            Slide result = null;
            if (testId == 49 || testId == 150 || testId == 45 || testId == 46 || testId == 48 || testId == 195 || testId == 267)
            {
                result = new HAndESlide();
            }
            else if (testId == 348)
            {
                result = new HAndEAfterSlide();
            }
            else if (testId == 352)
            {
                result = new UnstainedSlide();
            }
            else
            {
                result = new IHCSlide();
            }
            return result;
        }
    }
}
