namespace LabProgram
{
    partial class Change_User_PWD_FRM
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
            this.radLabel3 = new System.Windows.Forms.Label();
            this.radLabel4 = new System.Windows.Forms.Label();
            this.txt_username = new System.Windows.Forms.TextBox();
            this.txt_old_userpassword = new System.Windows.Forms.TextBox();
            this.radButton1 = new System.Windows.Forms.Button();
            this.radButton2 = new System.Windows.Forms.Button();
            this.txt_new_userpassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // radLabel3
            // 
            this.radLabel3.Font = new System.Drawing.Font("Tahoma", 14F);
            this.radLabel3.Location = new System.Drawing.Point(403, 69);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(232, 27);
            this.radLabel3.TabIndex = 1;
            this.radLabel3.Text = "كلمة السر القديمه";
            this.radLabel3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // radLabel4
            // 
            this.radLabel4.Font = new System.Drawing.Font("Tahoma", 14F);
            this.radLabel4.Location = new System.Drawing.Point(403, 20);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(236, 27);
            this.radLabel4.TabIndex = 1;
            this.radLabel4.Text = "اسم المستخدم";
            this.radLabel4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txt_username
            // 
            this.txt_username.Font = new System.Drawing.Font("Tahoma", 14F);
            this.txt_username.Location = new System.Drawing.Point(125, 17);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(248, 30);
            this.txt_username.TabIndex = 1;
            this.txt_username.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_old_userpassword
            // 
            this.txt_old_userpassword.Font = new System.Drawing.Font("Tahoma", 14F);
            this.txt_old_userpassword.Location = new System.Drawing.Point(125, 66);
            this.txt_old_userpassword.Name = "txt_old_userpassword";
            this.txt_old_userpassword.Size = new System.Drawing.Size(248, 30);
            this.txt_old_userpassword.TabIndex = 2;
            this.txt_old_userpassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_old_userpassword.UseSystemPasswordChar = true;
            // 
            // radButton1
            // 
            this.radButton1.BackColor = System.Drawing.Color.Transparent;
            this.radButton1.Font = new System.Drawing.Font("Tahoma", 14F);
            this.radButton1.Location = new System.Drawing.Point(283, 181);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(116, 42);
            this.radButton1.TabIndex = 6;
            this.radButton1.Text = "الغاء";
            this.radButton1.UseVisualStyleBackColor = false;
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // radButton2
            // 
            this.radButton2.BackColor = System.Drawing.Color.Transparent;
            this.radButton2.Font = new System.Drawing.Font("Tahoma", 14F);
            this.radButton2.Location = new System.Drawing.Point(125, 181);
            this.radButton2.Name = "radButton2";
            this.radButton2.Size = new System.Drawing.Size(116, 42);
            this.radButton2.TabIndex = 5;
            this.radButton2.Text = "حفظ";
            this.radButton2.UseVisualStyleBackColor = false;
            this.radButton2.Click += new System.EventHandler(this.radButton2_Click);
            // 
            // txt_new_userpassword
            // 
            this.txt_new_userpassword.Font = new System.Drawing.Font("Tahoma", 14F);
            this.txt_new_userpassword.Location = new System.Drawing.Point(126, 113);
            this.txt_new_userpassword.Name = "txt_new_userpassword";
            this.txt_new_userpassword.Size = new System.Drawing.Size(248, 30);
            this.txt_new_userpassword.TabIndex = 8;
            this.txt_new_userpassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_new_userpassword.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F);
            this.label1.Location = new System.Drawing.Point(403, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 27);
            this.label1.TabIndex = 7;
            this.label1.Text = "كلمة السر الجديده";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Change_User_PWD_FRM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 235);
            this.Controls.Add(this.txt_new_userpassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radButton2);
            this.Controls.Add(this.radButton1);
            this.Controls.Add(this.txt_old_userpassword);
            this.Controls.Add(this.txt_username);
            this.Controls.Add(this.radLabel3);
            this.Controls.Add(this.radLabel4);
            this.Font = new System.Drawing.Font("Tahoma", 10F);
            this.Name = "Change_User_PWD_FRM";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "المستخدمين";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label radLabel3;
        private System.Windows.Forms.Label radLabel4;
        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.TextBox txt_old_userpassword;
        private System.Windows.Forms.Button radButton1;
        private System.Windows.Forms.Button radButton2;
        private System.Windows.Forms.TextBox txt_new_userpassword;
        private System.Windows.Forms.Label label1;
    }
}

