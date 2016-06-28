using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace YellowstonePathology.Business.Scanning
{
    public class ScanningProcessing
    {
        ScanFolderList m_ScanFolderList;
        FileList m_ScanFileList;

        public ScanningProcessing()
        {
            this.m_ScanFolderList = new ScanFolderList();
            this.m_ScanFileList = new FileList();
        }

        public ScanFolderList ScanFolderList
        {
            get { return this.m_ScanFolderList; }
        }

        public FileList ScanFileList
        {
            get { return this.m_ScanFileList; }
        }

        public static void SplitTiff(string fileName)
        {            
            System.Drawing.Image imageFile = System.Drawing.Image.FromFile(fileName);
            System.Drawing.Imaging.FrameDimension frameDimensions = new System.Drawing.Imaging.FrameDimension(imageFile.FrameDimensionsList[0]);
            int numberOfImages = imageFile.GetFrameCount(frameDimensions);
            System.Drawing.Image[] img = new System.Drawing.Image[numberOfImages];
            for (int intFrame = 0; intFrame < numberOfImages; intFrame++)
            {
                imageFile.SelectActiveFrame(frameDimensions, intFrame);
                img[intFrame] = imageFile;
            }
            img[0].Save(@"C:\YpiiData\test55.tif");         
        }
    }

    public class ScanFolderList : DirectoryList
    {
        public ScanFolderList() 
        {
            this.Fill(@"\\CFileServer\Documents\Scanning");
            System.Windows.MessageBox.Show(this.Count.ToString());
        }
    } 
 
}
