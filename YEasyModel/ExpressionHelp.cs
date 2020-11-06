using System;
using System.Linq.Expressions;

namespace YEasyModel
{
    /// <summary>
    /// 表达式工具类
    /// </summary>
    public static class ExpressionUtil
    {
        /// <summary>
        /// 表达式合并
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="merge"></param>
        /// <returns></returns>
        private static Expression<T> Combine<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            MyExpressionVisitor visitor = new MyExpressionVisitor(first.Parameters[0]);
            Expression bodyone = visitor.Visit(first.Body);
            Expression bodytwo = visitor.Visit(second.Body);
            return Expression.Lambda<T>(merge(bodyone, bodytwo), first.Parameters[0]);
        }

        /// <summary>
        /// 表达式 AND 合并
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first">表达式1</param>
        /// <param name="second">表达式2</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> ExpressionAnd<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Combine(second, Expression.And);
        }

        /// <summary>
        /// 表达式 OR 合并
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first">表达式1</param>
        /// <param name="second">表达式2</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> ExpressionOr<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Combine(second, Expression.Or);
        }
    }

    /// <summary>
    /// 扩展表达式树访问
    /// </summary>
    public class MyExpressionVisitor : ExpressionVisitor
    {
        public ParameterExpression _Parameter { get; set; }

        public MyExpressionVisitor(ParameterExpression Parameter)
        {
            _Parameter = Parameter;
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            return _Parameter;
        }

        public override Expression Visit(Expression node)
        {
            return base.Visit(node);//Visit会根据VisitParameter()方法返回的Expression修改这里的node变量
        }
    }
}
