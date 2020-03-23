using System;
using YEasyModel;

namespace DBModel
{
    /// <summary>
    /// ST_CardCrashLog:记录所有卡操作异常的信息
    /// </summary>
    [TableAttribute(TableName = "ST_CardCrashLog")]
    public partial class CardCrashLogModel
    {

        /// <summary>
        /// 日志ID
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Log_ID", ColumnType = ColumnType.intType, ColumnTitle = "日志ID", IsIdentity =true, IsPrimaryKey=true, Length=10, Size=4)]
        public int Log_ID
        {
            get;
            set;
        }

        /// <summary>
        /// 卡号
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Card_No", ColumnType = ColumnType.varcharType, ColumnTitle = "卡号", Length=100, Size=100)]
        public string Card_No
        {
            get;
            set;
        }

        /// <summary>
        /// 用户编号
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Person_No", ColumnType = ColumnType.varcharType, ColumnTitle = "用户编号", Length=50, Size=50)]
        public string Person_No
        {
            get;
            set;
        }

        /// <summary>
        /// 操作类型：1、查询；2、消费；3、充值
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Oper_Type", ColumnType = ColumnType.intType, ColumnTitle = "操作类型：1、查询；2、消费；3、充值", Length=10, Size=4)]
        public int Oper_Type
        {
            get;
            set;
        }

        /// <summary>
        /// 日志类型：服务器；本地；
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Log_Type", ColumnType = ColumnType.varcharType, ColumnTitle = "日志类型：服务器；本地；", Length=100, Size=100)]
        public string Log_Type
        {
            get;
            set;
        }

        /// <summary>
        /// 标题
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Title", ColumnType = ColumnType.varcharType, ColumnTitle = "标题", Length=200, Size=200)]
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 日志详情
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Log_Info", ColumnType = ColumnType.varcharType, ColumnTitle = "日志详情", Length=4000, Size=4000)]
        public string Log_Info
        {
            get;
            set;
        }

        /// <summary>
        /// 类标记
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Tag", ColumnType = ColumnType.varcharType, ColumnTitle = "类标记", Length=100, Size=100)]
        public string Tag
        {
            get;
            set;
        }

        /// <summary>
        /// 完整类名（包名+类名）
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Package", ColumnType = ColumnType.varcharType, ColumnTitle = "完整类名（包名+类名）", Length=200, Size=200)]
        public string Package
        {
            get;
            set;
        }

        /// <summary>
        /// 是否已同步：1表示已同步（上传），否则未同步|不成功
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Is_Async", ColumnType = ColumnType.intType, ColumnTitle = "是否已同步：1表示已同步（上传），否则未同步|不成功", Length=10, Size=4)]
        public int Is_Async
        {
            get;
            set;
        }

        /// <summary>
        /// 同步日期
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Async_Date", ColumnType = ColumnType.datetimeType, ColumnTitle = "同步日期", Length=23, Size=8)]
        public DateTime Async_Date
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Remark", ColumnType = ColumnType.varcharType, ColumnTitle = "备注", Length=200, Size=200)]
        public string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 预留1
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Reserve1", ColumnType = ColumnType.varcharType, ColumnTitle = "预留1", Length=200, Size=200)]
        public string Reserve1
        {
            get;
            set;
        }

        /// <summary>
        /// 预留2
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Reserve2", ColumnType = ColumnType.varcharType, ColumnTitle = "预留2", Length=200, Size=200)]
        public string Reserve2
        {
            get;
            set;
        }

        /// <summary>
        /// 预留3
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Reserve3", ColumnType = ColumnType.varcharType, ColumnTitle = "预留3", Length=200, Size=200)]
        public string Reserve3
        {
            get;
            set;
        }

        /// <summary>
        /// 记录日期
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Create_Date", ColumnType = ColumnType.datetimeType, ColumnTitle = "记录日期", Length=23, Size=8)]
        public DateTime Create_Date
        {
            get;
            set;
        }

        /// <summary>
        /// 操作人员
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Create_User", ColumnType = ColumnType.varcharType, ColumnTitle = "操作人员", Length=100, Size=100)]
        public string Create_User
        {
            get;
            set;
        }

        /// <summary>
        /// 设备IP地址
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "IPAddress", ColumnType = ColumnType.varcharType, ColumnTitle = "设备IP地址", Length=200, Size=200)]
        public string IPAddress
        {
            get;
            set;
        }

        /// <summary>
        /// 设备序列号
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "SN", ColumnType = ColumnType.varcharType, ColumnTitle = "设备序列号", Length=200, Size=200)]
        public string SN
        {
            get;
            set;
        }

    }
}
