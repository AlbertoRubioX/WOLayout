namespace PlaybookSystem
{
    partial class wfDhPending
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chDetenidos = new System.Windows.Forms.CheckBox();
            this.cbDivision = new System.Windows.Forms.ComboBox();
            this.chDivision = new System.Windows.Forms.CheckBox();
            this.cbEstatus = new System.Windows.Forms.ComboBox();
            this.chEstatus = new System.Windows.Forms.CheckBox();
            this.cbLinea = new System.Windows.Forms.ComboBox();
            this.chLinea = new System.Windows.Forms.CheckBox();
            this.btExport = new System.Windows.Forms.Button();
            this.btLoad = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgwData = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtFechaFin = new System.Windows.Forms.DateTimePicker();
            this.dtFechaIni = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl01 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssTable = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssJob = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.cbInspector = new System.Windows.Forms.ComboBox();
            this.chInspector = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwData)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.btExport);
            this.panel1.Controls.Add(this.btLoad);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.lbl01);
            this.panel1.Location = new System.Drawing.Point(7, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1161, 624);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chDetenidos);
            this.groupBox2.Controls.Add(this.cbDivision);
            this.groupBox2.Controls.Add(this.chDivision);
            this.groupBox2.Controls.Add(this.cbEstatus);
            this.groupBox2.Controls.Add(this.chEstatus);
            this.groupBox2.Controls.Add(this.cbLinea);
            this.groupBox2.Controls.Add(this.chLinea);
            this.groupBox2.Location = new System.Drawing.Point(436, 46);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(559, 89);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // chDetenidos
            // 
            this.chDetenidos.AutoSize = true;
            this.chDetenidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.139131F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chDetenidos.Location = new System.Drawing.Point(405, 21);
            this.chDetenidos.Name = "chDetenidos";
            this.chDetenidos.Size = new System.Drawing.Size(137, 21);
            this.chDetenidos.TabIndex = 4;
            this.chDetenidos.Text = "Solo Detenidos";
            this.chDetenidos.UseVisualStyleBackColor = true;
            // 
            // cbDivision
            // 
            this.cbDivision.FormattingEnabled = true;
            this.cbDivision.Location = new System.Drawing.Point(298, 47);
            this.cbDivision.Name = "cbDivision";
            this.cbDivision.Size = new System.Drawing.Size(94, 24);
            this.cbDivision.TabIndex = 5;
            // 
            // chDivision
            // 
            this.chDivision.AutoSize = true;
            this.chDivision.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.139131F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chDivision.Location = new System.Drawing.Point(298, 21);
            this.chDivision.Name = "chDivision";
            this.chDivision.Size = new System.Drawing.Size(84, 21);
            this.chDivision.TabIndex = 4;
            this.chDivision.Text = "División";
            this.chDivision.UseVisualStyleBackColor = true;
            this.chDivision.CheckedChanged += new System.EventHandler(this.chDivision_CheckedChanged);
            // 
            // cbEstatus
            // 
            this.cbEstatus.FormattingEnabled = true;
            this.cbEstatus.Location = new System.Drawing.Point(146, 47);
            this.cbEstatus.Name = "cbEstatus";
            this.cbEstatus.Size = new System.Drawing.Size(137, 24);
            this.cbEstatus.TabIndex = 3;
            // 
            // chEstatus
            // 
            this.chEstatus.AutoSize = true;
            this.chEstatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.139131F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chEstatus.Location = new System.Drawing.Point(146, 21);
            this.chEstatus.Name = "chEstatus";
            this.chEstatus.Size = new System.Drawing.Size(81, 21);
            this.chEstatus.TabIndex = 2;
            this.chEstatus.Text = "Estatus";
            this.chEstatus.UseVisualStyleBackColor = true;
            this.chEstatus.CheckedChanged += new System.EventHandler(this.chEstatus_CheckedChanged_1);
            // 
            // cbLinea
            // 
            this.cbLinea.FormattingEnabled = true;
            this.cbLinea.Location = new System.Drawing.Point(35, 47);
            this.cbLinea.Name = "cbLinea";
            this.cbLinea.Size = new System.Drawing.Size(94, 24);
            this.cbLinea.TabIndex = 1;
            // 
            // chLinea
            // 
            this.chLinea.AutoSize = true;
            this.chLinea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.139131F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chLinea.Location = new System.Drawing.Point(35, 21);
            this.chLinea.Name = "chLinea";
            this.chLinea.Size = new System.Drawing.Size(67, 21);
            this.chLinea.TabIndex = 0;
            this.chLinea.Text = "Linea";
            this.chLinea.UseVisualStyleBackColor = true;
            this.chLinea.CheckedChanged += new System.EventHandler(this.chLinea_CheckedChanged_1);
            // 
            // btExport
            // 
            this.btExport.BackgroundImage = global::PlaybookSystem.Properties.Resources.excel_down;
            this.btExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btExport.Location = new System.Drawing.Point(1071, 66);
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(64, 56);
            this.btExport.TabIndex = 4;
            this.btExport.UseVisualStyleBackColor = true;
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // btLoad
            // 
            this.btLoad.BackgroundImage = global::PlaybookSystem.Properties.Resources.Sync;
            this.btLoad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btLoad.Location = new System.Drawing.Point(1001, 66);
            this.btLoad.Name = "btLoad";
            this.btLoad.Size = new System.Drawing.Size(64, 56);
            this.btLoad.TabIndex = 3;
            this.btLoad.UseVisualStyleBackColor = true;
            this.btLoad.Click += new System.EventHandler(this.btLoad_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgwData);
            this.groupBox3.Location = new System.Drawing.Point(19, 156);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1122, 451);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // dgwData
            // 
            this.dgwData.AllowUserToAddRows = false;
            this.dgwData.AllowUserToDeleteRows = false;
            this.dgwData.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgwData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwData.Location = new System.Drawing.Point(6, 11);
            this.dgwData.Name = "dgwData";
            this.dgwData.ReadOnly = true;
            this.dgwData.RowHeadersVisible = false;
            this.dgwData.RowTemplate.Height = 24;
            this.dgwData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgwData.ShowCellErrors = false;
            this.dgwData.ShowCellToolTips = false;
            this.dgwData.ShowEditingIcon = false;
            this.dgwData.ShowRowErrors = false;
            this.dgwData.Size = new System.Drawing.Size(1110, 434);
            this.dgwData.TabIndex = 0;
            this.dgwData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwData_CellDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbInspector);
            this.groupBox1.Controls.Add(this.dtFechaFin);
            this.groupBox1.Controls.Add(this.chInspector);
            this.groupBox1.Controls.Add(this.dtFechaIni);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(19, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 89);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // dtFechaFin
            // 
            this.dtFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaFin.Location = new System.Drawing.Point(202, 49);
            this.dtFechaFin.Name = "dtFechaFin";
            this.dtFechaFin.Size = new System.Drawing.Size(114, 22);
            this.dtFechaFin.TabIndex = 3;
            // 
            // dtFechaIni
            // 
            this.dtFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaIni.Location = new System.Drawing.Point(57, 49);
            this.dtFechaIni.Name = "dtFechaIni";
            this.dtFechaIni.Size = new System.Drawing.Size(114, 22);
            this.dtFechaIni.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.139131F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(199, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fecha Final";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.139131F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(54, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fecha Inicial";
            // 
            // lbl01
            // 
            this.lbl01.AutoSize = true;
            this.lbl01.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl01.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(56)))), ((int)(((byte)(166)))));
            this.lbl01.Location = new System.Drawing.Point(390, 14);
            this.lbl01.Name = "lbl01";
            this.lbl01.Size = new System.Drawing.Size(167, 25);
            this.lbl01.TabIndex = 0;
            this.lbl01.Text = "Reporte de DHR";
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 693);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1174, 25);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssTable
            // 
            this.tssTable.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tssTable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(56)))), ((int)(((byte)(166)))));
            this.tssTable.Name = "tssTable";
            this.tssTable.Size = new System.Drawing.Size(133, 20);
            this.tssTable.Text = "Medline Playbook";
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
            this.tssJob.Size = new System.Drawing.Size(96, 20);
            this.tssJob.Text = "DHR Tracker";
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
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(81, 20);
            this.toolStripStatusLabel5.Text = "Registros : ";
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
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(19, 19);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.toolStripLabel1,
            this.btnExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1174, 26);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = global::PlaybookSystem.Properties.Resources.New;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23, 23);
            this.btnNew.Text = "New";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 8.765218F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel1.ForeColor = System.Drawing.Color.DarkRed;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(22, 23);
            this.toolStripLabel1.Text = " | ";
            // 
            // btnExit
            // 
            this.btnExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExit.Image = global::PlaybookSystem.Properties.Resources.bt_exit;
            this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(23, 23);
            this.btnExit.Text = "Salir";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cbInspector
            // 
            this.cbInspector.FormattingEnabled = true;
            this.cbInspector.Location = new System.Drawing.Point(346, 47);
            this.cbInspector.Name = "cbInspector";
            this.cbInspector.Size = new System.Drawing.Size(354, 24);
            this.cbInspector.TabIndex = 7;
            this.cbInspector.Visible = false;
            // 
            // chInspector
            // 
            this.chInspector.AutoSize = true;
            this.chInspector.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.139131F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chInspector.Location = new System.Drawing.Point(346, 20);
            this.chInspector.Name = "chInspector";
            this.chInspector.Size = new System.Drawing.Size(94, 21);
            this.chInspector.TabIndex = 6;
            this.chInspector.Text = "Inspector";
            this.chInspector.UseVisualStyleBackColor = true;
            this.chInspector.Visible = false;
            this.chInspector.CheckedChanged += new System.EventHandler(this.chInspector_CheckedChanged);
            // 
            // wfDhPending
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PlaybookSystem.Properties.Resources.Blue_Background;
            this.ClientSize = new System.Drawing.Size(1174, 718);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "wfDhPending";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DHR Tracker Report";
            this.Activated += new System.EventHandler(this.wfDhPending_Activated);
            this.Load += new System.EventHandler(this.wfDhPending_Load);
            this.Resize += new System.EventHandler(this.wfDhPending_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwData)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl01;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssTable;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel tssJob;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel tssTotal;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgwData;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.Button btLoad;
        private System.Windows.Forms.Button btExport;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chDetenidos;
        private System.Windows.Forms.ComboBox cbDivision;
        private System.Windows.Forms.ComboBox cbEstatus;
        private System.Windows.Forms.CheckBox chEstatus;
        private System.Windows.Forms.ComboBox cbLinea;
        private System.Windows.Forms.CheckBox chLinea;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtFechaFin;
        private System.Windows.Forms.DateTimePicker dtFechaIni;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chDivision;
        private System.Windows.Forms.ComboBox cbInspector;
        private System.Windows.Forms.CheckBox chInspector;
    }
}