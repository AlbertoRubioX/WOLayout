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
    public partial class wfOptionRep : Form
    {
        public string _lsOption;
        public wfOptionRep()
        {
            InitializeComponent();
        }

        private void btnWO_Click(object sender, EventArgs e)
        {
            _lsOption = "R";
            Close();
        }

        private void btnDYN_Click(object sender, EventArgs e)
        {
            _lsOption = "G";
            Close();
        }

        private void wfOptionRep_Load(object sender, EventArgs e)
        {
            btnWO.Focus();
        }
        
    }
}
