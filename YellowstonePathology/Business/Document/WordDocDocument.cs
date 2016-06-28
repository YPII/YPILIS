using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace YellowstonePathology.Business.Document
{
	public class WordDocDocument : CaseDocument
	{
		public WordDocDocument()
		{
		}

		public override void Show(System.Windows.Controls.ContentControl contentControl, object writer)
		{
			string newFile = string.Empty;
			if (File.Exists(this.FullFileName) == true)
			{
				string[] dotsplit = this.FullFileName.Split('.');
				string extenstion = dotsplit[dotsplit.Length - 1];
				newFile = @"\\cfileserver\Documents\Distribution\OpenedWordViewerFiles\" + Guid.NewGuid() + "." + extenstion;
				File.Copy(this.FullFileName, newFile);

				Process p1 = new Process();
				p1.StartInfo = new ProcessStartInfo("wordview.exe", newFile);
				p1.Start();
				p1.WaitForExit();
				p1.Close();
			}
			else
			{
				System.Windows.MessageBox.Show("The file does not exist. " + newFile);
			}
		}
	}
}
