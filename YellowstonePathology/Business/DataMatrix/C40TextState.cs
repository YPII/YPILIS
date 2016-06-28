using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.DataMatrix
{
    internal struct C40TextState
    {
        #region Fields
        int _shift;
        bool _upperShift;
        #endregion

        #region Properties
        internal int Shift
        {
            get
            {
                return _shift;
            }
            set
            {
                _shift = value;
            }
        }

        internal bool UpperShift
        {
            get
            {
                return _upperShift;
            }
            set
            {
                _upperShift = value;
            }
        }
        #endregion

    }
}