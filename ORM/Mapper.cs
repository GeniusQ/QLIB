using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace QLIB.ORM
{
    public class Mapper
    {
        /// <summary>
        /// 将表的一行记录，转成一个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="row">数据记录</param>
        /// <returns></returns>
        public static T DataRowToEntity<T>(System.Data.DataRow row) where T:new()
        {
            T obj = new T();
            //TableAttribute tableAttr = GetTableAttribute<T>(obj);
            //if (row.Table.TableName.ToUpper() != tableAttr.FullName.ToUpper())
            //    throw new ApplicationException("泛型类" + tableAttr.FullName + "与表名" + row.Table.TableName + "不对应。");

            //取出T的属性，于row中的字段匹配，实现属性值注入
            PropertyInfo[] pis = obj.GetType().GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                object[] fieldAttrs = pi.GetCustomAttributes(typeof(FieldAttribute), false);
                if (fieldAttrs.Length > 0)
                {
                    FieldAttribute fieldAttr = fieldAttrs[0] as FieldAttribute;
                    string fieldName = fieldAttr.ColumnName.ToUpper();
                    object value = fieldAttr.DefaultValue;
                    try
                    {
                        if (row[fieldName] != null)
                            value = row[fieldName];
                    }
                    catch (Exception ex)
                    {//如果字段不存在，会抛异常
                        Debug.WriteLine(ex.ToString());
                    }
                    pi.SetValue(obj, value, null);
                }
            }
            return obj;
        }

        /// <summary>
        /// 获取任意实体类的Table特性标签
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static TableAttribute GetTableAttribute<T>(T obj)
        {
            object[] tableAttrs = obj.GetType().GetCustomAttributes(false);
            if (tableAttrs.Length < 1)
                return null;
            else
            {
                TableAttribute tableAttr = tableAttrs[0] as TableAttribute;
                return tableAttr;
            }
        }

        public static string GenerateInsertSql<T>(T obj)
        {
            Type type = obj.GetType();
            string tableName = GetTableName<T>(obj);

            //属性
            string fileds = "";
            string values = "";
            PropertyInfo[] ps = type.GetProperties();
            foreach (PropertyInfo item in ps)
            {
                object[] attrs = item.GetCustomAttributes(typeof(FieldAttribute),false);
                if (attrs.Length > 0)
                {
                    FieldAttribute a = attrs[0] as FieldAttribute;
                    if (a.IsAutoIncrement == true)
                        continue;

                    fileds += a.ColumnName + ",";
                    //取某个对象中，当前属性的值
                    object value = item.GetValue(obj, null);
                    values += "'" + value + "',";
                }
            }
            fileds = fileds.Substring(0, fileds.Length - 1);
            values = values.Substring(0, values.Length - 1);

            //拼装SQL
            string sql = "INSERT INTO {0}({1}) VALUES({2})";
            sql = String.Format(sql, tableName, fileds, values);
            return sql;
        }

        public static string GenerateUpdateSql<T>(T obj)
        {
            Type type = obj.GetType();
            string tableName = GetTableName<T>(obj);

            //条件
            string conditions = "1=1";
            string values = "";
            PropertyInfo[] ps = type.GetProperties();
            foreach (PropertyInfo item in ps)
            {
                object[] attrs = item.GetCustomAttributes(typeof(FieldAttribute), false);
                if (attrs.Length > 0)
                {
                    FieldAttribute a = attrs[0] as FieldAttribute;
                    if (a.IsAutoIncrement && a.IsKey==false)
                        continue;

                    //取某个对象中，当前属性的值
                    object value = item.GetValue(obj, null);

                    if (a.IsKey)
                        conditions += " AND " + a.ColumnName + "='" + value + "'";
                    else
                        values += a.ColumnName + "='" + value + "',";
                }
            }
            values = values.Substring(0, values.Length - 1);

            //拼装SQL
            string sql = "UPDATE {0} SET {1} WHERE {2}";
            sql = String.Format(sql, tableName, values, conditions);
            return sql;
        }

        private static string GetTableName<T>(T obj)
        {
            TableAttribute tableAttr = GetTableAttribute<T>(obj);

            return tableAttr.FullName;
        }

        public static string GenerateDeleteSql<T>(T obj)
        {
            Type type = obj.GetType();
            string tableName = GetTableName<T>(obj);

            //条件
            string conditions = "1=1";
            PropertyInfo[] ps = type.GetProperties();
            foreach (PropertyInfo item in ps)
            {
                object[] attrs = item.GetCustomAttributes(typeof(FieldAttribute), false);
                if (attrs.Length > 0)
                {
                    FieldAttribute a = attrs[0] as FieldAttribute;

                    if (a.IsKey)
                    {
                        object value = item.GetValue(obj, null);
                        conditions += " AND " + a.ColumnName + "='" + value + "'";
                    }
                }
            }

            //拼装SQL
            string sql = "DELETE FROM {0} WHERE {1}";
            sql = String.Format(sql, tableName, conditions);
            return sql;
        }
    }
}
