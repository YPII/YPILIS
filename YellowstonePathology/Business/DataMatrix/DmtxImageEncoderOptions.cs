using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace YellowstonePathology.Business.DataMatrix
{
    public class DmtxImageEncoderOptions
    {
        #region Fields
        int _marginSize = 10;
        int _moduleSize = 5;
        DmtxScheme _scheme = DmtxScheme.DmtxSchemeAscii;
        DmtxSymbolSize _sizeIdx = DmtxSymbolSize.DmtxSymbolSquareAuto;
        Color _color = Color.Black;
        Color _bgColor = Color.White;
        #endregion

        #region Properties

        public int MarginSize
        {
            get { return _marginSize; }
            set { _marginSize = value; }
        }

        public int ModuleSize
        {
            get { return _moduleSize; }
            set { _moduleSize = value; }
        }

        public DmtxScheme Scheme
        {
            get { return _scheme; }
            set { _scheme = value; }
        }

        public DmtxSymbolSize SizeIdx
        {
            get { return _sizeIdx; }
            set { _sizeIdx = value; }
        }

        public Color ForeColor
        {
            get { return _color; }
            set
            {
                _color = value;
            }
        }

        public Color BackColor
        {
            get { return _bgColor; }
            set { _bgColor = value; }
        }

        #endregion
    }
}
