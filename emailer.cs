using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;

namespace EmailApplication1004
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnbrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtattachment.Text = openFileDialog1.FileName.ToString();


            }
        }

        private void btnsend_Click(object sender, EventArgs e)
        {
            try
            {

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                MailMessage message = new MailMessage();
                message.From = new MailAddress(txtemail.Text);
                message.To.Add(txtReceiver.Text);
                message.Body = txtbody.Text;
                message.Subject = txtsubject.Text;
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;

                if (txtattachment.Text != "")
                {


                    message.Attachments.Add(new Attachment(txtattachment.Text));
                }
                client.Credentials = new System.Net.NetworkCredential(txtemail.Text, txtpassword.Text);
                client.Send(message);
                message = null;

                MessageBox.Show("Message sent!");
            }

            catch (Exception s)
            {
                MessageBox.Show("Failed to send message");

            }
        }
    }

}
    

