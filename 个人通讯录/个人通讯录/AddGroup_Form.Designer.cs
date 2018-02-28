namespace 个人通讯录
{
    partial class AddGroup_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddGroup_Form));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除选中ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.包括组和组联系人ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.仅删除组ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改分组名ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全部删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.qqTabControl1 = new Paway.Windows.Forms.QQTabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.textBox1 = new Paway.Windows.Forms.QQTextBoxEx();
            this.qqButton2 = new Paway.Windows.Forms.QQButton();
            this.qqButton1 = new Paway.Windows.Forms.QQButton();
            this.contextMenuStrip1.SuspendLayout();
            this.qqTabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除选中ToolStripMenuItem,
            this.修改分组名ToolStripMenuItem,
            this.全部删除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(110, 70);
            // 
            // 删除选中ToolStripMenuItem
            // 
            this.删除选中ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.包括组和组联系人ToolStripMenuItem,
            this.仅删除组ToolStripMenuItem});
            this.删除选中ToolStripMenuItem.Name = "删除选中ToolStripMenuItem";
            this.删除选中ToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.删除选中ToolStripMenuItem.Text = "删除选中";
            this.删除选中ToolStripMenuItem.Click += new System.EventHandler(this.删除选中ToolStripMenuItem_Click);
            // 
            // 包括组和组联系人ToolStripMenuItem
            // 
            this.包括组和组联系人ToolStripMenuItem.Name = "包括组和组联系人ToolStripMenuItem";
            this.包括组和组联系人ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.包括组和组联系人ToolStripMenuItem.Text = "删除";
            this.包括组和组联系人ToolStripMenuItem.ToolTipText = "慎重！！该操作会删除该组下的所有联系人！！";
            this.包括组和组联系人ToolStripMenuItem.Visible = false;
            this.包括组和组联系人ToolStripMenuItem.Click += new System.EventHandler(this.包括组和组联系人ToolStripMenuItem_Click);
            // 
            // 仅删除组ToolStripMenuItem
            // 
            this.仅删除组ToolStripMenuItem.Name = "仅删除组ToolStripMenuItem";
            this.仅删除组ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.仅删除组ToolStripMenuItem.Text = "仅删除组";
            this.仅删除组ToolStripMenuItem.ToolTipText = "该操作仅仅是删除组，不会将组下的联系人一同删除，会将他们放在默认分组里面(推荐)";
            this.仅删除组ToolStripMenuItem.Visible = false;
            this.仅删除组ToolStripMenuItem.Click += new System.EventHandler(this.包括组和组联系人ToolStripMenuItem_Click);
            // 
            // 修改分组名ToolStripMenuItem
            // 
            this.修改分组名ToolStripMenuItem.Name = "修改分组名ToolStripMenuItem";
            this.修改分组名ToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.修改分组名ToolStripMenuItem.Text = "修改分组名";
            this.修改分组名ToolStripMenuItem.Visible = false;
            this.修改分组名ToolStripMenuItem.Click += new System.EventHandler(this.包括组和组联系人ToolStripMenuItem_Click);
            // 
            // 全部删除ToolStripMenuItem
            // 
            this.全部删除ToolStripMenuItem.Name = "全部删除ToolStripMenuItem";
            this.全部删除ToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.全部删除ToolStripMenuItem.Text = "全部删除";
            this.全部删除ToolStripMenuItem.Visible = false;
            this.全部删除ToolStripMenuItem.Click += new System.EventHandler(this.包括组和组联系人ToolStripMenuItem_Click);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.GreenYellow;
            this.listBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(275, 150);
            this.listBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "分组名";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "page1.jpg");
            // 
            // qqTabControl1
            // 
            this.qqTabControl1.BackColor = System.Drawing.Color.Transparent;
            this.qqTabControl1.BaseColor = System.Drawing.Color.White;
            this.qqTabControl1.BorderColor = System.Drawing.Color.White;
            this.qqTabControl1.Controls.Add(this.tabPage3);
            this.qqTabControl1.Controls.Add(this.tabPage4);
            this.qqTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.qqTabControl1.ItemSize = new System.Drawing.Size(80, 32);
            this.qqTabControl1.Location = new System.Drawing.Point(0, 0);
            this.qqTabControl1.Name = "qqTabControl1";
            this.qqTabControl1.PageColor = System.Drawing.Color.White;
            this.qqTabControl1.SelectedIndex = 0;
            this.qqTabControl1.Size = new System.Drawing.Size(289, 196);
            this.qqTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.qqTabControl1.TabIndex = 7;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.White;
            this.tabPage3.Controls.Add(this.listBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 36);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(281, 156);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "已有分组";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.White;
            this.tabPage4.Controls.Add(this.textBox1);
            this.tabPage4.Controls.Add(this.qqButton2);
            this.tabPage4.Controls.Add(this.qqButton1);
            this.tabPage4.Controls.Add(this.label1);
            this.tabPage4.Location = new System.Drawing.Point(4, 36);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(281, 156);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "新建分组";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Transparent;
            this.textBox1.Icon = null;
            this.textBox1.IconIsButton = false;
            this.textBox1.IsPasswordChat = '\0';
            this.textBox1.IsSystemPasswordChar = false;
            this.textBox1.Lines = new string[0];
            this.textBox1.Location = new System.Drawing.Point(93, 26);
            this.textBox1.MaxLength = 32767;
            this.textBox1.MinimumSize = new System.Drawing.Size(20, 24);
            this.textBox1.Multiline = false;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = false;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBox1.Size = new System.Drawing.Size(142, 24);
            this.textBox1.TabIndex = 6;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBox1.WaterColor = System.Drawing.Color.DarkGray;
            this.textBox1.WaterText = "";
            this.textBox1.WordWrap = true;
            // 
            // qqButton2
            // 
            this.qqButton2.BackColor = System.Drawing.Color.Transparent;
            this.qqButton2.DownImage = ((System.Drawing.Image)(resources.GetObject("qqButton2.DownImage")));
            this.qqButton2.Image = null;
            this.qqButton2.IsShowBorder = true;
            this.qqButton2.Location = new System.Drawing.Point(156, 101);
            this.qqButton2.MoveImage = ((System.Drawing.Image)(resources.GetObject("qqButton2.MoveImage")));
            this.qqButton2.Name = "qqButton2";
            this.qqButton2.NormalImage = ((System.Drawing.Image)(resources.GetObject("qqButton2.NormalImage")));
            this.qqButton2.Size = new System.Drawing.Size(75, 28);
            this.qqButton2.TabIndex = 5;
            this.qqButton2.Text = "返回";
            this.qqButton2.UseVisualStyleBackColor = false;
            this.qqButton2.Click += new System.EventHandler(this.qqButton2_Click);
            // 
            // qqButton1
            // 
            this.qqButton1.BackColor = System.Drawing.Color.Transparent;
            this.qqButton1.DownImage = ((System.Drawing.Image)(resources.GetObject("qqButton1.DownImage")));
            this.qqButton1.Image = null;
            this.qqButton1.IsShowBorder = true;
            this.qqButton1.Location = new System.Drawing.Point(56, 101);
            this.qqButton1.MoveImage = ((System.Drawing.Image)(resources.GetObject("qqButton1.MoveImage")));
            this.qqButton1.Name = "qqButton1";
            this.qqButton1.NormalImage = ((System.Drawing.Image)(resources.GetObject("qqButton1.NormalImage")));
            this.qqButton1.Size = new System.Drawing.Size(75, 28);
            this.qqButton1.TabIndex = 4;
            this.qqButton1.Text = "确定";
            this.qqButton1.UseVisualStyleBackColor = false;
            this.qqButton1.Click += new System.EventHandler(this.qqButton1_Click);
            // 
            // addGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 196);
            this.Controls.Add(this.qqTabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "addGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加分组";
            this.Load += new System.EventHandler(this.addGroup_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.qqTabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除选中ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全部删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改分组名ToolStripMenuItem;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem 包括组和组联系人ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 仅删除组ToolStripMenuItem;
        private Paway.Windows.Forms.QQTabControl qqTabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private Paway.Windows.Forms.QQButton qqButton2;
        private Paway.Windows.Forms.QQButton qqButton1;
        private Paway.Windows.Forms.QQTextBoxEx textBox1;

    }
}