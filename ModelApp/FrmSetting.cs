using System;
using System.Data;
using System.Windows.Forms;

namespace ModelApp
{
    public partial class FrmSetting : Form
    {
        public FrmSetting()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                string connstr = ParamSetting.GetConfigValue(Global.config_file, "ConnectionString");
                string[] config = connstr.Split(';');
                
                if (config.Length > 1)
                {
                    InitDatabaseList(connstr);
                    this.txtServer.Text = config[0].Substring(config[0].IndexOf("=") + 1);
                    this.cbxDatabase.SelectedValue = config[1].Substring(config[1].IndexOf("=") + 1);
                    this.txtUserName.Text = config[2].Substring(config[2].IndexOf("=") + 1);
                    this.txtPassword.Text = config[3].Substring(config[3].IndexOf("=") + 1);
                }
            }
            catch { }
            btnCancel.Click += BtnCancel_Click;
            btnOK.Click += BtnOK_Click;
            //txtPassword.TextChanged += TxtPassword_TextChanged;
            txtPassword.KeyDown += TxtPassword_KeyDown;
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
            {
                TxtPassword_TextChanged(null, null);
            }
        }

        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {
            string _connStr = "server=" + this.txtServer.Text + ";database=master;uid=" + this.txtUserName.Text + ";pwd=" + this.txtPassword.Text;
            //if (!YEasyModel.DbHelperSQL.IsConnection(_connStr, out Error, false))
            //{
            //    Dialog.ShowError(Error);
            //    return;
            //}
            InitDatabaseList(_connStr);
        }

        private void InitDatabaseList(string conn)
        {
            try
            {
                YEasyModel.DbHelperSQL.connectionString = conn;
                var dt = YEasyModel.DbHelperSQL.Query("SELECT NAME FROM MASTER.DBO.SYSDATABASES ORDER BY NAME").Tables[0];
                DataRow dr = dt.NewRow();
                dr[0] = "选择数据库";
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();
                cbxDatabase.DataSource = dt;
                cbxDatabase.DisplayMember = "Name";
                cbxDatabase.ValueMember = "Name";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            string _connStr = "";
            if (this.cbxDatabase.SelectedIndex == 0)
            {
                MessageBox.Show("请选择数据库");
                return;
            }
            _connStr = "server=" + this.txtServer.Text + ";database=" + this.cbxDatabase.Text + ";uid=" + this.txtUserName.Text + ";pwd=" + this.txtPassword.Text;
            //将数据库连接参数保存到配置文件里
            if (ParamSetting.SaveConfig(Global.config_file, "ConnectionString", _connStr))
            {
                MessageBox.Show("保存数据库配置成功");
                YEasyModel.DbHelperSQL.connectionString = _connStr;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
