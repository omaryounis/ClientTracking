namespace LabProgram
{
    partial class Login_frm
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
            this.radLabel1 = new System.Windows.Forms.Label();
            this.radLabel2 = new System.Windows.Forms.Label();
            this.txt_usname = new System.Windows.Forms.TextBox();
            this.txt_pass = new System.Windows.Forms.TextBox();
            this.radButton1 = new System.Windows.Forms.Button();
            this.radButton2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 14F);
            this.radLabel1.Location = new System.Drawing.Point(380, 45);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(240, 27);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "اسم المستخدم";
            this.radLabel1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Tahoma", 14F);
            this.radLabel2.Location = new System.Drawing.Point(380, 88);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(201, 27);
            this.radLabel2.TabIndex = 1;
            this.radLabel2.Text = "كلمة السر";
            this.radLabel2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txt_usname
            // 
            this.txt_usname.Font = new System.Drawing.Font("Tahoma", 14F);
            this.txt_usname.Location = new System.Drawing.Point(59, 29);
            this.txt_usname.Name = "txt_usname";
            this.txt_usname.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_usname.Size = new System.Drawing.Size(275, 30);
            this.txt_usname.TabIndex = 0;
            this.txt_usname.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_pass
            // 
            this.txt_pass.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txt_pass.Location = new System.Drawing.Point(59, 84);
            this.txt_pass.Name = "txt_pass";
            this.txt_pass.PasswordChar = '*';
            this.txt_pass.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_pass.Size = new System.Drawing.Size(275, 31);
            this.txt_pass.TabIndex = 1;
            this.txt_pass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // radButton1
            // 
            this.radButton1.BackColor = System.Drawing.Color.Transparent;
            this.radButton1.Font = new System.Drawing.Font("Tahoma", 14F);
            this.radButton1.Location = new System.Drawing.Point(218, 140);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(116, 42);
            this.radButton1.TabIndex = 3;
            this.radButton1.Text = "دخول";
            this.radButton1.UseVisualStyleBackColor = false;
            this.radButton1.Click += new System.EventHandler(this.RadButton1_Click);
            // 
            // radButton2
            // 
            this.radButton2.Font = new System.Drawing.Font("Tahoma", 14F);
            this.radButton2.Location = new System.Drawing.Point(59, 140);
            this.radButton2.Name = "radButton2";
            this.radButton2.Size = new System.Drawing.Size(116, 42);
            this.radButton2.TabIndex = 4;
            this.radButton2.Text = "الغاء";
            this.radButton2.Click += new System.EventHandler(this.RadButton2_Click);
            // 
            // Login_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 242);
            this.Controls.Add(this.radButton2);
            this.Controls.Add(this.radButton1);
            this.Controls.Add(this.txt_pass);
            this.Controls.Add(this.txt_usname);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.radLabel1);
            this.Font = new System.Drawing.Font("Tahoma", 10F);
            this.Name = "Login_frm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تسجيل الدخول";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label radLabel1;
        private System.Windows.Forms.Label radLabel2;
        private System.Windows.Forms.TextBox txt_usname;
        private System.Windows.Forms.TextBox txt_pass;
        private System.Windows.Forms.Button radButton1;
        //private Telerik.WinControls.Themes.Windows8Theme Windows8Theme1;
        private System.Windows.Forms.Button radButton2;
        //private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
    }
}