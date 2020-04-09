using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlaybookSystem
{
    public partial class wfLineAlert : Form
    {
        public string _lsOption;
        public string _lsHora; 
        public wfLineAlert()
        {
            InitializeComponent(); 
        }
        
        private void wfLineAlert_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            timer1.Start();
        }

        private void btnKIT_Click(object sender, EventArgs e)
        { 
            Close();
        }

        private void wfLineAlert_SizeChanged(object sender, EventArgs e)
        {
            int iW = this.Width / 2;
            int iH = this.Height / 2;

            int iW2 = panel1.Width / 2;
            int iH2 = panel1.Height;

            int iX = iW - iW2;
            int iY = iH - iH2;

            panel1.Location = new Point(iX, panel1.Location.Y);
        }

        private void wfLineAlert_Resize(object sender, EventArgs e)
        {
             

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(btnKIT.BackColor == Color.Red)
            {
                btnKIT.BackColor = Color.White;
                btnKIT.ForeColor = Color.Red;
            }
            else
            {
                btnKIT.BackColor = Color.Red;
                btnKIT.ForeColor = Color.White;
            }
            
        }
    }
}
