namespace PlaybookSystem
{
    partial class wfLineAlert
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnKIT = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnKIT);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(945, 601);
            this.panel1.TabIndex = 0;
            // 
            // btnKIT
            // 
            this.btnKIT.BackColor = System.Drawing.Color.Red;
            this.btnKIT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnKIT.FlatAppearance.BorderSize = 0;
            this.btnKIT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKIT.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKIT.ForeColor = System.Drawing.Color.White;
            this.btnKIT.Location = new System.Drawing.Point(18, 23);
            this.btnKIT.Name = "btnKIT";
            this.btnKIT.Size = new System.Drawing.Size(909, 554);
            this.btnKIT.TabIndex = 2;
            this.btnKIT.Text = "CAPTURAR HORA X HORA";
            this.btnKIT.UseVisualStyleBackColor = false;
            this.btnKIT.Click += new System.EventHandler(this.btnKIT_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // wfLineAlert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(969, 625);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Name = "wfLineAlert";
            this.Text = "Medline - Hora x Hora";
            this.Load += new System.EventHandler(this.wfLineAlert_Load);
            this.SizeChanged += new System.EventHandler(this.wfLineAlert_SizeChanged);
            this.Resize += new System.EventHandler(this.wfLineAlert_Resize);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnKIT;
        private System.Windows.Forms.Timer timer1;
    }
}