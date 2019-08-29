namespace ModelApp
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.btnRemoveCurrent = new System.Windows.Forms.Button();
            this.btnSelectCurrent = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.dgvCheckedTable = new System.Windows.Forms.DataGridView();
            this.dgvTable = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbxDatabase = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtAddSuffix = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtReplacePrefix = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBuildingModel = new System.Windows.Forms.Button();
            this.btnSelectDir = new System.Windows.Forms.Button();
            this.txtDir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ckbShowView = new System.Windows.Forms.CheckBox();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheckedTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Gainsboro;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSetting});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1193, 43);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuSetting
            // 
            this.menuSetting.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.menuSetting.Image = ((System.Drawing.Image)(resources.GetObject("menuSetting.Image")));
            this.menuSetting.Name = "menuSetting";
            this.menuSetting.Padding = new System.Windows.Forms.Padding(4);
            this.menuSetting.Size = new System.Drawing.Size(148, 39);
            this.menuSetting.Text = "配置数据库";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 43);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.btnBuildingModel);
            this.splitContainer1.Panel2.Controls.Add(this.btnSelectDir);
            this.splitContainer1.Panel2.Controls.Add(this.txtDir);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(1193, 558);
            this.splitContainer1.SplitterDistance = 367;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRemoveAll);
            this.groupBox1.Controls.Add(this.btnRemoveCurrent);
            this.groupBox1.Controls.Add(this.btnSelectCurrent);
            this.groupBox1.Controls.Add(this.btnSelectAll);
            this.groupBox1.Controls.Add(this.dgvCheckedTable);
            this.groupBox1.Controls.Add(this.dgvTable);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 55);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1193, 312);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择表";
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRemoveAll.Location = new System.Drawing.Point(538, 262);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(115, 35);
            this.btnRemoveAll.TabIndex = 5;
            this.btnRemoveAll.Text = "<<";
            this.btnRemoveAll.UseVisualStyleBackColor = true;
            // 
            // btnRemoveCurrent
            // 
            this.btnRemoveCurrent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnRemoveCurrent.Location = new System.Drawing.Point(538, 197);
            this.btnRemoveCurrent.Name = "btnRemoveCurrent";
            this.btnRemoveCurrent.Size = new System.Drawing.Size(115, 35);
            this.btnRemoveCurrent.TabIndex = 4;
            this.btnRemoveCurrent.Text = "<";
            this.btnRemoveCurrent.UseVisualStyleBackColor = true;
            // 
            // btnSelectCurrent
            // 
            this.btnSelectCurrent.Location = new System.Drawing.Point(538, 132);
            this.btnSelectCurrent.Name = "btnSelectCurrent";
            this.btnSelectCurrent.Size = new System.Drawing.Size(115, 35);
            this.btnSelectCurrent.TabIndex = 3;
            this.btnSelectCurrent.Text = ">";
            this.btnSelectCurrent.UseVisualStyleBackColor = true;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(538, 67);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(115, 35);
            this.btnSelectAll.TabIndex = 2;
            this.btnSelectAll.Text = ">>";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            // 
            // dgvCheckedTable
            // 
            this.dgvCheckedTable.AllowUserToAddRows = false;
            this.dgvCheckedTable.AllowUserToDeleteRows = false;
            this.dgvCheckedTable.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvCheckedTable.ColumnHeadersHeight = 30;
            this.dgvCheckedTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCheckedTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Column4,
            this.dataGridViewTextBoxColumn2});
            this.dgvCheckedTable.Dock = System.Windows.Forms.DockStyle.Right;
            this.dgvCheckedTable.Location = new System.Drawing.Point(699, 26);
            this.dgvCheckedTable.Name = "dgvCheckedTable";
            this.dgvCheckedTable.ReadOnly = true;
            this.dgvCheckedTable.RowHeadersWidth = 20;
            this.dgvCheckedTable.RowTemplate.Height = 25;
            this.dgvCheckedTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCheckedTable.Size = new System.Drawing.Size(491, 283);
            this.dgvCheckedTable.TabIndex = 1;
            // 
            // dgvTable
            // 
            this.dgvTable.AllowUserToAddRows = false;
            this.dgvTable.AllowUserToDeleteRows = false;
            this.dgvTable.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvTable.ColumnHeadersHeight = 30;
            this.dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.dgv_Id,
            this.Column2});
            this.dgvTable.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvTable.Location = new System.Drawing.Point(3, 26);
            this.dgvTable.Name = "dgvTable";
            this.dgvTable.ReadOnly = true;
            this.dgvTable.RowHeadersWidth = 20;
            this.dgvTable.RowTemplate.Height = 25;
            this.dgvTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTable.Size = new System.Drawing.Size(491, 283);
            this.dgvTable.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ckbShowView);
            this.panel1.Controls.Add(this.cbxDatabase);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtServer);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1193, 55);
            this.panel1.TabIndex = 1;
            // 
            // cbxDatabase
            // 
            this.cbxDatabase.Font = new System.Drawing.Font("宋体", 10F);
            this.cbxDatabase.FormattingEnabled = true;
            this.cbxDatabase.Location = new System.Drawing.Point(466, 13);
            this.cbxDatabase.Name = "cbxDatabase";
            this.cbxDatabase.Size = new System.Drawing.Size(238, 28);
            this.cbxDatabase.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F);
            this.label5.Location = new System.Drawing.Point(384, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "数据库：";
            // 
            // txtServer
            // 
            this.txtServer.Font = new System.Drawing.Font("宋体", 10F);
            this.txtServer.Location = new System.Drawing.Point(92, 13);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(238, 30);
            this.txtServer.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10F);
            this.label8.Location = new System.Drawing.Point(11, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "服务器：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtAddSuffix);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtReplacePrefix);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtNamespace);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1193, 89);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "参数设定";
            // 
            // txtAddSuffix
            // 
            this.txtAddSuffix.Location = new System.Drawing.Point(1012, 40);
            this.txtAddSuffix.Name = "txtAddSuffix";
            this.txtAddSuffix.Size = new System.Drawing.Size(158, 30);
            this.txtAddSuffix.TabIndex = 6;
            this.txtAddSuffix.Text = "Model";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(893, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "加类名后缀：";
            // 
            // txtReplacePrefix
            // 
            this.txtReplacePrefix.Location = new System.Drawing.Point(702, 39);
            this.txtReplacePrefix.Name = "txtReplacePrefix";
            this.txtReplacePrefix.Size = new System.Drawing.Size(158, 30);
            this.txtReplacePrefix.TabIndex = 4;
            this.txtReplacePrefix.Text = "t_;tb_";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(350, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(359, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "去掉表名前缀(多个以“;”分号隔开)：";
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(127, 39);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(201, 30);
            this.txtNamespace.TabIndex = 2;
            this.txtNamespace.Text = "DBModel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "命名空间：";
            // 
            // btnBuildingModel
            // 
            this.btnBuildingModel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnBuildingModel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBuildingModel.Location = new System.Drawing.Point(1025, 116);
            this.btnBuildingModel.Name = "btnBuildingModel";
            this.btnBuildingModel.Size = new System.Drawing.Size(145, 35);
            this.btnBuildingModel.TabIndex = 6;
            this.btnBuildingModel.Text = "生成表模型";
            this.btnBuildingModel.UseVisualStyleBackColor = true;
            // 
            // btnSelectDir
            // 
            this.btnSelectDir.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSelectDir.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSelectDir.Location = new System.Drawing.Point(538, 116);
            this.btnSelectDir.Name = "btnSelectDir";
            this.btnSelectDir.Size = new System.Drawing.Size(115, 35);
            this.btnSelectDir.TabIndex = 5;
            this.btnSelectDir.Text = "选择";
            this.btnSelectDir.UseVisualStyleBackColor = true;
            // 
            // txtDir
            // 
            this.txtDir.Location = new System.Drawing.Point(127, 119);
            this.txtDir.Name = "txtDir";
            this.txtDir.Size = new System.Drawing.Size(405, 30);
            this.txtDir.TabIndex = 0;
            this.txtDir.Text = "D:\\DBModel";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "输出目录：";
            // 
            // ckbShowView
            // 
            this.ckbShowView.AutoSize = true;
            this.ckbShowView.Checked = true;
            this.ckbShowView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbShowView.Location = new System.Drawing.Point(742, 13);
            this.ckbShowView.Name = "ckbShowView";
            this.ckbShowView.Size = new System.Drawing.Size(115, 24);
            this.ckbShowView.TabIndex = 8;
            this.ckbShowView.Text = "显示视图";
            this.ckbShowView.UseVisualStyleBackColor = true;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.DataPropertyName = "name";
            this.Column1.FillWeight = 50F;
            this.Column1.HeaderText = "表名";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // dgv_Id
            // 
            this.dgv_Id.DataPropertyName = "id";
            this.dgv_Id.HeaderText = "ID";
            this.dgv_Id.Name = "dgv_Id";
            this.dgv_Id.ReadOnly = true;
            this.dgv_Id.Visible = false;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.DataPropertyName = "Description";
            this.Column2.FillWeight = 50F;
            this.Column2.HeaderText = "备注";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "name";
            this.dataGridViewTextBoxColumn1.FillWeight = 50F;
            this.dataGridViewTextBoxColumn1.HeaderText = "表名";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "id";
            this.Column4.HeaderText = "ID";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn2.FillWeight = 50F;
            this.dataGridViewTextBoxColumn2.HeaderText = "备注";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 601);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("宋体", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "表模型生成";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheckedTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTable)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuSetting;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRemoveAll;
        private System.Windows.Forms.Button btnRemoveCurrent;
        private System.Windows.Forms.Button btnSelectCurrent;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.DataGridView dgvCheckedTable;
        private System.Windows.Forms.DataGridView dgvTable;
        private System.Windows.Forms.Button btnBuildingModel;
        private System.Windows.Forms.Button btnSelectDir;
        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtAddSuffix;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtReplacePrefix;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbxDatabase;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox ckbShowView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}

