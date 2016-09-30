using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Client.Model
{
    [XmlType("PhysicianCollection")]
    public class PhysicianCollection : Collection<Physician>
    {

    }
}
