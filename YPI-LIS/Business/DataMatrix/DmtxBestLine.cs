using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.DataMatrix
{
    internal struct DmtxBestLine
    {
        int _angle;
        int _hOffset;
        int _mag;
        int _stepBeg;
        int _stepPos;
        int _stepNeg;
        int _distSq;
        double _devn;
        DmtxPixelLoc _locBeg;
        DmtxPixelLoc _locPos;
        DmtxPixelLoc _locNeg;

        internal int Angle
        {
            get
            {
                return _angle;
            }
            set
            {
                _angle = value;
            }
        }

        internal int HOffset
        {
            get
            {
                return _hOffset;
            }
            set
            {
                _hOffset = value;
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

        internal int StepBeg
        {
            get
            {
                return _stepBeg;
            }
            set
            {
                _stepBeg = value;
            }
        }

        internal int StepPos
        {
            get
            {
                return _stepPos;
            }
            set
            {
                _stepPos = value;
            }
        }

        internal int StepNeg
        {
            get
            {
                return _stepNeg;
            }
            set
            {
                _stepNeg = value;
            }
        }

        internal int DistSq
        {
            get
            {
                return _distSq;
            }
            set
            {
                _distSq = value;
            }
        }

        internal double Devn
        {
            get
            {
                return _devn;
            }
            set
            {
                _devn = value;
            }
        }

        internal DmtxPixelLoc LocBeg
        {
            get
            {
                return _locBeg;
            }
            set
            {
                _locBeg = value;
            }
        }

        internal DmtxPixelLoc LocPos
        {
            get
            {
                return _locPos;
            }
            set
            {
                _locPos = value;
            }
        }

        internal DmtxPixelLoc LocNeg
        {
            get
            {
                return _locNeg;
            }
            set
            {
                _locNeg = value;
            }
        }
    }
}
