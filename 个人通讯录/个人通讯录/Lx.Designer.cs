namespace 个人通讯录
{
    partial class Lx
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lx));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtBody = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSubject = new Paway.Windows.Forms.QQTextBoxEx();
            this.txtUser = new Paway.Windows.Forms.QQTextBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.qqButton1 = new Paway.Windows.Forms.QQButton();
            this.benSend = new Paway.Windows.Forms.QQButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(-160, -125);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(418, 116);
            this.panel1.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "您的邮件地址";
            // 
            // btnAddFile
            // 
            this.btnAddFile.Location = new System.Drawing.Point(457, 223);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(78, 23);
            this.btnAddFile.TabIndex = 19;
            this.btnAddFile.Text = "添加附件";
            this.btnAddFile.UseVisualStyleBackColor = true;
            this.btnAddFile.Visible = false;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(426, 110);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(182, 88);
            this.listBox1.TabIndex = 18;
            this.listBox1.Visible = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(106, 52);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(214, 21);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Text = "txtPassword";
            // 
            // txtBody
            // 
            this.txtBody.BackColor = System.Drawing.Color.GreenYellow;
            this.txtBody.Location = new System.Drawing.Point(3, 20);
            this.txtBody.Multiline = true;
            this.txtBody.Name = "txtBody";
            this.txtBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBody.Size = new System.Drawing.Size(395, 204);
            this.txtBody.TabIndex = 9;
            this.txtBody.Text = "txtBody";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "您的邮箱密码";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.qqButton1);
            this.groupBox1.Controls.Add(this.benSend);
            this.groupBox1.Controls.Add(this.txtBody);
            this.groupBox1.Location = new System.Drawing.Point(9, 134);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(403, 263);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "您要说的话";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.txtSubject);
            this.groupBox2.Controls.Add(this.txtUser);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(9, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(403, 122);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "基本信息";
            // 
            // txtSubject
            // 
            this.txtSubject.BackColor = System.Drawing.Color.Transparent;
            this.txtSubject.Icon = null;
            this.txtSubject.IconIsButton = false;
            this.txtSubject.IsPasswordChat = '\0';
            this.txtSubject.IsSystemPasswordChar = false;
            this.txtSubject.Lines = new string[] {
        "txtSubject"};
            this.txtSubject.Location = new System.Drawing.Point(106, 83);
            this.txtSubject.MaxLength = 32767;
            this.txtSubject.MinimumSize = new System.Drawing.Size(20, 24);
            this.txtSubject.Multiline = false;
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.ReadOnly = false;
            this.txtSubject.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSubject.Size = new System.Drawing.Size(214, 24);
            this.txtSubject.TabIndex = 11;
            this.txtSubject.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtSubject.WaterColor = System.Drawing.Color.DarkGray;
            this.txtSubject.WaterText = "";
            this.txtSubject.WordWrap = true;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.Transparent;
            this.txtUser.Icon = null;
            this.txtUser.IconIsButton = false;
            this.txtUser.IsPasswordChat = '\0';
            this.txtUser.IsSystemPasswordChar = false;
            this.txtUser.Lines = new string[] {
        "txtUser"};
            this.txtUser.Location = new System.Drawing.Point(106, 20);
            this.txtUser.MaxLength = 32767;
            this.txtUser.MinimumSize = new System.Drawing.Size(20, 24);
            this.txtUser.Multiline = false;
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = false;
            this.txtUser.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUser.Size = new System.Drawing.Size(214, 24);
            this.txtUser.TabIndex = 10;
            this.txtUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtUser.WaterColor = System.Drawing.Color.DarkGray;
            this.txtUser.WaterText = "";
            this.txtUser.WordWrap = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "您的标题主题";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(455, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "附件";
            this.label6.Visible = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "未标题-1.png");
            // 
            // qqButton1
            // 
            this.qqButton1.BackColor = System.Drawing.Color.Transparent;
            this.qqButton1.DownImage = ((System.Drawing.Image)(resources.GetObject("qqButton1.DownImage")));
            this.qqButton1.Image = null;
            this.qqButton1.IsShowBorder = true;
            this.qqButton1.Location = new System.Drawing.Point(205, 229);
            this.qqButton1.MoveImage = ((System.Drawing.Image)(resources.GetObject("qqButton1.MoveImage")));
            this.qqButton1.Name = "qqButton1";
            this.qqButton1.NormalImage = ((System.Drawing.Image)(resources.GetObject("qqButton1.NormalImage")));
            this.qqButton1.Size = new System.Drawing.Size(75, 28);
            this.qqButton1.TabIndex = 16;
            this.qqButton1.Text = "算了,不说了";
            this.qqButton1.UseVisualStyleBackColor = false;
            this.qqButton1.Click += new System.EventHandler(this.qqButton1_Click);
            // 
            // benSend
            // 
            this.benSend.BackColor = System.Drawing.Color.Transparent;
            this.benSend.DownImage = ((System.Drawing.Image)(resources.GetObject("benSend.DownImage")));
            this.benSend.Image = null;
            this.benSend.IsShowBorder = true;
            this.benSend.Location = new System.Drawing.Point(76, 230);
            this.benSend.MoveImage = ((System.Drawing.Image)(resources.GetObject("benSend.MoveImage")));
            this.benSend.Name = "benSend";
            this.benSend.NormalImage = ((System.Drawing.Image)(resources.GetObject("benSend.NormalImage")));
            this.benSend.Size = new System.Drawing.Size(75, 28);
            this.benSend.TabIndex = 15;
            this.benSend.Text = "ok,说完了";
            this.benSend.UseVisualStyleBackColor = false;
            this.benSend.Click += new System.EventHandler(this.benSend_Click);
            // 
            // Lx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(419, 399);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnAddFile);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Lx";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "联系我...";
            this.Load += new System.EventHandler(this.Lx_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddFile;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtBody;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ImageList imageList1;
        private Paway.Windows.Forms.QQTextBoxEx txtUser;
        private Paway.Windows.Forms.QQTextBoxEx txtSubject;
        private Paway.Windows.Forms.QQButton benSend;
        private Paway.Windows.Forms.QQButton qqButton1;
    }
}