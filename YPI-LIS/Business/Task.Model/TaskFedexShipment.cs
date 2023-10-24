using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Task.Model
{
	public class TaskFedexShipment : Task
    {
        private Facility.Model.Facility m_ShipToFacility;

        public TaskFedexShipment(string assignedTo, string description, Facility.Model.Facility shipToFacility) 
            : base(assignedTo, description)
        {
            this.m_TaskId = "FDXSHPMNT";
            this.m_TaskName = "Fedex Shipment";
            this.m_ShipToFacility = shipToFacility;
        }       

        public Facility.Model.Facility ShipToFacility
        {
            get { return this.m_ShipToFacility; }
        }
	}
}
