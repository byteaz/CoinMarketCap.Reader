namespace QueryEx
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.tcLeft = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gvQuery = new System.Windows.Forms.DataGridView();
            this.IdQuery = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QueryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.gbResult = new System.Windows.Forms.GroupBox();
            this.ssResult = new System.Windows.Forms.StatusStrip();
            this.ssResultCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.gvMain = new System.Windows.Forms.DataGridView();
            this.gbQuery = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssMain.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.tcLeft.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvQuery)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.gbResult.SuspendLayout();
            this.ssResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            this.gbQuery.SuspendLayout();
            this.SuspendLayout();
            // 
            // ssMain
            // 
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.ssMain.Location = new System.Drawing.Point(0, 728);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(1381, 22);
            this.ssMain.TabIndex = 0;
            this.ssMain.Text = "statusStrip1";
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.tcLeft);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(385, 728);
            this.pnlLeft.TabIndex = 1;
            // 
            // tcLeft
            // 
            this.tcLeft.Controls.Add(this.tabPage1);
            this.tcLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcLeft.Location = new System.Drawing.Point(0, 0);
            this.tcLeft.Name = "tcLeft";
            this.tcLeft.SelectedIndex = 0;
            this.tcLeft.Size = new System.Drawing.Size(385, 728);
            this.tcLeft.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gvQuery);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(377, 702);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Sorğular";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gvQuery
            // 
            this.gvQuery.AllowUserToAddRows = false;
            this.gvQuery.AllowUserToDeleteRows = false;
            this.gvQuery.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.gvQuery.BackgroundColor = System.Drawing.Color.White;
            this.gvQuery.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvQuery.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdQuery,
            this.QueryName});
            this.gvQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvQuery.Location = new System.Drawing.Point(3, 3);
            this.gvQuery.MultiSelect = false;
            this.gvQuery.Name = "gvQuery";
            this.gvQuery.ReadOnly = true;
            this.gvQuery.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvQuery.Size = new System.Drawing.Size(371, 696);
            this.gvQuery.TabIndex = 1;
            this.gvQuery.DoubleClick += new System.EventHandler(this.gvQuery_DoubleClick);
            this.gvQuery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvQuery_KeyDown);
            // 
            // IdQuery
            // 
            this.IdQuery.DataPropertyName = "id_query";
            this.IdQuery.HeaderText = "ID";
            this.IdQuery.Name = "IdQuery";
            this.IdQuery.ReadOnly = true;
            this.IdQuery.Visible = false;
            this.IdQuery.Width = 50;
            // 
            // QueryName
            // 
            this.QueryName.DataPropertyName = "query_name";
            this.QueryName.HeaderText = "Başlıq";
            this.QueryName.Name = "QueryName";
            this.QueryName.ReadOnly = true;
            this.QueryName.Width = 500;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(385, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 728);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.gbResult);
            this.pnlMain.Controls.Add(this.gbQuery);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(388, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(993, 728);
            this.pnlMain.TabIndex = 3;
            // 
            // gbResult
            // 
            this.gbResult.Controls.Add(this.ssResult);
            this.gbResult.Controls.Add(this.gvMain);
            this.gbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbResult.Location = new System.Drawing.Point(0, 343);
            this.gbResult.Name = "gbResult";
            this.gbResult.Size = new System.Drawing.Size(993, 385);
            this.gbResult.TabIndex = 1;
            this.gbResult.TabStop = false;
            this.gbResult.Text = "Nəticə";
            // 
            // ssResult
            // 
            this.ssResult.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ssResultCount});
            this.ssResult.Location = new System.Drawing.Point(3, 360);
            this.ssResult.Name = "ssResult";
            this.ssResult.Size = new System.Drawing.Size(987, 22);
            this.ssResult.TabIndex = 1;
            this.ssResult.Text = "statusStrip1";
            // 
            // ssResultCount
            // 
            this.ssResultCount.Name = "ssResultCount";
            this.ssResultCount.Size = new System.Drawing.Size(13, 17);
            this.ssResultCount.Text = "0";
            // 
            // gvMain
            // 
            this.gvMain.AllowUserToAddRows = false;
            this.gvMain.AllowUserToDeleteRows = false;
            this.gvMain.BackgroundColor = System.Drawing.Color.White;
            this.gvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvMain.Location = new System.Drawing.Point(3, 16);
            this.gvMain.Name = "gvMain";
            this.gvMain.ReadOnly = true;
            this.gvMain.Size = new System.Drawing.Size(987, 366);
            this.gvMain.TabIndex = 0;
            this.gvMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvMain_KeyDown);
            // 
            // gbQuery
            // 
            this.gbQuery.Controls.Add(this.btnClear);
            this.gbQuery.Controls.Add(this.btnRun);
            this.gbQuery.Controls.Add(this.txtQuery);
            this.gbQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbQuery.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gbQuery.Location = new System.Drawing.Point(0, 0);
            this.gbQuery.Name = "gbQuery";
            this.gbQuery.Size = new System.Drawing.Size(993, 343);
            this.gbQuery.TabIndex = 0;
            this.gbQuery.TabStop = false;
            this.gbQuery.Text = "Sorğu";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(6, 314);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Təmizlə";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRun.Location = new System.Drawing.Point(87, 314);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(900, 23);
            this.btnRun.TabIndex = 1;
            this.btnRun.Text = "Nəticə";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // txtQuery
            // 
            this.txtQuery.BackColor = System.Drawing.Color.Black;
            this.txtQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuery.ForeColor = System.Drawing.Color.White;
            this.txtQuery.Location = new System.Drawing.Point(3, 16);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtQuery.Size = new System.Drawing.Size(987, 292);
            this.txtQuery.TabIndex = 0;
            this.txtQuery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuery_KeyDown);
            this.txtQuery.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuery_KeyPress);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(263, 17);
            this.toolStripStatusLabel1.Text = "BYTE.AZ - Azərbaycan Dilində Proqram Təminatı";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1381, 750);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.ssMain);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QueryEx";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.tcLeft.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvQuery)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.gbResult.ResumeLayout(false);
            this.gbResult.PerformLayout();
            this.ssResult.ResumeLayout(false);
            this.ssResult.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            this.gbQuery.ResumeLayout(false);
            this.gbQuery.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TabControl tcLeft;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox gbResult;
        private System.Windows.Forms.GroupBox gbQuery;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.StatusStrip ssResult;
        private System.Windows.Forms.ToolStripStatusLabel ssResultCount;
        private System.Windows.Forms.DataGridView gvMain;
        private System.Windows.Forms.DataGridView gvQuery;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdQuery;
        private System.Windows.Forms.DataGridViewTextBoxColumn QueryName;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

