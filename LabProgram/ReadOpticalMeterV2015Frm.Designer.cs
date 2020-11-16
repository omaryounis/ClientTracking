namespace LabProgram
{
    partial class ReadOpticalMeterV2015Frm
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.cboPort = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.radButton1 = new System.Windows.Forms.Button();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SerialPort = new System.IO.Ports.SerialPort(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(602, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "البورت";
            // 
            // cboPort
            // 
            this.cboPort.FormattingEnabled = true;
            this.cboPort.Location = new System.Drawing.Point(289, 36);
            this.cboPort.Name = "cboPort";
            this.cboPort.Size = new System.Drawing.Size(240, 21);
            this.cboPort.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(453, 113);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(184, 34);
            this.button1.TabIndex = 2;
            this.button1.Text = "قراءة بيانات الوصله الضوئية";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // radButton1
            // 
            this.radButton1.Location = new System.Drawing.Point(175, 113);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(133, 34);
            this.radButton1.TabIndex = 3;
            this.radButton1.Text = "حفظ البيانات";
            this.radButton1.UseVisualStyleBackColor = true;
            this.radButton1.Visible = false;
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // reportViewer1
            // 
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "LabProgram.bin.Debug.Reports.OpticalMeterData.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(-6, 199);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(828, 266);
            this.reportViewer1.TabIndex = 4;
            // 
            // ReadOpticalMeterV2015Frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 466);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.radButton1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cboPort);
            this.Controls.Add(this.label1);
            this.Name = "ReadOpticalMeterV2015Frm";
            this.Text = "ReadOpticalMeterV2015Frm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ReadOpticalMeterV2015Frm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPort;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button radButton1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.IO.Ports.SerialPort SerialPort;
    }
}