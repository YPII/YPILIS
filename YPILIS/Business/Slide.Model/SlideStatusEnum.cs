using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Slide.Model
{
    public enum SlideStatusEnum
    {
        Created,
        PrintRequested,
        Printed,
        Validated,
        ClientAccessioned
    }
}
