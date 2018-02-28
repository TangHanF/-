ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
            DialogResult result;
            if (tsm.Text == "删除选中")
            {
                result = MessageBox.Show("注意!此操作会删除分组并将该分组下的所有联系人一起删除,是否继续?","删除提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                { 
                
                }
            
            }
            else if (tsm.Text == "删除全部")
            {
                result = MessageBox.Show("注意!此操作会删除所有分组和联系人,是否继续?", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                { 
                
                }
            }