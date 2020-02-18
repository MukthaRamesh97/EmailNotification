using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;


namespace SendEmailNotification
{
    public partial class Form1 : Form
    {
        OpenFileDialog ofAttachment;
        String filename = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                ofAttachment = new OpenFileDialog();
                ofAttachment.Filter = "Images(.jpg,.png)| *.png;*.jpg;|Pdf Files|*.pdf";
                if(ofAttachment.ShowDialog()==DialogResult.OK)
                {
                    filename = ofAttachment.FileName;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            try
            {  
                //smtp details
                SmtpClient clientDetails = new SmtpClient();
                clientDetails.Port = Convert.ToInt32(txtPortNumber.Text.Trim());
                clientDetails.Host = txtSmtpServer.Text.Trim();
                clientDetails.EnableSsl = cbxSSL.Checked;
                clientDetails.DeliveryMethod = SmtpDeliveryMethod.Network;
                clientDetails.UseDefaultCredentials = false;
                clientDetails.Credentials = new NetworkCredential(txtSenderEmail.Text.Trim(), txtSenderPassword.Text.Trim());

                // message details 
                MailMessage maildetails = new MailMessage();
                maildetails.From = new MailAddress(txtSenderEmail.Text.Trim());
                maildetails.To.Add(txtRecipientEmail.Text.Trim());

                //multiple recipients
                maildetails.Subject = txtSubject.Text.Trim();
                maildetails.IsBodyHtml = cbxHtmlBody.Checked;
                maildetails.Body = rtbBody.Text.Trim();


                //file attachment 
                if(filename.Length>0)
                {
                    Attachment attachment = new Attachment(filename);
                    maildetails.Attachments.Add(attachment);

                }
                clientDetails.Send(maildetails);
                MessageBox.Show("Your mail has been sent");
                filename = "";



            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
