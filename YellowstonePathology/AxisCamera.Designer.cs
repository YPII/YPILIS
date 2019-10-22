namespace YellowstonePathology
{
    partial class AxisCamera
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AxisCamera));
            this.amc = new AxAXISMEDIACONTROLLib.AxAxisMediaControl();            
            ((System.ComponentModel.ISupportInitialize)(this.amc)).BeginInit();
            this.SuspendLayout();
            // 
            // amc
            // 
            this.amc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.amc.Enabled = true;
            this.amc.Location = new System.Drawing.Point(8, 8);
            this.amc.Name = "amc";
            this.amc.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("amc.OcxState")));
            this.amc.Size = new System.Drawing.Size(468, 312);
            this.amc.TabIndex = 0;
            this.amc.TabStop = false;
            this.amc.OnError += new AxAXISMEDIACONTROLLib._IAxisMediaControlEvents_OnErrorEventHandler(this.amc_OnError);
            this.amc.OnMouseMove += new AxAXISMEDIACONTROLLib._IAxisMediaControlEvents_OnMouseMoveEventHandler(this.amc_OnMouseMove);
            this.amc.OnStatusChange += new AxAXISMEDIACONTROLLib._IAxisMediaControlEvents_OnStatusChangeEventHandler(this.amc_OnStatusChange);
            this.amc.OnNewVideoSize += new AxAXISMEDIACONTROLLib._IAxisMediaControlEvents_OnNewVideoSizeEventHandler(this.amc_OnNewVideoSize);            
            // 
            // AxisCamera
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(560, 326);            
            this.Controls.Add(this.amc);
            this.Name = "AxisCamera";
            this.Text = "Axis Camera";
            ((System.ComponentModel.ISupportInitialize)(this.amc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}