using System;
using System.Linq.Expressions;

namespace YEasyModel
{
    public class LinqCompileExt : LinqCompile
    {
        /// <summary>
        ///  Expression 转成 Join String
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="databaseType"></param>
        /// <returns></returns>
        public static string GetJoinByLambda<T1, T2>(Expression<Func<T1, T2, bool>> predicate, string databaseType)
        {
            return JoinExpression(predicate, databaseType);
        }


        /// <summary>
        ///  Expression 转成 Join String
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="databaseType"></param>
        /// <returns></returns>
        public static string GetJoinByLambda<T1, T2, T3>(Expression<Func<T1, T2, T3, bool>> predicate, string databaseType)
        {
           return JoinExpression(predicate, databaseType);
        }


        /// <summary>
        ///  Expression 转成 Join String
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="databaseType"></param>
        /// <returns></returns>
        public static string GetJoinByLambda<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, bool>> predicate, string databaseType)
        {
            return JoinExpression(predicate, databaseType);
        }


        /// <summary>
        ///  Expression 转成 Join String
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="databaseType"></param>
        /// <returns></returns>
        public static string GetJoinByLambda<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, bool>> predicate, string databaseType)
        {
            return JoinExpression(predicate, databaseType);
        }


        /// <summary>
        ///  Expression 转成 Join String
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="databaseType"></param>
        /// <returns></returns>
        public static string GetJoinByLambda<T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, bool>> predicate, string databaseType)
        {
            return JoinExpression(predicate, databaseType);
        }

        /// <summary>
        ///  Expression 转成 Join String
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="databaseType"></param>
        /// <returns></returns>
        public static string JoinExpression(Expression predicate, string databaseType)
        {
            try
            {
                bool withQuotationMarks = GetWithQuotationMarks(databaseType);

                ConditionBuilderExt conditionBuilder = new ConditionBuilderExt();
                conditionBuilder.SetIfWithQuotationMarks(withQuotationMarks); //字段是否加引号（PostGreSql,Oracle）
                conditionBuilder.SetDataBaseType(databaseType);
                conditionBuilder.BuildJoin(predicate);

                for (int i = 0; i < conditionBuilder.Arguments.Length; i++)
                {
                    object ce = conditionBuilder.Arguments[i];
                    if (ce == null)
                    {
                        conditionBuilder.Arguments[i] = "NULL";
                    }
                    else if (ce is string || ce is char)
                    {
                        if (ce.ToString().ToLower().Trim().IndexOf(@"in(") == 0 ||
                            ce.ToString().ToLower().Trim().IndexOf(@"not in(") == 0 ||
                             ce.ToString().ToLower().Trim().IndexOf(@" like '") == 0 ||
                            ce.ToString().ToLower().Trim().IndexOf(@"not like") == 0)
                        {
                            conditionBuilder.Arguments[i] = string.Format(" {0} ", ce.ToString());
                        }
                        else
                        {
                            //****************************************
                            conditionBuilder.Arguments[i] = string.Format("'{0}'", ce.ToString());
                        }
                    }
                    else if (ce is DateTime)
                    {
                        conditionBuilder.Arguments[i] = string.Format("'{0}'", ((DateTime)ce).ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    else if (ce is int || ce is long || ce is short || ce is decimal || ce is double || ce is float || ce is bool || ce is byte || ce is sbyte)
                    {
                        conditionBuilder.Arguments[i] = ce.ToString();
                    }
                    else if (ce is ValueType)
                    {
                        conditionBuilder.Arguments[i] = ce.ToString();
                    }
                    else
                    {
                        conditionBuilder.Arguments[i] = string.Format("'{0}'", ce.ToString());
                    }
                }

                string strWhere = string.Format(conditionBuilder.Condition, conditionBuilder.Arguments);
                strWhere = strWhere.Replace("<> NULL", "IS NOT NULL");
                strWhere = strWhere.Replace("= NULL", "IS NULL");

                return strWhere;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


}
