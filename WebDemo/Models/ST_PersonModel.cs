using System;
using YEasyModel;

namespace DBModel
{
    /// <summary>
    /// ST_Person:
    /// </summary>
    [TableAttribute(TableName = "ST_Person")]
    public partial class ST_PersonModel
    {

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Person_ID", ColumnType = ColumnType.intType, ColumnTitle = "", IsPrimaryKey=true, Length=10, Size=4)]
        public int Person_ID
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Firm_ID", ColumnType = ColumnType.intType, ColumnTitle = "", Length=10, Size=4)]
        public int Firm_ID
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Person_No", ColumnType = ColumnType.varcharType, ColumnTitle = "", Length=50, Size=50)]
        public string Person_No
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Person_Name", ColumnType = ColumnType.varcharType, ColumnTitle = "", Length=100, Size=100)]
        public string Person_Name
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Sex", ColumnType = ColumnType.charType, ColumnTitle = "", Length=2, Size=2)]
        public string Sex
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Card_No", ColumnType = ColumnType.varcharType, ColumnTitle = "", Length=100, Size=100)]
        public string Card_No
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Card_SN", ColumnType = ColumnType.varcharType, ColumnTitle = "", Length=100, Size=100)]
        public string Card_SN
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Card_State", ColumnType = ColumnType.intType, ColumnTitle = "", Length=10, Size=4)]
        public int Card_State
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "CardType_ID", ColumnType = ColumnType.intType, ColumnTitle = "", Length=10, Size=4)]
        public int CardType_ID
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Card_Balance", ColumnType = ColumnType.moneyType, ColumnTitle = "", Length=19, Size=8)]
        public decimal Card_Balance
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Purse_Balance", ColumnType = ColumnType.moneyType, ColumnTitle = "", Length=19, Size=8)]
        public decimal Purse_Balance
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Person_Code", ColumnType = ColumnType.varcharType, ColumnTitle = "", Length=50, Size=50)]
        public string Person_Code
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Batch_No", ColumnType = ColumnType.varcharType, ColumnTitle = "", Length=100, Size=100)]
        public string Batch_No
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Begin_Date", ColumnType = ColumnType.datetimeType, ColumnTitle = "", Length=23, Size=8)]
        public DateTime Begin_Date
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "End_Date", ColumnType = ColumnType.datetimeType, ColumnTitle = "", Length=23, Size=8)]
        public DateTime End_Date
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Pass", ColumnType = ColumnType.varcharType, ColumnTitle = "", Length=30, Size=30)]
        public string Pass
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Bank_CardNo", ColumnType = ColumnType.varcharType, ColumnTitle = "", Length=100, Size=100)]
        public string Bank_CardNo
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Bank_Code", ColumnType = ColumnType.varcharType, ColumnTitle = "", Length=100, Size=100)]
        public string Bank_Code
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Bank_Type", ColumnType = ColumnType.intType, ColumnTitle = "", Length=10, Size=4)]
        public int Bank_Type
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "PSAM_Marking", ColumnType = ColumnType.varcharType, ColumnTitle = "", Length=30, Size=30)]
        public string PSAM_Marking
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Black_Count", ColumnType = ColumnType.intType, ColumnTitle = "", Length=10, Size=4)]
        public int Black_Count
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Card_Operate_Count", ColumnType = ColumnType.intType, ColumnTitle = "", Length=10, Size=4)]
        public int Card_Operate_Count
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Card_Supply_Count", ColumnType = ColumnType.intType, ColumnTitle = "", Length=10, Size=4)]
        public int Card_Supply_Count
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Card_Supply_Fund", ColumnType = ColumnType.moneyType, ColumnTitle = "", Length=19, Size=8)]
        public decimal Card_Supply_Fund
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Card_Take_Count", ColumnType = ColumnType.intType, ColumnTitle = "", Length=10, Size=4)]
        public int Card_Take_Count
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Card_Take_Fund", ColumnType = ColumnType.moneyType, ColumnTitle = "", Length=19, Size=8)]
        public decimal Card_Take_Fund
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Integral", ColumnType = ColumnType.decimalType, ColumnTitle = "", Length=10, Size=9)]
        public decimal Integral
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Cert_ID", ColumnType = ColumnType.intType, ColumnTitle = "", Length=10, Size=4)]
        public int Cert_ID
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Cert_Code", ColumnType = ColumnType.varcharType, ColumnTitle = "", Length=100, Size=100)]
        public string Cert_Code
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Birthday", ColumnType = ColumnType.datetimeType, ColumnTitle = "", Length=23, Size=8)]
        public DateTime Birthday
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Address", ColumnType = ColumnType.varcharType, ColumnTitle = "", Length=500, Size=500)]
        public string Address
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Reserve1", ColumnType = ColumnType.varcharType, ColumnTitle = "", Length=100, Size=100)]
        public string Reserve1
        {
            get;
            set;
        }

        /// <summary>
        /// 用户类别：system；guest；weixin；zhifubao
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Reserve2", ColumnType = ColumnType.varcharType, ColumnTitle = "用户类别：system；guest；weixin；zhifubao", Length=100, Size=100)]
        public string Reserve2
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Reserve3", ColumnType = ColumnType.varcharType, ColumnTitle = "", Length=100, Size=100)]
        public string Reserve3
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "User_ID", ColumnType = ColumnType.intType, ColumnTitle = "", Length=10, Size=4)]
        public int User_ID
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Modify_Date", ColumnType = ColumnType.datetimeType, ColumnTitle = "", Length=23, Size=8)]
        public DateTime Modify_Date
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Create_Date", ColumnType = ColumnType.datetimeType, ColumnTitle = "", Length=23, Size=8)]
        public DateTime Create_Date
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Remark", ColumnType = ColumnType.varcharType, ColumnTitle = "", Length=100, Size=100)]
        public string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Subsidy_Fund", ColumnType = ColumnType.moneyType, ColumnTitle = "", Length=19, Size=8)]
        public decimal Subsidy_Fund
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Subsidy_Flag", ColumnType = ColumnType.tinyintType, ColumnTitle = "", Length=3, Size=1)]
        public byte Subsidy_Flag
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// <summary>
        [Model(AutoReflect = true, ColumnName = "Comsume_Count", ColumnType = ColumnType.decimalType, ColumnTitle = "", Length=10, Size=9)]
        public decimal Comsume_Count
        {
            get;
            set;
        }

    }
}
