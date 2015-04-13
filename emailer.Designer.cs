namespace EmailApplication1004
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtemail = new System.Windows.Forms.TextBox();
            this.txtReceiver = new System.Windows.Forms.TextBox();
            this.txtattachment = new System.Windows.Forms.TextBox();
            this.txtsubject = new System.Windows.Forms.TextBox();
            this.txtbody = new System.Windows.Forms.TextBox();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnbrowse = new System.Windows.Forms.Button();
            this.btnsend = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Your Email ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Receiver\'s Email";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Subject";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Body";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Add Attachment";
            // 
            // txtemail
            // 
            this.txtemail.Location = new System.Drawing.Point(136, 11);
            this.txtemail.Name = "txtemail";
            this.txtemail.Size = new System.Drawing.Size(265, 20);
            this.txtemail.TabIndex = 5;
            // 
            // txtReceiver
            // 
            this.txtReceiver.Location = new System.Drawing.Point(136, 37);
            this.txtReceiver.Name = "txtReceiver";
            this.txtReceiver.Size = new System.Drawing.Size(265, 20);
            this.txtReceiver.TabIndex = 6;
            // 
            // txtattachment
            // 
            this.txtattachment.Location = new System.Drawing.Point(136, 69);
            this.txtattachment.Name = "txtattachment";
            this.txtattachment.Size = new System.Drawing.Size(265, 20);
            this.txtattachment.TabIndex = 7;
            // 
            // txtsubject
            // 
            this.txtsubject.Location = new System.Drawing.Point(136, 99);
            this.txtsubject.Name = "txtsubject";
            this.txtsubject.Size = new System.Drawing.Size(265, 20);
            this.txtsubject.TabIndex = 8;
            // 
            // txtbody
            // 
            this.txtbody.Location = new System.Drawing.Point(136, 169);
            this.txtbody.Multiline = true;
            this.txtbody.Name = "txtbody";
            this.txtbody.Size = new System.Drawing.Size(265, 114);
            this.txtbody.TabIndex = 9;
            // 
            // txtpassword
            // 
            this.txtpassword.Location = new System.Drawing.Point(503, 11);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.Size = new System.Drawing.Size(100, 20);
            this.txtpassword.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(419, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Your Password";
            // 
            // btnbrowse
            // 
            this.btnbrowse.Location = new System.Drawing.Point(443, 71);
            this.btnbrowse.Name = "btnbrowse";
            this.btnbrowse.Size = new System.Drawing.Size(75, 23);
            this.btnbrowse.TabIndex = 12;
            this.btnbrowse.Text = "browse";
            this.btnbrowse.UseVisualStyleBackColor = true;
            this.btnbrowse.Click += new System.EventHandler(this.btnbrowse_Click);
            // 
            // btnsend
            // 
            this.btnsend.Location = new System.Drawing.Point(443, 259);
            this.btnsend.Name = "btnsend";
            this.btnsend.Size = new System.Drawing.Size(75, 23);
            this.btnsend.TabIndex = 13;
            this.btnsend.Text = "Send";
            this.btnsend.UseVisualStyleBackColor = true;
            this.btnsend.Click += new System.EventHandler(this.btnsend_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 316);
            this.Controls.Add(this.btnsend);
            this.Controls.Add(this.btnbrowse);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtpassword);
            this.Controls.Add(this.txtbody);
            this.Controls.Add(this.txtsubject);
            this.Controls.Add(this.txtattachment);
            this.Controls.Add(this.txtReceiver);
            this.Controls.Add(this.txtemail);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Email Sender";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtemail;
        private System.Windows.Forms.TextBox txtReceiver;
        private System.Windows.Forms.TextBox txtattachment;
        private System.Windows.Forms.TextBox txtsubject;
        private System.Windows.Forms.TextBox txtbody;
        private System.Windows.Forms.TextBox txtpassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnbrowse;
        private System.Windows.Forms.Button btnsend;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

