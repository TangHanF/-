using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StusContactE;
using StusContactD;

namespace StusContactB
{
    public class addUsersForm_B
    {
        MainFormTools_D mftd=new MainFormTools_D();
        public Boolean addUsers_B(ContactUsers cu, Boolean isImg)
        {
            if (mftd.addUser_D(cu, isImg))
                return true;
            else
                return false;
        }
    }
}
