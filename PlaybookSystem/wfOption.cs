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
    public partial class wfOption : Form
    {
        public string _lsOption;
        public wfOption()
        {
            InitializeComponent();
        }

        private void btnWO_Click(object sender, EventArgs e)
        {
            _lsOption = "W";
            Close();
        }

        private void btnDYN_Click(object sender, EventArgs e)
        {
            _lsOption = "D";
            Close();
        }

        private void wfOption_Load(object sender, EventArgs e)
        {
            btnWO.Focus();
        }

        private void btnKIT_Click(object sender, EventArgs e)
        {
            _lsOption = "K";
            Close();
        }
    }
}
