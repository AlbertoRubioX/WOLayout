﻿using System;
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
    public partial class wfLayout : Form
    {
        private string _lsUser = string.Empty;
        private double _dAssyTime;  
        private double _dTackIdeal;
        private double _dTackTime;
        private int _iMaxTable;     
        private int _iMesaEns;     
        private int _iMesaWrap;    
        private int _iEstSub;       
        private int _iSurtidor;
        private int _iInspSell;
        private int _iSellador;
        private decimal _dHori;
        private decimal _dVertical;
        private decimal _dTape;
        private decimal _dEnvelope;
        private decimal _dHrsDisp;
        private decimal _dSegDisp;
        private decimal _dKits;
        private decimal _dCajas;
        private decimal _dKitCaja;
        private int _iInspeccion;

        FormWindowState _WindowStateAnt;
        private int _iWidthAnt;
        private int _iHeightAnt;
        DataTable _dtConf = new DataTable();
        public wfLayout()
        {
            InitializeComponent();

            _iWidthAnt = Width;
            _iHeightAnt = Height;
            _WindowStateAnt = WindowState;
        }

        #region regInicio
        private void wfLayout_Load(object sender, EventArgs e)
        {
            _lsUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            _lsUser = _lsUser.Substring(_lsUser.IndexOf("\\") + 1).ToUpper();

            WindowState = FormWindowState.Maximized;

            Inicio();

            txtWO.Text = "0000000";
            txtWO.SelectAll();
        }

        private void Inicio()
        {

            tssUserName.Text = _lsUser;
            tssVersion.Text = "V 1.0.0.1";

            txtWO.Clear();

            lblProduct.Text = "";
            dgwItem.DataSource = null;
            dgwWO.DataSource = null;
            dgwTables.DataSource = null;
            CargarColumnas();
            lblMesas.Text = "0";
            lblOper.Text = "0";

            ConfigLogica conf = new ConfigLogica();
            _dtConf = ConfigLogica.Consultar();
          
            if (!string.IsNullOrEmpty(_dtConf.Rows[0][2].ToString()))
                _dHrsDisp = decimal.Parse(_dtConf.Rows[0][2].ToString());
            if (!string.IsNullOrEmpty(_dtConf.Rows[0][3].ToString()))
                _dSegDisp = decimal.Parse(_dtConf.Rows[0][3].ToString());
            if (!string.IsNullOrEmpty(_dtConf.Rows[0][4].ToString()))
                _dCajas = decimal.Parse(_dtConf.Rows[0][4].ToString());
            if (!string.IsNullOrEmpty(_dtConf.Rows[0][5].ToString()))
                _dKitCaja = decimal.Parse(_dtConf.Rows[0][5].ToString());
            if (!string.IsNullOrEmpty(_dtConf.Rows[0][6].ToString()))
                _dKits= decimal.Parse(_dtConf.Rows[0][6].ToString());

            if (!string.IsNullOrEmpty(_dtConf.Rows[0][7].ToString()))
                _dTackIdeal = double.Parse(_dtConf.Rows[0][7].ToString());
            if (!string.IsNullOrEmpty(_dtConf.Rows[0][8].ToString()))
                _dTackTime = double.Parse(_dtConf.Rows[0][8].ToString());

            if (!string.IsNullOrEmpty(_dtConf.Rows[0][9].ToString()))
                _dAssyTime = double.Parse(_dtConf.Rows[0][9].ToString());
            if (!string.IsNullOrEmpty(_dtConf.Rows[0][10].ToString()))
                _iMaxTable = int.Parse(_dtConf.Rows[0][10].ToString());
            if (!string.IsNullOrEmpty(_dtConf.Rows[0][11].ToString()))
                _iMesaEns = int.Parse(_dtConf.Rows[0][11].ToString());
            if (!string.IsNullOrEmpty(_dtConf.Rows[0][12].ToString()))
                _iMesaWrap = int.Parse(_dtConf.Rows[0][12].ToString());
            if (!string.IsNullOrEmpty(_dtConf.Rows[0][13].ToString()))
                _iEstSub = int.Parse(_dtConf.Rows[0][13].ToString());

            if (!string.IsNullOrEmpty(_dtConf.Rows[0][15].ToString()))
                _iSurtidor = int.Parse(_dtConf.Rows[0][15].ToString());
            if (!string.IsNullOrEmpty(_dtConf.Rows[0][16].ToString()))
                _iInspSell = int.Parse(_dtConf.Rows[0][16].ToString());
            if (!string.IsNullOrEmpty(_dtConf.Rows[0][17].ToString()))
                _iSellador = int.Parse(_dtConf.Rows[0][17].ToString());
            if (!string.IsNullOrEmpty(_dtConf.Rows[0][18].ToString()))
                _iInspeccion = int.Parse(_dtConf.Rows[0][18].ToString());

            if (!string.IsNullOrEmpty(_dtConf.Rows[0][21].ToString()))
                _dHori = decimal.Parse(_dtConf.Rows[0][21].ToString());
            if (!string.IsNullOrEmpty(_dtConf.Rows[0][22].ToString()))
                _dVertical = decimal.Parse(_dtConf.Rows[0][22].ToString());
            if (!string.IsNullOrEmpty(_dtConf.Rows[0][23].ToString()))
                _dEnvelope = decimal.Parse(_dtConf.Rows[0][23].ToString());
            if (!string.IsNullOrEmpty(_dtConf.Rows[0][25].ToString()))
                _dTape = decimal.Parse(_dtConf.Rows[0][25].ToString());
            
            txtWO.Focus();
            
            LimpiarLayout();
        }

        private void wfLayout_Activated(object sender, EventArgs e)
        {
            txtWO.Focus();
            txtWO.SelectAll();
        }
        #endregion

        #region regLayout
        private string getWrapDesc(string _asWrap)
        {
            string sWrapDesc = string.Empty;
            if (_asWrap == "1") sWrapDesc = "Sobre";
            if (_asWrap == "2") sWrapDesc = "Vertical";
            if (_asWrap == "3") sWrapDesc = "Horizontal";
            if (_asWrap == "4") sWrapDesc = "Detroit";
            if (_asWrap == "8") sWrapDesc = "Truck and Tape";

            return sWrapDesc;
        }

        private double getWrapTime(string _asWrap)
        {
            double dWrapTime = 0;
            if (_asWrap == "1") dWrapTime = (double)_dEnvelope;
            if (_asWrap == "2") dWrapTime = (double)_dVertical;
            if (_asWrap == "3") dWrapTime = (double)_dHori;
            if (_asWrap == "8") dWrapTime = (double)_dTape;

            return dWrapTime;
        }

        private void txtWO_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtWO_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode != Keys.Enter)
                    return;

                string sValue = txtWO.Text.ToString().ToUpper();
                if (sValue.Length < 7)
                {
                    sValue = sValue.PadLeft(7, '0');
                    txtWO.Text = sValue;
                }

                Inicio();
                txtWO.Text = sValue;

                AS4Logica AS4 = new AS4Logica();
                AS4.WO = sValue;
                //DataTable dt = AS4Logica.WorkOrder(AS4);

                SetupLogica set = new SetupLogica();
                set.WorkOrder = sValue;
                DataTable dt = SetupLogica.ConsultarWO(set);
                if (dt.Rows.Count > 0)
                {
                    lblProduct.Text = string.Empty;
                    dgwWO.DataSource = dt;
                    dgwWO.CurrentCell = null;

                    string sItem = dt.Rows[0][0].ToString();
                    string sName = dt.Rows[0][1].ToString();
                    AS4.Item = sItem;
                    lblProduct.Text = sItem + " - " + sName.ToUpper();

                    DataTable dt2 = AS4Logica.ComponentsLayer(AS4);
                    dgwItem.DataSource = null;
                    CargarColumnas();
                    if (dt2.Rows.Count > 0)
                    {

                        string sPre = dt2.Rows[0][0].ToString();
                        int iComp = int.Parse(dt2.Rows[0][1].ToString());

                        string sLevel1 = dt2.Rows[1][0].ToString();
                        string sWrap1 = dt2.Rows[1][1].ToString();
                        string sDuraW1 = string.Empty;
                        string sDuraW2 = string.Empty;
                        string sWrap2 = string.Empty;
                        string sLevel2 = string.Empty;

                        if (dt2.Rows.Count > 2)
                        {
                            sLevel2 = dt2.Rows[2][0].ToString();
                            sWrap2 = dt2.Rows[2][1].ToString();
                        }
                        string sLevelM = string.Empty;
                        string sLevelS = string.Empty;
                        string sWrapMain = string.Empty;
                        string sWrapSub = string.Empty;
                        string sWCodeM = string.Empty;
                        string sWCodeS = string.Empty;
                        double dWrapTime = 0;
                        double dWrapTime2 = 0;

                        if (sLevel1 == "W")
                        {
                            sWrapMain = getWrapDesc(sWrap1);
                            dWrapTime = getWrapTime(sWrap1);
                            sLevelM = sLevel1;
                            if (!string.IsNullOrEmpty(sLevel2))
                            {
                                sWrapSub = getWrapDesc(sWrap2);
                                dWrapTime2 = getWrapTime(sWrap2);
                                sLevelS = sLevel2;
                            }
                            sWCodeM = sWrap1;
                            sWCodeS = sWrap2;
                        }
                        else
                        {
                            sWrapMain = getWrapDesc(sWrap2);
                            dWrapTime = getWrapTime(sWrap2);
                            sLevelM = sLevel2;
                            sWrapSub = getWrapDesc(sWrap1);
                            dWrapTime2 = getWrapTime(sWrap1);
                            sLevelS = sLevel1;
                            sWCodeM = sWrap2;
                            sWCodeS = sWrap1;
                        }

                        sDuraW1 = dWrapTime.ToString();
                        sDuraW2 = dWrapTime2.ToString();

                        DataTable dt3 = dgwItem.DataSource as DataTable;
                        dt3.Rows.Add(iComp, sPre, sWCodeM, sWrapMain, sDuraW1, sWCodeS, sWrapSub, sDuraW2);

                        //mesas / operadores
                        DataTable dt4 = AS4Logica.LineLayout(AS4);
                        if (dt4.Rows.Count > 0)
                        {
                            int iBasin = 0;
                            int iPiggy = 0;
                            int iOut = 0;
                            int iSub = 0;
                            int iMain = 0;

                            int iO = _iSurtidor + _iInspeccion + _iInspSell + _iSellador;
                            int iTotalOps = 0;
                            int iTotalMes = 0;
                            for (int i = 0; i < dt4.Rows.Count; i++)
                            {
                                string sLevel = dt4.Rows[i][0].ToString();
                                int iCompx = Convert.ToInt16(dt4.Rows[i][1].ToString());

                                if (sLevel == "S") iSub = iCompx;
                                if (sLevel == "B") iBasin = iCompx;
                                if (sLevel == "F") iOut = iCompx;
                                if (sLevel == "M" || sLevel == "L") iMain = iCompx;
                                if (sLevel == "Y") iPiggy = iCompx;

                            }

                            int iOper = 0;
                            int iMesas = 0;
                            DataTable dtN = dgwTables.DataSource as DataTable;

                            iMesas = iOut + iBasin + iPiggy;
                            iMesas = (int)Math.Ceiling((decimal)iMesas / (decimal)_iMaxTable);
                            iOper = iMesas;
                            dtN.Rows.Add("Out", "OutFolder/Basin", iMesas, iOper);
                            iTotalMes += iMesas;
                            iTotalOps += iOper;

                            int outfolderm = iMesas;
                            int outfoldero = iOper;

                            decimal cM = Math.Ceiling((decimal)iSub / (decimal)_iMaxTable);
                            iMesas = (int)cM;
                            iMesas = (int)Math.Ceiling((decimal)iMesas / (decimal)_iEstSub);
                            iOper = iMesas * _iEstSub;
                            dtN.Rows.Add("Sub-Ensamble", "Sub-Ensambles", iMesas, iOper);
                            iTotalMes += iMesas;
                            iTotalOps += iOper;

                            int subassym = iMesas;
                            int subassyo = iOper;

                            iMesas = (int)Math.Ceiling((decimal)iMain / (decimal)_iMaxTable);
                            iOper = iMesas;
                            dtN.Rows.Add("Ensamble", "Ensamble Sobre Conveyor", iMesas, iOper);
                            iTotalMes += iMesas;
                            iTotalOps += iOper;

                            int assym = iMesas;
                            int assyo = iOper;

                            double dMax = (double)_iMaxTable;
                            double dW = Math.Ceiling(dWrapTime / (dMax * _dAssyTime));
                            iMesas = (int)dW;
                            if (sWCodeM == "1" || sWCodeM == "8")
                                iOper = iMesas;
                            else
                                iOper = (iMesas * 2);

                            dtN.Rows.Add("Wrap", "Wrap 1", iMesas, iOper);
                            iTotalMes += iMesas;
                            iTotalOps += iOper;

                            int wrap1m = iMesas;
                            int wrap1o = iOper;

                            iMesas = 0;
                            iOper = 0;
                            if (sLevelS == "W") /******Validar con Ingenieria******/
                            {
                                dW = Math.Ceiling(dWrapTime2 / (dMax * _dAssyTime));
                                iMesas = (int)dW;
                                if (sWCodeS == "1" || sWCodeS == "8")
                                    iOper = iMesas;
                                else
                                    iOper = (iMesas * 2);
                                dtN.Rows.Add("Wrap", "Wrap 2", iMesas, iOper);
                            }
                            /****************************************************/

                            iTotalMes += iMesas;
                            iTotalOps += iOper;

                            int wrap2m = iMesas;
                            int wrap2o = iOper;

                            dtN.Rows.Add("Otros", "Surtidor", 0, _iSurtidor);
                            dtN.Rows.Add("Otros", "Inspección Selladora", 0, _iInspSell);
                            dtN.Rows.Add("Otros", "Selladora", 0, _iSellador);
                            dtN.Rows.Add("Otros", "Inspección 100%", 0, _iInspeccion);
                            iTotalOps += iO;

                            lblMesas.Text = iTotalMes.ToString();
                            lblOper.Text = iTotalOps.ToString();

                            dgwTables.ClearSelection();

                            LimpiarLayout();
                            if (wrap1m + wrap2m > 6)
                            {
                                wrap2m = 6 - wrap1m;
                                MessageBox.Show("Capacidad de Mesas Excedida en Estación de Empaque", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }


                            if (outfolderm + subassym + assym > 8)
                                MessageBox.Show("Capacidad de Operadores Excedida", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                            {
                                llenarmesa(outfolderm, outfoldero, true);
                                llenarmesa(subassym, subassyo, true);
                                llenarmesa(assym, assyo, true);
                                llenarmesa(wrap1m, wrap1o, false);
                                llenarmesa(wrap2m, wrap2o, false);

                            }
                        }

                        dgwItem.ClearSelection();
                        txtWO.SelectAll();
                    }
                }
                else
                    MessageBox.Show("Work Order Not Fund!", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region regGrid
        private void dgwTables_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int iRow = e.RowIndex;
            string sValue = e.Value.ToString();


            sValue = dgwTables[0, e.RowIndex].Value.ToString();
            if (sValue == "Sub-Ensamble")
            {
                e.CellStyle.BackColor = Color.Gold;
            }
            if (sValue == "Ensamble")
            {
                e.CellStyle.BackColor = Color.SkyBlue;
                //e.CellStyle.ForeColor = Color.White;
            }
            if (sValue == "Wrap")
            {
                e.CellStyle.BackColor = Color.MediumPurple;
            }
            if (sValue == "Out")
            {
                e.CellStyle.BackColor = Color.LightGreen;
            }
            if (sValue == "Otros")
            {
                e.CellStyle.BackColor = Color.WhiteSmoke;
                e.CellStyle.ForeColor = Color.MediumBlue;
            }
        }

        private void CargarColumnas()
        {
            if (dgwWO.Rows.Count == 0)
            {
                DataTable dtNew = new DataTable("WO");
                dtNew.Columns.Add("PRODUCTO", typeof(string));
                dtNew.Columns.Add("NOMBRE", typeof(string));
                dtNew.Columns.Add("CAJAS", typeof(int));
                dtNew.Columns.Add("KITS", typeof(int));
                dtNew.Columns.Add("TOTAL EN KITS", typeof(int));
                dtNew.Columns.Add("W.O. DURACION (min)", typeof(decimal));
                dgwWO.DataSource = dtNew;
            }
            dgwWO.Columns[0].Visible = false;
            dgwWO.Columns[1].Visible = false;
            dgwWO.Columns[2].Width = ColumnWith(dgwWO, 15);
            dgwWO.Columns[3].Width = ColumnWith(dgwWO, 15);
            dgwWO.Columns[4].Width = ColumnWith(dgwWO, 30);
            dgwWO.Columns[5].Width = ColumnWith(dgwWO, 45);

            if (dgwItem.Rows.Count == 0)
            {
                DataTable dtNew = new DataTable("Item");
                dtNew.Columns.Add("COMPONENTES", typeof(int));
                dtNew.Columns.Add("PRE-ENSAMBLE", typeof(string));
                dtNew.Columns.Add("wrap_code", typeof(string));
                dtNew.Columns.Add("WRAP PRINCIPAL", typeof(string));
                dtNew.Columns.Add("WRAP DURACION (seg)", typeof(string));
                dtNew.Columns.Add("wrap_code2", typeof(string));
                dtNew.Columns.Add("WRAP SUB", typeof(string));
                dtNew.Columns.Add("WRAP SUB DURACION (seg)", typeof(string));
                dgwItem.DataSource = dtNew;
            }

            dgwItem.Columns[0].Width = ColumnWith(dgwItem, 20);
            dgwItem.Columns[1].Width = ColumnWith(dgwItem, 20);
            dgwItem.Columns[2].Visible = false;
            dgwItem.Columns[3].Width = ColumnWith(dgwItem, 15);
            dgwItem.Columns[4].Width = ColumnWith(dgwItem, 16);
            dgwItem.Columns[5].Visible = false;
            dgwItem.Columns[6].Width = ColumnWith(dgwItem, 15);
            dgwItem.Columns[7].Width = ColumnWith(dgwItem, 16);

            if (dgwTables.Rows.Count == 0)
            {
                DataTable dtNew = new DataTable("Tables");
                dtNew.Columns.Add("AREA", typeof(string));
                dtNew.Columns.Add("DESCRIPCION", typeof(string));
                dtNew.Columns.Add("MESAS", typeof(int));
                dtNew.Columns.Add("H.C.", typeof(int));
                dgwTables.DataSource = dtNew;
            }

            dgwTables.Columns[0].Width = ColumnWith(dgwTables, 25);
            dgwTables.Columns[1].Width = ColumnWith(dgwTables, 48);
            dgwTables.Columns[2].Width = ColumnWith(dgwTables, 15);
            dgwTables.Columns[3].Width = ColumnWith(dgwTables, 15);
            dgwTables.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwTables.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwTables.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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

        #region regBottons

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Inicio();
        }
        
        private void btnConfig_Click(object sender, EventArgs e)
        {
            if (ValidaAcceso("CONF"))
            {
                wfConfig Config = new wfConfig();
                Config.Show();
            }
            else
                MessageBox.Show("Usuario sin Acceso a la Configuración", "System Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private bool ValidaAcceso(string _asProcess)
        {
            UsuarioLogica user = new UsuarioLogica();
            user.Usuario = _lsUser;
            if (UsuarioLogica.AccesoConfig(user))
                return true;

            return false;
        }
        #endregion
      
        #region regRes
        private void wfLayout_Resize(object sender, EventArgs e)
        {
            if (WindowState != _WindowStateAnt && WindowState != FormWindowState.Minimized)
            {
                _WindowStateAnt = WindowState;
                ResizeControl(panel1, 3, ref _iWidthAnt, ref _iHeightAnt, 0);
                ResizeControl(panel2, 1, ref _iWidthAnt, ref _iHeightAnt, 0);
                ResizeControl(panel3, 1, ref _iWidthAnt, ref _iHeightAnt, 0);
                ResizeControl(groupBox3, 1, ref _iWidthAnt, ref _iHeightAnt, 0);
                ResizeControl(dgwTables, 1, ref _iWidthAnt, ref _iHeightAnt, 1);
            }
        }


        public void ResizeControl(Control ac_Control, int ai_Hor, ref int ai_WidthAnt, ref int ai_HegihtAnt, int ai_Retorna)
        {
            if (ai_WidthAnt == 0)
                ai_WidthAnt = ac_Control.Width;
            if (ai_WidthAnt == ac_Control.Width)
                return;

            int _dif = ai_WidthAnt - ac_Control.Width;
            int _difh = ai_HegihtAnt - ac_Control.Height;

            if (ai_Hor == 1)
                ac_Control.Height = this.Height - _difh;
            if (ai_Hor == 2)
                ac_Control.Width = this.Width - _dif;
            if (ai_Hor == 3)
            {
                ac_Control.Width = this.Width - _dif;
                ac_Control.Height = this.Height - _difh;
            }
            if (ai_Retorna == 1)
            {
                ai_WidthAnt = this.Width;
                ai_HegihtAnt = this.Height;
            }
        }
        #endregion

        #region regDraw
        public PictureBox[] getmesas()
        {
            PictureBox[] mesas = new PictureBox[14];
            mesas[0] = E1;
            mesas[1] = E2;
            mesas[2] = E3;
            mesas[3] = E4;
            mesas[4] = E5;
            mesas[5] = E6;
            mesas[6] = E7;
            mesas[7] = E8;
            mesas[8] = W1;
            mesas[9] = W2;
            mesas[10] = W3;
            mesas[11] = W4;
            mesas[12] = W5;
            mesas[13] = W6;

            return mesas;
        }

        public PictureBox[] getoperadores()
        {
            PictureBox[] operadores = new PictureBox[28];
            operadores[0] = O1;
            operadores[1] = O2;
            operadores[2] = O3;
            operadores[3] = O4;
            operadores[4] = O5;
            operadores[5] = O6;
            operadores[6] = O7;
            operadores[7] = O8;
            operadores[8] = O9;
            operadores[9] = O10;
            operadores[10] = O11;
            operadores[11] = O12;
            operadores[12] = O13;
            operadores[13] = O14;
            operadores[14] = O15;
            operadores[15] = O16;
            operadores[16] = O17;
            operadores[17] = O18;
            operadores[18] = O19;
            operadores[19] = O20;
            operadores[20] = O21;
            operadores[21] = O22;
            operadores[22] = O23;
            operadores[23] = O24;
            operadores[24] = O25;
            operadores[25] = O26;
            operadores[26] = O27;
            operadores[27] = O28;

            return operadores;
        }

        public void LimpiarLayout()
        {
            PictureBox[] mesas = getmesas();
            PictureBox[] operadores = getoperadores();

            for (int i = 0; i <= 13; i++)
            {
                mesas[i].Visible = false;
            }

            for (int i = 0; i <= 27; i++)
            {
                operadores[i].Visible = false;
            }
        }

        public void llenarmesa(int nummesas, int nummoperadores, bool ensamable)
        {
            PictureBox[] mesas = getmesas();
            PictureBox[] operadores = getoperadores();

            Boolean libre = true;
            int posicionlibre;

            if (ensamable)
                posicionlibre = 0;
            else
                posicionlibre = 8;

            do
            {
                if (mesas[posicionlibre].Visible)
                {
                    posicionlibre++;
                }
                else libre = false;
            }
            while (libre);


            for (int i = posicionlibre; i < posicionlibre + nummesas; i++)
            {
                mesas[i].Visible = true;
                operadores[i * 2].Visible = true;
                if (nummoperadores == nummesas * 2)
                    operadores[(i * 2) + 1].Visible = true;
            }

        }

        #endregion

    }
}
