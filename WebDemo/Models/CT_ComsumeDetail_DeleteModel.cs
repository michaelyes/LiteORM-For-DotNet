using System;
using YEasyModel;

namespace DBModel.Proc
{
    /// <summary>
    /// CT_ComsumeDetail_Delete:
    /// </summary>
    [ProcAttribute(ProcName = "CT_ComsumeDetail_Delete")]
    public partial class CT_ComsumeDetail_DeleteModel
    {

        /// <summary>
        /// 
        /// <summary>
        [ProcParaAttribute(AutoReflect = true, ParaName = "@Detail_ID", DataType = ColumnType.intType, ParaTitle = "", Length=4, Size=4)]
        public int @Detail_ID
        {
            get;
            set;
        }

    }
}
