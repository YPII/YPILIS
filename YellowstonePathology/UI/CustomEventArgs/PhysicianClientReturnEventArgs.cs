using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class PhysicianClientReturnEventArgs : System.EventArgs
    {
        private Business.Client.Model.PhysicianClient m_PhysicianClient;

        public PhysicianClientReturnEventArgs(Business.Client.Model.PhysicianClient physicianClient)
        {
            this.m_PhysicianClient = physicianClient;
        }

        public Business.Client.Model.PhysicianClient PhysicianClient
        {
            get { return this.m_PhysicianClient; }
        }
    }
}
