using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace YEasyModel
{
    /// <summary>
    /// 用于单表CURD操作；
    /// </summary>
    public class ModelDAL : DALBase
    {
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Insert(object model)
        {
            return Insert(model, false);
        }

        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="writeIdentityKey">是否写入自增长ID</param>
        /// <returns></returns>
        public static int Insert(object model, bool writeIdentityKey)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            bool IsIdentity = false;//是否自增
            Type t = model.GetType();//获得该类的Type
            StringBuilder strSql = new StringBuilder();
            string columns = string.Empty;//字段名
            string values = string.Empty;//字段值对象    
            string tbName = GetTableName(t);//表名

            strSql.AppendFormat("insert into {0}(", tbName);

            foreach (PropertyInfo pi in t.GetProperties())
            {
                ModelAttribute attr = (ModelAttribute)Attribute.GetCustomAttribute(pi, typeof(ModelAttribute));// 属性值
                if (attr != null)
                {
                    if (attr.AutoReflect && !string.IsNullOrEmpty(attr.ColumnType))
                    {
                        object value = pi.GetValue(model, null);
                        if (attr.ColumnType == ColumnType.datetimeType &&
                            (null == value || (System.DateTime)value == default(System.DateTime)))
                        {
                            //空时间类型或空值，不写入数据库
                            continue;
                        }
                        if (attr.IsIdentity)
                        {
                            IsIdentity = true;
                            if (writeIdentityKey)
                            {
                                columns = columns + "," + attr.ColumnName;
                                values = values + ",@" + attr.ColumnName;
                            }
                        }
                        else
                        {
                            columns = columns + "," + attr.ColumnName;
                            values = values + ",@" + attr.ColumnName;
                        }
                        parameters.Add(CreateSqlParameter(attr.ColumnName, value, attr.ColumnType, attr.Size));

                    }
                }
            }

            strSql.Append(columns.Trim(new char[] { ',' }));
            strSql.Append(") values (");
            strSql.Append(values.Trim(new char[] { ',' }));
            strSql.Append(");");

            object obj = null;
            if (IsIdentity)
            {
                if (writeIdentityKey)
                {
                    strSql.Insert(0, string.Format("SET IDENTITY_INSERT {0} ON; ", tbName));
                    strSql.Append(string.Format("SET IDENTITY_INSERT {0} OFF; ", tbName));
                }
                else
                {
                    strSql.Append("select @@IDENTITY");
                }
                obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters.ToArray());
            }
            else
            {
                obj = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters.ToArray());
            }

            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 根据主键更新一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Update(object model)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            Type t = model.GetType();//获得该类的Type
            StringBuilder strSql = new StringBuilder();
            string columns = string.Empty;//字段名 
            List<string> keyNames = new List<string>();//主键字段名
            string tbName = GetTableName(t);//表名

            strSql.AppendFormat("update {0} set ", tbName);

            foreach (PropertyInfo pi in t.GetProperties())
            {
                ModelAttribute attr = (ModelAttribute)Attribute.GetCustomAttribute(pi, typeof(ModelAttribute));// 属性值
                if (attr != null)
                {
                    if (attr.AutoReflect && !string.IsNullOrEmpty(attr.ColumnType))
                    {
                        object value = pi.GetValue(model, null);
                        if (attr.ColumnType == ColumnType.datetimeType &&
                            (null == value || (System.DateTime)value == default(System.DateTime)))
                        {
                            //空时间类型或空值，不写入数据库
                            continue;
                        }
                        if (!attr.IsPrimaryKey)//跳过更新主键
                        {
                            columns = columns + "," + attr.ColumnName + "= @" + attr.ColumnName;
                        }
                        parameters.Add(CreateSqlParameter(attr.ColumnName, value, attr.ColumnType, attr.Size));
                    }
                }

                if (attr != null && attr.IsPrimaryKey)
                {
                    keyNames.Add(attr.ColumnName);
                }
            }

            strSql.Append(columns.Trim(new char[] { ',' }));
            strSql.Append(" where ");
            for (int i = 0; i < keyNames.Count; i++)
            {
                if (i > 0)
                {
                    strSql.Append(" and ");
                }
                strSql.AppendFormat("{0} = @{0}", keyNames[i]);
            }

            int result = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters.ToArray());

            return result;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="model">数据实体</param>
        /// <param name="filter">过滤条件</param>
        /// <param name="fields">更新字段</param>
        /// <returns></returns>
        public static int Update<T>(T model, Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] fields)
        {
            string strWhere = string.Empty;
            List<string> strFields = new List<string>();

            if (filter != null)
                strWhere = LinqCompile.GetWhereByLambda(filter, DataBaseType.SqlServer);

            if (fields != null)
            {
                foreach (var f in fields)
                {
                    var fieldName = ExpressionField.GetFieldName<T>(f);
                    strFields.Add(fieldName);
                }
            }

            List<SqlParameter> parameters = new List<SqlParameter>();
            Type t = model.GetType();//获得该类的Type
            StringBuilder strSql = new StringBuilder();
            string columns = string.Empty;//字段名 
            string tbName = GetTableName(t);//表名

            strSql.AppendFormat("update {0} set ", tbName);

            foreach (PropertyInfo pi in t.GetProperties())
            {
                ModelAttribute attr = (ModelAttribute)Attribute.GetCustomAttribute(pi, typeof(ModelAttribute));// 属性值
                if (attr != null && !attr.IsIdentity)//跳过更新主键字段
                {
                    if (strFields.Count == 0 || strFields.Contains(attr.ColumnName))
                    {
                        object value = pi.GetValue(model, null);
                        if (attr.ColumnType == ColumnType.datetimeType &&
                            (null == value || (System.DateTime)value == default(System.DateTime)))
                        {
                            //空时间类型或空值，不写入数据库
                            continue;
                        }
                        //else if(attr.ColumnType == ColumnType.datetimeType)
                        //{
                        //    value = ((System.DateTime)value).ToString("yyyy-MM-dd HH:mm:ss.fff");
                        //}
                        columns = columns + "," + attr.ColumnName + "= @" + attr.ColumnName;
                        parameters.Add(CreateSqlParameter(attr.ColumnName, value, attr.ColumnType, attr.Size));
                    }
                }
            }

            strSql.Append(columns.Trim(new char[] { ',' }));
            strSql.AppendFormat(" where {0}", strWhere);

            int result = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters.ToArray());

            return result;
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <typeparam name="T">表实体类</typeparam>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public static int Delete<T>(object keyValue)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            StringBuilder strSql = new StringBuilder();
            string tbName = string.Empty;//表名
            List<string> keyNames = new List<string>();//主键字段名

            CreateKeyParameter<T>(keyValue, ref parameters, ref tbName, ref keyNames);

            strSql.AppendFormat(" delete {0}", tbName);
            if (keyNames.Count > 0)
                strSql.Append(" where ");
            for (int i = 0; i < keyNames.Count; i++)
            {
                if (i > 0)
                {
                    strSql.Append(" and ");
                }
                strSql.AppendFormat("{0} = @{0}", keyNames[i]);
            }

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters.ToArray());
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <typeparam name="T">表实体类</typeparam>
        /// <param name="filter">查询条件：lambda条件过滤表达式</param>
        /// <returns></returns>
        public static int Delete<T>(Expression<Func<T, bool>> filter)
        {
            string strSql = CreateSqlByExpression<T>(filter, "delete from {0}");

            return DbHelperSQL.ExecuteSql(strSql);
        }

        /// <summary>
        /// SqlDataAdapter批量更新
        /// </summary>
        /// <typeparam name="T">表实体类</typeparam>
        /// <param name="modelList">列表实体</param>
        /// <returns>返回更新影响的行数</returns>
        public static int BatchUpdate<T>(List<T> modelList)
        {
            DataTable dataTable = ModelUtil.ModelList2DataTable<T>(modelList);

            return DbHelperSQL.BatchUpdate(dataTable.TableName, dataTable);
        }

        /// <summary>
        /// SqlBulkCopy批量提交数据[复制]
        /// </summary>
        /// <typeparam name="T">表实体类</typeparam>
        /// <param name="modelList">列表实体</param>
        /// <returns></returns>
        public static void SqlBulkCopy<T>(List<T> modelList)
        {
            DataTable dataTable = ModelUtil.ModelList2DataTable<T>(modelList);

            DbHelperSQL.SqlBulkCopyByDataTable(dataTable.TableName, dataTable);
        }

        /// <summary>
        /// 根据主键值查询获取一条数据
        /// </summary>
        /// <typeparam name="T">表实体类</typeparam>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public static T GetModel<T>(object keyValue)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            var model = Activator.CreateInstance<T>();
            StringBuilder strSql = new StringBuilder();
            string tbName = string.Empty;//表名
            List<string> keyNames = new List<string>();//主键字段名

            CreateKeyParameter<T>(keyValue, ref parameters, ref tbName, ref keyNames);

            strSql.AppendFormat("select * from {0}", tbName);
            if (keyNames.Count > 0)
                strSql.Append(" where ");
            for (int i = 0; i < keyNames.Count; i++)
            {
                if (i > 0)
                {
                    strSql.Append(" and ");
                }
                strSql.AppendFormat("{0} = @{0}", keyNames[i]);
            }

            var dt = DbHelperSQL.Query(strSql.ToString(), parameters.ToArray()).Tables[0];
            model = ModelUtil.DataTableParseSingle<T>(dt);

            return model;
        }

        /// <summary>
        /// 查询当前主键是否已存在
        /// </summary>
        /// <typeparam name="T">表实体类</typeparam>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public static bool Exists<T>(object keyValue)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            StringBuilder strSql = new StringBuilder();
            string tbName = string.Empty;//表名
            List<string> keyNames = new List<string>();//主键字段名

            CreateKeyParameter<T>(keyValue, ref parameters, ref tbName, ref keyNames);

            strSql.AppendFormat("select count(1) from {0}", tbName);
            if (keyNames.Count > 0)
                strSql.Append(" where ");
            for (int i = 0; i < keyNames.Count; i++)
            {
                if (i > 0)
                {
                    strSql.Append(" and ");
                }
                strSql.AppendFormat("{0} = @{0}", keyNames[i]);
            }

            return DbHelperSQL.Exists(strSql.ToString(), parameters.ToArray());
        }

        /// <summary>
        /// 查询当前字段值是否已存在
        /// </summary>
        /// <typeparam name="T">表实体类</typeparam>
        /// <param name="filter">查询条件：lambda条件过滤表达式</param>
        /// <returns></returns>
        public static bool Exists<T>(Expression<Func<T, bool>> filter)
        {
            string strSql = CreateSqlByExpression<T>(filter, "select count(1) from {0}");

            return DbHelperSQL.Exists(strSql);
        }

        /// <summary>
        /// 查询当前字段值是否已存在
        /// </summary>
        /// <typeparam name="T">表实体类</typeparam>
        /// <param name="filter">查询条件：lambda条件过滤表达式</param>
        /// <returns></returns>
        public static int GetRecordCount<T>(Expression<Func<T, bool>> filter)
        {
            string strSql = CreateSqlByExpression<T>(filter, "select count(1) from {0}");

            object obj = DbHelperSQL.GetSingle(strSql);
            if (obj == null)
                return 0;
            else
                return (int)obj;
        }

        /// <summary>
        /// 查询当前表最大的主键值
        /// </summary>
        /// <typeparam name="T">表实体类</typeparam>
        /// <returns></returns>
        public static object GetMaxID<T>()
        {
            Type t = typeof(T);//获得该类的Type
            string tbName = GetTableName(t);//表名
            string keyNames = "";

            foreach (PropertyInfo pi in t.GetProperties())
            {
                ModelAttribute attr = (ModelAttribute)Attribute.GetCustomAttribute(pi, typeof(ModelAttribute));// 属性值
                if (attr != null && attr.IsPrimaryKey)
                {
                    keyNames = attr.ColumnName;
                    break;
                }
            }
            string strSql = string.Format("select isnull(max({1}),0) from {0}", tbName, keyNames);

            return DbHelperSQL.GetSingle(strSql.ToString());
        }
               
        /// <summary>
        /// 查询当前指定字段的最大值
        /// </summary>
        /// <typeparam name="T">表实体类</typeparam>
        /// <param name="filter">查询条件：lambda条件过滤表达式</param>
        /// <param name="order">排序表达式</param>
        /// <param name="field">查询字段：lambda字段表达式</param>
        /// <returns>返回当前行的查询字段值</returns>
        public static object GetMaxValue<T>(Expression<Func<T, bool>> filter = null, OrderBy<T> order = null, Expression<Func<T, object>> field = null)
        {
            string strWhere = string.Empty;
            string strFields = string.Empty;
            string orderBy = string.Empty;

            if (field == null)
            {
                throw new Exception("请指定查询字段");
                //return null;
            }

            if (filter != null)
                strWhere = LinqCompile.GetWhereByLambda(filter, DataBaseType.SqlServer);

            if (order != null)
                orderBy = OrderByUtil.GetOrderBy<T>(order.GetOrderByList());

            strFields = ExpressionField.GetFieldName<T>(field);

            List<T> list = new List<T>();
            StringBuilder strSql = new StringBuilder();
            var model = Activator.CreateInstance<T>();
            Type t = model.GetType();//获得该类的Type
            string tbName = GetTableName(t);//表名

            strSql.AppendFormat("select isnull(max({0}),0) ", strFields);

            strSql.AppendFormat(" from {0} ", tbName);
            if (!string.IsNullOrEmpty(strWhere))
                strSql.AppendFormat(" where {0} ", strWhere);
            if (!string.IsNullOrEmpty(orderBy))
                strSql.Append(orderBy);

            return DbHelperSQL.GetSingle(strSql.ToString());
        }


        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <typeparam name="T">表实体类</typeparam>
        /// <param name="filter">查询条件：lambda条件过滤表达式</param>
        /// <param name="order">排序表达式</param>
        /// <param name="fields">查询字段：lambda字段表达式【可多组】</param>
        /// <returns>DataTable数据集</returns>
        public static DataTable SelectForDataTable<T>(Expression<Func<T, bool>> filter = null, OrderBy<T> order = null, params Expression<Func<T, object>>[] fields)
        {
            string strSql = SelectScript(filter, order, fields);
            var dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];

            return dt;
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <typeparam name="T">表实体类</typeparam>
        /// <param name="filter">查询条件：lambda条件过滤表达式</param>
        /// <param name="order">排序表达式</param>
        /// <param name="fields">查询字段：lambda字段表达式【可多组】</param>
        /// <returns>列表实体</returns>
        public static List<T> Select<T>(Expression<Func<T, bool>> filter = null, OrderBy<T> order = null, params Expression<Func<T, object>>[] fields)
        {
            string strSql = SelectScript(filter, order, fields);

            List<T> list = new List<T>();
            var dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
            list = ModelUtil.DataTableParse<T>(dt);

            return list;
        }


        /// <summary>
        /// 根据条件查询一条记录
        /// </summary>
        /// <typeparam name="T">表实体类</typeparam>
        /// <param name="filter">查询条件：lambda条件过滤表达式</param>
        /// <param name="order">排序表达式</param>
        /// <param name="fields">查询字段：lambda字段表达式【可多组】</param>
        /// <returns>列表实体</returns>
        public static T SelectSingleRecord<T>(Expression<Func<T, bool>> filter = null, OrderBy<T> order = null, params Expression<Func<T, object>>[] fields)
        {
            string strSql = SelectScript(filter, order, fields);

            List<T> list = new List<T>();
            var dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
            list = ModelUtil.DataTableParse<T>(dt);

            if (list != null && list.Count > 0)
                return list[0];
            else
                return default(T);
        }

        /// <summary>
        /// 根据条件查询第一条记录
        /// </summary>
        /// <typeparam name="T">表实体类</typeparam>
        /// <param name="topNumber">记录数：默认1条记录</param>
        /// <param name="filter">查询条件：lambda条件过滤表达式</param>
        /// <param name="order">排序表达式</param>
        /// <param name="fields">查询字段：lambda字段表达式【可多组】</param>
        /// <returns>列表实体</returns>
        public static T SelectTopRecord<T>(int topNumber = 1, Expression<Func<T, bool>> filter = null, OrderBy<T> order = null, params Expression<Func<T, object>>[] fields)
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


            List<T> list = new List<T>();
            StringBuilder strSql = new StringBuilder();
            var model = Activator.CreateInstance<T>();
            Type t = model.GetType();//获得该类的Type
            string tbName = GetTableName(t);//表名

            strSql.AppendFormat("select top {0} {1} ", topNumber, strFields);

            strSql.AppendFormat(" from {0} ", tbName);
            if (!string.IsNullOrEmpty(strWhere))
                strSql.AppendFormat(" where {0} ", strWhere);
            if (!string.IsNullOrEmpty(orderBy))
                strSql.Append(orderBy);

            var dt = DbHelperSQL.Query(strSql.ToString(), null).Tables[0];
            list = ModelUtil.DataTableParse<T>(dt);


            if (list != null && list.Count > 0)
                return list[0];
            else
                return default(T);
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
        public static List<T> Join<T, T1, T2>(Expression<Func<T1, T2, bool>> joinExpression, Expression<Func<T1, T2, bool>> filter = null, OrderBy<T> order = null,
            params Expression<Func<T1, T2, object>>[] fields)
        {
            string strSql = JoinScript(joinExpression, filter, order, fields);
            List<T> list = new List<T>();
            var dt = DbHelperSQL.Query(strSql).Tables[0];
            list = ModelUtil.DataTableParse<T>(dt);

            return list;
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
        /// <returns>DataTable数据集</returns>
        public static DataTable JoinForDataTable<T, T1, T2>(Expression<Func<T1, T2, bool>> joinExpression, Expression<Func<T1, T2, bool>> filter = null, OrderBy<T> order = null,
            params Expression<Func<T1, T2, object>>[] fields)
        {
            string strSql = JoinScript(joinExpression, filter, order, fields);
            var dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];

            return dt;
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
        public static List<T> Join<T, T1, T2, T3>(Expression<Func<T1, T2, T3, bool>> joinExpression, Expression<Func<T1, T2, T3, bool>> filter = null, OrderBy<T> order = null,
            params Expression<Func<T1, T2, T3, object>>[] fields)
        {
            string strSql = JoinScript(joinExpression, filter, order, fields);
            List<T> list = new List<T>();
            var dt = DbHelperSQL.Query(strSql).Tables[0];
            list = ModelUtil.DataTableParse<T>(dt);

            return list;
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
        /// <returns>DataTable数据集</returns>
        public static DataTable JoinForDataTable<T, T1, T2, T3>(Expression<Func<T1, T2, T3, bool>> joinExpression, Expression<Func<T1, T2, T3, bool>> filter = null, OrderBy<T> order = null,
            params Expression<Func<T1, T2, T3, object>>[] fields)
        {
            string strSql = JoinScript(joinExpression, filter, order, fields);
            var dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];

            return dt;
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
        public static List<T> Join<T, T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, bool>> joinExpression, Expression<Func<T1, T2, T3, T4, bool>> filter = null, OrderBy<T> order = null,
            params Expression<Func<T1, T2, T3, T4, object>>[] fields)
        {
            string strSql = JoinScript(joinExpression, filter, order, fields);
            List<T> list = new List<T>();
            var dt = DbHelperSQL.Query(strSql).Tables[0];
            list = ModelUtil.DataTableParse<T>(dt);

            return list;
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
        /// <returns>DataTable数据集</returns>
        public static DataTable JoinForDataTable<T, T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, bool>> joinExpression, Expression<Func<T1, T2, T3, T4, bool>> filter = null, OrderBy<T> order = null,
            params Expression<Func<T1, T2, T3, T4, object>>[] fields)
        {
            string strSql = JoinScript(joinExpression, filter, order, fields);
            var dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];

            return dt;
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
        public static List<T> Join<T, T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, bool>> joinExpression, Expression<Func<T1, T2, T3, T4, T5, bool>> filter = null, OrderBy<T> order = null,
            params Expression<Func<T1, T2, T3, T4, T5, object>>[] fields)
        {
            string strSql = JoinScript(joinExpression, filter, order, fields);
            List<T> list = new List<T>();
            var dt = DbHelperSQL.Query(strSql).Tables[0];
            list = ModelUtil.DataTableParse<T>(dt);

            return list;
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
        /// <returns>DataTable数据集</returns>
        public static DataTable JoinForDataTable<T, T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, bool>> joinExpression, Expression<Func<T1, T2, T3, T4, T5, bool>> filter = null, OrderBy<T> order = null,
            params Expression<Func<T1, T2, T3, T4, T5, object>>[] fields)
        {
            string strSql = JoinScript(joinExpression, filter, order, fields);
            var dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];

            return dt;
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
        public static List<T> Join<T, T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, bool>> joinExpression, Expression<Func<T1, T2, T3, T4, T5, T6, bool>> filter = null, OrderBy<T> order = null,
            params Expression<Func<T1, T2, T3, T4, T5, T6, object>>[] fields)
        {
            string strSql = JoinScript(joinExpression, filter, order, fields);
            List<T> list = new List<T>();
            var dt = DbHelperSQL.Query(strSql).Tables[0];
            list = ModelUtil.DataTableParse<T>(dt);

            return list;
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
        /// <returns>DataTable数据集</returns>
        public static DataTable JoinForDataTable<T, T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, bool>> joinExpression, Expression<Func<T1, T2, T3, T4, T5, T6, bool>> filter = null, OrderBy<T> order = null,
            params Expression<Func<T1, T2, T3, T4, T5, T6, object>>[] fields)
        {
            string strSql = JoinScript(joinExpression, filter, order, fields);
            var dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];

            return dt;
        }

        /// <summary>
        /// 查询当前指定字段的值
        /// </summary>
        /// <typeparam name="T">表实体类</typeparam>
        /// <param name="filter">查询条件：lambda条件过滤表达式</param>
        /// <param name="order">排序表达式</param>
        /// <param name="field">查询字段：lambda字段表达式</param>
        /// <returns>返回当前行的查询字段值</returns>
        public static object GetValue<T>(Expression<Func<T, bool>> filter = null, OrderBy<T> order = null, Expression<Func<T, object>> field = null)
        {
            string strWhere = string.Empty;
            string strFields = string.Empty;
            string orderBy = string.Empty;

            if (field == null)
            {
                throw new Exception("请指定查询字段");
                //return null;
            }

            if (filter != null)
                strWhere = LinqCompile.GetWhereByLambda(filter, DataBaseType.SqlServer);

            if (order != null)
                orderBy = OrderByUtil.GetOrderBy<T>(order.GetOrderByList());

            strFields = ExpressionField.GetFieldName<T>(field);

            List<T> list = new List<T>();
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

            return DbHelperSQL.GetSingle(strSql.ToString());
        }

        /// <summary>
        /// 执行sql脚本查询返回单个字段值
        /// </summary>
        /// <param name="sqlScript">sql脚本</param>
        /// <returns>字段值</returns>
        public static object GetValue(string sqlScript)
        {
            return DbHelperSQL.GetSingle(sqlScript);
        }

        /// <summary>
        /// 执行sql脚本查询数据
        /// </summary>
        /// <param name="sqlScript">sql脚本</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string sqlScript)
        {
            return DbHelperSQL.Query(sqlScript);
        }

        /// <summary>
        /// 执行sql脚本
        /// </summary>
        /// <param name="sqlScript">sql脚本</param>
        /// <returns></returns>
        public static int ExecuteSql(string sqlScript)
        {
            return DbHelperSQL.ExecuteSql(sqlScript);
        }
    }
}
