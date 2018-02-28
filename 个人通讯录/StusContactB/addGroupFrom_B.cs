using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StusContactD;

namespace StusContactB
{
    public class addGroupFrom_B
    {
        addGroupFromInit_D agfd = new addGroupFromInit_D();
        //初始化列表框
        public void addGroupFromInit_B(ListBox listBox1)
        {
            agfd.addGroupFromInit_B(listBox1);
        }

        public Boolean addGroup_B(ListBox listBox1, string groupName)
        {
            if (agfd.addGroup_D(listBox1, groupName))
                return true;
            else
                return false;
        }

        //删除组和组下面的联系人
        public Boolean deleteAll_B(string groupName)
        {
            if (agfd.deleteAll_D(groupName))
                return true;
            else
                return false;
        }
    }
}
