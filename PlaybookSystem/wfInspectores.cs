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
    public partial class wfInspectores : Form
    {
        public int _liFolio;
        public int _liCode;
        public string _lsReturn;

        public DataTable _dt = new DataTable();
        Globals _gs = new Globals();
        public wfInspectores()
        {
            InitializeComponent();
        }

        private void wfInspectores_Load(object sender, EventArgs e)
        {
            
            txtInput.Focus();
            dgwData.DataSource = null;
            CargarColumnas();


            if (_liFolio > 0)
            {
                DhTracinsLogica dhr = new DhTracinsLogica();
                dhr.Folio = _liFolio;
                dhr.Falla = _liCode;

                _dt = DhTracinsLogica.ConsultarInspView(dhr);
                if (_dt.Rows.Count > 0)
                {
                    dgwData.DataSource = _dt;
                    CargarColumnas();
                }
                 
            }
        }

        private void CargarColumnas()
        {
            int iRows = dgwData.Rows.Count;
            if (iRows == 0)
            {
                DataTable dtNew = new DataTable("Falla");
                dtNew.Columns.Add("folio", typeof(int));
                dtNew.Columns.Add("consec", typeof(int));
                dtNew.Columns.Add("Inspector", typeof(string));
                dtNew.Columns.Add("Nombre", typeof(string));
                dgwData.DataSource = dtNew;
            }

            dgwData.Columns[0].Visible = false;
            dgwData.Columns[1].Visible = false;

            dgwData.Columns[2].Width = ColumnWith(dgwData, 20);
            dgwData.Columns[3].Width = ColumnWith(dgwData, 70);
        }
        private int ColumnWith(DataGridView _dtGrid, double _dColWith)
        {

            double dW = _dtGrid.Width - 10;
            double dTam = _dColWith;
            double dPor = dTam / 100;
            dTam = dW * dPor;
            dTam = Math.Truncate(dTam);

            return Convert.ToInt32(dTam);
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
                DataTable data = dgwData.DataSource as DataTable;
                data.Rows.Add(_liFolio,_liCode,iNumber,sNombre);
                txtInput.Clear();
                txtInput.Focus();
            }
            else
            {
                MessageBox.Show("Numero de Empleado no encontrado", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtInput.Clear();
                return;
            }
        }
       
        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        
        private void wfInspectores_Activated(object sender, EventArgs e)
        {
            txtInput.Focus();
        }

        private void wfInspectores_FormClosing(object sender, FormClosingEventArgs e)
        {
            _dt = dgwData.DataSource as DataTable;
            if (dgwData.Rows.Count == 0)
            {
                MessageBox.Show("El indicador de Inspector sera omitido",Text,MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
            }
        }

        private void dgwData_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Delete)
            {
                if (dgwData.Rows.Count <= 0) return;

                foreach(DataGridViewRow row in dgwData.SelectedRows)
                {
                    dgwData.Rows.Remove(row);
                }
            }
        }
    }
}
