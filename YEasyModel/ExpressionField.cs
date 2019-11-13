using System;
using System.Linq.Expressions;
using System.Reflection;

namespace YEasyModel
{
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
            string fieldName = string.Empty;
            MemberExpression body = null;
            if (fieldExpression.Body is MemberExpression)
            {
                body = fieldExpression.Body as MemberExpression;
            }
            if (fieldExpression.Body is UnaryExpression)
            {
                UnaryExpression expression2 = fieldExpression.Body as UnaryExpression;
                if (expression2.Operand is MemberExpression)
                {
                    body = expression2.Operand as MemberExpression;
                }
            }
            if (body == null)
            {
                //throw new InvalidOperationException("Not a member access.");
            }
            PropertyInfo member = body.Member as PropertyInfo;
            fieldName = member.Name;

            return fieldName;
        }

        /// <summary>
        /// 根据表达式获取字段名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldExpression">表达式</param>
        /// <returns></returns>
        public static string GetFieldName<T1,T2>(Expression<Func<T1, T2, object>> fieldExpression)
        {
            string fieldName = string.Empty;
            MemberExpression body = null;
            if (fieldExpression.Body is MemberExpression)
            {
                body = fieldExpression.Body as MemberExpression;
            }
            if (fieldExpression.Body is UnaryExpression)
            {
                UnaryExpression expression2 = fieldExpression.Body as UnaryExpression;
                if (expression2.Operand is MemberExpression)
                {
                    body = expression2.Operand as MemberExpression;
                }
            }
            if (body == null)
            {
                //throw new InvalidOperationException("Not a member access.");
            }
            PropertyInfo member = body.Member as PropertyInfo;
            fieldName = body.ToString();

            return fieldName;
        }
    }
}
