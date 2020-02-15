using System.Data;

namespace ModelApp
{
    public class Dal
    {
        /// <summary>
        /// 查询数据库表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTableList()
        {
            string sql = "SELECT s.Name,s.id,Convert(varchar(max),tbp.value) as Description, 1 type"
                + " FROM sysobjects s LEFT JOIN sys.extended_properties as tbp ON s.id = tbp.major_id and tbp.minor_id = 0"
                + " AND(tbp.Name = 'MS_Description' OR tbp.Name is null) WHERE s.xtype IN('U') order by s.Name asc";

            return YEasyModel.DbHelperSQL.Query(sql).Tables[0];
        }

        /// <summary>
        /// 查询数据库视图
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTableViewList()
        {
            string sql = "SELECT s.Name,s.id,Convert(varchar(max),tbp.value) as Description, 2 type"
                + " FROM sysobjects s LEFT JOIN sys.extended_properties as tbp ON s.id = tbp.major_id and tbp.minor_id = 0"
                + " AND(tbp.Name = 'MS_Description' OR tbp.Name is null) WHERE s.xtype IN('V') order by s.Name asc";

            return YEasyModel.DbHelperSQL.Query(sql).Tables[0];
        }

        /// <summary>
        /// 查询表字段信息
        /// </summary>
        /// <param name="tbName">表/视图对象 名</param>
        /// <returns>字段信息列表</returns>
        public static DataTable GetTableColumnList(string tbName)
        {
            string sql = "SELECT [ColumnName] = [Columns].name,[SystemTypeName] = [Types].name,[Precision] = [Columns].precision,[Scale] = [Columns].scale ,[MaxLength] = [Columns].max_length"
                + " ,[IsNullable] = [Columns].is_nullable,[IsRowGUIDCol] = [Columns].is_rowguidcol,[IsIdentity] = [Columns].is_identity,[Description] = [Properties].value"
                + " ,(SELECT 1 FROM dbo.sysindexes si"
                + " INNER JOIN dbo.sysindexkeys sik ON si.id = sik.id AND si.indid = sik.indid"
                + " INNER JOIN dbo.syscolumns sc ON sc.id = sik.id AND sc.colid = sik.colid"
                + " INNER JOIN dbo.sysobjects so ON so.name = si.name AND so.xtype = 'PK'"
                + " WHERE sc.id = [Columns].object_id AND sc.colid = [Columns].column_id) IsPrimaryKey"
                + " FROM sysobjects AS [Tables]"
                + " INNER JOIN sys.columns AS [Columns] ON [Tables].id = [Columns].object_id"
                + " INNER JOIN sys.types AS [Types] ON [Columns].system_type_id = [Types].system_type_id"
                + " AND is_user_defined = 0 AND [Types].name <> 'sysname'"
                + " LEFT OUTER JOIN sys.extended_properties AS [Properties] ON [Properties].major_id = [Tables].id"
                + " AND [Properties].minor_id = [Columns].column_id AND [Properties].name = 'MS_Description'"
                + " WHERE [Tables].name = '" + tbName + "'"
                + " ORDER BY [Columns].column_id";

            return YEasyModel.DbHelperSQL.Query(sql).Tables[0];
        }

        /// <summary>
        /// 查询数据库存储过程
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProcedureList()
        {
            string sql = "select object_Id as id,name,type_desc as Description,1 type from sys.objects where type='P'";

            return YEasyModel.DbHelperSQL.Query(sql).Tables[0];
        }


        /// <summary>
        /// 查询数据库存储过程脚本
        /// </summary>
        /// <returns></returns>
        public static string GetProcedureText(int object_id)
        {
            string sql = "exec sp_executesql N'select c.definition from sys.sql_modules c where c.object_id = @_msparam_0',N'@_msparam_0 nvarchar(4000)',@_msparam_0 = N'" + object_id + "'";
            //select * from syscomments where id=object_id
            object obj = YEasyModel.DbHelperSQL.GetSingle(sql);
            if (obj == null)
                return "";
            else
                return obj.ToString();
        }

        /// <summary>
        /// 查询数据库存储过程参数信息
        /// </summary>
        /// <returns>Columns:ParamName,DataType,Length,SortId,is_output,has_default_value,default_value</returns>
        public static DataTable GetProcedureParameters(int object_id)
        {
            string sql = @"Select isnull(p.name, '') as ParaName,isnull(usrt.name, '') AS [DataType],
          CAST(CASE when usrt.name is null then 0 WHEN usrt.name IN('nchar', 'nvarchar') AND p.max_length <> -1 
            THEN p.max_length / 2 ELSE p.max_length END AS int) AS [Length],
           isnull(parameter_id, 0) as SortId,p.is_output,p.has_default_value,p.default_value,'' Description
            FROM sys.objects AS sp right JOIN sys.all_parameters AS p ON p.object_id = sp.object_Id
           LEFT OUTER JOIN sys.types AS usrt ON usrt.user_type_id = p.user_type_id
          LEFT OUTER JOIN sys.extended_properties E ON sp.object_id = E.major_id
           Where sp.TYPE in ('FN', 'IF', 'TF', 'P')  AND ISNULL(sp.is_ms_shipped, 0) = 0
           AND ISNULL(E.name, '') <> 'microsoft_database_tools_support' and sp.object_id = {0}
           orDER BY sp.name,p.parameter_id ASC";
            sql = string.Format(sql, object_id);

            return YEasyModel.DbHelperSQL.Query(sql).Tables[0];
        }
    }
}
