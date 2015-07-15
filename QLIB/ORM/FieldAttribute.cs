using System;
using System.Collections.Generic;
using System.Text;

namespace QLIB.ORM
{
    public class FieldAttribute : Attribute
    {
        private bool _isAutoIncrement = false;

        public bool IsAutoIncrement
        {
            get { return _isAutoIncrement; }
            set { _isAutoIncrement = value; }
        }

        private string _columnName = String.Empty;

        public string ColumnName
        {
            get { return _columnName; }
            set { _columnName = value; }
        }

        private bool _allowNull = false;

        public bool AllowNull
        {
            get { return _allowNull; }
            set { _allowNull = value; }
        }

        private object _defaultValue = null;

        public object DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

        private bool _isKey = false;

        public bool IsKey
        {
            get { return _isKey; }
            set { _isKey = value; }
        }        
    }
}
