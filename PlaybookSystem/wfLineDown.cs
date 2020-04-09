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
    public partial class wfLineDown : Form
    {
        public string _lsProceso;
        
        Globals _gs = new Globals();
        public wfLineDown()
        {
            InitializeComponent();
        }

        private void wfLineDown_Load(object sender, EventArgs e)
        {
            Inicio();
            
            
        }
        private void Inicio()
        {
            try
            {

                _lsProceso = "PRO030";

                dtpFecha.Value = DateTime.Today;
                lblLinea.Text = "Linea " + Globals._gsLineHr;

                int iHora = DateTime.Now.Hour;
                int iMin = DateTime.Now.Minute;

                string sHora = iHora.ToString().PadLeft(2, '0') + ":" + iMin.ToString().PadLeft(2, '0');
                txtHora.Text = sHora;

                txtDuracion.Text = string.Empty;
                txtMotivo.Text = string.Empty;
                txtNotas.Text = string.Empty;

                txtHora.Focus();
                txtHora.SelectAll();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
           
            
        }
       
        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
        
        private void wfLineDown_Activated(object sender, EventArgs e)
        {
            txtHora.Focus();
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Inicio();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!Valida())
                return;

            try
            {
                LineaDownLogica line = new LineaDownLogica();
                line.Folio = AccesoDatos.Consec(_lsProceso);
                line.Fecha = dtpFecha.Value;
                line.Turno = "1";//Globals._lsTurno;
                line.Linea = Globals._gsLineHr;
                line.Hora = txtHora.Text.ToString();
                line.Duracion = int.Parse(txtDuracion.Text);
                line.Motivo = txtMotivo.Text.ToString();
                line.Notas = txtNotas.Text.ToString();

                if (LineaDownLogica.GuardarSP(line) > 0)
                {
                    MessageBox.Show("Line down "+line.Folio.ToString()+" registrado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al intentar guardar. " + ex.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHora.Focus();
            }
            
        }
        bool IsTime(string myValue)
        {

            bool Succeed = true;

            try
            {
                DateTime temp = Convert.ToDateTime(myValue);
            }
            catch (Exception ex)
            {
                Succeed = false;
            }

            return Succeed;
        }
        private bool Valida()
        {
            bool bValida = true;
            if (string.IsNullOrEmpty(txtHora.Text) || string.IsNullOrWhiteSpace(txtHora.Text))
                bValida = false;
            else
            {
                
                bValida = IsTime(txtHora.Text.ToString());
                if (!bValida)
                {
                    MessageBox.Show("Formato de fecha invalido", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtHora.Focus();
                    txtHora.SelectAll();
                    return false;
                }
            }
        


            if (string.IsNullOrEmpty(txtDuracion.Text) || string.IsNullOrWhiteSpace(txtDuracion.Text))
                bValida = false;
            else
            {
                int iDura = 0;
                if (!int.TryParse(txtDuracion.Text, out iDura))
                    bValida = false;
            }

            if (string.IsNullOrEmpty(txtMotivo.Text) || string.IsNullOrWhiteSpace(txtMotivo.Text))
            {
                MessageBox.Show("Favor de ingresar el motivo del Line Down", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDuracion.Focus();
                bValida = false;
            }

            return bValida;
        }
        private void txtDuracion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
