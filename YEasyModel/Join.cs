using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace YEasyModel
{
    public class JoinTable
    {
        IList<string> tableList = new List<string>();

        public void JoinLeft<T1, T2>(Expression<Func<T1, T2, bool>> joinExpression, params Expression<Func<T1, T2, object>>[] fields)
        {
            string strWhere = LinqCompileExt.GetJoinByLambda(joinExpression, DataBaseType.SqlServer);
            Type t1 = typeof(T1);//获得该类的Type
            Type t2 = typeof(T2);//获得该类的Type
            //GetTable(joinExpression.Parameters, t1, t2);
        }

        public void JoinLeft<T1, T2, T3>(Expression<Func<T1, T2, T3, bool>> joinExpression, params Expression<Func<T1, T2, T3, object>>[] fields)
        {
            string strWhere = LinqCompileExt.GetJoinByLambda(joinExpression, DataBaseType.SqlServer);
            Type t1 = typeof(T1);//获得该类的Type
            Type t2 = typeof(T2);//获得该类的Type
            Type t3 = typeof(T3);//获得该类的Type
            //GetTable(joinExpression.Parameters, t1, t2, t3);
        }

        public static string JoinTable(ReadOnlyCollection<ParameterExpression> parameters, params Type[] agrs)
        {
            string join = string.Empty;
            for(int i=0;i<agrs.Length;i++)
            {
                Type t = agrs[i];
                string tableName = ModelDAL.GetTableName(t);
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
