using System.IO;
using System.Text;

namespace ModelApp
{
    public class BaseModel
    {
        public static void BuildBaseModel(string Namespace, string dir)
        {
            CreateBaseModel(Namespace, dir);
        }

        /// <summary>
        /// 保存实体类代码文件
        /// </summary>
        /// <param name="clsName">类名</param>
        /// <param name="content">文件内容</param>
        /// <returns>是否保存成功</returns>
        public static bool SaveFile2Java(string clsName, string content, string dir)
        {
            string path = dir.Trim();
            path = path + "\\" + clsName + ".java";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            byte[] buff = Encoding.UTF8.GetBytes(content);
            var fs = File.Create(path);
            fs.Write(buff, 0, buff.Length);
            fs.Close();
            fs.Dispose();

            return true;
        }

        /// <summary>
        /// 保存实体类代码文件
        /// </summary>
        /// <param name="clsName">类名</param>
        /// <param name="content">文件内容</param>
        /// <returns>是否保存成功</returns>
        public static bool SaveFile(string clsName, string content, string dir)
        {
            string path = dir.Trim();
            path = path + "\\" + clsName + ".cs";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            byte[] buff = Encoding.UTF8.GetBytes(content);
            var fs = File.Create(path);
            fs.Write(buff, 0, buff.Length);
            fs.Close();
            fs.Dispose();

            return true;
        }



        /// <summary>
        /// 根据表字段信息创建模型
        /// </summary>
        /// <param name="dtColumns">表字段信息</param>
        /// <param name="tbName">表名</param>
        /// <param name="description">表注释/说明</param>
        /// <returns>实体模型是否创建成功</returns>
        private static bool CreateBaseModel(string Namespace, string dir)
        {
            bool successed = false;
            string strNamespace = Namespace;
            string clsName = "BaseModel";

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("using System;");
            stringBuilder.AppendLine();
            stringBuilder.Append("using YEasyModel;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("namespace {0}", strNamespace);
            stringBuilder.AppendLine();
            stringBuilder.Append("{");
            stringBuilder.AppendLine();

            stringBuilder.Append("    /// <summary>");
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("    /// {0}", "实体模型基类");
            stringBuilder.AppendLine();
            stringBuilder.Append("    /// </summary>");
            stringBuilder.AppendLine();
            //stringBuilder.Append("    [Serializable]");
            //stringBuilder.AppendFormat("    [TableAttribute(TableName = \"{0}\")]", tbName);
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("    public partial class {0}", clsName);
            stringBuilder.AppendLine();
            stringBuilder.Append("    {");
            stringBuilder.AppendLine();
            ////创建实体类字段
            //foreach (DataRow dr in dtColumns.Rows)
            //{
            //    stringBuilder.AppendLine();
            //    stringBuilder.AppendFormat("        private {0} {1};",YEasyModel.TypeUtil.GetType(dr["SystemTypeName"].ToString()), dr["ColumnName"].ToString());
            //}

            stringBuilder.AppendLine();
            stringBuilder.Append("        /// <summary>");
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("        /// {0}", "保留字段 - 查询所有字段");
            stringBuilder.AppendLine();
            stringBuilder.Append("        /// <summary>");
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("        public {0} {1}", "string", YEasyModel.Common.Star_SelectAllColumn);
            stringBuilder.AppendLine();
            stringBuilder.Append("        {");
            stringBuilder.AppendLine();
            stringBuilder.Append("            get;");
            stringBuilder.AppendLine();
            stringBuilder.Append("            set;");
            stringBuilder.AppendLine();
            stringBuilder.Append("        }");
            stringBuilder.AppendLine();

            stringBuilder.AppendLine();
            stringBuilder.Append("    }");
            stringBuilder.AppendLine();

            stringBuilder.Append("}");
            stringBuilder.AppendLine();

            successed = BaseModel.SaveFile(clsName, stringBuilder.ToString(), dir);


            return successed;
        }
    }
}
