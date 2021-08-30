using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logica;
using Datos;

namespace PlaybookSystem
{
    public partial class wfRush : Form
    {

        Globals _gs = new Globals();
        public wfRush()
        {
            InitializeComponent();
        }

        private void wfRush_Load(object sender, EventArgs e)
        {
            Inicio();
        }
        private void Inicio()
        {
            txtSurte.Clear();
            txtTicket.Clear();

            lblLine.Text = "L - " + Globals._gsLine.ToString();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            Inicio();
        }
    }
}
