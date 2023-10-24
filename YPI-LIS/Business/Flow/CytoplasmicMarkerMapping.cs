using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Flow
{
    public class CytoplasmicMarkerMapping
    {
        private string m_Name;
        private string m_RegularMarkerId;
        private string m_CytoplasmicMarkerId;

        public CytoplasmicMarkerMapping(string name, string regularMarkerId, string cytoplasmicMarkerId)
        {
            this.m_Name = name;
            this.m_RegularMarkerId = regularMarkerId;
            this.m_CytoplasmicMarkerId = cytoplasmicMarkerId;
        }

        public string Name
        {
            get { return this.m_Name; }
        }

        public string RegularMarkerId
        {
            get { return this.m_RegularMarkerId; }
        }

        public string CytoplasmicMarkerId
        {
            get { return this.m_CytoplasmicMarkerId; }
        }
    }
}
