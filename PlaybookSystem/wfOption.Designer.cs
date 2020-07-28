namespace PlaybookSystem
{
    partial class wfOption
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
            this.btnKIT = new System.Windows.Forms.Button();
            this.btnDYN = new System.Windows.Forms.Button();
            this.btnWO = new System.Windows.Forms.Button();
            this.btnTIME = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnTIME);
            this.panel1.Controls.Add(this.btnKIT);
            this.panel1.Controls.Add(this.btnDYN);
            this.panel1.Controls.Add(this.btnWO);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(688, 142);
            this.panel1.TabIndex = 0;
            // 
            // btnKIT
            // 
            this.btnKIT.BackColor = System.Drawing.Color.Red;
            this.btnKIT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnKIT.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKIT.ForeColor = System.Drawing.Color.White;
            this.btnKIT.Location = new System.Drawing.Point(347, 19);
            this.btnKIT.Name = "btnKIT";
            this.btnKIT.Size = new System.Drawing.Size(160, 107);
            this.btnKIT.TabIndex = 2;
            this.btnKIT.Text = "KIT";
            this.btnKIT.UseVisualStyleBackColor = false;
            this.btnKIT.Click += new System.EventHandler(this.btnKIT_Click);
            // 
            // btnDYN
            // 
            this.btnDYN.BackgroundImage = global::PlaybookSystem.Properties.Resources.Blue_Background;
            this.btnDYN.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDYN.ForeColor = System.Drawing.Color.White;
            this.btnDYN.Location = new System.Drawing.Point(181, 19);
            this.btnDYN.Name = "btnDYN";
            this.btnDYN.Size = new System.Drawing.Size(160, 107);
            this.btnDYN.TabIndex = 1;
            this.btnDYN.Text = "DYN";
            this.btnDYN.UseVisualStyleBackColor = true;
            this.btnDYN.Click += new System.EventHandler(this.btnDYN_Click);
            // 
            // btnWO
            // 
            this.btnWO.BackgroundImage = global::PlaybookSystem.Properties.Resources.Yellow_Background_down1;
            this.btnWO.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWO.Location = new System.Drawing.Point(15, 19);
            this.btnWO.Name = "btnWO";
            this.btnWO.Size = new System.Drawing.Size(160, 107);
            this.btnWO.TabIndex = 0;
            this.btnWO.Text = "Work Order";
            this.btnWO.UseVisualStyleBackColor = true;
            this.btnWO.Click += new System.EventHandler(this.btnWO_Click);
            // 
            // btnTIME
            // 
            this.btnTIME.BackColor = System.Drawing.Color.SeaGreen;
            this.btnTIME.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnTIME.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTIME.ForeColor = System.Drawing.Color.White;
            this.btnTIME.Location = new System.Drawing.Point(513, 19);
            this.btnTIME.Name = "btnTIME";
            this.btnTIME.Size = new System.Drawing.Size(160, 107);
            this.btnTIME.TabIndex = 3;
            this.btnTIME.Text = "TIME";
            this.btnTIME.UseVisualStyleBackColor = false;
            this.btnTIME.Click += new System.EventHandler(this.btnTIME_Click);
            // 
            // wfOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(707, 166);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Name = "wfOption";
            this.Text = "Select Option";
            this.Load += new System.EventHandler(this.wfOption_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDYN;
        private System.Windows.Forms.Button btnWO;
        private System.Windows.Forms.Button btnKIT;
        private System.Windows.Forms.Button btnTIME;
    }
}