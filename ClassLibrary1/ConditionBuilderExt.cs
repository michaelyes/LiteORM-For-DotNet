using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace YEasyModel
{
    internal class ConditionBuilderExt: ConditionBuilder
    {

        protected override Expression VisitBinary(BinaryExpression b)
        {
            if (b == null) return b;

            string opr;
            switch (b.NodeType)
            {
                case ExpressionType.Equal:
                    opr = "=";
                    break;
                case ExpressionType.NotEqual:
                    opr = "<>";
                    break;
                case ExpressionType.GreaterThan:
                    opr = ">";
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    opr = ">=";
                    break;
                case ExpressionType.LessThan:
                    opr = "<";
                    break;
                case ExpressionType.LessThanOrEqual:
                    opr = "<=";
                    break;
                case ExpressionType.AndAlso:
                    opr = "AND";
                    break;
                case ExpressionType.OrElse:
                    opr = "OR";
                    break;
                case ExpressionType.Add:
                    opr = "+";
                    break;
                case ExpressionType.Subtract:
                    opr = "-";
                    break;
                case ExpressionType.Multiply:
                    opr = "*";
                    break;
                case ExpressionType.Divide:
                    opr = "/";
                    break;
                default:
                    throw new NotSupportedException(b.NodeType + "is not supported.");
            }

            this.Visit(b.Left);
            this.Visit(b.Right);

            string right = this.m_conditionParts.Pop();
            string left = this.m_conditionParts.Pop();

            string condition = string.Format("({0} {1} {2})", left, opr, right);
            this.m_conditionParts.Push(condition);

            return b;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            if (c == null) return c;

            this.m_arguments.Add(c.Value);
            this.m_conditionParts.Push(string.Format("{{{0}}}", this.m_arguments.Count - 1));

            return c;
        }

        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            if (m == null) return m;

            PropertyInfo propertyInfo = m.Member as PropertyInfo;
            if (propertyInfo == null) return m;

            //   this.m_conditionParts.Push(string.Format("(Where.{0})", propertyInfo.Name));
            //是否添加引号
            if (m_ifWithQuotationMarks)
            {
                this.m_conditionParts.Push(string.Format(" {0} ", AddQuotationMarks(m.ToString())));
            }
            else
            {
                // this.m_conditionParts.Push(string.Format("[{0}]", propertyInfo.Name));
                this.m_conditionParts.Push(string.Format(" {0} ", m.ToString()));
            }

            return m;
        }

        /// <summary>
        /// ConditionBuilder 并不支持生成Like操作，如 字符串的 StartsWith，Contains，EndsWith 并不能生成这样的SQL： Like ‘xxx%’, Like ‘%xxx%’ , Like ‘%xxx’ . 只要override VisitMethodCall 这个方法即可实现上述功能。
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            string connectorWords = GetLikeConnectorWords(m_dataBaseType); //获取like链接符

            if (m == null) return m;
            string format;
            switch (m.Method.Name)
            {
                case "StartsWith":
                    format = "({0} LIKE ''" + connectorWords + "{1}" + connectorWords + "'%')";
                    break;
                case "Contains":
                    format = "({0} LIKE '%'" + connectorWords + "{1}" + connectorWords + "'%')";
                    break;
                case "EndsWith":
                    format = "({0} LIKE '%'" + connectorWords + "{1}" + connectorWords + "'')";
                    break;

                case "Equals":
                    // not in 或者  in 或 not like
                    format = "{0} {1} ";
                    break;

                case "Parse":
                    format = "{1}";
                    break;

                case "IsNull":
                    if(m.Method.ReflectedType == typeof(SqlColumnUtil))
                    {
                        object obj = TypeUtil.GetSqlDataTypeValue(m.Arguments[1], m.Arguments[1].Type);
                        format = "isnull({1},"+ obj.ToString().Replace("\"", "") + ")";
                    }
                    else
                    {
                        format = "({1})";
                        throw new NotSupportedException(m.NodeType + " is not supported!");
                    }
                    break;

                default:
                    throw new NotSupportedException(m.NodeType + " is not supported!");
            }
            if (m.Object != null)
                this.Visit(m.Object);
            this.Visit(m.Arguments[0]);
            string right = this.m_conditionParts.Pop();
            string left = string.Empty;
            if (m.Object != null)
                left = this.m_conditionParts.Pop();

            this.m_conditionParts.Push(string.Format(format, left, right));
            return m;
        }



        public void BuildJoin(Expression expression)
        {
            PartialEvaluator evaluator = new PartialEvaluator();
            Expression evaluatedExpression = evaluator.Eval(expression);

            this.m_arguments = new List<object>();
            this.m_conditionParts = new Stack<string>();
            LambdaExpression lambdaExpression = (LambdaExpression)evaluatedExpression;
            this.Visit(evaluatedExpression);
            //this.VisitBinary2Join((BinaryExpression)lambdaExpression.Body);
            this.Arguments = this.m_arguments.ToArray();
            this.Condition = this.m_conditionParts.Count > 0 ? this.m_conditionParts.Pop() : null;
        }

    }
}
