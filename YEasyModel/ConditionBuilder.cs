using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace YEasyModel
{

    internal class ConditionBuilder : ExpressionVisitorExt
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        internal string m_dataBaseType = "";
        /// <summary>
        /// 字段是否加引号
        /// </summary>
        internal bool m_ifWithQuotationMarks = false;

        internal List<object> m_arguments;
        internal Stack<string> m_conditionParts;

        public string Condition { get; internal set; }

        public object[] Arguments { get; internal set; }




        #region 加双引号
        /// <summary>
        /// 加双引号
        /// </summary>
        /// <param name="str">字串</param>
        /// <returns></returns>
        public static string AddQuotationMarks(string str)
        {
            if (str != null)
            {
                return "\"" + str.Trim() + "\"";
            }
            else
            {
                return str;
            }
        }

        #endregion

        #region 设置是否加双引号
        public void SetIfWithQuotationMarks(bool ifwith)
        {
            m_ifWithQuotationMarks = ifwith;
        }
        #endregion

        #region 设置是数据库类型
        public void SetDataBaseType(string databaseType)
        {
            if (!string.IsNullOrEmpty(databaseType))
            {
                m_dataBaseType = databaseType;
            }

        }
        #endregion
        public void Build(Expression expression)
        {
            PartialEvaluator evaluator = new PartialEvaluator();
            Expression evaluatedExpression = evaluator.Eval(expression);

            this.m_arguments = new List<object>();
            this.m_conditionParts = new Stack<string>();

            this.Visit(evaluatedExpression);

            this.Arguments = this.m_arguments.ToArray();
            this.Condition = this.m_conditionParts.Count > 0 ? this.m_conditionParts.Pop() : null;
        }

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

            string condition = String.Format("({0} {1} {2})", left, opr, right);
            this.m_conditionParts.Push(condition);

            return b;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            if (c == null) return c;

            this.m_arguments.Add(c.Value);
            this.m_conditionParts.Push(String.Format("{{{0}}}", this.m_arguments.Count - 1));

            return c;
        }

        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            if (m == null) return m;

            PropertyInfo propertyInfo = m.Member as PropertyInfo;
            if (propertyInfo == null) return m;

            //   this.m_conditionParts.Push(String.Format("(Where.{0})", propertyInfo.Name));
            //是否添加引号
            if (m_ifWithQuotationMarks)
            {
                this.m_conditionParts.Push(String.Format(" {0} ", AddQuotationMarks(propertyInfo.Name)));
            }
            else
            {
                // this.m_conditionParts.Push(String.Format("[{0}]", propertyInfo.Name));
                this.m_conditionParts.Push(String.Format(" {0} ", propertyInfo.Name));
            }

            return m;
        }

        #region 其他
        static string BinarExpressionProvider(Expression left, Expression right, ExpressionType type)
        {
            string sb = "(";
            //先处理左边
            sb += ExpressionRouter(left);

            sb += ExpressionTypeCast(type);

            //再处理右边
            string tmpStr = ExpressionRouter(right);

            if (tmpStr == "null")
            {
                if (sb.EndsWith(" ="))
                    sb = sb.Substring(0, sb.Length - 1) + " is null";
                else if (sb.EndsWith("<>"))
                    sb = sb.Substring(0, sb.Length - 1) + " is not null";
            }
            else
                sb += tmpStr;
            return sb += ")";
        }

        static string ExpressionRouter(Expression exp)
        {
            string sb = string.Empty;
            if (exp is BinaryExpression)
            {

                BinaryExpression be = ((BinaryExpression)exp);
                return BinarExpressionProvider(be.Left, be.Right, be.NodeType);
            }
            else if (exp is MemberExpression)
            {

                MemberExpression me = ((MemberExpression)exp);
                return me.Member.Name;
            }
            else if (exp is NewArrayExpression)
            {
                NewArrayExpression ae = ((NewArrayExpression)exp);
                StringBuilder tmpstr = new StringBuilder();
                foreach (Expression ex in ae.Expressions)
                {
                    tmpstr.Append(ExpressionRouter(ex));
                    tmpstr.Append(",");
                }
                return tmpstr.ToString(0, tmpstr.Length - 1);
            }
            else if (exp is MethodCallExpression)
            {
                MethodCallExpression mce = (MethodCallExpression)exp;
                if (mce.Method.Name == "Like")
                    return string.Format("({0} like {1})", ExpressionRouter(mce.Arguments[0]), ExpressionRouter(mce.Arguments[1]));
                else if (mce.Method.Name == "NotLike")
                    return string.Format("({0} Not like {1})", ExpressionRouter(mce.Arguments[0]), ExpressionRouter(mce.Arguments[1]));
                else if (mce.Method.Name == "In")
                    return string.Format("{0} In ({1})", ExpressionRouter(mce.Arguments[0]), ExpressionRouter(mce.Arguments[1]));
                else if (mce.Method.Name == "NotIn")
                    return string.Format("{0} Not In ({1})", ExpressionRouter(mce.Arguments[0]), ExpressionRouter(mce.Arguments[1]));
                else if (mce.Method.Name == "StartWith")
                    return string.Format("{0} like '{1}%'", ExpressionRouter(mce.Arguments[0]), ExpressionRouter(mce.Arguments[1]));
            }
            else if (exp is ConstantExpression)
            {
                ConstantExpression ce = ((ConstantExpression)exp);
                if (ce.Value == null)
                    return "null";
                else if (ce.Value is ValueType)
                    return ce.Value.ToString();
                else if (ce.Value is string || ce.Value is DateTime || ce.Value is char)
                {

                    return string.Format("'{0}'", ce.Value.ToString());
                }
            }
            else if (exp is UnaryExpression)
            {
                UnaryExpression ue = ((UnaryExpression)exp);
                return ExpressionRouter(ue.Operand);
            }
            return null;
        }

        static string ExpressionTypeCast(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return " AND ";
                case ExpressionType.Equal:
                    return " =";
                case ExpressionType.GreaterThan:
                    return " >";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return " Or ";
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                    return "+";
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    return "-";
                case ExpressionType.Divide:
                    return "/";
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                    return "*";
                default:
                    return null;
            }
        }
        #endregion


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
                    format = "({0} {1} )";
                    break;

                case "Parse":
                    format = "({1})";
                    break;

                default:
                    if(m.Method.ReturnType == typeof(int))
                    {
                        if (DataBaseType.SqlServer.Equals(m_dataBaseType))
                        {
                            format = "(convert(int,{1}))";
                            break;
                        }
                    }
                    throw new NotSupportedException(m.NodeType + " is not supported!");
            }
            if (m.Object != null)
                this.Visit(m.Object);
            this.Visit(m.Arguments[0]);
            string right = this.m_conditionParts.Pop();
            string left = string.Empty;
            if (m.Object != null)
                left = this.m_conditionParts.Pop();

            this.m_conditionParts.Push(String.Format(format, left, right));
            return m;
        }

        /// <summary>
        /// 获得like语句链接符
        /// </summary>
        /// <param name="databaseType"></param>
        /// <returns></returns>
        public static string GetLikeConnectorWords(string databaseType)
        {
            string result = "+";
            switch (databaseType.ToLower())
            {

                case DataBaseType.PostGreSql:
                case DataBaseType.Oracle:
                case DataBaseType.MySql:
                case DataBaseType.Sqlite:
                    result = "||";
                    break;
            }

            return result;
        }

    }

}
