using System;
using YEasyModel;

namespace DBModel
{
    /// <summary>
    /// ST_Person_FaceInfo:用户人脸信息
    /// </summary>
    [TableAttribute(TableName = "ST_Person_FaceInfo")]
    public partial class Person_FaceInfoModel
    {

        /// <summary>
        /// 人脸信息ID
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "FaceID", ColumnType = ColumnType.intType, ColumnTitle = "人脸信息ID", IsIdentity = true, IsPrimaryKey = true, Length = 10, Size = 4)]
        public int FaceID
        {
            get;
            set;
        }

        /// <summary>
        /// 用户ID
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Person_ID", ColumnType = ColumnType.intType, ColumnTitle = "用户ID", Length = 10, Size = 4)]
        public int Person_ID
        {
            get;
            set;
        }

        /// <summary>
        /// 用户人脸图片
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "FaceImage", ColumnType = ColumnType.varbinaryType, ColumnTitle = "用户人脸图片", Length = 0, Size = -1)]
        public byte[] FaceImage
        {
            get;
            set;
        }

        /// <summary>
        /// 图片路径
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "FacePath", ColumnType = ColumnType.varcharType, ColumnTitle = "图片路径", Length = 1000, Size = 1000)]
        public string FacePath
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Remark", ColumnType = ColumnType.varcharType, ColumnTitle = "备注", Length = 200, Size = 200)]
        public string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Create_User", ColumnType = ColumnType.intType, ColumnTitle = "创建人", Length = 10, Size = 4)]
        public int Create_User
        {
            get;
            set;
        }

        /// <summary>
        /// 创建日期
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Create_Date", ColumnType = ColumnType.datetimeType, ColumnTitle = "创建日期", Length = 23, Size = 8)]
        public DateTime Create_Date
        {
            get;
            set;
        }

        /// <summary>
        /// 预留1
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Reserve1", ColumnType = ColumnType.varcharType, ColumnTitle = "预留1", Length = 100, Size = 100)]
        public string Reserve1
        {
            get;
            set;
        }

        /// <summary>
        /// 预留2
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Reserve2", ColumnType = ColumnType.varcharType, ColumnTitle = "预留2", Length = 100, Size = 100)]
        public string Reserve2
        {
            get;
            set;
        }

        /// <summary>
        /// 预留3
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Reserve3", ColumnType = ColumnType.varcharType, ColumnTitle = "预留3", Length = 100, Size = 100)]
        public string Reserve3
        {
            get;
            set;
        }

        /// <summary>
        /// 预留4
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Reserve4", ColumnType = ColumnType.varcharType, ColumnTitle = "预留4", Length = 100, Size = 100)]
        public string Reserve4
        {
            get;
            set;
        }

        /// <summary>
        /// 预留5
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Reserve5", ColumnType = ColumnType.varcharType, ColumnTitle = "预留5", Length = 100, Size = 100)]
        public string Reserve5
        {
            get;
            set;
        }

        /// <summary>
        /// 图片
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "FaceImage2", ColumnType = ColumnType.imageType, ColumnTitle = "图片", Length = 0, Size = 16)]
        public string FaceImage2
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Person_No", ColumnType = ColumnType.varcharType, ColumnTitle = "", Length = 50, Size = 50)]
        public string Person_No
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Regedit_ID", ColumnType = ColumnType.varcharType, ColumnTitle = "", Length = 100, Size = 100)]
        public string Regedit_ID
        {
            get;
            set;
        }

    }
}
