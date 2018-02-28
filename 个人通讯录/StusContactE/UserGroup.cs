using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StusContactE
{
    public class UserGroup
    {
        private int group_id;
        private string group_name;
        public int Group_id
        {
            get { return group_id; }
            set { group_id = value; }
        }

        public string Group_name
        {
            get { return group_name; }
            set { group_name = value; }
        }
    }
}
