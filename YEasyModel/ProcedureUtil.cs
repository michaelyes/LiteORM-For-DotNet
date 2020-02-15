using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace YEasyModel
{
    /// <summary>
    /// 存储过程工具类
    /// </summary>
    public class ProcedureUtil
    {
        /// <summary>
        /// 获取类属性标识的存储过程名，如果没有存储过程标识，则取类名
        /// </summary>
        /// <param name="t">该类的Type</param>
        /// <returns>存储过程名</returns>
        public static string GetProcName(Type t)
        {
            string tbName;
            //获取类属性标识的表名，如果没有表标识，则取类名
            ProcAttribute[] tbAttr = (ProcAttribute[])t.GetCustomAttributes(typeof(ProcAttribute), true);
            if (tbAttr != null && tbAttr.Length > 0)
            {
                tbName = tbAttr[0].ProcName;
            }
            else
            {
                tbName = t.Name;
            }

            return tbName;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Execute<T>(T model)
        {
            return Execute<T>(ref model);
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Execute<T>(ref T model)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            Type t = model.GetType();//获得该类的Type
            string columns = string.Empty;//字段名 
            List<string> keyNames = new List<string>();//主键字段名
            string procName = GetProcName(t);//存储过程名
            bool has_ouput = false;

            GetParameter(model, t.GetProperties(), ref parameters, ref has_ouput);

            int rowsAffected;
            SqlParameter[] parameterArray = parameters.ToArray();
            int result = DbHelperSQL.RunProcedure(procName, parameterArray, out rowsAffected);
            if (has_ouput)
            {
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    try
                    {
                        ProcParaAttribute attr = (ProcParaAttribute)Attribute.GetCustomAttribute(pi, typeof(ProcParaAttribute));// 属性值
                        if (attr != null && attr.Is_Output)
                        {
                            var para = parameterArray.First(q => q.ParameterName == attr.ParaName);
                            if (para != null)
                                pi.SetValue(model, para.Value, null);
                        }
                    }
                    catch { }
                }
            }

            return result;
        }


        /// <summary>
        /// 执行存储过程-查询数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable Select<T>(T model)
        {
            return Select<T>(ref model);
        }

        /// <summary>
        /// 执行存储过程-查询数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable Select<T>(ref T model)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            Type t = model.GetType();//获得该类的Type
            string columns = string.Empty;//字段名 
            List<string> keyNames = new List<string>();//主键字段名
            string procName = GetProcName(t);//存储过程名
            bool has_ouput = false;

            GetParameter(model, t.GetProperties(), ref parameters, ref has_ouput);

            SqlParameter[] parameterArray = parameters.ToArray();
            DataTable result = DbHelperSQL.RunProcedure(procName, parameterArray, "table").Tables[0];
            if (has_ouput)
            {
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    try
                    {
                        ProcParaAttribute attr = (ProcParaAttribute)Attribute.GetCustomAttribute(pi, typeof(ProcParaAttribute));// 属性值
                        if (attr != null && attr.Is_Output)
                        {
                            var para = parameterArray.First(q => q.ParameterName == attr.ParaName);
                            if (para != null)
                                pi.SetValue(model, para.Value, null);
                        }
                    }
                    catch { }
                }
            }

            return result;
        }

        /// <summary>
        /// 根据实体对象字段生成参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="piArray"></param>
        /// <param name="parameters"></param>
        /// <param name="has_ouput"></param>
        private static void GetParameter<T>(T model, PropertyInfo[] piArray, ref List<SqlParameter> parameters, ref bool has_ouput)
        {
            foreach (PropertyInfo pi in piArray)
            {
                ProcParaAttribute attr = (ProcParaAttribute)Attribute.GetCustomAttribute(pi, typeof(ProcParaAttribute));// 属性值
                if (attr != null)
                {
                    if (attr.AutoReflect && !string.IsNullOrEmpty(attr.DataType))
                    {
                        object value = pi.GetValue(model, null);
                        if (attr.DataType == ColumnType.datetimeType &&
                            (null == value || (System.DateTime)value == default(System.DateTime)))
                        {
                            //空时间类型或空值，不写入数据库
                            //continue;
                            value = DBNull.Value;
                        }
                        if (attr.Is_Output)//输出参数
                        {
                            has_ouput = true;
                        }
                        parameters.Add(CreateSqlParameter(attr, value));
                    }
                }
            }
        }

        /// <summary>
        /// 创建SqlParameter
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">值</param>
        /// <param name="type">SqlDbType数据类型</param>
        /// <param name="length">最大长度</param>
        /// <returns></returns>
        public static SqlParameter CreateSqlParameter(ProcParaAttribute attr, object value)
        {
            SqlParameter parameter = new SqlParameter(attr.ParaName, value);
            if (!string.IsNullOrEmpty(attr.DataType))
            {
                object obj = null;
                //if (type == ColumnType.datetimeType)
                //    obj = SqlDbType.VarChar;
                //else
                obj = Enum.Parse(typeof(SqlDbType), attr.DataType, true);

                if (obj != null)
                {
                    SqlDbType sqlDbType = (SqlDbType)obj;
                    parameter.SqlDbType = sqlDbType;
                }
                if (attr.Length > 0)
                {
                    parameter.Size = attr.Length;
                }
                if (attr.Is_Output)
                {
                    parameter.Direction = ParameterDirection.Output;
                }
            }
            return parameter;
        }
    }
}
