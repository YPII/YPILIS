using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace YellowstonePathology.Business.Common
{
	public class PrintMate
	{
        private PrintMateCarousel m_PrintMateCarousel;

		public PrintMate()
		{
            this.m_PrintMateCarousel = new PrintMateCarousel();
		}

        public PrintMateCarousel Carousel
        {
            get { return this.m_PrintMateCarousel; }
        }

		public static void Print(YellowstonePathology.Business.Common.BlockCollection blockCollection)
		{
			if (blockCollection.Count > 0)
			{
				string path = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.CassettePrinter;
                if(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.UseLaserCassettePrinter == true)
                {
                    path = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LaserCassettePrinter;
                }

				try
				{
					using (StreamWriter file = new StreamWriter(path + System.Guid.NewGuid().ToString() + ".gdc"))
					{
						foreach (Block block in blockCollection)
						{
							if (block.PrintRequested == true)
							{
                                string line = null;
                                if(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.UseLaserCassettePrinter == true)
                                {
                                    line = block.ToLaserString();
                                }
                                else
                                {
                                    line = block.ToString();
                                }
                                
								file.Write(line + "\r\n");
								block.PrintRequested = false;
							}
						}
					}
				}
				catch(Exception e)
				{
					System.Windows.MessageBox.Show(path + ": " + e.Message, "Cassette Printer Location.", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
				}
			}
		}
	}
}
