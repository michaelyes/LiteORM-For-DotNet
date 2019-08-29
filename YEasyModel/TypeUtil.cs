using System;

namespace YEasyModel
{
    public class TypeUtil
    {
        /// <summary>
        /// 通用类型转换。对可空类型进行判断转换，要不然会报错 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        public static object ChangeType(object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                    return null;

                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType);
        }

        /// <summary>
        /// 类型转换，已用ChangeType方法代替
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetValue(object obj, string type)
        {
            object value = obj;
            switch (type.ToLower())
            {
                case ColumnType.intType:
                    value = (int)obj;
                    break;
                case ColumnType.moneyType:
                    value = (decimal)obj;
                    break;
                case ColumnType.varcharType:
                    value = (string)obj;
                    break;
                case ColumnType.varbinaryType:
                    value = (byte[])obj;
                    break;
                case ColumnType.datetimeType:
                    value = (DateTime)obj;
                    break;
                case ColumnType.decimalType:
                    value = (decimal)obj;
                    break;
                case ColumnType.floatType:
                    value = (double)obj;
                    break;
                case ColumnType.bigintType:
                    value = (long)obj;
                    break;
                case ColumnType.bitType:
                    value = (bool)obj;
                    break;
                case ColumnType.charType:
                    value = (string)obj;
                    break;
                case ColumnType.dateType:
                    value = (DateTime)obj;
                    break;
                case ColumnType.datetime2Type:
                    value = (DateTime)obj;
                    break;
                case ColumnType.nvarcharType:
                    value = (string)obj;
                    break;
                case ColumnType.ntextType:
                    value = (string)obj;
                    break;
                case ColumnType.ncharType:
                    value = (string)obj;
                    break;
                case ColumnType.smallintType:
                    value = (short)obj;
                    break;
                case ColumnType.smalldatetimeType:
                    value = (DateTime)obj;
                    break;
                case ColumnType.smallmoneyType:
                    value = (decimal)obj;
                    break;
                case ColumnType.tinyintType:
                    value = (byte)obj;
                    break;
            }

            return value;
        }

        /// <summary>
        /// 类型转换，Sql Server 数据类型转C#类型
        /// </summary>
        /// <param name="type">Sql Server 数据类型</param>
        /// <returns></returns>
        public static Type GetType(string type)
        {
            Type value = typeof(string);
            switch (type.ToLower())
            {
                case ColumnType.smallintType:
                    value = typeof(short);
                    break;
                case ColumnType.intType:
                    value = typeof(int);
                    break;
                case ColumnType.bigintType:
                    value = typeof(long);
                    break;
                case ColumnType.moneyType:
                    value = typeof(decimal);
                    break;
                case ColumnType.decimalType:
                    value = typeof(decimal);
                    break;
                case ColumnType.smallmoneyType:
                    value = typeof(decimal);
                    break;
                case ColumnType.floatType:
                    value = typeof(double);
                    break;
                case ColumnType.tinyintType:
                    value = typeof(byte);
                    break;
                case ColumnType.varbinaryType:
                    value = typeof(byte[]);
                    break;
                case ColumnType.bitType:
                    value = typeof(bool);
                    break;
                case ColumnType.datetimeType:
                    value = typeof(DateTime);
                    break;
                case ColumnType.dateType:
                    value = typeof(DateTime);
                    break;
                case ColumnType.datetime2Type:
                    value = typeof(DateTime);
                    break;
                case ColumnType.smalldatetimeType:
                    value = typeof(DateTime);
                    break;
                case ColumnType.varcharType:
                case ColumnType.charType:
                case ColumnType.nvarcharType:
                case ColumnType.ntextType:
                case ColumnType.ncharType:
                    value = typeof(string);
                    break;
            }

            return value;
        }

        /// <summary>
        /// 类型转换，Sql Server 数据类型转C#类型
        /// </summary>
        /// <param name="type">Sql Server 数据类型</param>
        /// <returns></returns>
        public static string GetTypeName(string type)
        {
            string value = "string";
            switch (type.ToLower())
            {
                case ColumnType.smallintType:
                    value = "short";
                    break;
                case ColumnType.intType:
                    value = "int";
                    break;
                case ColumnType.bigintType:
                    value = "long";
                    break;
                case ColumnType.moneyType:
                    value = "decimal";
                    break;
                case ColumnType.decimalType:
                    value = "decimal";
                    break;
                case ColumnType.smallmoneyType:
                    value = "decimal";
                    break;
                case ColumnType.floatType:
                    value = "double";
                    break;
                case ColumnType.tinyintType:
                    value = "byte";
                    break;
                case ColumnType.varbinaryType:
                    value = "byte[]";
                    break;
                case ColumnType.bitType:
                    value = "bool";
                    break;
                case ColumnType.datetimeType:
                    value = "DateTime";
                    break;
                case ColumnType.dateType:
                    value = "DateTime";
                    break;
                case ColumnType.datetime2Type:
                    value = "DateTime";
                    break;
                case ColumnType.smalldatetimeType:
                    value = "DateTime";
                    break;
                case ColumnType.varcharType:
                case ColumnType.charType:
                case ColumnType.nvarcharType:
                case ColumnType.ntextType:
                case ColumnType.ncharType:
                    value = "string";
                    break;
            }

            return value;
        }
    }
}
