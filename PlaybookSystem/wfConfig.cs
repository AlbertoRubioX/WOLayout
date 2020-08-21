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
    public partial class wfConfig : Form
    {
        private string _lsUsuario;
        private decimal _ldHrsDisp;
        private string _lsCajas;
        private string _lsKitCaja;
        public string _lsLen;
        Globals _gs = new Globals();
        DataTable _dtLeng = new DataTable();
        public wfConfig()
        {
            InitializeComponent();
        }
        private void wfConfig_Activated(object sender, EventArgs e)
        {
            txtJornada.Focus();
        }
        #region regInicio
        private void wfConfig_Load(object sender, EventArgs e)
        {

            ConfigLogica con = new ConfigLogica();
            con.Form = this.Name;
            _dtLeng = ConfigLogica.GetLanguage(con);

            Dictionary<string, string> List = new Dictionary<string, string>();
            List.Add("SP", "Spanish");
            List.Add("EN", "English");
            cbxLang.DataSource = new BindingSource(List, null);
            cbxLang.DisplayMember = "Value";
            cbxLang.ValueMember = "Key";
            cbxLang.SelectedIndex = 0;

            Dictionary<string, string> List2 = new Dictionary<string, string>();
            List2.Add("100", "100");
            List2.Add("110", "110");
            List2.Add("130", "130");
            List2.Add("200", "200");
            List2.Add("300", "300");
            List2.Add("405", "405");
            List2.Add("500", "500");
            List2.Add("686", "686");
            List2.Add("710", "710");
            List2.Add("790", "790");
            List2.Add("800", "800");
            List2.Add("951", "951");
            cbxCompany.DataSource = new BindingSource(List2, null);
            cbxCompany.DisplayMember = "Value";
            cbxCompany.ValueMember = "Key";
            cbxCompany.SelectedIndex = -1;

            if (!string.IsNullOrEmpty(Globals._gsCompany))
                cbxCompany.SelectedValue = Globals._gsCompany;
            else
                tabControl1.SelectedTab = tabControl1.TabPages[3];

            Dictionary<string, string> ListW = new Dictionary<string, string>();
            ListW.Add("3", "Horizontal");
            ListW.Add("2", "Vertical");
            ListW.Add("1", "Envelope");
            cbbWrap.DataSource = new BindingSource(ListW, null);
            cbbWrap.DisplayMember = "Value";
            cbbWrap.ValueMember = "Key";
            cbbWrap.SelectedIndex = -1;

            Dictionary<string, string> ListS = new Dictionary<string, string>();
            ListS.Add("L", "Large");
            ListS.Add("M", "Medium");
            ListS.Add("S", "Small");
            ListS.Add("N", "N/A");
            cbbSize.DataSource = new BindingSource(ListS, null);
            cbbSize.DisplayMember = "Value";
            cbbSize.ValueMember = "Key";
            cbbSize.SelectedIndex = -1;

            dgwWrap.DataSource = null;
            

            Inicio();
        }
        private void Inicio()
        {
            //load data
            try
            {
                _lsUsuario = Globals._gsUser;

                cbxLang.SelectedIndex = 0;
                txtJornada.Clear();
                txtHrDisp.Clear();
                txtHori.Clear();
                txtSegDisp.Clear();
                txtCajas.Clear();
                txtKitCaja.Clear();
                txtKits.Clear();
                txtTack.Clear();
                txtTack80.Clear();
                txtAssyTime.Clear();
                txtMaxComp.Clear();
                txtMesas.Clear();
                txtMesaWrap.Clear();
                txtMesaSub.Clear();
                txtOperNA.Clear();
                txtSurtidor.Clear();
                txtInspSella.Clear();
                txtSellador.Clear();
                txtInspec.Clear();
                txtVertical.Clear();
                txtSobre.Clear();
                txtHori.Clear();
                txtNA.Clear();
                txtTape.Clear();
                txtDetroit.Clear();
                txtMaxHC.Clear();
                txtMinHC.Clear();
                txtOutAdd.Clear();

                chbSpeed.Checked = false;
                chbWrapSetup.Checked = false;
                cbbWrap.SelectedIndex = -1;
                cbbSize.SelectedIndex = -1;

                chbBoxHr.Checked = false;
                chbCycleTimer.Checked = false;
                chbActive.Checked = false;

                ConfigLogica con = new ConfigLogica();
                con.CN = Globals._gsCompany;
                DataTable data = ConfigLogica.Consultar(con);
                if (data.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(data.Rows[0][1].ToString()))
                        txtJornada.Text = data.Rows[0][1].ToString();
                    if (!string.IsNullOrEmpty(data.Rows[0][2].ToString()))
                        txtHrDisp.Text = data.Rows[0][2].ToString();
                    if (!string.IsNullOrEmpty(data.Rows[0][3].ToString()))
                        txtSegDisp.Text = data.Rows[0][3].ToString();
                    if (!string.IsNullOrEmpty(data.Rows[0][4].ToString()))
                        txtCajas.Text = data.Rows[0][4].ToString();
                    if (!string.IsNullOrEmpty(data.Rows[0][5].ToString()))
                        txtKitCaja.Text = data.Rows[0][5].ToString();
                    if (!string.IsNullOrEmpty(data.Rows[0][6].ToString()))
                        txtKits.Text = data.Rows[0][6].ToString();
                    if (!string.IsNullOrEmpty(data.Rows[0][7].ToString()))
                        txtTack.Text = data.Rows[0][7].ToString();
                    if (!string.IsNullOrEmpty(data.Rows[0][8].ToString()))
                        txtTack80.Text = data.Rows[0][8].ToString();
                    if (!string.IsNullOrEmpty(data.Rows[0][9].ToString()))
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
                    if (!string.IsNullOrEmpty(data.Rows[0]["detroit"].ToString()))
                        txtDetroit.Text = data.Rows[0]["detroit"].ToString();
                    cbxLang.SelectedValue = data.Rows[0]["lenguage"].ToString();
                    txtMaxHC.Text = data.Rows[0]["max_hc"].ToString();
                    txtMinHC.Text = data.Rows[0]["min_hc"].ToString();
                    txtOutAdd.Text = data.Rows[0]["out_addtime"].ToString();

                    if (data.Rows[0]["ind_boxhr"].ToString() == "1")
                        chbBoxHr.Checked = true;

                    if (data.Rows[0]["cycle_timer"].ToString() == "1")
                        chbCycleTimer.Checked = true;

                    if (data.Rows[0]["active"].ToString() == "1")
                        chbActive.Checked = true;

                    if (data.Rows[0]["band_speed"].ToString() == "1")
                        chbSpeed.Checked = true;

                    if (data.Rows[0]["wrap_setup"].ToString() == "1")
                        chbWrapSetup.Checked = true;
                }

                groupBox11.Enabled = chbWrapSetup.Checked;

                LineaRampeoLogica lin = new LineaRampeoLogica();
                lin.CN = Globals._gsCompany;
                dgwLine.DataSource = LineaRampeoLogica.Vista(lin);
                CargarColumnas();

                ControlGridText(dgwLine);
                
                ControlText(tabPage1);
                ControlText(tabPage2);
                ControlText(tabPage3);

                cbbClave.DataSource = LineaConfigLogica.Consultar();
                cbbClave.ValueMember = "clave";
                cbbClave.DisplayMember = "clave";
                cbbClave.SelectedIndex = -1;


                Dictionary<string, string> List = new Dictionary<string, string>();
                List.Add("CJ", "Cajas");
                List.Add("KT", "Kits");
                cbbMetaum.DataSource = new BindingSource(List, null);
                cbbMetaum.DisplayMember = "Value";
                cbbMetaum.ValueMember = "Key";
                cbbMetaum.SelectedIndex = 0;

                cbbTurno.DataSource = LineaConfigLogica.ConsultarTurnos();
                cbbTurno.DisplayMember = "turno";
                cbbTurno.ValueMember = "turno";
                cbbTurno.SelectedValue = 0;

                dgwHrs.DataSource = null;
                dgwLineas.DataSource = null;
                dgwWrap.DataSource = null;

                txtJornada.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void ControlGridText(DataGridView _control)
        {
            string sControl = _control.Name;
            foreach (DataGridViewColumn c in _control.Columns)
            {
                if (c.Visible)
                {
                    string sSubControl = c.Name;
                    string sValue = GetLengText(sControl, sSubControl);
                    if (!string.IsNullOrEmpty(sValue))
                        c.HeaderText = sValue;
                }
            }
        }

        public void ControlText(Control _control)
        {

            int iControls = _control.Controls.Count;
            for (int i = 0; i < iControls; i++)
            {
                string sControl = _control.Controls[i].Name.ToString();
                string sValue = GetLengText(sControl, null);
                if (!string.IsNullOrEmpty(sValue))
                    _control.Controls[i].Text = sValue;
            }
            foreach (Control c in _control.Controls)
            {
                if (c is GroupBox || c is Button || c is Label)
                {
                    string sValue = GetLengText(c.Name, null);
                    if (!string.IsNullOrEmpty(sValue))
                        c.Text = sValue;
                }

                foreach (Control cs in c.Controls)
                {
                    if (cs is Label || cs is GroupBox || cs is CheckBox)
                    {
                        string sValue = GetLengText(cs.Name, null);
                        if (!string.IsNullOrEmpty(sValue))
                            cs.Text = sValue;
                    }
                }
            }
        }
        private string GetLengText(string _asName, string _asName2)
        {
            string sText = string.Empty;
            int i = 2;
            if (Globals._gsLang == "EN")
                i = 3;

            for (int x = 0; x < _dtLeng.Rows.Count; x++)
            {
                string sControl = _dtLeng.Rows[x][1].ToString();
                if (sControl == _asName)
                {
                    if (string.IsNullOrEmpty(_asName2))
                    {
                        sText = _dtLeng.Rows[x][i].ToString();
                        break;
                    }
                    else
                    {//gridviews
                        sControl = _dtLeng.Rows[x][4].ToString().ToUpper();
                        if (sControl == _asName2.ToUpper())
                        {
                            sText = _dtLeng.Rows[x][i].ToString();
                            break;
                        }
                    }
                }
            }

            return sText;
        }
        private void CargarColumnas()
        {
            if (dgwLine.Rows.Count == 0)
            {
                DataTable dtNew = new DataTable("Line");
                dtNew.Columns.Add("Station", typeof(string));
                dtNew.Columns.Add("Line", typeof(string));
                dtNew.Columns.Add("Factor", typeof(decimal));
                dgwLine.DataSource = dtNew;
            }

            //dgwLine.Columns[0].Width = ColumnWith(dgwLine, 20);
            dgwLine.Columns[2].DefaultCellStyle.Format = "N2";
            dgwLine.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        private void CargarColumnasW()
        {
            if (dgwWrap.Rows.Count == 0)
            {
                DataTable dtNew = new DataTable("Line");
                dtNew.Columns.Add("Fold Type", typeof(string));
                dtNew.Columns.Add("Duration", typeof(decimal));
                dgwWrap.DataSource = dtNew;
            }

            dgwWrap.Columns[0].Width = ColumnWith(dgwWrap, 50);
            dgwWrap.Columns[1].Width = ColumnWith(dgwWrap, 50);
            dgwWrap.Columns[1].DefaultCellStyle.Format = "N2";
            dgwWrap.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        private void CargarColumnasHr()
        {
            if (dgwHrs.Rows.Count == 0)
            {
                DataTable dtNew = new DataTable("Horario");
                dtNew.Columns.Add("consec", typeof(decimal));
                dtNew.Columns.Add("Hora", typeof(string));
                dtNew.Columns.Add("Meta", typeof(decimal));
                dtNew.Columns.Add("Meta Acumulada", typeof(decimal));
                dgwHrs.DataSource = dtNew;
            }
            dgwHrs.Columns[0].Visible = false;
            dgwHrs.Columns[1].Width = ColumnWith(dgwHrs, 50);
            dgwHrs.Columns[2].Width = ColumnWith(dgwHrs, 20); ;
            dgwHrs.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgwHrs.Columns[3].Width = ColumnWith(dgwHrs, 25); ;
            dgwHrs.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            if (dgwLineas.Rows.Count == 0)
            {
                DataTable dtNew = new DataTable("Lineas");
                dtNew.Columns.Add("consec", typeof(decimal));
                dtNew.Columns.Add("Linea", typeof(string));
                dgwLineas.DataSource = dtNew;
            }
            dgwLineas.Columns[0].Visible = false;

            dgwHrs.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


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

        #endregion  
        //private void ControlText(Control _control)
        //{
        //    ConfigLogica con = new ConfigLogica();
        //    con.Language = Globals._gsLang;
        //    con.Form = this.Name;

        //    foreach (Control c in _control.Controls)
        //    {
        //        if (c is GroupBox)
        //        {
        //            con.Control = c.Name;
        //            string sValue = ConfigLogica.ChangeLanguageCont(con);
        //            if (!string.IsNullOrEmpty(sValue))
        //                c.Text = sValue;
        //        }

        //        foreach (Control cs in c.Controls)
        //        {
        //            if (cs is Label || cs is GroupBox || cs is CheckBox)
        //            {
        //                con.Control = cs.Name;
        //                string sValue = ConfigLogica.ChangeLanguageCont(con);
        //                if (!string.IsNullOrEmpty(sValue))
        //                    cs.Text = sValue;
        //            }
        //        }
        //    }
        //}

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
                decimal dWrapDet = 0;
                if (!decimal.TryParse(txtDetroit.Text.ToString(), out dWrapDet))
                    dWrapDet = 0;
                decimal dMaxHC = 0;
                if (!decimal.TryParse(txtMaxHC.Text.ToString(), out dMaxHC))
                    dMaxHC = 0;
                decimal dMinHC = 0;
                if (!decimal.TryParse(txtMinHC.Text.ToString(), out dMinHC))
                    dMinHC = 0;
                string sBoxHr = "0";
                if (chbBoxHr.Checked)
                    sBoxHr = "1";
                string sActive = "0";
                if (chbActive.Checked)
                    sActive = "1";
                string sSpeed = "0";
                if (chbSpeed.Checked)
                    sSpeed = "1";

                decimal dOutAdd = 0;
                if (!decimal.TryParse(txtOutAdd.Text.ToString(), out dOutAdd))
                    dOutAdd = 0;


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
                conf.Detroit = dWrapDet;
                conf.Language = cbxLang.SelectedValue.ToString();
                conf.MaxHC = (int)dMaxHC;
                conf.MinHC = (int)dMinHC;
                conf.BoxHr = sBoxHr;
                if (chbCycleTimer.Checked)
                    conf.CycleTimer = "1";
                else
                    conf.CycleTimer = "0";
                conf.Speed = sSpeed;
                conf.OutAddTime = dOutAdd;
                conf.Usuario = _lsUsuario;
                conf.CN = Globals._gsCompany;
                conf.ActiveCN = sActive;

                if (ConfigLogica.GuardarSP(conf) > 0)
                {
                    _lsLen = conf.Language;
                    Globals._gsLang = _lsLen;
                    Close();
                }
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
            if (string.IsNullOrEmpty(txtTack.Text.ToString()))
                return;

            double dTak = double.Parse(txtTack.Text.ToString());
            double dTak8 = 0;
            if (dTak > 0)
                dTak8 = dTak * .8;

            txtTack80.Text = dTak8.ToString();
        }
        private void txtHrDisp_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHrDisp.Text.ToString()))
                return;

            decimal dVal = decimal.Parse(txtHrDisp.Text.ToString());
            if (dVal != _ldHrsDisp)
            {
                decimal dSeg = dVal * 60 * 60;
                txtSegDisp.Text = dSeg.ToString();
            }

            _ldHrsDisp = dVal;
        }

        private void txtCajas_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCajas.Text.ToString()))
                return;

            if (!string.IsNullOrEmpty(_lsCajas) && _lsCajas != txtCajas.Text.ToString())
            {
                decimal dVal = decimal.Parse(txtCajas.Text.ToString());
                decimal dVal2 = 0;
                if (!decimal.TryParse(txtKitCaja.Text.ToString(), out dVal2))
                    dVal2 = 0;
                decimal dRes = dVal * dVal2;
                txtKits.Text = dRes.ToString();
            }
            _lsCajas = txtCajas.Text.ToString();

        }

        private void txtKitCaja_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKitCaja.Text.ToString()))
                return;

            if (!string.IsNullOrEmpty(_lsKitCaja) && _lsKitCaja != txtKitCaja.Text.ToString())
            {
                decimal dVal = 0;
                if (!decimal.TryParse(txtCajas.Text.ToString(), out dVal))
                    dVal = 0;
                decimal dVal2 = decimal.Parse(txtKitCaja.Text.ToString());
                decimal dRes = dVal * dVal2;
                txtKits.Text = dRes.ToString();
            }
            _lsKitCaja = txtKitCaja.Text.ToString();
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

        private void cbxLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            _lsLen = cbxLang.SelectedValue.ToString();

            Globals gs = new Globals();
            ControlText(tabPage1);
            ControlText(tabPage2);
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (dgwLine.Rows.Count <= 1)
                return;

            Cursor = Cursors.WaitCursor;

            try
            {
                foreach (DataGridViewRow row in dgwLine.Rows)
                {
                    if (row.Index == dgwLine.Rows.Count - 1)
                        continue;

                    string sStation = row.Cells[0].Value.ToString();
                    string sLine = row.Cells[1].Value.ToString();
                    decimal dFactor = 0;
                    if (!decimal.TryParse(row.Cells[2].Value.ToString(), out dFactor))
                        dFactor = 0;

                    LineaRampeoLogica line = new LineaRampeoLogica();
                    line.CN = Globals._gsCompany;
                    line.Estacion = sStation;
                    line.Linea = sLine;
                    line.Factor = dFactor;
                    line.Usuario = Globals._gsUser;
                    LineaRampeoLogica.GuardarSP(line);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            Cursor = Cursors.Default;
        }

        private void btRemove_Click(object sender, EventArgs e)
        {
            if (dgwLine.Rows.Count <= 1)
                return;

            foreach (DataGridViewRow row in dgwLine.SelectedRows)
            {
                if (row.Index == dgwLine.Rows.Count - 1)
                    continue;

                string sStation = row.Cells[0].Value.ToString();

                if (MessageBox.Show(string.Format(_gs.ControlGridRows(this.Name, dgwLine, "rest01") + "{0} ?", sStation), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dgwLine.Rows.RemoveAt(row.Index);
                    LineaRampeoLogica line = new LineaRampeoLogica();
                    line.CN = Globals._gsCompany;
                    line.Estacion = sStation;
                    LineaRampeoLogica.Eliminar(line);

                }
                else
                    continue;
            }
        }

        private void cbxCompany_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Globals._gsCompany = cbxCompany.SelectedValue.ToString();
            Inicio();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != 3)
            {
                if (cbxCompany.SelectedIndex == -1)
                    tabControl1.SelectedTab = tabControl1.TabPages[3];
            }
        }
        // hora por hora
        

        private void cbbClave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();


            if (e.KeyCode != Keys.Enter)
                return;

            try
            {

                if (!string.IsNullOrEmpty(cbbClave.Text) && !string.IsNullOrWhiteSpace(cbbClave.Text))
                {

                    string sCodigo = cbbClave.Text.ToUpper().ToString();

                    LineaConfigLogica con = new LineaConfigLogica();
                    con.Clave = sCodigo;

                    DataTable datos = LineaConfigLogica.ConsultarClave(con);
                    if (datos.Rows.Count != 0)
                    {
                        txtDescrip.Text = datos.Rows[0]["descrip"].ToString();
                        lblMeta.Text = datos.Rows[0]["meta"].ToString();
                        cbbMetaum.SelectedValue = datos.Rows[0]["meta_um"].ToString();
                        cbbTurno.SelectedValue = datos.Rows[0]["turno"].ToString();

                        DataTable dtH = LineaConfigLogica.ConsultarVistaHorario(con);
                        dgwHrs.DataSource = dtH;

                        DataTable dtL = LineaConfigLogica.VistaClaveLine(con);
                        dgwLineas.DataSource = dtL;

                        CargarColumnasHr();
                    }
                    else
                    {
                        // nuevo codigo
                        InicioHr();
                        cbbClave.Text = sCodigo;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Favor de notificar al Administrador." + Environment.NewLine + ex.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
        private void InicioHr()
        {
            txtDescrip.Clear();
            lblMeta.Text = "0";
            cbbMetaum.SelectedIndex = -1;
            cbbTurno.SelectedIndex = -1;
            dgwHrs.DataSource = null;
            dgwLineas.DataSource = null;

            CargarColumnasHr();
        }
        private void cbbClave_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbClave.SelectedIndex == -1)
                return;

            dgwHrs.DataSource = null;
            dgwLineas.DataSource = null;

            string sCve = cbbClave.SelectedValue.ToString();

            LineaConfigLogica con = new LineaConfigLogica();
            con.Clave = sCve;
            DataTable data = LineaConfigLogica.ConsultarClave(con);
            if (data.Rows.Count > 0)
            {
                txtDescrip.Text = data.Rows[0]["descrip"].ToString();
                lblMeta.Text = data.Rows[0]["meta"].ToString();
                cbbMetaum.SelectedValue = data.Rows[0]["meta_um"].ToString();
                cbbTurno.SelectedValue = data.Rows[0]["turno"].ToString();

                DataTable dtH = LineaConfigLogica.ConsultarVistaHorario(con);
                dgwHrs.DataSource = dtH;

                DataTable dtL = LineaConfigLogica.VistaClaveLine(con);
                dgwLineas.DataSource = dtL;
                CargarColumnasHr();
            }

        }

        private void dgwHrs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int iRow = e.RowIndex;
            if ((iRow % 2) == 0)
                e.CellStyle.BackColor = Color.LightSkyBlue;
            else
                e.CellStyle.BackColor = Color.White;
        }

        private void dgwLineas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int iRow = e.RowIndex;
            if ((iRow % 2) == 0)
                e.CellStyle.BackColor = Color.LightSkyBlue;
            else
                e.CellStyle.BackColor = Color.White;
        }

        private void btnSaveHr_Click(object sender, EventArgs e)
        {
            if (!Valida())
                return;

            if (!ValidaAcceso())
            {
                MessageBox.Show("No tiene permiso para modificar esta configuración", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                LineaConfigLogica con = new LineaConfigLogica();
                string sCve = cbbClave.Text.ToString();
                con.Clave = sCve;
                con.Descrip = txtDescrip.Text.ToString();
                con.Meta = int.Parse(lblMeta.Text.ToString());
                con.MetaUm = cbbMetaum.SelectedValue.ToString();
                con.Turno = cbbTurno.SelectedValue.ToString();

                if (LineaConfigLogica.GuardarSP(con) > 0)
                {
                    foreach (DataGridViewRow row in dgwHrs.Rows)
                    {
                        if (row.Index == dgwHrs.Rows.Count - 1)
                            continue;

                        if (string.IsNullOrEmpty(row.Cells[1].Value.ToString()))
                            continue;

                        LineaConfigHrLogica conh = new LineaConfigHrLogica();
                        conh.Clave = con.Clave;
                        conh.Consec = int.Parse(row.Cells[0].Value.ToString());
                        conh.Horario = row.Cells[1].Value.ToString();
                        conh.Meta = int.Parse(row.Cells[2].Value.ToString());
                        conh.MetaAc = int.Parse(row.Cells[3].Value.ToString());

                        LineaConfigHrLogica.GuardarSP(conh);

                    }
                    if (dgwLineas.RowCount > 1)
                    {
                        foreach (DataGridViewRow row in dgwLineas.Rows)
                        {
                            if (row.Index == dgwLineas.Rows.Count - 1)
                                continue;

                            int iLine = 0;
                            if (!int.TryParse(row.Cells[1].Value.ToString(), out iLine))
                                continue;

                            LineaConfigLnLogica conL = new LineaConfigLnLogica();
                            conL.Clave = con.Clave;
                            int iCons = 0;
                            if (!int.TryParse(row.Cells[0].Value.ToString(), out iCons))
                                iCons = 0;
                            conL.Consec = iCons;
                            conL.Linea = row.Cells[1].Value.ToString();

                            LineaConfigLnLogica.GuardarSP(conL);

                        }
                    }
                }
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
          
           
        }
        private bool Valida()
        {
            bool bValida = false;
            if (string.IsNullOrEmpty(cbbClave.Text))
            {
                return bValida;
            }
            if (string.IsNullOrEmpty(txtDescrip.Text))
            {
                MessageBox.Show("No se a especificado la Descripción", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbbClave.Focus();
                return bValida;
            }


            if (string.IsNullOrEmpty(lblMeta.Text))
            {
                MessageBox.Show("No se a especificado la Meta", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return bValida;
            }
            else
            {
                int iMeta = 0;
                if (!int.TryParse(lblMeta.Text, out iMeta))
                    iMeta = 0;
                if (iMeta <= 0)
                {
                    MessageBox.Show("La Meta capturada no es valida", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return bValida;
                }
            }

            if (cbbMetaum.SelectedIndex == -1)
            {
                MessageBox.Show("No se a especificado la unidad de medida", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbbMetaum.Focus();
                return bValida;
            }

            if (cbbTurno.SelectedIndex == -1)
            {
                MessageBox.Show("No se a especificado el turno", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbbTurno.Focus();
                return bValida;
            }

            if (dgwHrs.RowCount <= 0)
            {
                MessageBox.Show("No se a especificado el Horario", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgwHrs.Focus();
                return bValida;
            }

            return true;
        }
        private bool ValidaAcceso()
        {
            UsuarioLogica user = new UsuarioLogica();
            user.Usuario = Globals._gsUser;
            user.Acceso = "2";
            if (UsuarioLogica.ValidaAcceso2(user))
                return true;

            return false;
        }

        private void dgwHrs_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 2)
            {

                try
                {
                    string sValor = dgwHrs[e.ColumnIndex, e.RowIndex].Value.ToString();
                    if (!string.IsNullOrEmpty(sValor) && !string.IsNullOrWhiteSpace(sValor))
                    {

                        int iCant = 0;
                        if (!int.TryParse(sValor, out iCant))
                        {
                            MessageBox.Show("El valor capturado no es válido", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgwHrs[e.ColumnIndex, e.RowIndex].Value = 0;
                            return;
                        }
                        else
                        {
                            int iActualAc = 0;
                            foreach (DataGridViewRow row in dgwHrs.Rows)
                            {
                                if (row.Index == dgwHrs.RowCount - 1)
                                    continue;

                                if (string.IsNullOrEmpty(dgwHrs[2, row.Index].Value.ToString()))
                                    continue;

                                int iActual = int.Parse(dgwHrs[2, row.Index].Value.ToString());
                                if (iActual == 0)
                                    dgwHrs[3, row.Index].Value = "0";
                                else
                                {
                                    if (row.Index == 0)
                                        iActualAc = iActual;
                                    else
                                    {
                                        int iActualAcAnt = int.Parse(dgwHrs[3, row.Index - 1].Value.ToString());
                                        iActualAc = iActual + iActualAcAnt;
                                    }
                                    dgwHrs[3, row.Index].Value = iActualAc.ToString();
                                }
                            }
                            lblMeta.Text = iActualAc.ToString();

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Favor de Notificar al Administrador" + Environment.NewLine + "dgwLine_CellValueChanged(6)" + Environment.NewLine + ex.ToString(), "Error en " + Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void cbbTurno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbTurno.SelectedIndex == -1 || cbbClave.SelectedIndex != -1)
                return;

            string sTurno = cbbTurno.SelectedValue.ToString();
            LineaConfigLogica con = new LineaConfigLogica();
            con.Turno = sTurno;
            DataTable dt = LineaConfigLogica.VistaTurnoHr(con);
            dgwHrs.DataSource = dt;
            
            dgwHrs.CurrentCell = dgwHrs[2, 0];
            dgwHrs.Focus();

            CargarColumnasHr();
        }

        private void btnRemoveHr_Click(object sender, EventArgs e)
        {

            if (dgwLineas.Rows.Count <= 1)
                return;

            if (cbbClave.SelectedIndex == -1)
                return;

            if (dgwLineas.CurrentCell == null)
                return;
            else
            {
                int iRow = dgwLineas.CurrentRow.Index;
                if (string.IsNullOrEmpty(dgwLineas[1, iRow].Value.ToString()))
                    return;
                
                string sLine = dgwLineas[1,iRow].Value.ToString();
                int iCons = 0;
                if (!int.TryParse(dgwLineas[0, iRow].Value.ToString(), out iCons))
                    iCons = 0;

                if (MessageBox.Show("Deseas eliminar la Linea " + sLine+ "?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        LineaConfigLnLogica line = new LineaConfigLnLogica();
                        line.Clave = cbbClave.SelectedValue.ToString();
                        line.Consec = iCons;
                        LineaConfigLnLogica.Eliminar(line);
                        dgwLineas.Rows.Remove(dgwLineas.CurrentRow);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Favor de Notificar al Administrador" + Environment.NewLine + ex.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    
                }
            }
        }

        private void dgwLine_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dgwLine.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void dgwHrs_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dgwHrs.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void dgwLineas_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dgwLineas.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void cbbWrap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbSize.SelectedIndex > -1)
                cbbSize.SelectedIndex = -1;

            dgwWrap.DataSource = null;
            CargarColumnasW();
        }

        private void cbbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbSize.SelectedIndex > -1 && cbbWrap.SelectedIndex > -1)
            {
                //load data
                WrapSetup(cbbWrap.SelectedValue.ToString(),cbbSize.SelectedValue.ToString());
            }
        }

        private void WrapSetup(string _asWrap,string _asSize)
        {
            string sWrap = cbbWrap.SelectedValue.ToString();
            string sSize = cbbSize.SelectedValue.ToString();

            ConfigWrapLogica conf = new ConfigWrapLogica();
            conf.CN = Globals._gsCompany;
            conf.Wrap = _asWrap;
            conf.Size = _asSize;

            dgwWrap.DataSource = ConfigWrapLogica.VistaWrap(conf);
            
            CargarColumnasW();
        }

        private void dgwWrap_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int iRow = e.RowIndex;
            if ((iRow % 2) == 0)
                e.CellStyle.BackColor = Color.LightSkyBlue;
            else
                e.CellStyle.BackColor = Color.White;
        }

        private void btSavew_Click(object sender, EventArgs e)
        {
            if (cbbWrap.SelectedIndex == -1 )
                return;

            if(cbbSize.SelectedIndex == -1)
            {
                MessageBox.Show(string.Format(_gs.ControlGridRows(this.Name, dgwWrap, "err1")), this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                ConfigWrapLogica data = new ConfigWrapLogica();
                data.CN = Globals._gsCompany;
                data.Wrap = cbbWrap.SelectedValue.ToString();
                data.Size = cbbSize.SelectedValue.ToString();
                int iRows = dgwWrap.Rows.Count - 1;

                for (int x=0; x < iRows; x++)
                {
                    string sFold = string.Empty;
                    if (string.IsNullOrEmpty(dgwWrap[0, x].Value.ToString()))
                        sFold = "N/A";
                    else
                        sFold = dgwWrap[0, x].Value.ToString();

                    if (string.IsNullOrEmpty(dgwWrap[1, x].Value.ToString()))
                        continue;

                    decimal dDuration = 0;
                    if (!decimal.TryParse(dgwWrap[1,x].Value.ToString(),out dDuration))
                        dDuration = 0;

                    if (dDuration == 0)
                        continue;

                    data.Fold = sFold;
                    data.Duration = dDuration;
                    data.User = Globals._gsUser;

                    ConfigWrapLogica.GuardarSP(data);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void chbWrapSetup_CheckedChanged(object sender, EventArgs e)
        {
            groupBox11.Enabled = chbWrapSetup.Checked;
        }

        private void btRemovew_Click(object sender, EventArgs e)
        {
            if (dgwWrap.Rows.Count <= 1)
                return;

            if (dgwWrap.CurrentCell == null)
                return;
            else
            {
                int iRow = dgwWrap.CurrentRow.Index;
                if (string.IsNullOrEmpty(dgwWrap[0, iRow].Value.ToString()))
                    return;
                
                string sFold = dgwWrap[0,iRow].Value.ToString();
                
                if (MessageBox.Show(string.Format(_gs.ControlGridRows(this.Name, dgwWrap, "rest01") + " {0} ?", sFold), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dgwWrap.Rows.RemoveAt(iRow);
                    ConfigWrapLogica data = new ConfigWrapLogica();
                    data.CN = Globals._gsCompany;
                    data.Wrap = cbbWrap.SelectedValue.ToString();
                    data.Size = cbbSize.SelectedValue.ToString();
                    data.Fold = sFold;
                    ConfigWrapLogica.Eliminar(data);

                }
            }
        }
    }
}
