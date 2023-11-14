using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.DataMatrix
{
    internal class DmtxPointFlow
    {
        #region Fields
        int _plane;
        int _arrive;
        int _depart;
        int _mag;
        DmtxPixelLoc _loc;
        #endregion

        #region Properties
        internal int Plane
        {
            get
            {
                return _plane;
            }
            set
            {
                _plane = value;
            }
        }

        internal int Arrive
        {
            get
            {
                return _arrive;
            }
            set
            {
                _arrive = value;
            }
        }

        internal int Depart
        {
            get
            {
                return _depart;
            }
            set
            {
                _depart = value;
            }
        }

        internal int Mag
        {
            get
            {
                return _mag;
            }
            set
            {
                _mag = value;
            }
        }

        internal DmtxPixelLoc Loc
        {
            get
            {
                return _loc;
            }
            set
            {
                _loc = value;
            }
        }
        #endregion
    }
}
