using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace YellowstonePathology.Business.Document
{
	public class PdfDocument : CaseDocument
	{
		public PdfDocument()
		{
		}

		public override void Show(System.Windows.Controls.ContentControl contentControl, object writer)
		{
			Process p = new Process();
			ProcessStartInfo info = new ProcessStartInfo(this.FullFileName);
			p.StartInfo = info;
			p.Start();
			//p.WaitForExit();
		}
	}
}
