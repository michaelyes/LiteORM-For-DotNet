using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ModelApp
{
    public partial class FrmProc : Form
    {
        DataTable dataTable;
        DataTable checkedData;
        int fileNum = 0;

        public FrmProc()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitEvent();
            try
            {
                string outputPath = ParamSetting.GetConfigValue(Global.config_file, "OutputPath");
                txtDir.Text = outputPath;

                string connstr = ParamSetting.GetConfigValue(Global.config_file, "ConnectionString");
                string[] config = connstr.Split(';');
                if (config.Length > 1)
                {
                    GetTableList();
                    this.txtServer.Text = config[0].Substring(config[0].IndexOf("=") + 1);
                    this.cbxDatabase.Text = config[1].Substring(config[1].IndexOf("=") + 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitEvent()
        {
            btnBuildingModel.Click += BtnBuildingModel_Click;
            btnRemoveAll.Click += BtnRemoveAll_Click;
            btnRemoveCurrent.Click += BtnRemoveCurrent_Click;
            btnSelectAll.Click += BtnSelectAll_Click;
            btnSelectCurrent.Click += BtnSelectCurrent_Click;
            btnSelectDir.Click += BtnSelectDir_Click;
            dgvCheckedTable.DoubleClick += DgvCheckedTable_DoubleClick;
            dgvTable.DoubleClick += DgvTable_DoubleClick;
        }

        private void DgvTable_DoubleClick(object sender, EventArgs e)
        {
            BtnSelectCurrent_Click(null, null);
        }

        private void DgvCheckedTable_DoubleClick(object sender, EventArgs e)
        {
            BtnRemoveCurrent_Click(null, null);
        }

        private void BtnRemoveCurrent_Click(object sender, EventArgs e)
        {
            if (dgvCheckedTable.CurrentRow != null)
            {
                RemoveCheckedRow(dgvCheckedTable.CurrentRow);
            }
        }

        private void BtnSelectDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择输出目录";
            dialog.SelectedPath = txtDir.Text;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                txtDir.Text = foldPath;
                ParamSetting.SaveConfig(Global.config_file, "OutputPath", foldPath);
            }
        }

        private void BtnSelectCurrent_Click(object sender, EventArgs e)
        {
            if (dgvTable.CurrentRow != null)
            {
                AddCheckedRow(dgvTable.CurrentRow);
            }
        }

        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dataGridViewRow in dgvTable.Rows)
            {
                AddCheckedRow(dataGridViewRow);
            }
        }

        private void BtnRemoveAll_Click(object sender, EventArgs e)
        {
            if (checkedData != null)
            {
                checkedData.Clear();
            }
        }

        private void BtnBuildingModel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(txtDir.Text))
                {
                    Directory.CreateDirectory(txtDir.Text);
                }

                BuildingModel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MenuSetting_Click(object sender, EventArgs e)
        {
            FrmSetting frmSetting = new FrmSetting();
            frmSetting.StartPosition = FormStartPosition.CenterParent;
            if (frmSetting.ShowDialog() == DialogResult.OK)
            {
                string connstr = ParamSetting.GetConfigValue(Global.config_file, "ConnectionString");
                string[] config = connstr.Split(';');
                if (config.Length > 1)
                {
                    GetTableList();
                    this.txtServer.Text = config[0].Substring(config[0].IndexOf("=") + 1);
                    this.cbxDatabase.Text = config[1].Substring(config[1].IndexOf("=") + 1);
                }
            }
        }

        /// <summary>
        /// 读取数据库所有的存储过程
        /// </summary>
        private void GetTableList()
        {
            try
            {
                //string connstr = ParamSetting.GetConfigValue(Global._config_file, "ConnectionString");
                dgvTable.AutoGenerateColumns = false;
                dgvCheckedTable.AutoGenerateColumns = false;
                dataTable = Dal.GetProcedureList();
                //dataTable.Merge(Dal.GetFuntionList());
                dgvTable.DataSource = dataTable;
                checkedData = dataTable.Clone();
                dgvCheckedTable.DataSource = checkedData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddCheckedRow(DataGridViewRow dataGridViewRow)
        {
            if (checkedData != null && checkedData.Select("id=" + dataGridViewRow.Cells[dgv_Id.Index].Value.ToString()).Length > 0)
            {
                return;
            }
            List<object> objArray = new List<object>();
            foreach (DataGridViewCell cell in dataGridViewRow.Cells)
            {
                if (!string.IsNullOrEmpty(dgvTable.Columns[cell.ColumnIndex].DataPropertyName))
                {
                    objArray.Add(cell.Value);
                }
            }
            checkedData.Rows.Add(objArray.ToArray());
            dgvCheckedTable.ClearSelection();
            dgvCheckedTable.Rows[checkedData.Rows.Count - 1].Selected = true;
        }

        private void RemoveCheckedRow(DataGridViewRow dataGridViewRow)
        {
            if (dgvCheckedTable.Rows.Count != 0)
            {
                dgvCheckedTable.Rows.Remove(dataGridViewRow);
                dgvCheckedTable.EndEdit();
                checkedData.AcceptChanges();
            }
        }

        /// <summary>
        /// 生成存储过程模型
        /// </summary>
        private void BuildingModel()
        {
            string path = txtDir.Text.Trim();
            string strNamespace = txtNamespace.Text.Trim();
            string replaceprefix = txtReplacePrefix.Text.Trim();
            string addSuffix = txtAddSuffix.Text.Trim();

            if (string.IsNullOrEmpty(strNamespace))
            {
                MessageBox.Show("命名空间不能为空");
                return;
            }
            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show("输出目录不能为空");
                return;
            }
            if (this.checkedData == null || this.checkedData.Rows.Count == 0)
            {
                MessageBox.Show("请选择要生成模型的表");
                return;
            }
            //BaseModel.BuildBaseModel(txtNamespace.Text.Trim(), txtDir.Text);
            foreach (DataRow dr in this.checkedData.Rows)
            {
                DataTable dtColumns = Dal.GetProcedureParameters(int.Parse(dr["id"].ToString()));
                string description = "";
                CreateModel(dtColumns, dr["name"].ToString(), description);
            }

            if (MessageBox.Show("生成表模型完成，是否打开到输出目录", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("Explorer.exe", path);
            }
        }

        /// <summary>
        /// 根据存储过程参数信息创建模型
        /// </summary>
        /// <param name="dtColumns">存储过程参数</param>
        /// <param name="tbName">存储过程名</param>
        /// <param name="description">存储过程 注释/说明</param>
        /// <returns>实体模型是否创建成功</returns>
        private bool CreateModel(DataTable dtColumns, string tbName, string description)
        {
            bool successed = false;
            string strNamespace = txtNamespace.Text.Trim();
            string clsName = GetClassName(tbName);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("using System;");
            stringBuilder.AppendLine();
            stringBuilder.Append("using YEasyModel;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("namespace {0}.Proc", strNamespace);
            stringBuilder.AppendLine();
            stringBuilder.Append("{");
            stringBuilder.AppendLine();

            stringBuilder.Append("    /// <summary>");
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("    /// {0}:{1}", tbName, description);
            stringBuilder.AppendLine();
            stringBuilder.Append("    /// </summary>");
            stringBuilder.AppendLine();
            //stringBuilder.Append("    [Serializable]");
            stringBuilder.AppendFormat("    [ProcAttribute(ProcName = \"{0}\")]", tbName);
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat("    public partial class {0}", clsName);
            stringBuilder.AppendLine();
            stringBuilder.Append("    {");
            stringBuilder.AppendLine();
            
            //创建实体类属性
            foreach (DataRow dr in dtColumns.Rows)
            {
                stringBuilder.AppendLine();
                stringBuilder.Append("        /// <summary>");
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("        /// {0}", dr["Description"].ToString());
                stringBuilder.AppendLine();
                stringBuilder.Append("        /// <summary>");
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("        [ProcParaAttribute(AutoReflect = true, ParaName = \"{0}\", DataType = ColumnType.{1}Type, ParaTitle = \"{2}\"", dr["ParaName"].ToString(), dr["DataType"].ToString(), dr["Description"].ToString());
                if ((bool)dr["is_output"])
                {
                    stringBuilder.Append(", Is_Output =true");
                }
                if (dr["has_default_value"].ToString().Equals("1"))
                {
                    stringBuilder.AppendFormat(", Has_Default_Value=true,Default_Value={0}", dr["default_value"]);
                }
                    stringBuilder.AppendFormat(", Length={0}", dr["Length"].ToString());
               
                stringBuilder.AppendFormat(", Size={0}", dr["Length"].ToString());
                stringBuilder.Append(")]");
                stringBuilder.AppendLine();
                stringBuilder.AppendFormat("        public {0} {1}", YEasyModel.TypeUtil.GetTypeName(dr["DataType"].ToString()), dr["ParaName"].ToString());
                stringBuilder.AppendLine();
                stringBuilder.Append("        {");
                stringBuilder.AppendLine();
                stringBuilder.Append("            get;");
                stringBuilder.AppendLine();
                stringBuilder.Append("            set;");
                stringBuilder.AppendLine();
                stringBuilder.Append("        }");
                stringBuilder.AppendLine();
            }
            stringBuilder.AppendLine();
            stringBuilder.Append("    }");
            stringBuilder.AppendLine();

            stringBuilder.Append("}");
            stringBuilder.AppendLine();

            successed = BaseModel.SaveFile(clsName, stringBuilder.ToString(), txtDir.Text);
            if (successed)
                fileNum++;

            return successed;
        }

        /// <summary>
        /// 根据表名、前缀、后缀规则，获取实体类名
        /// </summary>
        /// <param name="tbName">表名</param>
        /// <returns>实体类名</returns>
        private string GetClassName(string tbName)
        {
            string clsName = tbName;
            string replaceprefix = txtReplacePrefix.Text.Trim();
            string addSuffix = txtAddSuffix.Text.Trim();

            if (!string.IsNullOrEmpty(replaceprefix))
            {
                string[] prefixArray = replaceprefix.Split(';');
                foreach (string prefix in prefixArray)
                {
                    if (prefix.ToLower().Equals(tbName.Substring(0, prefix.Length).ToLower()))
                    {
                        clsName = tbName.Substring(prefix.Length, tbName.Length - prefix.Length);
                        break;
                    }
                }
            }
            if (!string.IsNullOrEmpty(addSuffix))
            {
                clsName = clsName + addSuffix;
            }

            return clsName;
        }

        ///<summary>
        /// 移除前缀字符串
        ///</summary>
        ///<param name="val">原字符串</param>
        ///<param name="str">前缀字符串</param>
        ///<returns></returns>
        private string GetRemovePrefixString(string val, string str)
        {
            string strRegex = @"^(" + str + ")";
            return Regex.Replace(val, strRegex, "");
        }
    }
}
