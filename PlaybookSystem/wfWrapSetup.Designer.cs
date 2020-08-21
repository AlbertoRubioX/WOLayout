namespace PlaybookSystem
{
    partial class wfWrapSetup
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.cbbFold = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbtL = new System.Windows.Forms.RadioButton();
            this.rbtM = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.rbtS = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btWrap = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(9, 10);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(359, 230);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Orange;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.cbbFold);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Location = new System.Drawing.Point(14, 148);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(334, 69);
            this.panel4.TabIndex = 2;
            // 
            // cbbFold
            // 
            this.cbbFold.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbFold.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbFold.FormattingEnabled = true;
            this.cbbFold.Location = new System.Drawing.Point(94, 34);
            this.cbbFold.Margin = new System.Windows.Forms.Padding(2);
            this.cbbFold.Name = "cbbFold";
            this.cbbFold.Size = new System.Drawing.Size(132, 28);
            this.cbbFold.TabIndex = 1;
            this.cbbFold.SelectionChangeCommitted += new System.EventHandler(this.cbbFold_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(95, 2);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 33);
            this.label3.TabIndex = 0;
            this.label3.Text = "FOLD TYPE";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.rbtL);
            this.panel3.Controls.Add(this.rbtM);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.rbtS);
            this.panel3.Location = new System.Drawing.Point(14, 54);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(334, 96);
            this.panel3.TabIndex = 2;
            // 
            // rbtL
            // 
            this.rbtL.AutoSize = true;
            this.rbtL.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtL.ForeColor = System.Drawing.Color.Black;
            this.rbtL.Location = new System.Drawing.Point(224, 55);
            this.rbtL.Margin = new System.Windows.Forms.Padding(2);
            this.rbtL.Name = "rbtL";
            this.rbtL.Size = new System.Drawing.Size(79, 26);
            this.rbtL.TabIndex = 2;
            this.rbtL.Text = "Large";
            this.rbtL.UseVisualStyleBackColor = true;
            this.rbtL.CheckedChanged += new System.EventHandler(this.rbtL_CheckedChanged);
            // 
            // rbtM
            // 
            this.rbtM.AutoSize = true;
            this.rbtM.BackColor = System.Drawing.Color.Transparent;
            this.rbtM.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtM.ForeColor = System.Drawing.Color.Black;
            this.rbtM.Location = new System.Drawing.Point(118, 55);
            this.rbtM.Margin = new System.Windows.Forms.Padding(2);
            this.rbtM.Name = "rbtM";
            this.rbtM.Size = new System.Drawing.Size(96, 26);
            this.rbtM.TabIndex = 1;
            this.rbtM.Text = "Medium";
            this.rbtM.UseVisualStyleBackColor = false;
            this.rbtM.CheckedChanged += new System.EventHandler(this.rbtM_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(138, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 33);
            this.label2.TabIndex = 0;
            this.label2.Text = "SIZE";
            // 
            // rbtS
            // 
            this.rbtS.AutoSize = true;
            this.rbtS.Cursor = System.Windows.Forms.Cursors.Default;
            this.rbtS.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtS.ForeColor = System.Drawing.Color.Black;
            this.rbtS.Location = new System.Drawing.Point(26, 55);
            this.rbtS.Margin = new System.Windows.Forms.Padding(2);
            this.rbtS.Name = "rbtS";
            this.rbtS.Size = new System.Drawing.Size(77, 26);
            this.rbtS.TabIndex = 0;
            this.rbtS.Text = "Small";
            this.rbtS.UseVisualStyleBackColor = true;
            this.rbtS.Click += new System.EventHandler(this.rbtS_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btWrap);
            this.panel2.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.panel2.Location = new System.Drawing.Point(14, 10);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(334, 45);
            this.panel2.TabIndex = 1;
            // 
            // btWrap
            // 
            this.btWrap.BackColor = System.Drawing.Color.Transparent;
            this.btWrap.Enabled = false;
            this.btWrap.FlatAppearance.BorderSize = 0;
            this.btWrap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btWrap.Font = new System.Drawing.Font("Calibri", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btWrap.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.btWrap.Location = new System.Drawing.Point(74, 1);
            this.btWrap.Margin = new System.Windows.Forms.Padding(2);
            this.btWrap.Name = "btWrap";
            this.btWrap.Size = new System.Drawing.Size(192, 39);
            this.btWrap.TabIndex = 1;
            this.btWrap.Text = "\'\'";
            this.btWrap.UseVisualStyleBackColor = false;
            // 
            // wfWrapSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PlaybookSystem.Properties.Resources.Blue_Background;
            this.ClientSize = new System.Drawing.Size(377, 249);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "wfWrapSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Playbook Wrapping Type";
            this.Load += new System.EventHandler(this.wfWrapSetup_Load);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox cbbFold;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbtL;
        private System.Windows.Forms.RadioButton rbtM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbtS;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btWrap;
    }
}