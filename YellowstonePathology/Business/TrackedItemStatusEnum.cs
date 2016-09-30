using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business
{
    public enum TrackedItemStatusEnum
    {
        Created,
        PrintRequested,
        Printed,
        Validated,
        ClientAccessioned
    }
}
