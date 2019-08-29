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
                +" FROM sysobjects s LEFT JOIN sys.extended_properties as tbp ON s.id = tbp.major_id and tbp.minor_id = 0"
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
    }
}
