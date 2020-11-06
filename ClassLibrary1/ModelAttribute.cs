using System;

namespace YEasyModel
{
    public class ModelAttribute : Attribute
    {
        /// <summary>
        /// 字段代码
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 字段名称[标题]
        /// </summary>
        public string ColumnTitle { get; set; }

        /// <summary>
        /// 字段描述
        /// </summary>
        public string ColumnDesc { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string ColumnType { get; set; }

        /// <summary>
        /// （字节）大小
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 自动反射赋值
        /// </summary>
        public bool AutoReflect { get; set; }

        /// <summary>
        /// 是否自增
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }
    }

    public class TableAttribute : Attribute
    {
        public string TableName { get; set; }
    }
}
