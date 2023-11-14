using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace YellowstonePathology.Business
{
	public class TifDocumentPage
	{
        System.Guid m_PageId;
        BitmapImage m_BitmapImage;

        public TifDocumentPage()
        {
            this.m_PageId = System.Guid.NewGuid();
        }

        public System.Guid PageId
        {
            get { return this.m_PageId; }            
        }

        public BitmapImage BitmapImage
        {
            get { return this.m_BitmapImage; }
            set { this.m_BitmapImage = value; }
        }
	}
}
