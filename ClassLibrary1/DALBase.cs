using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace YEasyModel
{
    /// <summary>
    /// 基础方法类
    /// </summary>
    public class DALBase
    {
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <typeparam name="T">表实体类</typeparam>
        /// <param name="filter">查询条件：lambda条件过滤表达式</param>
        /// <param name="order">排序表达式</param>
        /// <param name="fields">查询字段：lambda字段表达式【可多组】</param>
        /// <returns>列表实体</returns>
        protected static string SelectScript<T>(Expression<Func<T, bool>> filter = null, OrderBy<T> order = null, params Expression<Func<T, object>>[] fields)
        {
            string strWhere = string.Empty;
            string strFields = string.Empty;
            string orderBy = string.Empty;

            if (filter != null)
                strWhere = LinqCompile.GetWhereByLambda(filter, DataBaseType.SqlServer);

            if (order != null)
                orderBy = OrderByUtil.GetOrderBy<T>(order.GetOrderByList());

            foreach (var f in fields)
            {
                var fieldName = ExpressionField.GetFieldName<T>(f);
                strFields = strFields + "," + fieldName;
            }
            if (string.IsNullOrEmpty(strFields))
                strFields = "*";
            else
                strFields = strFields.Trim(',');

            StringBuilder strSql = new StringBuilder();
            var model = Activator.CreateInstance<T>();
            Type t = model.GetType();//获得该类的Type
            string tbName = GetTableName(t);//表名

            strSql.AppendFormat("select {0} ", strFields);

            strSql.AppendFormat(" from {0} ", tbName);
            if (!string.IsNullOrEmpty(strWhere))
                strSql.AppendFormat(" where {0} ", strWhere);
            if (!string.IsNullOrEmpty(orderBy))
                strSql.Append(orderBy);

            return strSql.ToString();
        }


        /// <summary>
        /// 多表联合查询(left join)
        /// </summary>
        /// <typeparam name="T">返回的数据实体类型</typeparam>
        /// <typeparam name="T1">表1</typeparam>
        /// <typeparam name="T2">表2</typeparam>
        /// <param name="joinExpression">联接条件</param>
        /// <param name="filter">查询条件</param>
        /// <param name="order">排序</param>
        /// <param name="fields">查询字段</param>
        /// <returns>数据实体列表</returns>
        protected static string JoinScript<T, T1, T2>(Expression<Func<T1, T2, bool>> joinExpression, Expression<Func<T1, T2, bool>> filter = null, OrderBy<T> order = null,
            params Expression<Func<T1, T2, object>>[] fields)
        {
            string join = string.Empty;
            string strWhere = string.Empty;
            string strFields = string.Empty;
            string orderBy = string.Empty;

            join = LinqCompileExt.GetJoinByLambda(joinExpression, DataBaseType.SqlServer);

            if (filter != null)
                strWhere = LinqCompileExt.GetJoinByLambda(filter, DataBaseType.SqlServer);

            if (order != null)
                orderBy = OrderByUtil.GetOrderBy<T>(order.GetOrderByList());

            foreach (var f in fields)
            {
                var fieldName = ExpressionField.GetFieldName<T1, T2>(f);
                strFields = strFields + "," + fieldName;
            }
            if (string.IsNullOrEmpty(strFields))
                strFields = "*";
            else
                strFields = strFields.Trim(',');

            StringBuilder strSql = new StringBuilder();
            var model = Activator.CreateInstance<T>();
            Type[] agrs = new Type[] { typeof(T1), typeof(T2) };//获得该类的Type
            string tbName = JoinTable(joinExpression.Parameters, agrs);//表名

            strSql.AppendFormat("select {0} ", strFields);
            strSql.AppendFormat(" from {0} ", tbName);
            strSql.AppendFormat(" on {0}", join);
            if (!string.IsNullOrEmpty(strWhere))
                strSql.AppendFormat(" where {0} ", strWhere);
            if (!string.IsNullOrEmpty(orderBy))
                strSql.Append(orderBy);

            return strSql.ToString();
        }


        /// <summary>
        /// 多表联合查询(left join)
        /// </summary>
        /// <typeparam name="T">返回的数据实体类型</typeparam>
        /// <typeparam name="T1">表1</typeparam>
        /// <typeparam name="T2">表2</typeparam>
        /// <typeparam name="T3">表3</typeparam>
        /// <param name="joinExpression">联接条件</param>
        /// <param name="filter">查询条件</param>
        /// <param name="order">排序</param>
        /// <param name="fields">查询字段</param>
        /// <returns>数据实体列表</returns>
        protected static string JoinScript<T, T1, T2, T3>(Expression<Func<T1, T2, T3, bool>> joinExpression, Expression<Func<T1, T2, T3, bool>> filter = null, OrderBy<T> order = null,
            params Expression<Func<T1, T2, T3, object>>[] fields)
        {
            string join = string.Empty;
            string strWhere = string.Empty;
            string strFields = string.Empty;
            string orderBy = string.Empty;

            join = LinqCompileExt.GetJoinByLambda(joinExpression, DataBaseType.SqlServer);

            if (filter != null)
                strWhere = LinqCompileExt.GetJoinByLambda(filter, DataBaseType.SqlServer);

            if (order != null)
                orderBy = OrderByUtil.GetOrderBy<T>(order.GetOrderByList());

            foreach (var f in fields)
            {
                var fieldName = ExpressionField.GetFieldName<T1, T2, T3>(f);
                strFields = strFields + "," + fieldName;
            }
            if (string.IsNullOrEmpty(strFields))
                strFields = "*";
            else
                strFields = strFields.Trim(',');

            StringBuilder strSql = new StringBuilder();
            var model = Activator.CreateInstance<T>();
            Type[] agrs = new Type[] { typeof(T1), typeof(T2), typeof(T3) };//获得该类的Type
            string tbName = JoinTable(joinExpression.Parameters, agrs);//表名

            strSql.AppendFormat("select {0} ", strFields);
            strSql.AppendFormat(" from {0} ", tbName);
            strSql.AppendFormat(" on {0}", join);
            if (!string.IsNullOrEmpty(strWhere))
                strSql.AppendFormat(" where {0} ", strWhere);
            if (!string.IsNullOrEmpty(orderBy))
                strSql.Append(orderBy);

            return strSql.ToString();
        }


        /// <summary>
        /// 多表联合查询(left join)
        /// </summary>
        /// <typeparam name="T">返回的数据实体类型</typeparam>
        /// <typeparam name="T1">表1</typeparam>
        /// <typeparam name="T2">表2</typeparam>
        /// <typeparam name="T3">表3</typeparam>
        /// <typeparam name="T4">表4</typeparam>
        /// <param name="joinExpression">联接条件</param>
        /// <param name="filter">查询条件</param>
        /// <param name="order">排序</param>
        /// <param name="fields">查询字段</param>
        /// <returns>数据实体列表</returns>
        protected static string JoinScript<T, T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, bool>> joinExpression, Expression<Func<T1, T2, T3, T4, bool>> filter = null, OrderBy<T> order = null,
            params Expression<Func<T1, T2, T3, T4, object>>[] fields)
        {
            string join = string.Empty;
            string strWhere = string.Empty;
            string strFields = string.Empty;
            string orderBy = string.Empty;

            join = LinqCompileExt.GetJoinByLambda(joinExpression, DataBaseType.SqlServer);

            if (filter != null)
                strWhere = LinqCompileExt.GetJoinByLambda(filter, DataBaseType.SqlServer);

            if (order != null)
                orderBy = OrderByUtil.GetOrderBy<T>(order.GetOrderByList());

            foreach (var f in fields)
            {
                var fieldName = ExpressionField.GetFieldName<T1, T2, T3, T4>(f);
                strFields = strFields + "," + fieldName;
            }
            if (string.IsNullOrEmpty(strFields))
                strFields = "*";
            else
                strFields = strFields.Trim(',');

            StringBuilder strSql = new StringBuilder();
            var model = Activator.CreateInstance<T>();
            Type[] agrs = new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) };//获得该类的Type
            string tbName = JoinTable(joinExpression.Parameters, agrs);//表名

            strSql.AppendFormat("select {0} ", strFields);
            strSql.AppendFormat(" from {0} ", tbName);
            strSql.AppendFormat(" on {0}", join);
            if (!string.IsNullOrEmpty(strWhere))
                strSql.AppendFormat(" where {0} ", strWhere);
            if (!string.IsNullOrEmpty(orderBy))
                strSql.Append(orderBy);

            return strSql.ToString();
        }


        /// <summary>
        /// 多表联合查询(left join)
        /// </summary>
        /// <typeparam name="T">返回的数据实体类型</typeparam>
        /// <typeparam name="T1">表1</typeparam>
        /// <typeparam name="T2">表2</typeparam>
        /// <typeparam name="T3">表3</typeparam>
        /// <typeparam name="T4">表4</typeparam>
        /// <typeparam name="T5">表5</typeparam>
        /// <param name="joinExpression">联接条件</param>
        /// <param name="filter">查询条件</param>
        /// <param name="order">排序</param>
        /// <param name="fields">查询字段</param>
        /// <returns>数据实体列表</returns>
        protected static string JoinScript<T, T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, bool>> joinExpression, Expression<Func<T1, T2, T3, T4, T5, bool>> filter = null, OrderBy<T> order = null,
            params Expression<Func<T1, T2, T3, T4, T5, object>>[] fields)
        {
            string join = string.Empty;
            string strWhere = string.Empty;
            string strFields = string.Empty;
            string orderBy = string.Empty;

            join = LinqCompileExt.GetJoinByLambda(joinExpression, DataBaseType.SqlServer);

            if (filter != null)
                strWhere = LinqCompileExt.GetJoinByLambda(filter, DataBaseType.SqlServer);

            if (order != null)
                orderBy = OrderByUtil.GetOrderBy<T>(order.GetOrderByList());

            foreach (var f in fields)
            {
                var fieldName = ExpressionField.GetFieldName<T1, T2, T3, T4, T5>(f);
                strFields = strFields + "," + fieldName;
            }
            if (string.IsNullOrEmpty(strFields))
                strFields = "*";
            else
                strFields = strFields.Trim(',');

            StringBuilder strSql = new StringBuilder();
            var model = Activator.CreateInstance<T>();
            Type[] agrs = new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5) };//获得该类的Type
            string tbName = JoinTable(joinExpression.Parameters, agrs);//表名

            strSql.AppendFormat("select {0} ", strFields);
            strSql.AppendFormat(" from {0} ", tbName);
            strSql.AppendFormat(" on {0}", join);
            if (!string.IsNullOrEmpty(strWhere))
                strSql.AppendFormat(" where {0} ", strWhere);
            if (!string.IsNullOrEmpty(orderBy))
                strSql.Append(orderBy);

            return strSql.ToString();
        }

        /// <summary>
        /// 多表联合查询(left join)
        /// </summary>
        /// <typeparam name="T">返回的数据实体类型</typeparam>
        /// <typeparam name="T1">表1</typeparam>
        /// <typeparam name="T2">表2</typeparam>
        /// <typeparam name="T3">表3</typeparam>
        /// <typeparam name="T4">表4</typeparam>
        /// <typeparam name="T5">表5</typeparam>
        /// <typeparam name="T6">表6</typeparam>
        /// <param name="joinExpression">联接条件</param>
        /// <param name="filter">查询条件</param>
        /// <param name="order">排序</param>
        /// <param name="fields">查询字段</param>
        /// <returns>数据实体列表</returns>
        protected static string JoinScript<T, T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, bool>> joinExpression, Expression<Func<T1, T2, T3, T4, T5, T6, bool>> filter = null, OrderBy<T> order = null,
            params Expression<Func<T1, T2, T3, T4, T5, T6, object>>[] fields)
        {
            string join = string.Empty;
            string strWhere = string.Empty;
            string strFields = string.Empty;
            string orderBy = string.Empty;

            join = LinqCompileExt.GetJoinByLambda(joinExpression, DataBaseType.SqlServer);

            if (filter != null)
                strWhere = LinqCompileExt.GetJoinByLambda(filter, DataBaseType.SqlServer);

            if (order != null)
                orderBy = OrderByUtil.GetOrderBy<T>(order.GetOrderByList());

            foreach (var f in fields)
            {
                var fieldName = ExpressionField.GetFieldName<T1, T2, T3, T4, T5, T6>(f);
                strFields = strFields + "," + fieldName;
            }
            if (string.IsNullOrEmpty(strFields))
                strFields = "*";
            else
                strFields = strFields.Trim(',');

            StringBuilder strSql = new StringBuilder();
            var model = Activator.CreateInstance<T>();
            Type[] agrs = new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6) };//获得该类的Type
            string tbName = JoinTable(joinExpression.Parameters, agrs);//表名

            strSql.AppendFormat("select {0} ", strFields);
            strSql.AppendFormat(" from {0} ", tbName);
            strSql.AppendFormat(" on {0}", join);
            if (!string.IsNullOrEmpty(strWhere))
                strSql.AppendFormat(" where {0} ", strWhere);
            if (!string.IsNullOrEmpty(orderBy))
                strSql.Append(orderBy);

            return strSql.ToString();
        }

        /// <summary>
        /// 根据表达式生成sql语句
        /// </summary>
        /// <typeparam name="T">表实体类</typeparam>
        /// <param name="filter">查询条件：lambda条件过滤表达式</param>
        /// <param name="dml">数据操作:select * from {0};delete {0} </param>
        /// <returns></returns>
        protected static string CreateSqlByExpression<T>(Expression<Func<T, bool>> filter, string dml)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            StringBuilder strSql = new StringBuilder();
            string strWhere = string.Empty;
            strWhere = LinqCompile.GetWhereByLambda(filter, DataBaseType.SqlServer);
            var model = Activator.CreateInstance<T>();
            Type t = model.GetType();//获得该类的Type
            string tbName = GetTableName(t);//表名

            strSql.AppendFormat(dml, tbName);
            if (!string.IsNullOrEmpty(strWhere))
                strSql.AppendFormat(" where {0}", strWhere);

            return strSql.ToString();
        }

        /// <summary>
        /// 创建SqlParameter
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">值</param>
        /// <param name="type">SqlDbType数据类型</param>
        /// <param name="length">最大长度</param>
        /// <returns></returns>
        protected static SqlParameter CreateSqlParameter(string parameterName, object value, string type = null, int length = 0)
        {
            SqlParameter parameter = new SqlParameter(parameterName, value);
            if (!string.IsNullOrEmpty(type))
            {
                object obj = null;
                //if (type == ColumnType.datetimeType)
                //    obj = SqlDbType.VarChar;
                //else
                obj = Enum.Parse(typeof(SqlDbType), type, true);

                if (obj != null)
                {
                    SqlDbType sqlDbType = (SqlDbType)obj;
                    parameter.SqlDbType = sqlDbType;
                    if (length > 0)
                    {
                        parameter.Size = length;
                    }
                }
            }
            return parameter;
        }

        /// <summary>
        /// 根据模型创建主键参数、表名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyValue"></param>
        /// <param name="parameters"></param>
        /// <param name="tbName"></param>
        /// <param name="keyNames"></param>
        protected static void CreateKeyParameter<T>(object keyValue, ref List<SqlParameter> parameters, ref string tbName, ref List<string> keyNames)
        {
            Type t = typeof(T);//获得该类的Type
            tbName = GetTableName(t);

            foreach (PropertyInfo pi in t.GetProperties())
            {
                ModelAttribute attr = (ModelAttribute)Attribute.GetCustomAttribute(pi, typeof(ModelAttribute));// 属性值
                if (attr != null)
                {
                    if (attr.AutoReflect && !string.IsNullOrEmpty(attr.ColumnType))
                    {
                        if (attr.IsPrimaryKey)
                        {
                            keyNames.Add(attr.ColumnName);
                            parameters.Add(CreateSqlParameter(attr.ColumnName, keyValue, attr.ColumnType, attr.Size));
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取类属性标识的表名，如果没有表标识，则取类名
        /// </summary>
        /// <param name="t">该类的Type</param>
        /// <returns></returns>
        public static string GetTableName(Type t)
        {
            string tbName;
            //获取类属性标识的表名，如果没有表标识，则取类名
            TableAttribute[] tbAttr = (TableAttribute[])t.GetCustomAttributes(typeof(TableAttribute), true);
            if (tbAttr != null && tbAttr.Length > 0)
            {
                tbName = tbAttr[0].TableName;
            }
            else
            {
                tbName = t.Name;
            }

            return tbName;
        }

        /// <summary>
        /// 获取join联接表
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="agrs"></param>
        /// <returns></returns>
        protected static string JoinTable(ReadOnlyCollection<ParameterExpression> parameters, params Type[] agrs)
        {
            string join = string.Empty;
            for (int i = 0; i < agrs.Length; i++)
            {
                Type t = agrs[i];
                string tableName = GetTableName(t);
                if (parameters.Count > i)
                {
                    tableName = tableName + " as " + parameters[i];
                }

                if (i > 0)
                    join = join + " left join " + tableName;
                else
                    join = tableName;
            }

            return join;
        }

    }
}
