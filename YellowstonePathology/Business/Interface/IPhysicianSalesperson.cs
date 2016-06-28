using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Interface
{
    public interface IPhysicianSalesperson
    {
        int PhysicianSalespersonId { get; set; }
        int PhysicianId { get; set; }
        int OrderableTestId { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }
}
