using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace PlaybookSystem
{
    public partial class wfTimer : Form
    {
        public decimal _dCycle;
        public wfTimer()
        {
            InitializeComponent();
        }

        private void wfTimer_Load(object sender, EventArgs e)
        {
            tssCycle.Text = _dCycle.ToString();
            tssCycle.Text = _dCycle.ToString();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(64, 64, 64);
            lblTimer.Visible =true;

            int iTime = int.Parse(lblTimer.Text.ToString());
            int iCycle = (int)_dCycle;
            if (iTime < iCycle)
                iTime++;
            else
            {
                //beep
                string[] sSounds = { "notify", "ringout", "tada","ir_inter" };
                int iS = 2;//set as parameter
                string sSound = @"C:\Windows\media\"+sSounds[iS]+".wav";
                if(File.Exists(sSound))
                {
                    SoundPlayer simpleSound = new SoundPlayer(sSound);
                    simpleSound.Play();
                }
                else
                    SystemSounds.Beep.Play();

                iTime = 0;
                lblTimer.Visible = false;
                this.BackColor = Color.DodgerBlue;
            }

            lblTimer.Text = iTime.ToString().PadLeft(2,'0');
        }
    }
}
