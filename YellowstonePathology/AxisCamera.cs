using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AXISMEDIACONTROLLib;

namespace YellowstonePathology
{
    enum MediaType
    {
        mjpeg,
        h264,
        h265,
        mpeg4
    }

    public partial class AxisCamera : Form
    {
        private AxAXISMEDIACONTROLLib.AxAxisMediaControl amc;

        public AxisCamera()
        {
            InitializeComponent();            

            this.amc.StretchToFit = true;
            this.amc.MaintainAspectRatio = true;
            this.amc.ShowStatusBar = true;
            this.amc.BackgroundColor = 0; // black
            this.amc.VideoRenderer = (int)AMC_VIDEO_RENDERER.AMC_VIDEO_RENDERER_EVR;
            this.amc.EnableOverlays = true;

            // Configure context menu
            this.amc.EnableContextMenu = true;
            this.amc.ToolbarConfiguration = "+play,+fullscreen,-settings"; //"-pixcount" to remove pixel counter

            // AMC messaging setting
            this.amc.Popups = 0;
            this.amc.Popups |= (int)AMC_POPUPS.AMC_POPUPS_LOGIN_DIALOG; // Allow login dialog
            this.amc.Popups |= (int)AMC_POPUPS.AMC_POPUPS_NO_VIDEO; // "No Video" message when stopped
                                                                    //amc.Popups |= (int)AMC_POPUPS.AMC_POPUPS_MESSAGES; // Yellow-balloon notification

            this.amc.UIMode = "digital-zoom";

            this.amc.Stop();

            // Set properties, deciding what url completion to use by MediaType.
            this.amc.MediaUsername = "ypii";
            this.amc.MediaPassword = "ypii";
            //amc.MediaURL = CompleteURL(myUrlBox.Text, (MediaType)myTypeBox.SelectedItem);
            this.amc.MediaURL = CompleteURL("10.1.1.200", MediaType.mjpeg);

            // Start the streaming
            this.amc.Play();
        }

        private string CompleteURL(string theMediaURL, MediaType theMediaType)
        {
            string anURL = theMediaURL;
            if (!anURL.EndsWith("/")) anURL += "/";

            switch (theMediaType)
            {
                case MediaType.mjpeg:
                    anURL += "axis-cgi/mjpg/video.cgi";
                    break;
                case MediaType.mpeg4:
                    anURL += "mpeg4/media.amp";
                    break;
                case MediaType.h264:
                    anURL += "axis-media/media.amp?videocodec=h264";
                    break;
                case MediaType.h265:
                    anURL += "axis-media/media.amp?videocodec=h265";
                    break;
            }

            anURL = CompleteProtocol(anURL, theMediaType);
            return anURL;
        }

        private string CompleteProtocol(string theMediaURL, MediaType theMediaType)
        {
            if (theMediaURL.IndexOf("://") >= 0) return theMediaURL;

            string anURL = theMediaURL;

            switch (theMediaType)
            {
                case MediaType.mjpeg:
                    // This example streams Motion JPEG over HTTP multipart (only video)
                    // See docs on how to receive unsynchronized audio with Motion JPEG
                    anURL = "http://" + anURL;
                    break;
                case MediaType.mpeg4:
                case MediaType.h264:
                case MediaType.h265:
                    // Use RTP over RTSP over HTTP (for other transport mechanisms see docs)
                    anURL = "axrtsphttp://" + anURL;
                    break;
            }

            return anURL;
        }

        void amc_OnNewVideoSize(object sender, AxAXISMEDIACONTROLLib._IAxisMediaControlEvents_OnNewVideoSizeEvent e)
        {
            /*
			if (e.theWidth >= 320 && e.theHeight >= 240)
			{
				// Adapt window size to video size
				Width += e.theWidth - amc.Width;
				Height += e.theHeight - amc.Height;

				if (amc.ShowStatusBar)
				{
					Height += 22;
				}

				if (amc.ShowToolbar)
				{
					Height += 32;
				}
			}
            */
        }

        void amc_OnStatusChange(object sender, AxAXISMEDIACONTROLLib._IAxisMediaControlEvents_OnStatusChangeEvent e)
        {
            if ((e.theOldStatus & (int)AMC_STATUS.AMC_STATUS_RECORDING) == 0 && // was not recording
                    (e.theNewStatus & (int)AMC_STATUS.AMC_STATUS_RECORDING) > 0) // is recording
            {
                //myRecordButton.Text = "Stop Recording";
            }
            else
            {
                //myRecordButton.Text = "Record";
            }
        }

        private void amc_OnError(object sender, AxAXISMEDIACONTROLLib._IAxisMediaControlEvents_OnErrorEvent e)
        {
            System.Windows.Forms.MessageBox.Show(this, e.theErrorInfo, "Error code " + e.theErrorCode.ToString("X8"));
        }

        private void amc_OnMouseMove(object sender, AxAXISMEDIACONTROLLib._IAxisMediaControlEvents_OnMouseMoveEvent e)
        {
            if (amc.UIMode == "digital-zoom")
            {
                if ((amc.Status & (int)AMC_STATUS.AMC_STATUS_PLAYING) > 0)
                {
                    // set focus to AMC in order to zoom using mouse wheel
                    amc.Focus();
                }
            }
        }
    }
}
