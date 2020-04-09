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
    public partial class wfHC : Form
    {
        public string _lsOper;
        private int _liMax;
        private int _liMin;
        private DataTable dt = new DataTable();
        Globals _gs = new Globals();
        public wfHC()
        {
            InitializeComponent();
        }

        private void wfHC_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigLogica con = new ConfigLogica();
                con.CN = Globals._gsCompany;
                dt = ConfigLogica.Consultar(con);

                string sMax = dt.Rows[0]["max_hc"].ToString();
                string sMin = dt.Rows[0]["min_hc"].ToString();

                if (!int.TryParse(dt.Rows[0]["max_hc"].ToString(), out _liMax))
                    _liMax = 0;
                if (!int.TryParse(dt.Rows[0]["min_hc"].ToString(), out _liMin))
                    _liMin = 0;

                _gs.ControlText(this.Name, this);
                _gs.ControlText(this.Name, this.panel1);

                this.Text = (Globals._gsLang == "SP") ? "Modo Manual" : "Manual Mode";

                txtInput2.Text = _lsOper;
                txtInput2.Enabled = false;

                txtInput.Text = _lsOper;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
            string sValueAnt = _lsOper;
            _lsOper = sValue;
            if (ValidaHC())
                Close();
            else
            {
                txtInput.Text = sValueAnt;
                _lsOper = sValueAnt;
                txtInput.Focus();
                txtInput.SelectAll();
            }
        }
        private bool ValidaHC()
        {
            //validar min y max
            if (string.IsNullOrEmpty(_lsOper))
                return false;

            int iOper = 0;
            if (!int.TryParse(_lsOper, out iOper))
                return false;
            
            if (_liMax > 0 && _liMin > 0)
            {
                if (iOper <= _liMax && iOper >= _liMin)
                    return true;
                else
                {
                    if(iOper > _liMax)
                        MessageBox.Show(_gs.MessageText(this.Name,txtInput.Name, "err1") + " ( "+_liMax.ToString()+" )", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (iOper < _liMin)
                        MessageBox.Show(_gs.MessageText(this.Name,txtInput.Name, "err2") + " ( " + _liMin.ToString() + " )", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
                return true;

            return false;
            
        }
        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        
        private void wfHC_Activated(object sender, EventArgs e)
        {
            txtInput.Focus();
            txtInput.SelectAll();
        }
    }
}
