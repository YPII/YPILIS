using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.DataMatrix
{
    internal class DmtxRay2
    {
        #region Fields
        double _tMin;
        double _tMax;
        DmtxVector2 _p;
        DmtxVector2 _v;
        #endregion

        #region Constructors
        internal DmtxRay2()
        {
            // pass
        }
        #endregion

        #region Properties
        internal DmtxVector2 P
        {
            get
            {
                if (_p == null)
                {
                    _p = new DmtxVector2();
                }
                return _p;
            }
            set
            {
                _p = value;
            }
        }

        internal DmtxVector2 V
        {
            get
            {
                if (_v == null)
                {
                    _v = new DmtxVector2();
                }
                return _v;
            }
            set
            {
                _v = value;
            }
        }


        internal double TMin
        {
            get
            {
                return _tMin;
            }
            set
            {
                _tMin = value;
            } 
        }

        internal double TMax
        {
            get
            {
                return _tMax;
            }
            set
            {
                _tMax = value;
            }
        }
        #endregion
    }
}
