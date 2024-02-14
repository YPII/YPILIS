using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.DataMatrix
{
    internal struct DmtxFollow
    {
        #region Fields
        byte[] _ptr;
        int _step;
        DmtxPixelLoc _loc;
        int _ptrIndex;
        #endregion

        #region Properties
        internal int PtrIndex
        {
            set
            {
                _ptrIndex = value;
            }
        }

        internal byte CurrentPtr
        {
            get
            {
                return _ptr[_ptrIndex];
            }
            set
            {
                _ptr[_ptrIndex] = value;
            }
        }

        internal Byte[] Ptr
        {
            get
            {
                return _ptr;
            }
            set
            {
                _ptr = value;
            }
        }

        internal byte Neighbor
        {
            get
            {
                return _ptr[_ptrIndex];
            }
            set
            {
                _ptr[_ptrIndex] = value;
            }
        }

        internal int Step
        {
            get
            {
                return _step;
            }
            set
            {
                _step = value;
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