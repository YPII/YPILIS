using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.DataMatrix
{
    internal struct DmtxTriplet
    {
        byte[] _value;

        internal byte[] Value
        {
            get
            {
                if (_value == null)
                {
                    _value = new byte[3];
                }
                return _value;
            }
        }
    }

    /**
     * @struct DmtxQuadruplet
     * @brief DmtxQuadruplet
     */
    internal struct DmtxQuadruplet
    {
        byte[] _value;

        internal byte[] Value
        {
            get
            {
                if (_value == null)
                {
                    _value = new byte[4];
                }
                return _value;
            }
        }
    }
}
