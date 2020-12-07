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

namespace PlaybookSystem
{
    public partial class wfCN : Form
    {
        public string _lsCode;
        public string _lsReturn;

        private DataTable dt = new DataTable();
        Globals _gs = new Globals();
        public wfCN()
        {
            InitializeComponent();
        }

        private void wfCN_Load(object sender, EventArgs e)
        {
            if(_lsCode == "QA010")
            {
                this.Text = "DHR Tracker - Employee";
                lblText1.Text = "Employee Number";
            }
            
            txtInput.Focus();
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();


            if (e.KeyCode != Keys.Enter)
                return;

            string sValue = txtInput.Text.ToString().Trim();

            if (string.IsNullOrEmpty(sValue))
                return;

            if(_lsCode == "QA010")
            {
                int iNumber = 0;
                if(!int.TryParse(sValue,out iNumber))
                {
                    MessageBox.Show("Numero de Empleado no encontrado", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtInput.Clear();
                    return;
                }

                TressLogica tress = new TressLogica();
                tress.Empleado = iNumber;
                DataTable dt = TressLogica.ConsultarEmpleado(tress);
                if(dt.Rows.Count > 0)
                {
                    string sNombre = dt.Rows[0]["PRETTYNAME"].ToString();
                    _lsCode = sValue;
                    _lsReturn = sNombre;
                    Close();
                }
                else
                {
                    MessageBox.Show("Numero de Empleado no encontrado", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtInput.Clear();
                    _lsReturn = string.Empty;
                    return;
                }
            }
            else
            {
                ConfigLogica con = new ConfigLogica();
                con.CN = sValue;
                DataTable dtC = ConfigLogica.Consultar(con);
                if (dtC.Rows.Count > 0)
                {
                    _lsCode = sValue;
                    Close();
                }
                else
                {
                    MessageBox.Show(_gs.MessageText(this.Name, txtInput.Name, "err1"), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtInput.Clear();
                    txtInput.Focus();
                }
            }
        }
       
        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        
        private void wfCN_Activated(object sender, EventArgs e)
        {
            txtInput.Focus();
            
        }
    }
}
