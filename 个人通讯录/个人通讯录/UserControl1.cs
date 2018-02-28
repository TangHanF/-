using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace 个人通讯录
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private Boolean flag = false;
        private void benSend_Click(object sender, EventArgs e)
        {
            if (txtUser.Text != string.Empty && txtPassword.Text != string.Empty && txtSubject.Text != string.Empty)
            {
                MailAddress from = new MailAddress(txtUser.Text.Trim());
                MailAddress to = new MailAddress("992470084@qq.com");
                string subject = txtSubject.Text.Trim();
                string body = txtBody.Text;
                List<string> attachFiles = new List<string>();
                MailMessage message = CreateMail(from, to, subject, body);
                SendMail(from, txtPassword.Text, message);
                if (flag)
                {
                    MessageBox.Show("OK！感谢您的意见反馈昂！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                }
            }
            else
                MessageBox.Show("信息输入不完整！");
        }
        private MailMessage CreateMail(MailAddress from, MailAddress to, string subject, string body)
        {
            MailMessage message = new MailMessage(from, to);
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.Subject = subject;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Body = body;
            return message;
        }

        public void SendMail(MailAddress from, string password, MailMessage message)
        {
            SmtpClient client = new SmtpClient("smtp." + from.Host);
            client.UseDefaultCredentials = false;//不使用默认凭证
            client.Credentials = new NetworkCredential(from.Address, password);//指定用户名、密码
            client.DeliveryMethod = SmtpDeliveryMethod.Network;//邮件通过网络发送到服务器
            try
            {
                client.Send(message);//发送邮件
                flag = true;

            }
            catch (SmtpException ex)
            {
                MessageBox.Show("发送失败,参考：" + ex.Message);
            }
            finally
            {
                message.Dispose();
            }

        }
    }
}
