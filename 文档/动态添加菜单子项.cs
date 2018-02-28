private ToolStripMenuItem toolStripMenuItem2;
private ToolStripMenuItem toolStripMenuItem3;

//初始化

toolStripMenuItem2 = new ToolStripMenuItem();//111
toolStripMenuItem3 = new ToolStripMenuItem();
toolStripMenuItem2.Text = "111";
this.toolStripMenuItem3.Text = "11111111111";
contextMenuStrip1.Items.AddRange(new ToolStripItem[] {toolStripMenuItem2});
toolStripMenuItem2.DropDownItems.AddRange(new ToolStripItem[] {toolStripMenuItem3});