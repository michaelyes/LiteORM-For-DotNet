using System;
using System.Linq.Expressions;
using System.Reflection;

namespace YEasyModel
{
    /// <summary>
    /// 获取表达式字段名类
    /// </summary>
    public class ExpressionField
    {
        /// <summary>
        /// 根据表达式获取字段名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldExpression">表达式</param>
        /// <returns></returns>
        public static string GetFieldName<T>(Expression<Func<T, object>> fieldExpression)
        {
            string fieldName = GetFieldName(fieldExpression.Body);

            return fieldName;
        }

        /// <summary>
        /// 根据表达式获取字段名
        /// </summary>
        /// <typeparam name="T1">表1</typeparam>
        /// <typeparam name="T2">表2</typeparam>
        /// <param name="fieldExpression">表达式</param>
        /// <returns></returns>
        public static string GetFieldName<T1, T2>(Expression<Func<T1, T2, object>> fieldExpression)
        {
            string fieldName = GetFieldName(fieldExpression.Body);

            return fieldName;
        }

        /// <summary>
        /// 根据表达式获取字段名
        /// </summary>
        /// <typeparam name="T1">表1</typeparam>
        /// <typeparam name="T2">表2</typeparam>
        /// <typeparam name="T3">表3</typeparam>
        /// <param name="fieldExpression">表达式</param>
        /// <returns></returns>
        public static string GetFieldName<T1, T2, T3>(Expression<Func<T1, T2, T3, object>> fieldExpression)
        {
            string fieldName = GetFieldName(fieldExpression.Body);

            return fieldName;
        }

        /// <summary>
        /// 根据表达式获取字段名
        /// </summary>
        /// <typeparam name="T1">表1</typeparam>
        /// <typeparam name="T2">表2</typeparam>
        /// <typeparam name="T3">表3</typeparam>
        /// <typeparam name="T4">表4</typeparam>
        /// <param name="fieldExpression">表达式</param>
        /// <returns></returns>
        public static string GetFieldName<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, object>> fieldExpression)
        {
            string fieldName = GetFieldName(fieldExpression.Body);

            return fieldName;
        }

        /// <summary>
        /// 根据表达式获取字段名
        /// </summary>
        /// <typeparam name="T1">表1</typeparam>
        /// <typeparam name="T2">表2</typeparam>
        /// <typeparam name="T3">表3</typeparam>
        /// <typeparam name="T4">表4</typeparam>
        /// <typeparam name="T5">表5</typeparam>
        /// <param name="fieldExpression">表达式</param>
        /// <returns></returns>
        public static string GetFieldName<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, object>> fieldExpression)
        {
            string fieldName = GetFieldName(fieldExpression.Body);

            return fieldName;
        }

        /// <summary>
        /// 根据表达式获取字段名
        /// </summary>
        /// <typeparam name="T1">表1</typeparam>
        /// <typeparam name="T2">表2</typeparam>
        /// <typeparam name="T3">表3</typeparam>
        /// <typeparam name="T4">表4</typeparam>
        /// <typeparam name="T5">表5</typeparam>
        /// <typeparam name="T6">表6</typeparam>
        /// <param name="fieldExpression">表达式</param>
        /// <returns></returns>
        public static string GetFieldName<T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, object>> fieldExpression)
        {
            string fieldName = GetFieldName(fieldExpression.Body);

            return fieldName;
        }

        /// <summary>
        /// 根据表达式获取字段名
        /// </summary>
        /// <param name="expressionBody">表达式</param>
        /// <returns></returns>
        public static string GetFieldName(Expression expressionBody)
        {
            string fieldName = string.Empty;
            MemberExpression body = null;
            if (expressionBody is MemberExpression)
            {
                body = expressionBody as MemberExpression;
            }
            if (expressionBody is UnaryExpression)
            {
                UnaryExpression expression2 = expressionBody as UnaryExpression;
                if (expression2.Operand is MemberExpression)
                {
                    body = expression2.Operand as MemberExpression;
                }
            }
            if (body == null)
            {
                throw new InvalidOperationException("Not a member access.");
            }
            PropertyInfo member = body.Member as PropertyInfo;
            fieldName = body.ToString();

            return fieldName;
        }
    }
}
