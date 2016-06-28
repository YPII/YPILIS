using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Client
{
	public class Envelope
	{
		Microsoft.Office.Interop.Word.Application oWord;
		Object oMissing = System.Reflection.Missing.Value;
		Object oTrue = true;
		Object oFalse = false;

		public Envelope()
		{

		}

		public void PrintEnvelope(string name, string address, string city, string state, string zip)
		{
			string line1 = name + "\n\n";
			string line2 = address + "\n\n";
			string line3 = city + ", " + state + " " + zip;
			this.PrintEnvelope(line1, line2, line3);
		}

		public void PrintEnvelope(string line1, string line2, string line3)
		{
			line1 += "\n\n";
			line2 += "\n\n";
			line3 += "\n\n";

			oWord = new Microsoft.Office.Interop.Word.Application();
			oWord.Visible = false;

			Microsoft.Office.Interop.Word.Document doc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);
			object oAddress = line1 + line2 + line3;

			doc.Envelope.PrintOut(ref oMissing, ref oAddress, ref oMissing, ref oMissing,
				ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
				ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
				ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
				ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

			while (oWord.BackgroundPrintingStatus.ToString() == "1")
			{
				//do nothing while waiting for printer to get moving
			}
			oWord.Quit(ref oFalse, ref oMissing, ref oMissing);
		}
	}
}
