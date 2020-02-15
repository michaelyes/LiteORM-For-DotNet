using System;

namespace YEasyModel
{
    public class ProcParaAttribute : Attribute
    {
        /// <summary>
        /// 字段代码
        /// </summary>
        public string ParaName { get; set; }

        /// <summary>
        /// 字段名称[标题]
        /// </summary>
        public string ParaTitle { get; set; }

        /// <summary>
        /// 字段描述
        /// </summary>
        public string ParaDesc { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }

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
        /// 是否输出
        /// </summary>
        public bool Is_Output { get; set; }

        /// <summary>
        /// 是否默认值
        /// </summary>
        public bool Has_Default_Value { get; set; }

        /// <summary>
        /// 是否默认值
        /// </summary>
        public object Default_Value { get; set; }
    }

    public class ProcAttribute : Attribute
    {
        public string ProcName { get; set; }
    }
}
