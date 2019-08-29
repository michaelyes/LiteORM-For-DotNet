using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace YEasyModel
{
    /// <summary>
    /// 排序表达式类型
    /// </summary>
    /// <typeparam name="T">实体类</typeparam>
    public class OrderBy<T>
    {
        Dictionary<Expression<Func<T, object>>, OrderByEnum> orderList;

        public OrderBy()
        {
            orderList = new Dictionary<Expression<Func<T, object>>, OrderByEnum>();
        }

        /// <summary>
        /// 添加字段排序
        /// </summary>
        /// <param name="field">字段表达式</param>
        /// <param name="orderByEnum">排序规则</param>
        public void Add(Expression<Func<T, object>> field, OrderByEnum orderByEnum)
        {
            orderList.Add(field, orderByEnum);
        }

        /// <summary>
        /// 获取字段排序表达式
        /// </summary>
        /// <returns></returns>
        public Dictionary<Expression<Func<T, object>>, OrderByEnum> GetOrderByList()
        {
            return orderList;
        }
    }
}
