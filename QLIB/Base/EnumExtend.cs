using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLIB.Base
{
    public static class EnumExtend
    {
        public static string GetName(this Enum obj)
        {
            return Enum.GetName(obj.GetType(), obj);
        }

        public static List<EnumItem> ToList<T>()
        {

            var ts = new List<EnumItem>();
            string[] ns = Enum.GetNames(typeof(T));
            Array vs = Enum.GetValues(typeof(T));
            for (int i = 0; i < ns.Length; i++)
            {
                ts.Add(new EnumItem()
                {
                    Value = Convert.ToInt32(vs.GetValue(i)),
                    Name = ns[i]
                });
            }
            return ts;
        }
    }
}
