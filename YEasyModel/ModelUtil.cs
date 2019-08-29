using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace YEasyModel
{
    /// <summary>
    /// 实体模型工具类：数据转实体
    /// </summary>
    public class ModelUtil
    {
        /// <summary>
        /// DataTable数据表转实体列表
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="dataTable">数据表</param>
        /// <returns>列表实体</returns>
        public static List<T> DataTableParse<T>(DataTable dataTable)
        {
            // Type generic = typeof(List<T>);
            //generic = generic.MakeGenericType(new Type { generic.GetType() };
            List<T> list = null;//(List<T>)Activator.CreateInstance(generic);
            if (dataTable == null) return list;

            list = new List<T>();
            foreach (DataRow dr in dataTable.Rows)
            {
                var model = DataRowParse<T>(dr);
                if (model != null)
                    list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// DataTable数据表转实体
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="dataTable">数据表</param>
        /// <returns>单个实体</returns>
        public static T DataTableParseSingle<T>(DataTable dataTable)
        {
            T model = default(T);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                model = DataRowParse<T>(dataTable.Rows[0]);
            }

            return model;
        }

        /// <summary>
        /// DataRow数据行转实体
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="dr">DataRow数据行</param>
        /// <returns>单个实体</returns>
        public static T DataRowParse<T>(DataRow dr)
        {
            var model = Activator.CreateInstance<T>();
            Type t = model.GetType();//获得该类的Type
            foreach (PropertyInfo pi in t.GetProperties())
            {
                ModelAttribute attr = (ModelAttribute)Attribute.GetCustomAttribute(pi, typeof(ModelAttribute));// 属性值
                if (attr != null)
                {
                    if (attr.AutoReflect && dr.Table.Columns.Contains(attr.ColumnName))
                    {
                        object value = DBNull.Value;
                        if (!IsNullOrDBNull(dr[attr.ColumnName]))
                        {
                            value = TypeUtil.ChangeType(dr[attr.ColumnName], pi.PropertyType);
                        }
                        else
                        {
                            value = null;
                        }
                        pi.SetValue(model, value, null);
                    }
                }
                else if (dr.Table.Columns.Contains(pi.Name))
                {
                    object value = null;
                    if (!IsNullOrDBNull(dr[pi.Name]))
                    {
                        value = TypeUtil.ChangeType(dr[pi.Name], pi.PropertyType);
                    }
                    pi.SetValue(model, value, null);
                }
            }
            return model;
        }

        /// <summary>
        /// 实体列表转DataTable
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="modelList">实体列表</param>
        /// <returns></returns>
        public static DataTable ModelList2DataTable<T>(List<T> modelList)
        {
            if (modelList == null) return null;

            DataTable dataTable = new DataTable();
            var model = modelList[0];
            Type t = model.GetType();//获得该类的Type
            //创建表结构、设置字段信息
            foreach (PropertyInfo pi in t.GetProperties())
            {
                DataColumn dataColumn = new DataColumn();
                ModelAttribute attr = (ModelAttribute)Attribute.GetCustomAttribute(pi, typeof(ModelAttribute));// 属性值
                if (attr != null)
                {
                    dataColumn.ColumnName = attr.ColumnName;
                    dataColumn.DataType = TypeUtil.GetType(attr.ColumnType);
                    dataColumn.Caption = attr.ColumnTitle;
                    if (attr.IsPrimaryKey)
                    {
                        dataColumn.Unique = true;
                    }
                }
                else
                {
                    object value = pi.GetValue(model, null);

                    dataColumn.ColumnName = pi.Name;
                    if (value != null)
                    {
                        dataColumn.DataType = value.GetType();
                    }
                    //else
                    //{
                    //    dataColumn.DataType = typeof(string);
                    //}
                }
                dataTable.Columns.Add(dataColumn);
            }

            //添加行数据
            foreach (var m in modelList)
            {
                object[] values = new object[t.GetProperties().Length];
                int idx = 0;
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    object value = pi.GetValue(model, null);
                    //ModelAttribute attr = (ModelAttribute)Attribute.GetCustomAttribute(pi, typeof(ModelAttribute));// 属性值
                    //if (attr != null && !string.IsNullOrEmpty(attr.ColumnType))
                    //{
                    //    value = pi.GetValue(model, null);
                    //}
                    //else
                    //{
                    //    value = pi.GetValue(model, null);
                    //}
                    values[idx] = value;
                    idx++;
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public static bool IsNullOrDBNull(object obj)
        {
            return ((obj is DBNull) || string.IsNullOrEmpty(obj.ToString())) ? true : false;
        }
    }
}
