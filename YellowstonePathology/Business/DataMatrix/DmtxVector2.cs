using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.DataMatrix
{
    internal class DmtxVector2
    {
        #region Fields
        private double _x;
        private double _y;
        #endregion

        #region Constructors
        internal DmtxVector2()
        {
            _x = 0.0;
            _y = 0.0;
        }

        internal DmtxVector2(double x, double y)
        {
            _x = x;
            _y = y;
        }
        #endregion

        #region Operators
        public static DmtxVector2 operator +(DmtxVector2 v1, DmtxVector2 v2)
        {
            DmtxVector2 result = new DmtxVector2(v1.X, v1.Y);
            result.X += v2.X;
            result.Y += v2.Y;
            return result;
        }

        public static DmtxVector2 operator -(DmtxVector2 v1, DmtxVector2 v2)
        {
            DmtxVector2 result = new DmtxVector2(v1.X, v1.Y);
            result.X -= v2.X;
            result.Y -= v2.Y;
            return result;
        }

        public static DmtxVector2 operator *(DmtxVector2 v1, double factor)
        {
            return new DmtxVector2(v1.X * factor, v1.Y * factor);
        }
        #endregion

        #region Methods
        internal double Cross(DmtxVector2 v2)
        {
            return (this._x * v2._y - this._y * v2._x);
        }

        internal double Norm()
        {
            double mag = Mag();
            if (mag <= DmtxConstants.DmtxAlmostZero)
            {
                return -1.0; // FIXXXME: This doesn't look clean, as noted in original dmtx source
            }
            this._x /= mag;
            this._y /= mag;
            return mag;
        }

        internal double Dot(DmtxVector2 v2)
        {
            return Math.Sqrt(_x * v2._x + _y * v2._y);
        }

        internal double Mag()
        {
            return Math.Sqrt(this._x * this._x + this._y * this._y);
        }

        internal double DistanceFromRay2(DmtxRay2 ray)
        {
            if (Math.Abs(1.0 - ray.V.Mag()) > DmtxConstants.DmtxAlmostZero)
            {
                throw new ArgumentException("DistanceFromRay2: The ray's V vector must be a unit vector");
            }
            return ray.V.Cross(this - ray.P);
        }

        internal double DistanceAlongRay2(DmtxRay2 ray)
        {
            if (Math.Abs(1.0 - ray.V.Mag()) > DmtxConstants.DmtxAlmostZero)
            {
                throw new ArgumentException("DistanceAlongRay2: The ray's V vector must be a unit vector");
            }
            return (this - ray.P).Dot(ray.V);
        }

        internal bool Intersect(DmtxRay2 p0, DmtxRay2 p1)
        {
            double denominator = p1.V.Cross(p0.V);
            if (Math.Abs(denominator) < DmtxConstants.DmtxAlmostZero)
            {
                return false;
            }
            double numerator = p1.V.Cross(p1.P - p0.P);
            return PointAlongRay2(p0, numerator / denominator);
        }

        internal bool PointAlongRay2(DmtxRay2 ray, double t)
        {
            if (Math.Abs(1.0 - ray.V.Mag()) > DmtxConstants.DmtxAlmostZero)
            {
                throw new ArgumentException("PointAlongRay: The ray's V vector must be a unit vector");
            }
            DmtxVector2 tmp = new DmtxVector2(ray.V._x * t, ray.V._y * t);
            this._x = ray.P._x + tmp._x;
            this._y = ray.P._y + tmp._y;
            return true;
        }
        #endregion

        #region Properties
        internal double X
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

        internal double Y
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
