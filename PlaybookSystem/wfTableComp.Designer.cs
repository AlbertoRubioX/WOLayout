namespace PlaybookSystem
{
    partial class wfTableComp
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbComp = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgwTable = new System.Windows.Forms.DataGridView();
            this.tb1 = new System.Windows.Forms.TabPage();
            this.dgwPre = new System.Windows.Forms.DataGridView();
            this.tb2 = new System.Windows.Forms.TabPage();
            this.dgwPre2 = new System.Windows.Forms.DataGridView();
            this.tb3 = new System.Windows.Forms.TabPage();
            this.dgwPre3 = new System.Windows.Forms.DataGridView();
            this.tb4 = new System.Windows.Forms.TabPage();
            this.dgwPre4 = new System.Windows.Forms.DataGridView();
            this.tb5 = new System.Windows.Forms.TabPage();
            this.dgwPre5 = new System.Windows.Forms.DataGridView();
            this.lbl01 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssTable = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssJob = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.btnRemove = new System.Windows.Forms.ToolStripButton();
            this.btAdd = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwTable)).BeginInit();
            this.tb1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwPre)).BeginInit();
            this.tb2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwPre2)).BeginInit();
            this.tb3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwPre3)).BeginInit();
            this.tb4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwPre4)).BeginInit();
            this.tb5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwPre5)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btAdd);
            this.panel1.Controls.Add(this.cbComp);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.lbl01);
            this.panel1.Location = new System.Drawing.Point(8, 34);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(681, 439);
            this.panel1.TabIndex = 0;
            // 
            // cbComp
            // 
            this.cbComp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbComp.FormattingEnabled = true;
            this.cbComp.Location = new System.Drawing.Point(158, 13);
            this.cbComp.Name = "cbComp";
            this.cbComp.Size = new System.Drawing.Size(426, 28);
            this.cbComp.TabIndex = 16;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tb1);
            this.tabControl1.Controls.Add(this.tb2);
            this.tabControl1.Controls.Add(this.tb3);
            this.tabControl1.Controls.Add(this.tb4);
            this.tabControl1.Controls.Add(this.tb5);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(14, 60);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(648, 359);
            this.tabControl1.TabIndex = 15;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.DodgerBlue;
            this.tabPage1.Controls.Add(this.dgwTable);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(640, 326);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Compenentes por Mesa";
            // 
            // dgwTable
            // 
            this.dgwTable.AllowUserToAddRows = false;
            this.dgwTable.AllowUserToDeleteRows = false;
            this.dgwTable.AllowUserToResizeRows = false;
            this.dgwTable.BackgroundColor = System.Drawing.Color.White;
            this.dgwTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwTable.Location = new System.Drawing.Point(5, 5);
            this.dgwTable.MultiSelect = false;
            this.dgwTable.Name = "dgwTable";
            this.dgwTable.RowHeadersVisible = false;
            this.dgwTable.RowTemplate.Height = 24;
            this.dgwTable.Size = new System.Drawing.Size(629, 315);
            this.dgwTable.TabIndex = 14;
            this.dgwTable.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgwTable_CellFormatting);

            this.dgwTable.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgwTable_ColumnAdded);
            this.dgwTable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgwTable_KeyDown);
            // 
            // tb1
            // 
            this.tb1.BackColor = System.Drawing.Color.LimeGreen;
            this.tb1.Controls.Add(this.dgwPre);
            this.tb1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb1.Location = new System.Drawing.Point(4, 29);
            this.tb1.Name = "tb1";
            this.tb1.Padding = new System.Windows.Forms.Padding(3);
            this.tb1.Size = new System.Drawing.Size(640, 326);
            this.tb1.TabIndex = 1;
            this.tb1.Text = "PS-01";
            // 
            // dgwPre
            // 
            this.dgwPre.AllowUserToAddRows = false;
            this.dgwPre.AllowUserToDeleteRows = false;
            this.dgwPre.AllowUserToResizeRows = false;
            this.dgwPre.BackgroundColor = System.Drawing.Color.White;
            this.dgwPre.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwPre.Location = new System.Drawing.Point(5, 5);
            this.dgwPre.Name = "dgwPre";
            this.dgwPre.RowHeadersVisible = false;
            this.dgwPre.RowTemplate.Height = 24;
            this.dgwPre.Size = new System.Drawing.Size(629, 315);
            this.dgwPre.TabIndex = 15;
            this.dgwPre.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgwPre_ColumnAdded);
            this.dgwPre.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgwPre_KeyDown);
            // 
            // tb2
            // 
            this.tb2.BackColor = System.Drawing.Color.OrangeRed;
            this.tb2.Controls.Add(this.dgwPre2);
            this.tb2.Location = new System.Drawing.Point(4, 29);
            this.tb2.Name = "tb2";
            this.tb2.Padding = new System.Windows.Forms.Padding(3);
            this.tb2.Size = new System.Drawing.Size(640, 326);
            this.tb2.TabIndex = 2;
            this.tb2.Text = "PS-02";
            // 
            // dgwPre2
            // 
            this.dgwPre2.AllowUserToAddRows = false;
            this.dgwPre2.AllowUserToDeleteRows = false;
            this.dgwPre2.AllowUserToResizeRows = false;
            this.dgwPre2.BackgroundColor = System.Drawing.Color.White;
            this.dgwPre2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwPre2.Location = new System.Drawing.Point(5, 5);
            this.dgwPre2.MultiSelect = false;
            this.dgwPre2.Name = "dgwPre2";
            this.dgwPre2.RowHeadersVisible = false;
            this.dgwPre2.RowTemplate.Height = 24;
            this.dgwPre2.Size = new System.Drawing.Size(629, 315);
            this.dgwPre2.TabIndex = 15;
            this.dgwPre2.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgwPre2_ColumnAdded);
            this.dgwPre2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgwPre2_KeyDown);
            // 
            // tb3
            // 
            this.tb3.BackColor = System.Drawing.Color.LightGray;
            this.tb3.Controls.Add(this.dgwPre3);
            this.tb3.Location = new System.Drawing.Point(4, 29);
            this.tb3.Name = "tb3";
            this.tb3.Padding = new System.Windows.Forms.Padding(3);
            this.tb3.Size = new System.Drawing.Size(640, 326);
            this.tb3.TabIndex = 3;
            this.tb3.Text = "PS-03";
            // 
            // dgwPre3
            // 
            this.dgwPre3.AllowUserToAddRows = false;
            this.dgwPre3.AllowUserToDeleteRows = false;
            this.dgwPre3.AllowUserToResizeRows = false;
            this.dgwPre3.BackgroundColor = System.Drawing.Color.White;
            this.dgwPre3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwPre3.Location = new System.Drawing.Point(5, 5);
            this.dgwPre3.MultiSelect = false;
            this.dgwPre3.Name = "dgwPre3";
            this.dgwPre3.RowHeadersVisible = false;
            this.dgwPre3.RowTemplate.Height = 24;
            this.dgwPre3.Size = new System.Drawing.Size(629, 315);
            this.dgwPre3.TabIndex = 15;
            // 
            // tb4
            // 
            this.tb4.BackColor = System.Drawing.Color.MediumPurple;
            this.tb4.Controls.Add(this.dgwPre4);
            this.tb4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tb4.Location = new System.Drawing.Point(4, 29);
            this.tb4.Name = "tb4";
            this.tb4.Padding = new System.Windows.Forms.Padding(3);
            this.tb4.Size = new System.Drawing.Size(640, 326);
            this.tb4.TabIndex = 4;
            this.tb4.Text = "PS-04";
            
            // 
            // dgwPre4
            // 
            this.dgwPre4.AllowUserToAddRows = false;
            this.dgwPre4.AllowUserToDeleteRows = false;
            this.dgwPre4.AllowUserToResizeRows = false;
            this.dgwPre4.BackgroundColor = System.Drawing.Color.White;
            this.dgwPre4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwPre4.Location = new System.Drawing.Point(5, 5);
            this.dgwPre4.MultiSelect = false;
            this.dgwPre4.Name = "dgwPre4";
            this.dgwPre4.RowHeadersVisible = false;
            this.dgwPre4.RowTemplate.Height = 24;
            this.dgwPre4.Size = new System.Drawing.Size(629, 315);
            this.dgwPre4.TabIndex = 15;
            // 
            // tb5
            // 
            this.tb5.BackColor = System.Drawing.Color.LightBlue;
            this.tb5.Controls.Add(this.dgwPre5);
            this.tb5.Location = new System.Drawing.Point(4, 29);
            this.tb5.Name = "tb5";
            this.tb5.Padding = new System.Windows.Forms.Padding(3);
            this.tb5.Size = new System.Drawing.Size(640, 326);
            this.tb5.TabIndex = 5;
            this.tb5.Text = "PS-05";
            // 
            // dgwPre5
            // 
            this.dgwPre5.AllowUserToAddRows = false;
            this.dgwPre5.AllowUserToDeleteRows = false;
            this.dgwPre5.AllowUserToResizeRows = false;
            this.dgwPre5.BackgroundColor = System.Drawing.Color.White;
            this.dgwPre5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwPre5.Location = new System.Drawing.Point(5, 5);
            this.dgwPre5.MultiSelect = false;
            this.dgwPre5.Name = "dgwPre5";
            this.dgwPre5.RowHeadersVisible = false;
            this.dgwPre5.RowTemplate.Height = 24;
            this.dgwPre5.Size = new System.Drawing.Size(629, 315);
            this.dgwPre5.TabIndex = 15;
            // 
            // lbl01
            // 
            this.lbl01.AutoSize = true;
            this.lbl01.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl01.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(56)))), ((int)(((byte)(166)))));
            this.lbl01.Location = new System.Drawing.Point(13, 14);
            this.lbl01.Name = "lbl01";
            this.lbl01.Size = new System.Drawing.Size(148, 25);
            this.lbl01.TabIndex = 0;
            this.lbl01.Text = "Componente: ";
            
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnSave,
            this.btnRemove,
            this.toolStripSeparator1,
            this.btnExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(696, 27);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssTable,
            this.toolStripStatusLabel2,
            this.tssJob,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel5,
            this.tssTotal,
            this.toolStripStatusLabel7});
            this.statusStrip1.Location = new System.Drawing.Point(0, 480);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(696, 25);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssTable
            // 
            this.tssTable.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tssTable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(56)))), ((int)(((byte)(166)))));
            this.tssTable.Name = "tssTable";
            this.tssTable.Size = new System.Drawing.Size(59, 20);
            this.tssTable.Text = "Mesa 1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(22, 20);
            this.toolStripStatusLabel2.Text = " | ";
            // 
            // tssJob
            // 
            this.tssJob.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tssJob.Name = "tssJob";
            this.tssJob.Size = new System.Drawing.Size(48, 20);
            this.tssJob.Text = "DYNJ";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(22, 20);
            this.toolStripStatusLabel4.Text = " | ";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripStatusLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(56)))), ((int)(((byte)(166)))));
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(112, 20);
            this.toolStripStatusLabel5.Text = "Componentes : ";
            // 
            // tssTotal
            // 
            this.tssTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tssTotal.Name = "tssTotal";
            this.tssTotal.Size = new System.Drawing.Size(18, 20);
            this.tssTotal.Text = "0";
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(22, 20);
            this.toolStripStatusLabel7.Text = " | ";
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = global::PlaybookSystem.Properties.Resources.New;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(24, 24);
            this.btnNew.Text = "New";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::PlaybookSystem.Properties.Resources.bt_save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(24, 24);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExit.Image = global::PlaybookSystem.Properties.Resources.bt_exit;
            this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(24, 24);
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRemove.Image = global::PlaybookSystem.Properties.Resources.remove_line;
            this.btnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(24, 24);
            this.btnRemove.Text = "Delete";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btAdd
            // 
            this.btAdd.BackgroundImage = global::PlaybookSystem.Properties.Resources.addfrom;
            this.btAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btAdd.FlatAppearance.BorderSize = 0;
            this.btAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btAdd.Location = new System.Drawing.Point(602, 9);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(41, 41);
            this.btAdd.TabIndex = 17;
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // wfTableComp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 505);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "wfTableComp";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mesa 1";
            this.Activated += new System.EventHandler(this.wfTableComp_Activated);
            this.Load += new System.EventHandler(this.wfTableComp_Load);
            this.Resize += new System.EventHandler(this.wfTableComp_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwTable)).EndInit();
            this.tb1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwPre)).EndInit();
            this.tb2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwPre2)).EndInit();
            this.tb3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwPre3)).EndInit();
            this.tb4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwPre4)).EndInit();
            this.tb5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwPre5)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl01;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgwTable;
        private System.Windows.Forms.TabPage tb1;
        private System.Windows.Forms.DataGridView dgwPre;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.ComboBox cbComp;
        private System.Windows.Forms.ToolStripStatusLabel tssTable;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel tssJob;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel tssTotal;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.TabPage tb2;
        private System.Windows.Forms.DataGridView dgwPre2;
        private System.Windows.Forms.TabPage tb3;
        private System.Windows.Forms.DataGridView dgwPre3;
        private System.Windows.Forms.TabPage tb4;
        private System.Windows.Forms.DataGridView dgwPre4;
        private System.Windows.Forms.TabPage tb5;
        private System.Windows.Forms.DataGridView dgwPre5;
        private System.Windows.Forms.ToolStripButton btnRemove;
    }
}