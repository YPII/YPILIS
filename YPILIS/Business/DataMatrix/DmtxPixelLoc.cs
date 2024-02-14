using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.DataMatrix
{
    internal struct DmtxPixelLoc
    {
        #region Fields
        int _x;
        int _y;
        #endregion

        #region Properties
        internal int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        internal int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }
        #endregion
    }
}
