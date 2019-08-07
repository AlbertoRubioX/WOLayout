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

namespace WOLayout
{
    public partial class wfConfig : Form
    {
        private string _lsUsuario;
        public wfConfig()
        {
            InitializeComponent();
        }
        private void wfConfig_Activated(object sender, EventArgs e)
        {
            txtJornada.Focus();
        }

        
        private void wfConfig_Load(object sender, EventArgs e)
        {
            Inicio();
        }
        private void Inicio()
        {
            //load data
            try
            {
                _lsUsuario = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                _lsUsuario = _lsUsuario.Substring(_lsUsuario.IndexOf("\\") + 1).ToUpper();

                DataTable data = ConfigLogica.Consultar();
                if (data.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(data.Rows[0][1].ToString()))
                        txtJornada.Text = data.Rows[0][1].ToString();
                    if (!string.IsNullOrEmpty(data.Rows[0][2].ToString()))
                        txtHrDisp.Text = data.Rows[0][2].ToString();
                    if (!string.IsNullOrEmpty(data.Rows[0][3].ToString()))
                        txtSegDisp.Text = data.Rows[0][3].ToString();
                    if(!string.IsNullOrEmpty(data.Rows[0][4].ToString()))
                        txtCajas.Text = data.Rows[0][4].ToString();
                    if(!string.IsNullOrEmpty(data.Rows[0][5].ToString()))
                        txtKitCaja.Text = data.Rows[0][5].ToString();
                    if(!string.IsNullOrEmpty(data.Rows[0][6].ToString()))
                        txtKits.Text = data.Rows[0][6].ToString();
                    if(!string.IsNullOrEmpty(data.Rows[0][7].ToString()))
                        txtTack.Text = data.Rows[0][7].ToString();
                    if(!string.IsNullOrEmpty(data.Rows[0][8].ToString()))
                        txtTack80.Text = data.Rows[0][8].ToString();
                    if(!string.IsNullOrEmpty(data.Rows[0][9].ToString()))
                        txtAssyTime.Text = data.Rows[0][9].ToString();
                    if (!string.IsNullOrEmpty(data.Rows[0][10].ToString()))
                        txtMaxComp.Text = data.Rows[0][10].ToString();
                    if (!string.IsNullOrEmpty(data.Rows[0][11].ToString()))
                        txtMesas.Text = data.Rows[0][11].ToString();
                    if (!string.IsNullOrEmpty(data.Rows[0][12].ToString()))
                        txtMesaWrap.Text = data.Rows[0][12].ToString();
                    if (!string.IsNullOrEmpty(data.Rows[0][13].ToString()))
                        txtMesaSub.Text = data.Rows[0][13].ToString();
                    if (!string.IsNullOrEmpty(data.Rows[0][14].ToString()))
                        txtOperNA.Text = data.Rows[0][14].ToString();
                    if (!string.IsNullOrEmpty(data.Rows[0][15].ToString()))
                        txtSurtidor.Text = data.Rows[0][15].ToString();
                    if (!string.IsNullOrEmpty(data.Rows[0][16].ToString()))
                        txtInspSella.Text = data.Rows[0][16].ToString();
                    if (!string.IsNullOrEmpty(data.Rows[0][17].ToString()))
                        txtSellador.Text = data.Rows[0][17].ToString();
                    if (!string.IsNullOrEmpty(data.Rows[0][18].ToString()))
                        txtInspec.Text = data.Rows[0][18].ToString();

                    if (!string.IsNullOrEmpty(data.Rows[0][21].ToString()))
                        txtHori.Text = data.Rows[0][21].ToString();
                    if (!string.IsNullOrEmpty(data.Rows[0][22].ToString()))
                        txtVertical.Text = data.Rows[0][22].ToString();
                    if (!string.IsNullOrEmpty(data.Rows[0][23].ToString()))
                        txtSobre.Text = data.Rows[0][23].ToString();
                    if (!string.IsNullOrEmpty(data.Rows[0][25].ToString()))
                        txtTape.Text = data.Rows[0][25].ToString();
                    if (!string.IsNullOrEmpty(data.Rows[0][24].ToString()))
                        txtNA.Text = data.Rows[0][24].ToString();

                }

                txtJornada.Focus();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                decimal dJornada = 0;
                decimal dHrsDisp = 0;
                if (!decimal.TryParse(txtJornada.Text.ToString(), out dJornada))
                    dJornada = 0;
                if (!decimal.TryParse(txtHrDisp.Text.ToString(), out dHrsDisp))
                    dHrsDisp = 0;
                decimal dSegDisp = 0;
                if (!decimal.TryParse(txtSegDisp.Text.ToString(), out dSegDisp))
                    dSegDisp = 0;
                decimal dCajas = 0;
                if (!decimal.TryParse(txtCajas.Text.ToString(), out dCajas))
                    dCajas = 0;
                decimal dKits = 0;
                if (!decimal.TryParse(txtKitCaja.Text.ToString(), out dKits))
                    dKits = 0;
                decimal dKitsLine = 0;
                if (!decimal.TryParse(txtKits.Text.ToString(), out dKitsLine))
                    dKitsLine = 0;
                decimal dTak = 0;
                if (!decimal.TryParse(txtTack.Text.ToString(), out dTak))
                    dTak = 0;
                decimal dTak80 = 0;
                if (!decimal.TryParse(txtTack80.Text.ToString(), out dTak80))
                    dTak80 = 0;
                decimal dAssy = 0;
                if (!decimal.TryParse(txtAssyTime.Text.ToString(), out dAssy))
                    dAssy = 0;
                decimal dMax = 0;
                if (!decimal.TryParse(txtMaxComp.Text.ToString(), out dMax))
                    dMax = 0;
                decimal dMesa = 0;
                if (!decimal.TryParse(txtMesas.Text.ToString(), out dMesa))
                    dMesa = 0;
                decimal dMesaW = 0;
                if (!decimal.TryParse(txtMesaWrap.Text.ToString(), out dMesaW))
                    dMesaW = 0;
                decimal dMesaS = 0;
                if (!decimal.TryParse(txtMesaSub.Text.ToString(), out dMesaS))
                    dMesaS = 0;

                decimal dOper = 0;
                if (!decimal.TryParse(txtOperNA.Text.ToString(), out dOper))
                    dOper = 0;
                decimal dSurte = 0;
                if (!decimal.TryParse(txtSurtidor.Text.ToString(), out dSurte))
                    dSurte = 0;
                decimal dInsS = 0;
                if (!decimal.TryParse(txtInspSella.Text.ToString(), out dInsS))
                    dInsS = 0;
                decimal dSella = 0;
                if (!decimal.TryParse(txtSellador.Text.ToString(), out dSella))
                    dSella = 0;
                decimal dInspec = 0;
                if (!decimal.TryParse(txtInspec.Text.ToString(), out dInspec))
                    dInspec = 0;
                decimal dHori = 0;
                if (!decimal.TryParse(txtHori.Text.ToString(), out dHori))
                    dHori = 0;
                decimal dVer = 0;
                if (!decimal.TryParse(txtVertical.Text.ToString(), out dVer))
                    dVer = 0;
                decimal dSobre = 0;
                if (!decimal.TryParse(txtSobre.Text.ToString(), out dSobre))
                    dSobre = 0;
                decimal dTape = 0;
                if (!decimal.TryParse(txtTape.Text.ToString(), out dTape))
                    dTape = 0;
                decimal dWrapNA = 0;
                if (!decimal.TryParse(txtNA.Text.ToString(), out dWrapNA))
                    dWrapNA = 0;

                ConfigLogica conf = new ConfigLogica();
                conf.Jornada = dJornada;
                conf.HorasDisp = dHrsDisp;
                conf.SegDisp = dSegDisp;
                conf.Cajas = dCajas;
                conf.Kits = dKits;
                conf.KitLinea = dKitsLine;
                conf.Tak = dTak;
                conf.Tak80 = dTak80;
                conf.Assy = dAssy;
                conf.MaxComp = dMax;
                conf.Mesas = dMesa;
                conf.MesaWrap = dMesaW;
                conf.MesaSub = dMesaS;
                conf.OperNA = dOper;
                conf.Surtidor = dSurte;
                conf.InspSella = dInsS;
                conf.Selladora = dSella;
                conf.Inspeccion = dInspec;
                conf.Horizontal = dHori;
                conf.Vertical = dVer;
                conf.Sobre = dSobre;
                conf.Tape = dTape;
                conf.WrapNA = dWrapNA;
                conf.Usuario = _lsUsuario;
                if (ConfigLogica.GuardarSP(conf) > 0)
                    Close();
                else
                {
                    MessageBox.Show("Unable to save changes", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtJornada.Focus();
                }
                    
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        #region regTextChanged
        private void txtTack_TextChanged(object sender, EventArgs e)
        {
            double dTak = double.Parse(txtTack.Text.ToString());
            double dTak8 = 0;
            if (dTak > 0)
                dTak8 = dTak * .8;

            txtTack80.Text = dTak8.ToString();
        }
        private void txtHrDisp_TextChanged(object sender, EventArgs e)
        {
            double dVal = double.Parse(txtHrDisp.Text.ToString());
            double dSeg = dVal * 60 * 60;
            txtSegDisp.Text = dSeg.ToString();
        }

        private void txtCajas_TextChanged(object sender, EventArgs e)
        {
            double dVal = double.Parse(txtCajas.Text.ToString());
            double dVal2 = double.Parse(txtKitCaja.Text.ToString());
            double dRes = dVal * dVal2;
            txtKits.Text = dRes.ToString();
        }

        private void txtKitCaja_TextChanged(object sender, EventArgs e)
        {
            double dVal = double.Parse(txtCajas.Text.ToString());
            double dVal2 = double.Parse(txtKitCaja.Text.ToString());
            double dRes = dVal * dVal2;
            txtKits.Text = dRes.ToString();
        }
        #endregion

        #region regKeypress
        private void txtJornada_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.');
        }

        private void txtHrDisp_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.');
        }

        private void txtSegDisp_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.');
        }

        private void txtCajas_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.');
        }

        private void txtKitCaja_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.');
        }

        private void txtKits_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.');
        }

        private void txtTack_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.');
        }

        private void txtTack80_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.');
        }

        private void txtAssyTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.');
        }

        private void txtMaxComp_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtMesas_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtMesaWrap_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtMesaSub_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtOperNA_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtSurtidor_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtInspSella_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtSellador_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtInspec_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtHori_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.');
        }

        private void txtVertical_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.');
        }

        private void txtSobre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.');
        }

        private void txtTape_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.');
        }

        private void txtNA_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.');
        }
        #endregion

        
    }
}
