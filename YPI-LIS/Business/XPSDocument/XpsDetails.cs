using System;
using System.Collections.Generic;
using System.Windows.Xps.Packaging;

namespace YellowstonePathology.Business.XPSDocument
{
    /// <summary>
    /// Class to represent the basic properties we use from XPS files.
    /// </summary>
    public class XpsDetails
    {
        public XpsResource resource { get; set; }
        public Uri sourceURI { get; set; }
        public Uri destURI { get; set; }
    }

}
