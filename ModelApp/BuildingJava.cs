using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ModelApp
{
    public class BuildingJava
    {
        private static SortedDictionary<string, string> m_values = new SortedDictionary<string, string>();


        public static void ReadExcelData()
        {
            m_values.Add("uuid", "String");
            m_values.Add("character", "String");
            m_values.Add("integer", "int");
            m_values.Add("boolean", "boolean");
            m_values.Add("timestamp", "String");
            m_values.Add("numeric", "BigDecimal");
            m_values.Add("real", "BigDecimal");
            m_values.Add("smallint", "int");
            m_values.Add("bytea", "String");

            var excel = new ExcelHelper(@"C:\Users\Administrator\Desktop\数据库说明_2_2020.12.23.xlsx");
            var dataTable = excel.ExcelToDataTable(null, true);
            BuildingModel(dataTable);
        }

        /// <summary>
        /// 生成存储过程模型
        /// </summary>
        private static void BuildingModel(DataTable checkedData)
        {
            List<string> tbNameList = new List<string>();
            foreach (DataRow dr in checkedData.Rows)
            {
                if (!tbNameList.Contains(dr["表名"].ToString()))
                {
                    tbNameList.Add(dr["表名"].ToString());
                }
            }
            //var tbName=checkedData.s
            foreach (string dr in tbNameList)
            {
                checkedData.DefaultView.RowFilter = "[表名]='" + dr + "'";
                DataTable dtColumns = checkedData.DefaultView.ToTable();
                string description = "";
                CreateModel_M(dtColumns, dr, description);
            }
            //CreateModel(checkedData, "tbName", "description");
        }

        /// <summary>
        /// 根据存储过程参数信息创建模型
        /// </summary>
        /// <param name="dtColumns">存储过程参数</param>
        /// <param name="tbName">存储过程名</param>
        /// <param name="description">存储过程 注释/说明</param>
        /// <returns>实体模型是否创建成功</returns>
        private static bool CreateModel(DataTable dtColumns, string tbName, string description)
        {
            bool successed = false;
            string strNamespace = tbName;
            string clsName = tbName;
            clsName = clsName + "Entity";

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("package com.stkj.smartbox.baseapp.data.entity;");
            stringBuilder.AppendLine();
            stringBuilder.Append("import com.stkj.smartbox.baseapp.data.PrimaryKeyNotAutoincrement;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine();

            stringBuilder.Append("    /**");
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("  *{0}:{1}", tbName, description);
            stringBuilder.AppendLine();
            stringBuilder.Append("    */");
            stringBuilder.AppendLine();
            stringBuilder.Append("    public class "+ clsName + " {");
            stringBuilder.AppendLine();
            stringBuilder.Append("    ");
            stringBuilder.AppendLine();

            //创建实体类属性
            foreach (DataRow dr in dtColumns.Rows)
            {
                stringBuilder.AppendLine();
                stringBuilder.Append("        /**");
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("        *{0}", dr["列描述"].ToString());
                stringBuilder.AppendLine();
                stringBuilder.Append("        */");
                stringBuilder.AppendLine();
                string typeName = "";
                try
                {
                    string key = dr["类型及长度"].ToString().Trim();
                    m_values.TryGetValue(key, out typeName);
                    if (string.IsNullOrEmpty(typeName))
                    {
                        foreach (var kv in m_values)
                        {
                            if (key.Contains(kv.Key))
                            {
                                typeName = kv.Value;
                                break;
                            }
                        }
                    }
                }
                catch { }
                if (dr["列id"].ToString().Equals("ID"))
                    stringBuilder.Append("@PrimaryKeyNotAutoincrement");

                stringBuilder.AppendFormat("   private {1} {0};", dr["列id"].ToString(), typeName);
                stringBuilder.AppendLine();

                stringBuilder.Append("        /**");
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("        *{0}", dr["列描述"].ToString());
                stringBuilder.AppendLine();
                stringBuilder.Append("        */");
                stringBuilder.AppendLine();
                stringBuilder.Append("   public void set" + dr["列id"].ToString().Substring(0, 1).ToUpper() + dr["列id"].ToString().Substring(1) + "(" + typeName + " " + dr["列id"].ToString() + ") {this." + dr["列id"].ToString() + " = " + dr["列id"].ToString() + ";}");
                stringBuilder.AppendLine();

                stringBuilder.Append("        /**");
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("        *{0}", dr["列描述"].ToString());
                stringBuilder.AppendLine();
                stringBuilder.Append("        */");
                stringBuilder.AppendLine();
                stringBuilder.Append("public " + typeName + " get" + dr["列id"].ToString().Substring(0, 1).ToUpper() + dr["列id"].ToString().Substring(1) + "() { return " + dr["列id"].ToString() + ";}");
                stringBuilder.AppendLine();
            }
            stringBuilder.AppendLine();
            stringBuilder.Append("    }");
            stringBuilder.AppendLine();

            successed = BaseModel.SaveFile2Java(clsName, stringBuilder.ToString(), @"F:\SmartBox\DBModel\");

            return successed;
        }



        /// <summary>
        /// 根据存储过程参数信息创建模型
        /// </summary>
        /// <param name="dtColumns">存储过程参数</param>
        /// <param name="tbName">存储过程名</param>
        /// <param name="description">存储过程 注释/说明</param>
        /// <returns>实体模型是否创建成功</returns>
        private static bool CreateModel_M(DataTable dtColumns, string tbName, string description)
        {
            bool successed = false;
            string strNamespace = tbName;
            string clsName = tbName;
            clsName = clsName + "Field";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("package com.stkj.smartbox.baseapp.data.field;");
            stringBuilder.AppendLine();
            stringBuilder.Append("import android.provider.BaseColumns;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine();

            stringBuilder.Append("    /**");
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("  *{0}:{1}", tbName, description);
            stringBuilder.AppendLine();
            stringBuilder.Append("    */");
            stringBuilder.AppendLine();
            stringBuilder.Append("    public class " + clsName + " implements BaseColumns{");
            stringBuilder.AppendLine();
            stringBuilder.Append("    ");
            stringBuilder.AppendLine();

            //创建实体类属性
            foreach (DataRow dr in dtColumns.Rows)
            {
                stringBuilder.AppendLine();
                stringBuilder.Append("        /**");
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("        *{0}", dr["列描述"].ToString());
                stringBuilder.AppendLine();
                stringBuilder.Append("        */");
                stringBuilder.AppendLine();

                stringBuilder.AppendFormat("   public static final String {0}=\"{1}\";", dr["列id"].ToString().ToUpper(), dr["列id"].ToString());
                stringBuilder.AppendLine();
            }
            stringBuilder.AppendLine();
            stringBuilder.Append("    }");
            stringBuilder.AppendLine();

            successed = BaseModel.SaveFile2Java(clsName, stringBuilder.ToString(), @"F:\SmartBox\DBModel\");

            return successed;
        }
    }
}
