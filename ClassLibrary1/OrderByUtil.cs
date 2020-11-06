using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace YEasyModel
{
    public class OrderByUtil
    {
        /// <summary>
        /// 根据表达式获取排序规则
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="orderList">表达式</param>
        /// <returns></returns>
        public static string GetOrderBy<T>(Dictionary<Expression<Func<T, object>>, OrderByEnum> orderList)
        {
            StringBuilder orderBy = new StringBuilder();
            if (orderList != null)
            {
                foreach (KeyValuePair<Expression<Func<T, object>>, OrderByEnum> keyValue in orderList)
                {
                    if (orderBy.Length == 0)
                    {
                        orderBy.Append(" order by ");
                    }
                    string fieldName = ExpressionField.GetFieldName<T>(keyValue.Key);
                    orderBy.AppendFormat(" {0} {1}", fieldName, keyValue.Value.ToString());
                    orderBy.Append(",");
                }                
            }

            return orderBy.ToString().TrimEnd(',');
        }
    }
}
