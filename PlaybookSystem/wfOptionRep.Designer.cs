﻿namespace PlaybookSystem
{
    partial class wfOptionRep
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
            this.btnDYN = new System.Windows.Forms.Button();
            this.btnWO = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnDYN);
            this.panel1.Controls.Add(this.btnWO);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(403, 142);
            this.panel1.TabIndex = 0;
            // 
            // btnDYN
            // 
            this.btnDYN.BackgroundImage = global::PlaybookSystem.Properties.Resources.Blue_Background;
            this.btnDYN.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDYN.ForeColor = System.Drawing.Color.White;
            this.btnDYN.Location = new System.Drawing.Point(218, 19);
            this.btnDYN.Name = "btnDYN";
            this.btnDYN.Size = new System.Drawing.Size(160, 107);
            this.btnDYN.TabIndex = 1;
            this.btnDYN.Text = "GDP Report";
            this.btnDYN.UseVisualStyleBackColor = true;
            this.btnDYN.Click += new System.EventHandler(this.btnDYN_Click);
            // 
            // btnWO
            // 
            this.btnWO.BackgroundImage = global::PlaybookSystem.Properties.Resources.Yellow_Background_down1;
            this.btnWO.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWO.Location = new System.Drawing.Point(24, 19);
            this.btnWO.Name = "btnWO";
            this.btnWO.Size = new System.Drawing.Size(154, 107);
            this.btnWO.TabIndex = 0;
            this.btnWO.Text = "General Report";
            this.btnWO.UseVisualStyleBackColor = true;
            this.btnWO.Click += new System.EventHandler(this.btnWO_Click);
            // 
            // wfOptionRep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(427, 166);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Name = "wfOptionRep";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Option";
            this.Load += new System.EventHandler(this.wfOptionRep_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDYN;
        private System.Windows.Forms.Button btnWO;
    }
}