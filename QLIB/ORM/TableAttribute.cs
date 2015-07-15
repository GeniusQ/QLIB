using System;
using System.Collections.Generic;
using System.Text;

namespace QLIB.ORM
{
    public class TableAttribute : Attribute
    {
        private string _fullName = String.Empty;
        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                _fullName = value;
            }
        }
    }
}
