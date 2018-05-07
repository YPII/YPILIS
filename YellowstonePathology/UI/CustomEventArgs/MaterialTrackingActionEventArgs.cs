﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
	public class MaterialTrackingActionEventArgs
	{
        string m_TrackingDescription;        

        YellowstonePathology.Business.Facility.Model.Facility m_FromFacility;
        string m_FromLocation;

        YellowstonePathology.Business.Facility.Model.Facility m_ToFacility;
        string m_ToLocation;

        public MaterialTrackingActionEventArgs(string trackingDescription, 
            YellowstonePathology.Business.Facility.Model.Facility fromFacility, string fromLocation,
            YellowstonePathology.Business.Facility.Model.Facility toFacility, string toLocation)
        {
            this.m_TrackingDescription = trackingDescription;            
            this.m_FromFacility = fromFacility;
            this.m_FromLocation = fromLocation;
            this.m_ToFacility = toFacility;
            this.m_ToLocation = toLocation;
        }

        public string TrackingDescription
        {
            get { return this.m_TrackingDescription; }
        }        

        public YellowstonePathology.Business.Facility.Model.Facility FromFacility
        {
            get { return this.m_FromFacility; }
        }

        public string FromLocation
        {
			get { return this.m_FromLocation; }
        }

        public YellowstonePathology.Business.Facility.Model.Facility ToFacility
        {
            get { return this.m_ToFacility; }
        }

        public string ToLocation
        {
            get { return this.m_ToLocation; }
        }
	}
}
