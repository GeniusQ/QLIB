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
        /// �����һ�м�¼��ת��һ��ʵ��
        /// </summary>
        /// <typeparam name="T">ʵ������</typeparam>
        /// <param name="row">���ݼ�¼</param>
        /// <returns></returns>
        public static T DataRowToEntity<T>(System.Data.DataRow row) where T:new()
        {
            T obj = new T();
            //TableAttribute tableAttr = GetTableAttribute<T>(obj);
            //if (row.Table.TableName.ToUpper() != tableAttr.FullName.ToUpper())
            //    throw new ApplicationException("������" + tableAttr.FullName + "�����" + row.Table.TableName + "����Ӧ��");

            //ȡ��T�����ԣ���row�е��ֶ�ƥ�䣬ʵ������ֵע��
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
                    {//����ֶβ����ڣ������쳣
                        Debug.WriteLine(ex.ToString());
                    }
                    pi.SetValue(obj, value, null);
                }
            }
            return obj;
        }

        /// <summary>
        /// ��ȡ����ʵ�����Table���Ա�ǩ
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

            //����
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
                    //ȡĳ�������У���ǰ���Ե�ֵ
                    object value = item.GetValue(obj, null);
                    values += "'" + value + "',";
                }
            }
            fileds = fileds.Substring(0, fileds.Length - 1);
            values = values.Substring(0, values.Length - 1);

            //ƴװSQL
            string sql = "INSERT INTO {0}({1}) VALUES({2})";
            sql = String.Format(sql, tableName, fileds, values);
            return sql;
        }

        public static string GenerateUpdateSql<T>(T obj)
        {
            Type type = obj.GetType();
            string tableName = GetTableName<T>(obj);

            //����
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

                    //ȡĳ�������У���ǰ���Ե�ֵ
                    object value = item.GetValue(obj, null);

                    if (a.IsKey)
                        conditions += " AND " + a.ColumnName + "='" + value + "'";
                    else
                        values += a.ColumnName + "='" + value + "',";
                }
            }
            values = values.Substring(0, values.Length - 1);

            //ƴװSQL
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

            //����
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

            //ƴװSQL
            string sql = "DELETE FROM {0} WHERE {1}";
            sql = String.Format(sql, tableName, conditions);
            return sql;
        }
    }
}
