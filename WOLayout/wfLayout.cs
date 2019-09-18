using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using Logica;

namespace WOLayout
{
    public partial class wfLayout : Form
    {
      
        private string _lsUser = string.Empty;
        private double _dAssyTime;
        private double _dTackIdeal;
        private decimal _dTackTime;
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
        private decimal _dDetroit;
        private decimal _dHrsDisp;
        private decimal _dSegDisp;
        private decimal _dKits;
        private decimal _dCajas;
        private decimal _dKitCaja;
        private decimal _dOutAdd;
        private string _sIndBoxHr;
        private int _iInspeccion;

        //Manual
        private bool _bModoManual=false;
        private int _iSub=0;
        private int _iMain=0;
        private double _sDuraW1=0;
        private double _sDuraW2=0;
        private double _sDuraWS = 0;
        private int _iOWrap1=0;
        private int _iOWrap2 = 0;
        private int _iOperNA=0;
        private int _iOut=0;
        private int _iOutO = 0;
        private string _sAssyTextoE;
        private string _sAssyTextoI;
        private int _iPosAssyTexto =0;

        FormWindowState _WindowStateAnt;
        private int _iWidthAnt;
        private int _iHeightAnt;
        DataTable _dtConf = new DataTable();

        Globals _gs = new Globals();

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
            Globals._gsLang = "SP";
            _lsUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            _lsUser = _lsUser.Substring(_lsUser.IndexOf("\\") + 1).ToUpper();

            Globals._gsUser = _lsUser;

            WindowState = FormWindowState.Maximized;
            
            tssUserName.Text = _lsUser;
            tssVersion.Text = "1.0.0.10";

            Inicio();

            ConfigLogica conf = new ConfigLogica();
            _dtConf = ConfigLogica.Consultar();

            if (!string.IsNullOrEmpty(_dtConf.Rows[0]["lenguage"].ToString()) && _dtConf.Rows[0]["lenguage"].ToString() != Globals._gsLang)
            {
                Globals._gsLang = _dtConf.Rows[0]["lenguage"].ToString();
                ChangeLen();
            }
            

            txtWO.Text = "0000000";
            txtWO.SelectAll();
        }
      
        private void Inicio()
        {
            txtWO.Clear();
            panel9.BackgroundImage = Properties.Resources.Blue_Background_down;
            panel2.BackgroundImage = Properties.Resources.Blue_Background_down;

            lblCycleTime.Text = "0";
            lblCycleTime.Text = _dTackTime.ToString();
            lblCycleTime.ForeColor = System.Drawing.Color.ForestGreen;

            _bModoManual = false;

            lblProduct.Text = "";
            dgwItem.DataSource = null;
            dgwWO.DataSource = null;
            dgwTables.DataSource = null;
            
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
                _dKits = decimal.Parse(_dtConf.Rows[0][6].ToString());

            if (!string.IsNullOrEmpty(_dtConf.Rows[0][7].ToString()))
                _dTackIdeal = double.Parse(_dtConf.Rows[0][7].ToString());
            if (!string.IsNullOrEmpty(_dtConf.Rows[0][8].ToString()))
                _dTackTime = decimal.Parse(_dtConf.Rows[0][8].ToString());

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


            if (!string.IsNullOrEmpty(_dtConf.Rows[0][14].ToString()))
                _iOperNA = int.Parse(_dtConf.Rows[0][14].ToString());
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
            if (!string.IsNullOrEmpty(_dtConf.Rows[0]["detroit"].ToString()))
                _dDetroit = decimal.Parse(_dtConf.Rows[0]["detroit"].ToString());
            string sVersion = _dtConf.Rows[0]["version"].ToString();
            _sIndBoxHr = _dtConf.Rows[0]["ind_boxhr"].ToString();
            if (!string.IsNullOrEmpty(_dtConf.Rows[0]["out_addtime"].ToString()))
                _dOutAdd = decimal.Parse(_dtConf.Rows[0]["out_addtime"].ToString());

            txtWO.Focus();

            CargarColumnas();
            LimpiarLayout();
            ChangeLen();
        }

        private void wfLayout_Activated(object sender, EventArgs e)
        {
            txtWO.Focus();
            txtWO.SelectAll();
        }
        #endregion

        #region regLanguage
        private void ChangeLen()
        {
            _gs.ControlText(this.Name,this);
            _gs.ControlText(this.Name,this.panel8);
            _gs.ControlGridText(this.Name, dgwWO);
            _gs.ControlGridText(this.Name, dgwItem);
            _gs.ControlGridText(this.Name, dgwTables);
            _gs.ControlGridRows2(this.Name, dgwTables);
            _gs.ControlGridRows3(this.Name, dgwItem); //change values
            //ControlText(this);
            //ControlText(this.panel8);
            //ControlGridText(dgwWO);
            //ControlGridText(dgwItem);
            //ControlGridText(dgwTables);
            //ControlGridRows2(dgwItem);
            //ControlGridRows2(dgwTables);

            if (Globals._gsLang == "EN")
            {
                btnLanguage.Image = Properties.Resources.mexico;

                if (_bModoManual)
                    dgwTables[1, _iPosAssyTexto].Value = _sAssyTextoI;

            }
                
            else
            {
                btnLanguage.Image = Properties.Resources.united_states;

                if (_bModoManual)
                    dgwTables[1, _iPosAssyTexto].Value = _sAssyTextoE;
            }
                

        }
        private void ControlText(Control _control)
        {
            ConfigLogica con = new ConfigLogica();
            con.Language = Globals._gsLang;
            con.Form = this.Name;

            foreach (Control c in _control.Controls)
            {
                foreach (Control cs in c.Controls)
                {
                    if (cs is Label)
                    {
                        con.Control = cs.Name;
                        string sValue = ConfigLogica.ChangeLanguageCont(con);
                        if (!string.IsNullOrEmpty(sValue))
                            cs.Text = sValue;
                    }
                }
            }
        }
        private void ControlGridText(DataGridView _control)
        {
            ConfigLogica con = new ConfigLogica();
            con.Language = Globals._gsLang;
            con.Form = this.Name;
            con.Control = _control.Name;
            foreach (DataGridViewColumn c in _control.Columns)
            {
                if (c.Visible)
                {
                    con.SubControl = c.Name;
                    string sValue = ConfigLogica.ChangeLanguageGrid(con);
                    if (!string.IsNullOrEmpty(sValue))
                        c.HeaderText = sValue;
                }

            }
        }
        private string ControlGridRows(Control _control, string _asRow)
        {
            ConfigLogica con = new ConfigLogica();
            con.Language = Globals._gsLang;
            con.Form = this.Name;
            con.Control = _control.Name;
            con.SubControl = _asRow;
            string sValue = ConfigLogica.ChangeLanguageGrid(con);
            if (!string.IsNullOrEmpty(sValue))
                return sValue;

            return null;
        }
        private void ControlGridRows2(DataGridView _control)
        {
            ConfigLogica con = new ConfigLogica();
            con.Language = Globals._gsLang;
            con.Form = this.Name;
            con.Control = _control.Name;
            foreach (DataGridViewRow row in _control.Rows)
            {
                con.SubControl = row.Cells["code"].Value.ToString();
                if (string.IsNullOrEmpty(con.SubControl))
                    continue;

                for (int i = 0; i < 2; i++)
                {
                    con.Columna = i;
                    string sValue = ConfigLogica.ChangeLanguageGridRow(con);
                    if (!string.IsNullOrEmpty(sValue))
                        row.Cells[i].Value = sValue;
                }
            }
        }
        private string MessageText(string _control, string _asMessage)
        {
            ConfigLogica con = new ConfigLogica();
            con.Language = Globals._gsLang;
            con.Form = this.Name;
            con.Control = _control;
            con.SubControl = _asMessage;
            string sValue = ConfigLogica.ChangeLanguageGrid(con);
            if (!string.IsNullOrEmpty(sValue))
                return sValue;

            return null;
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
            if (_asWrap == "4") dWrapTime = (double)_dDetroit;
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
                if (e.KeyCode == Keys.Escape)
                    Close();
                

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
                AS4.Takt = _dTackTime;
                SetupLogica set = new SetupLogica();
                set.WorkOrder = sValue;
                DataTable dt = AS4Logica.WorkOrder(AS4); 
                if (dt.Rows.Count == 0)
                    dt = SetupLogica.ConsultarWO(set);

                if (dt.Rows.Count > 0)
                {
                    lblProduct.Text = string.Empty;
                    dgwWO.DataSource = dt;
                    dgwWO.CurrentCell = null;
                    dgwItem.DataSource = null;

                    if (_sIndBoxHr == "1")
                    {
                        decimal dKits = decimal.Parse(dgwWO["kits", 0].Value.ToString());
                        decimal dBox = _dTackTime * dKits;
                        dBox = Math.Round( 3600 / dBox,2);
                        dgwWO["boxhr", 0].Value = dBox.ToString();
                    }

                    CargarColumnas();

                    string sItem = dt.Rows[0][0].ToString();

                    //if (sItem.IndexOf("DYN") == -1)
                    //{
                    //    MessageBox.Show(_gs.ControlGridRows(this.Name,txtWO, "err1"), "System Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    Inicio();
                    //    return;
                    //}

                    string sName = dt.Rows[0][1].ToString();
                    AS4.Item = sItem;
                    int iMaxComp = _iMaxTable * _iMesaEns;
                    AS4.MaxComp = iMaxComp; 

                    lblProduct.Text = sItem.Trim() + " - " + sName.ToUpper().TrimEnd();
                    
                    DataTable dt2 = AS4Logica.ComponentsLayer(AS4);                
                    if (dt2.Rows.Count > 1)
                    {

                        string sPre = dt2.Rows[0][1].ToString();
                        int iComp = int.Parse(dt2.Rows[0][2].ToString());

                        string sLevel1 = dt2.Rows[1][1].ToString();
                        string sWrap1 = dt2.Rows[1][2].ToString();
                        string sDuraW1 = string.Empty;
                        string sDuraW2 = string.Empty;
                        string sWrap2 = string.Empty;
                        string sLevel2 = string.Empty;

                        if (dt2.Rows.Count > 2)
                        {
                            sLevel2 = dt2.Rows[2][1].ToString();
                            sWrap2 = dt2.Rows[2][2].ToString();
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

                        //manual
                        _sDuraW1 = dWrapTime;
                        _sDuraW2 = dWrapTime2;

                        sDuraW1 = dWrapTime.ToString();
                        sDuraW2 = dWrapTime2.ToString();

                        DataTable dt3 = dgwItem.DataSource as DataTable;
                        dt3.Rows.Add(iComp, sPre, sWCodeM, sWrapMain, sDuraW1, sWCodeS, sWrapSub, sDuraW2,null);

                        //mesas / operadores
                        DataTable dt4 = AS4Logica.LineLayout(AS4);
                        if (dt4.Rows.Count > 0)
                        {
                            int iBasin = 0;
                            int iPiggy = 0;
                            int iOut = 0;
                            int iSub = 0;
                            int iMain = 0;
                            int iWrap = 0;

                            int iO = _iSurtidor + _iInspeccion + _iInspSell + _iSellador;
                            int iTotalOps = 0;
                            int iTotalMes = 0;
                            for (int i = 0; i < dt4.Rows.Count; i++)
                            {
                                string sLevel = dt4.Rows[i][0].ToString();
                                int iCompx = Convert.ToInt16(dt4.Rows[i][1].ToString());

                                if (sLevel == "S") iSub += iCompx;
                                if (sLevel == "B") iBasin += iCompx;
                                if (sLevel == "F") iOut += iCompx;
                                if (sLevel == "M" || sLevel == "L") iMain += iCompx;
                                if (sLevel == "Y") iPiggy += iCompx;
                                if (sLevel == "W") iWrap += iCompx;

                            }
                            

                            int iOper = 0;
                            int iMesas = 0;
                            DataTable dtN = dgwTables.DataSource as DataTable;

                            iOut += iBasin + iPiggy;
                            iMesas = (int)Math.Ceiling((decimal)iOut / _iMaxTable);
                            int iOutOper = 1;

                            if(iOut > 0)
                            {
                                //Outfolder config
                                AS4.Layer = "F";
                                DataTable dtO = AS4Logica.ComponentsLayerDetail(AS4);
                                if (dtO.Rows.Count > 0)
                                {
                                    int iFold = 0;
                                    int iLevel = 0;
                                    string sLevelAnt = string.Empty;
                                    for (int i = 0; i < dtO.Rows.Count; i++)
                                    {
                                        string sLevel = dtO.Rows[i][1].ToString();
                                        string sFold = dtO.Rows[i][2].ToString();
                                        if (!string.IsNullOrEmpty(sFold) && sFold != "0" && sFold != "7")
                                            iFold++;

                                        if (sLevel != sLevelAnt)
                                            iLevel++;

                                        sLevelAnt = sLevel;
                                    }

                                    _dOutAdd = _dOutAdd * iLevel; //Outfolder added time
                                    dWrapTime += (double)_dOutAdd; //add outfolder time to wrap time
                                    dgwItem[4, 0].Value = dWrapTime.ToString();
                                                      
                                    if (iFold > 0)
                                        iOutOper = 2;

                                    //if (AS4Logica.ComponentsLayerFold(AS4))
                                    //    iOutOper = 2;
                                }
                            }

                            iOper = iMesas * iOutOper;
                            dtN.Rows.Add("Out", "OutFolder/Basin", iOut, iMesas, iOper);
                            iTotalMes += iMesas;
                            iTotalOps += iOper;

                            _iOut = iOut;
                            int outfolderm = iMesas;
                            _iOutO = iOper;
                            int outfoldero = iOper;

                            decimal cM = Math.Ceiling((decimal)iSub / _iMaxTable);
                            iMesas = (int)cM;
                            iMesas = (int)Math.Ceiling((decimal)iMesas / (decimal)_iEstSub);
                            iOper = iMesas * _iEstSub;
                            string sCol1 = _gs.ControlGridRows(this.Name,dgwTables, "sub");
                            string sCol2 = _gs.ControlGridRows(this.Name,dgwTables, "sub_desc");
                            dtN.Rows.Add(sCol1, sCol2,iSub, iMesas, iOper,"sub");
                            _iSub = iSub;
                            iTotalMes += iMesas;
                            iTotalOps += iOper;

                            int subassym = iMesas;
                            int subassyo = iOper;

                            iMesas = (int)Math.Ceiling((decimal)iMain / _iMaxTable);
                            iOper = iMesas;
                            sCol1 = _gs.ControlGridRows(this.Name, dgwTables, "assy");
                            sCol2 = _gs.ControlGridRows(this.Name, dgwTables, "assy_desc");
                            dtN.Rows.Add(sCol1, sCol2, iMain, iMesas, iOper,"assy");
                            _iMain = iMain;
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
                            sCol2 = _gs.ControlGridRows(this.Name,dgwTables, "wrap1_desc");
                            dtN.Rows.Add("Wrap", sCol2,iWrap, iMesas, iOper,"wrap1");

                            iTotalMes += iMesas;
                            iTotalOps += iOper;

                            int wrap1m = iMesas;
                            int wrap1o = iOper;

                            iMesas = 0;
                            iOper = 0;
                            int iWrapSm = 0;
                            int iWrapSo = 0;
                            //if (sLevelS == "W")
                            if (sLevelS == "S")
                            {
                                dW = Math.Ceiling(dWrapTime2 / (dMax * _dAssyTime));
                                iMesas = (int)dW;
                                if (sWCodeS == "1" || sWCodeS == "8")
                                    iOper = iMesas;
                                else
                                    iOper = (iMesas * 2);

                                sCol2 = _gs.ControlGridRows(this.Name,dgwTables, "wrap2_desc");
                                dtN.Rows.Add("Wrap Sub", sCol2,iWrap, iMesas, iOper,"wrap2");


                                iTotalMes += iMesas;
                                iTotalOps += iOper;

                                iWrapSm = iMesas;
                                iWrapSo = iOper;

                            }

                            int wrap2m = 0;
                            //if (sLevelS == "W")
                            //{
                            //    dW = Math.Ceiling(dWrapTime2 / (dMax * _dAssyTime));
                            //    iMesas = (int)dW;
                            //    if (sWCodeS == "1" || sWCodeS == "8")
                            //        iOper = iMesas;
                            //    else
                            //        iOper = (iMesas * 2);

                            //    sCol2 = ControlGridRows(dgwTables, "wrap2_desc");
                            //    dtN.Rows.Add("Wrap Sub", sCol2, iWrap, iMesas, iOper, "wrap2");

                            //    iTotalMes += iMesas;
                            //    iTotalOps += iOper;

                            //    iWrapSm = iMesas;
                            //    iWwrapSo = iOper;
                            //    int wrap2m = iMesas;
                            //    int wrap2o = iOper;
                            //}

                            sCol1 = _gs.ControlGridRows(this.Name, dgwTables, "other");
                            sCol2 = _gs.ControlGridRows(this.Name, dgwTables, "deliver");
                            dtN.Rows.Add(sCol1, sCol2,0, 0, _iSurtidor,"deliver");
                            sCol2 = _gs.ControlGridRows(this.Name, dgwTables, "insp_sealer");
                            dtN.Rows.Add(sCol1, sCol2,0, 0, _iInspSell, "insp_sealer");
                            sCol2 = _gs.ControlGridRows(this.Name, dgwTables, "sealer");
                            dtN.Rows.Add(sCol1, sCol2,0, 0, _iSellador,"sealer");
                            sCol2 = _gs.ControlGridRows(this.Name, dgwTables, "inspection");
                            dtN.Rows.Add(sCol1, sCol2,0, 0, _iInspeccion,"inspection");
                            iTotalOps += iO;

                            lblMesas.Text = iTotalMes.ToString();
                            lblOper.Text = iTotalOps.ToString();

                            dgwTables.ClearSelection();

                            LimpiarLayout();
                            if (wrap1m + wrap2m > 6)
                            {
                                wrap2m = 6 - wrap1m;
                                MessageBox.Show(_gs.ControlGridRows(this.Name, txtWO, "err2"), "System Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            

                            llenarwrap(wrap1m, wrap1o);
                            _iOWrap1 = wrap1o / wrap1m;

                            //llenarmesaWS(iWrapSm, iWrapSo, wrap1m); // sub-assembly wrapping tables
                            

                            if (subassym + assym > 9 || assym > 5)
                            {
                                MessageBox.Show(ControlGridRows(txtWO, "err3"), "System Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                llenarOFWS(outfolderm, outfoldero, iWrapSm, iWrapSo);
                                llenarensamble(assym, assyo, subassym, subassyo, false);
                            }


                        }

                        ChangeLen();
                        dgwItem.ClearSelection();
                        txtWO.SelectAll();

                        //MODO MANUAL
                        //string sMensajeManual = (Globals._gsLang == "SP") ? "Operadores requeridos: " + lblOper.Text + "\n \nOperadores disponibles: " : "Required operators: " + lblOper.Text + "\n \nAvailable operators: ";
                        //string sEncabezadoManual = (Globals._gsLang == "SP") ? "Modo Manual" : "Manual Mode";

                        //var iODisponibles = Microsoft.VisualBasic.Interaction.InputBox(sMensajeManual, sEncabezadoManual, lblOper.Text);
                       

                        wfHC HeadCount = new wfHC();
                        HeadCount._lsOper = lblOper.Text.ToString();
                        HeadCount.ShowDialog();

                        var sODisponibles = HeadCount._lsOper;
                        int n;
                        //  
                        try
                        {
                            if (Int32.Parse(sODisponibles) != Int32.Parse(lblOper.Text) && int.TryParse(sODisponibles, out n))
                                ModoManual(Int32.Parse(sODisponibles));
                        }
                        catch (Exception)
                        {
                            
                        }

                        btnTimer_Click(sender, e);

                       
                    }
                    else
                    {
                        MessageBox.Show(_gs.ControlGridRows(this.Name, txtWO, "err4"), "System Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Inicio();
                    }
                }
                else
                {
                    MessageBox.Show(_gs.ControlGridRows(this.Name, txtWO, "err0"), "System Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Inicio();
                }
                    
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
            if (sValue == "Sub-Ensamble" || sValue == "Sub-Assembly")
            {
                e.CellStyle.BackColor = Color.Gold;
            }
            if (sValue == "Ensamble" || sValue == "Assembly")
            {
                e.CellStyle.BackColor = Color.SkyBlue;
            }
            if (sValue == "Wrap Sub")
            {
                e.CellStyle.BackColor = Color.FromArgb(250,193,182);
            }
            if (sValue == "Wrap")
            {
                e.CellStyle.BackColor = Color.MediumPurple;
            }
            if (sValue == "Out")
            {
                e.CellStyle.BackColor = Color.LightGreen;
            }
            if (sValue == "Otros" || sValue == "Others")
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
                dtNew.Columns.Add("product", typeof(string));
                dtNew.Columns.Add("name", typeof(string));
                dtNew.Columns.Add("box", typeof(int));
                dtNew.Columns.Add("kits", typeof(int));
                dtNew.Columns.Add("total_kits", typeof(int));
                dtNew.Columns.Add("duration", typeof(decimal));
                dtNew.Columns.Add("boxhr", typeof(decimal));
                dgwWO.DataSource = dtNew;
            }

            dgwWO.Columns[1].Visible = false;
            dgwWO.Columns[6].Visible = false;
            dgwWO.Columns[0].Width = ColumnWith(dgwWO, 20);
            dgwWO.Columns[2].Width = ColumnWith(dgwWO, 15);
            dgwWO.Columns[3].Width = ColumnWith(dgwWO, 20);
            dgwWO.Columns[4].Width = ColumnWith(dgwWO, 20);
            dgwWO.Columns[5].Width = ColumnWith(dgwWO, 30);
            dgwWO.Columns[5].DefaultCellStyle.Format = "N2";
            if (_sIndBoxHr == "1")
            {
                dgwWO.Columns[6].Visible = true;
                dgwWO.Columns[0].Width = ColumnWith(dgwWO, 19);
                dgwWO.Columns[2].Width = ColumnWith(dgwWO, 10);
                dgwWO.Columns[3].Width = ColumnWith(dgwWO, 8);
                dgwWO.Columns[4].Width = ColumnWith(dgwWO, 18);
                dgwWO.Columns[5].Width = ColumnWith(dgwWO, 28);
                dgwWO.Columns[6].Width = ColumnWith(dgwWO, 22);
                dgwWO.Columns[6].DefaultCellStyle.Format = "N2";
            }


            if (dgwItem.Rows.Count == 0)
            {
                DataTable dtNew = new DataTable("Item");
                dtNew.Columns.Add("component", typeof(int));
                dtNew.Columns.Add("passy", typeof(string));
                dtNew.Columns.Add("wrap_code", typeof(string));
                dtNew.Columns.Add("wrap1", typeof(string));
                dtNew.Columns.Add("wrap1_seg", typeof(string));
                dtNew.Columns.Add("wrap_code2", typeof(string));
                dtNew.Columns.Add("wrap2", typeof(string));
                dtNew.Columns.Add("wrap2_seg", typeof(string));
                dtNew.Columns.Add("code", typeof(string));
                dgwItem.DataSource = dtNew;
            }

            dgwItem.Columns[0].Width = ColumnWith(dgwItem, 20);
            dgwItem.Columns[1].Width = ColumnWith(dgwItem, 20);
            dgwItem.Columns[2].Visible = false;
            dgwItem.Columns[3].Width = ColumnWith(dgwItem, 15);
            dgwItem.Columns[4].Width = ColumnWith(dgwItem, 16);
            dgwItem.Columns[4].DefaultCellStyle.Format = "N2";
            dgwItem.Columns[5].Visible = false;
            dgwItem.Columns[6].Width = ColumnWith(dgwItem, 15);
            dgwItem.Columns[7].Width = ColumnWith(dgwItem, 16);
            dgwItem.Columns[7].DefaultCellStyle.Format = "N2";
            dgwItem.Columns[8].Visible = false;

            if (dgwTables.Rows.Count == 0)
            {
                DataTable dtNew = new DataTable("Tables");
                dtNew.Columns.Add("area", typeof(string));
                dtNew.Columns.Add("descrip", typeof(string));
                dtNew.Columns.Add("comp", typeof(string));
                dtNew.Columns.Add("tables", typeof(int));
                dtNew.Columns.Add("headcount", typeof(int));
                dtNew.Columns.Add("code", typeof(string));
                dgwTables.DataSource = dtNew;
            }

            dgwTables.Columns[5].Visible = false;
            dgwTables.Columns[0].Width = ColumnWith(dgwTables, 25);
            dgwTables.Columns[1].Width = ColumnWith(dgwTables, 48);
            dgwTables.Columns[2].Width = ColumnWith(dgwTables, 10);
            dgwTables.Columns[3].Width = ColumnWith(dgwTables, 10);
            dgwTables.Columns[4].Width = ColumnWith(dgwTables, 10);
            dgwTables.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwTables.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwTables.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwTables.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
        private void FillPlaybookFile(string _asFile,string _asFormat)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                Excel.Application xlApp = new Excel.Application();
                xlApp.AskToUpdateLinks = false;
                Excel.Workbooks xlWorkbookS = xlApp.Workbooks;
                Excel.Workbook xlWorkbook = xlWorkbookS.Open(_asFile);

                Excel.Worksheet xlWorksheet = new Excel.Worksheet();

                string sValue = string.Empty;

                int iSheets = xlWorkbook.Sheets.Count;
                int iSheet = 3;
                if (_asFormat == "D")
                    iSheet = 5;
                if (iSheets < iSheet)
                {
                    MessageBox.Show(_gs.MessageText(this.Name,btnExportFile.Name, "err5"), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Cursor = Cursors.Default;
                    return;
                }
                xlWorksheet = xlWorkbook.Sheets[iSheet];

                Excel.Range xlRange = xlWorksheet.UsedRange;

                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;

                for (int i = 1; i <= rowCount; i++)
                {

                    tssVersion.Text = i.ToString() + " of " + rowCount.ToString();

                    if (xlRange.Cells[i, 1].Value2 == null)
                        continue;

                    if (xlRange.Cells[i, 1].Value2 != null)
                        sValue = Convert.ToString(xlRange.Cells[i, 1].Value2.ToString());

                    //if (sValue == "WO NO" || sValue == "")
                    //{
                    //    sValue = string.Empty;
                    //    continue;
                    //}

                    if (string.IsNullOrEmpty(sValue))
                        continue;

                    if(_asFormat == "W")
                    {
                        decimal dWO = 0;
                        if (!decimal.TryParse(sValue, out dWO))
                        {
                            sValue = string.Empty;
                            continue;
                        }

                        if (xlRange.Cells[i, 8].Value2 != null)
                            continue;

                        if (sValue.Length < 7)
                        {
                            sValue = sValue.PadLeft(7, '0');
                            
                        }
                    }
                    else
                    {
                        
                        if (xlRange.Cells[i, 2].Value2 != null)
                            continue;

                        if (sValue.IndexOf("DYN") == -1)
                        {

                            sValue = string.Empty;
                            continue;
                        }
                    }

                    int iTotalOps = 0;
                    int iTotalMes = 0;

                    AS4Logica AS4 = new AS4Logica();


                    string sItem = string.Empty;
                    string sKits = string.Empty;

                    if(_asFormat == "W")
                    {
                        AS4.WO = sValue;
                        AS4.Takt = _dTackTime;
                        SetupLogica set = new SetupLogica();
                        set.WorkOrder = sValue;
                        DataTable dt = AS4Logica.WorkOrder(AS4);
                        if (dt.Rows.Count == 0)
                            dt = SetupLogica.ConsultarWO(set);

                        if (dt.Rows.Count > 0)
                        {
                            sItem = dt.Rows[0][0].ToString();
                            sKits = dt.Rows[0][3].ToString();
                            string sDuration = dt.Rows[0][5].ToString();

                            if (sItem.IndexOf("DYN") == -1)
                                continue;


                            xlRange.Cells[i, 8].Value2 = sKits;
                            xlRange.Cells[i, 11].Value2 = sDuration;
                        }
                    }
                    else
                    {
                        AS4.Item = sValue;
                        DataTable dt = AS4Logica.PartKits(AS4);
                        if (dt.Rows.Count > 0)
                        {
                            sItem = sValue;
                            sKits = dt.Rows[0][0].ToString();
                            xlRange.Cells[i, 2].Value2 = sKits;
                        }
                    }

                    if (string.IsNullOrEmpty(sItem))
                        continue;


                    AS4.Item = sItem;
                    AS4.MaxComp = _iMaxTable;

                    DataTable dt2 = AS4Logica.ComponentsLayer(AS4);
                    if (dt2.Rows.Count > 1)
                    {

                        string sPre = dt2.Rows[0][1].ToString();
                        int iComp = int.Parse(dt2.Rows[0][2].ToString());

                        string sLevel1 = dt2.Rows[1][1].ToString();
                        string sWrap1 = dt2.Rows[1][2].ToString();
                        string sDuraW1 = string.Empty;
                        string sDuraW2 = string.Empty;
                        string sWrap2 = string.Empty;
                        string sLevel2 = string.Empty;

                        if (dt2.Rows.Count > 2)
                        {
                            sLevel2 = dt2.Rows[2][1].ToString();
                            sWrap2 = dt2.Rows[2][2].ToString();
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

                        //manual
                        _sDuraW1 = dWrapTime;
                        _sDuraW2 = dWrapTime2;

                        sDuraW1 = dWrapTime.ToString();
                        sDuraW2 = dWrapTime2.ToString();
                        if (string.IsNullOrEmpty(sWCodeS))
                            sWCodeS = "No";
                        else
                            sWCodeS = "Yes";

                        if (_asFormat == "W")
                            xlRange.Cells[i, 12].Value2 = sWCodeS;
                        else
                            xlRange.Cells[i, 5].Value2 = sWCodeS;

                        //mesas / operadores
                        DataTable dt4 = AS4Logica.LineLayout(AS4);
                        if (dt4.Rows.Count > 0)
                        {
                            int iBasin = 0;
                            int iPiggy = 0;
                            int iOut = 0;
                            int iSub = 0;
                            int iMain = 0;
                            int iWrap = 0;

                            int iO = _iSurtidor + _iInspeccion + _iInspSell + _iSellador;

                            for (int x = 0; x < dt4.Rows.Count; x++)
                            {
                                string sLevel = dt4.Rows[x][0].ToString();
                                int iCompx = Convert.ToInt16(dt4.Rows[x][1].ToString());

                                if (sLevel == "S") iSub += iCompx;
                                if (sLevel == "B") iBasin += iCompx;
                                if (sLevel == "F") iOut += iCompx;
                                if (sLevel == "M" || sLevel == "L") iMain += iCompx;
                                if (sLevel == "Y") iPiggy += iCompx;
                                if (sLevel == "W") iWrap += iCompx;

                            }


                            int iOper = 0;
                            int iMesas = 0;
                            DataTable dtN = dgwTables.DataSource as DataTable;

                            iOut += iBasin + iPiggy;
                            iMesas = (int)Math.Ceiling((decimal)iOut / _iMaxTable);
                            int iOutOper = 1;
                            //si tiene wrap ++
                            if (AS4Logica.ComponentsLayerFold(AS4))
                                iOutOper = 2;
                            iOper = iMesas * iOutOper;

                            iTotalMes += iMesas;
                            iTotalOps += iOper;

                            _iOut = iOut;


                            decimal cM = Math.Ceiling((decimal)iSub / _iMaxTable);
                            iMesas = (int)cM;
                            iMesas = (int)Math.Ceiling((decimal)iMesas / (decimal)_iEstSub);
                            iOper = iMesas * _iEstSub;

                            _iSub = iSub;
                            iTotalMes += iMesas;
                            iTotalOps += iOper;



                            iMesas = (int)Math.Ceiling((decimal)iMain / _iMaxTable);
                            iOper = iMesas;

                            _iMain = iMain;
                            iTotalMes += iMesas;
                            iTotalOps += iOper;

                            double dMax = (double)_iMaxTable;
                            double dW = Math.Ceiling(dWrapTime / (dMax * _dAssyTime));
                            iMesas = (int)dW;
                            if (sWCodeM == "1" || sWCodeM == "8")
                                iOper = iMesas;
                            else
                                iOper = (iMesas * 2);

                            iTotalMes += iMesas;
                            iTotalOps += iOper;

                            iMesas = 0;
                            iOper = 0;

                            if (sLevelS == "S")
                            {
                                dW = Math.Ceiling(dWrapTime2 / (dMax * _dAssyTime));
                                iMesas = (int)dW;
                                if (sWCodeS == "1" || sWCodeS == "8")
                                    iOper = iMesas;
                                else
                                    iOper = (iMesas * 2);

                                iTotalMes += iMesas;
                                iTotalOps += iOper;
                            }


                        }
                    }

                    if (_asFormat == "W")
                    {
                        xlRange.Cells[i, 9].Value2 = iTotalOps.ToString();
                        xlRange.Cells[i, 10].Value2 = iTotalMes.ToString();
                    }
                    else
                    {
                        xlRange.Cells[i, 3].Value2 = iTotalOps.ToString();
                        xlRange.Cells[i, 4].Value2 = iTotalMes.ToString();
                    }
                }

                

                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlRange);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkbook.Sheets[iSheet]);
                xlApp.DisplayAlerts = false;
                xlWorkbook.Save();
                xlWorkbook.Close(true);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkbookS);
                xlApp.DisplayAlerts = true;
                xlApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);


                Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                ex.ToString();
                MessageBox.Show(ex.ToString());

            }
        }
        private void btnExportFile_Click(object sender, EventArgs e)
        {

            if (!ValidaAcceso("EXPF"))
            {
                MessageBox.Show(_gs.MessageText(this.Name,btnExportFile.Name, "rest02"), "System Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        

            string sArchivo = string.Empty;

            wfOption Option = new wfOption();
            Option.ShowDialog();
            string sOption = Option._lsOption;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                sArchivo = openFileDialog1.FileName;

            if (!File.Exists(sArchivo))
                return;

            
            FillPlaybookFile(sArchivo, sOption);


        }

        private void btnLenguage_Click(object sender, EventArgs e)
        {
            if (Globals._gsLang == "SP")
                Globals._gsLang = "EN";
            else
                Globals._gsLang = "SP";
              
            ChangeLen();
        }
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
                Config._lsLen = Globals._gsLang;
                Config.ShowDialog();

                string sLen = Globals._gsLang; ;
                if (sLen != Globals._gsLang)
                    btnLenguage_Click(sender, e);
                
            }
            else
                MessageBox.Show(ControlGridRows(txtWO, "rest01"), "System Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private bool ValidaAcceso(string _asProcess)
        {
            UsuarioLogica user = new UsuarioLogica();
            user.Usuario = _lsUser;
            user.Acceso = _asProcess;
            if (UsuarioLogica.ValidaAcceso(user))
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
                ResizeControl(panel9, 3, ref _iWidthAnt, ref _iHeightAnt, 0);
                ResizeControl(panel1, 3, ref _iWidthAnt, ref _iHeightAnt, 0);
                ResizeControl(panel2, 1, ref _iWidthAnt, ref _iHeightAnt, 0);
                //ResizeControl(pnlLayout, 2, ref _iWidthAnt, ref _iHeightAnt, 0);
                
                ResizeControl(groupBox3, 1, ref _iWidthAnt, ref _iHeightAnt, 0);
                ResizeControl(groupBox4, 2, ref _iWidthAnt, ref _iHeightAnt, 0);
                ResizeControl(dgwTables, 1, ref _iWidthAnt, ref _iHeightAnt, 1);
                int iH = panel9.Height;
                int iY = iH - ptbLogo.Height - 30;
                ptbLogo.Location = new Point(ptbLogo.Location.X,iY);

                iY = groupBox3.Height;
                iH = panel8.Height;
                iY = iY - iH - 10;
                panel8.Location = new Point(panel8.Location.X, iY);

                decimal iX = lblLayout.Width;
                iX = iX / 2;
                decimal iW = groupBox4.Width;
                iY = groupBox4.Location.X;
                iW = iY + iW / 2;
                iX = iW - iX;
                lblLayout.Location = new Point((int)iX, lblLayout.Location.Y);

                iW = Panel3.Width / 2;
                iX = groupBox4.Width / 2;
                iX -= iW + 10;
                Panel3.Location = new Point((int)iX, Panel3.Location.Y);
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
            PictureBox[] mesas = new PictureBox[28];
            mesas[0] = E1; //ensamble
            mesas[1] = E2;
            mesas[2] = E3;
            mesas[3] = E4;
            mesas[4] = E5;
            mesas[5] = E6;
            mesas[6] = E7;
            mesas[7] = E8;
            mesas[8] = E9;
            mesas[9] = W1; //wrap
            mesas[10] = W2;
            mesas[11] = W3;
            mesas[12] = W4;
            mesas[13] = W5;
            mesas[14] = W6;
            mesas[15] = OF1; //outfolder
            mesas[16] = OF2;
            mesas[17] = OF3;
            mesas[18] = OF4;
            mesas[19] = OF5;
            mesas[20] = WS1; //wrapsub
            mesas[21] = WS2;
            mesas[22] = WS3;
            mesas[23] = WS4;
            mesas[24] = WS5;
            mesas[25] = WS6;
            mesas[26] = WS7;
            mesas[27] = WS8;
            
            return mesas;
        }

        public PictureBox[] getoperadores()
        {
            PictureBox[] operadores = new PictureBox[56];
            operadores[0] = EO1; //ensamble
            operadores[1] = EO2;
            operadores[2] = EO3;
            operadores[3] = EO4;
            operadores[4] = EO5;
            operadores[5] = EO6;
            operadores[6] = EO7;
            operadores[7] = EO8;
            operadores[8] = EO9;
            operadores[9] = EO10;
            operadores[10] = EO11;
            operadores[11] = EO12;
            operadores[12] = EO13;
            operadores[13] = EO14;
            operadores[14] = EO15;
            operadores[15] = EO16;
            operadores[16] = EO17;
            operadores[17] = EO18; 
            operadores[18] = WO1; //wrap
            operadores[19] = WO2;
            operadores[20] = WO3;
            operadores[21] = WO4;
            operadores[22] = WO5;
            operadores[23] = WO6;
            operadores[24] = WO7;
            operadores[25] = WO8;
            operadores[26] = WO9;
            operadores[27] = WO10;
            operadores[28] = WO11;
            operadores[29] = WO12;
            operadores[30] = OFO1; //outfolder
            operadores[31] = OFO2;
            operadores[32] = OFO3;
            operadores[33] = OFO4;
            operadores[34] = OFO5;
            operadores[35] = OFO6;
            operadores[36] = OFO7;
            operadores[37] = OFO8;
            operadores[38] = OFO9;
            operadores[39] = OFO10;
            operadores[40] = WSO1; //wrapsub
            operadores[41] = WSO2;
            operadores[42] = WSO3;
            operadores[43] = WSO4;
            operadores[44] = WSO5;
            operadores[45] = WSO6;
            operadores[46] = WSO7;
            operadores[47] = WSO8;
            operadores[48] = WSO9;
            operadores[49] = WSO10;
            operadores[50] = WSO11;
            operadores[51] = WSO12;
            operadores[52] = WSO13;
            operadores[53] = WSO14;
            operadores[54] = WSO15;
            operadores[55] = WSO16;
 

            return operadores;
        }

        public void LimpiarLayout()
        {
            PictureBox[] mesas = getmesas();
            PictureBox[] operadores = getoperadores();

            for (int i = 0; i <= 27; i++)
            {
                mesas[i].Visible = false;
            }

            for (int i = 0; i <= 55; i++)
            {
                operadores[i].Visible = false;
            }
        }

        public void llenarwrap(int nummesas, int nummoperadores)
        {
            PictureBox[] mesas = getmesas();
            PictureBox[] operadores = getoperadores();


            if(nummesas < 7)
            {
                for (int i = 9; i < 9 + nummesas; i++)
                {
                    mesas[i].Visible = true;
                    operadores[i * 2].Visible = true;
                    if (nummoperadores == nummesas * 2 || nummoperadores == (nummesas * 2) - 1)
                        operadores[(i * 2) + 1].Visible = true;
                }
            }


        }
       
        public void llenarensamble(int emesas, int eoperadores, int smesas, int soperadores, bool manual)
        {
            PictureBox[] mesas = getmesas();
            PictureBox[] operadores = getoperadores();


            if (!manual)
            {
                //si cabe de un lado
                if (emesas + smesas <= 5)
                {
                    for (int i = 0; i < smesas; i++)
                    {
                        mesas[i].Image = Properties.Resources.sub;
                        mesas[i].Visible = true;
                        operadores[i * 2].Visible = true;
                        if (soperadores == smesas * 2)
                            operadores[(i * 2) + 1].Visible = true;
                    }

                    for (int i = smesas; i < smesas + emesas; i++)
                    {
                        mesas[i].Image = Properties.Resources.ensamble;
                        mesas[i].Visible = true;
                        operadores[i * 2].Visible = true;
                        if (eoperadores == emesas * 2)
                            operadores[(i * 2) + 1].Visible = true;
                    }
                }
                else
                // si no cabe de un lado
                {//LADO A
                    for (int i = 4; i > 4 - emesas; i--)
                    {
                        mesas[i].Image = Properties.Resources.ensamble;
                        mesas[i].Visible = true;
                        operadores[i * 2].Visible = true;
                        if (eoperadores == emesas * 2)
                            operadores[(i * 2) + 1].Visible = true;
                    }
                    for (int i = 4 - emesas; i >= 0; i--)
                    {
                        mesas[i].Image = Properties.Resources.sub;
                        mesas[i].Visible = true;
                        operadores[i * 2].Visible = true;
                        if (soperadores == smesas * 2)
                            operadores[(i * 2) + 1].Visible = true;
                    }

                    //LADO B
                    for (int i = 5; i < 5 + smesas - (5 - emesas); i++)
                    {
                        mesas[i].Image = Properties.Resources.sub;
                        mesas[i].Visible = true;
                        operadores[i * 2].Visible = true;
                        if (soperadores == smesas * 2)
                            operadores[(i * 2) + 1].Visible = true;
                    }

                }
            }
            else
            {

                if (emesas <= 5)
                {
                    for (int i = 0; i < emesas; i++)
                    {
                        mesas[i].Image = Properties.Resources.ensamble;
                        mesas[i].Visible = true;
                        operadores[i * 2].Visible = true;
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        mesas[i].Image = Properties.Resources.ensamble;
                        mesas[i].Visible = true;
                        operadores[i * 2].Visible = true;
                    }

                    for (int i = 0; i < emesas - 5; i++)
                    {
                        mesas[i].Image = Properties.Resources.ensamble;
                        mesas[i].Visible = true;
                        operadores[(i * 2) + 1].Visible = true;
                    }
                }
            }

        }

        public void llenarOFWS(int piOFMesas, int piOFOperadores, int piWSMesas, int piWSOperadores)

        {
            PictureBox[] mesas = getmesas();
            PictureBox[] operadores = getoperadores();

            for (int i = 15; i < 15 + piOFMesas; i++)
            {
                mesas[i].Visible = true;
                operadores[i * 2].Visible = true;
                if (piOFOperadores == piOFMesas * 2)
                    operadores[(i * 2) + 1].Visible = true;
            }

            for (int i = 20 + piOFMesas ; i < 20 + piWSMesas; i++)
            {
                mesas[i].Visible = true;
                operadores[i * 2].Visible = true;
                if (piWSOperadores == piWSMesas * 2)
                    operadores[(i * 2) + 1].Visible = true;
            }

        }


        #endregion

        #region LogicaModoManual
        public void ModoManual(int ipODisponibles)
        {
            _bModoManual = true;
            //Balance permitido = 20 (Estatico)
            double[,] ite = new double[40, 9];

            ite[0, 0] = Math.Ceiling((_iSub + _iMain + _iOut) *_dAssyTime / (double)_iMaxTable);
            ite[0, 1] = ipODisponibles - ite[0, 0] - (_iOperNA+_iOutO);
            ite[0, 2] =(_dAssyTime * (double)_dMaxTable);
            ite[0, 3] = Double.IsInfinity((_sDuraW1 + _sDuraW2 ) / (ite[0, 1] / (_iOWrap1))) ? 0: (_sDuraW1 + _sDuraW2) / (ite[0, 1] / (_iOWrap1)); //mas duracion wrab sub,  mas operadores por mesa
            ite[0, 4] = Math.Abs(ite[0, 2] - ite[0, 3]);
            ite[0, 5] = (ite[0, 4] < 20) ? 1 : 0;
            ite[0, 6] = ite[0, 4] * ite[0, 5];
            ite[0, 7] = (ite[0, 2] >= ite[0, 3]) ? ite[0, 2] : ite[0, 3];
            ite[0, 8] = (ite[0, 6] == 0) ? 0 : ite[0, 6];
           // ite[0, 9] = (ite[0, 0] < 0 || ite[0, 1] < 0) ? 0 : Math.Ceiling(ite[0, 8]);

            for (int i = 1; i <= 39; i++)
            {
                ite[i, 0] = (ite[i - 1, 0] - (_iOWrap1 / 2));
                ite[i, 1] = ipODisponibles - ite[i, 0] - (_iOperNA + _iOutO);
                ite[i, 2] = ((_iSub + _iMain ) * _dAssyTime / ite[i, 0]);
                ite[i, 3] = ((_sDuraW1 + _sDuraW2) / (ite[i, 1] / (_iOWrap1)  ));  //mas duracion wrab sub,  mas operadores por mesa
                ite[i, 4] = Math.Abs(ite[i, 2] - ite[i, 3]);
                ite[i, 5] = (ite[i, 4] < 20) ? 1 : 0;
                ite[i, 6] = ite[i, 4] * ite[i, 5];
                ite[i, 7] = (ite[i, 2] >= ite[i, 3]) ? ite[i, 2] : ite[i, 3];
                ite[i, 8] = (ite[i, 6] == 0) ? 0 : ite[i, 6];
             //   ite[i, 9] = (ite[i, 0] < 0 || ite[i, 1] < 0) ? 0 : Math.Ceiling(ite[i, 8]);
            }

            
            // Imprimir matriz en consola
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(ite[i,j] + ", ");
                }
                Console.WriteLine();
            }
            
            double dDeltaMenor = 1000;
            int iAssyO=0;
            int iWrapO=0;
            double iCycleTimeLine=0;

            for (int i = 1; i < 40; i++)
            {
                if (ite[i,8] > 0 && ite[i, 8] < dDeltaMenor && ite[i,0] > 0 && ite[i, 1] > 0 )
                {
                    dDeltaMenor = ite[i, 8];
                    iAssyO = (int)Math.Ceiling(ite[i, 0]);
                    iWrapO = (int)Math.Ceiling(ite[i, 1]);
                    iCycleTimeLine = ite[i, 7];
                }
            }

            // MessageBox.Show("Ensamble: "+iAssyO + " Wrap: " + iWrapO + " Cycle Time: " + iCycleTimeLine);

            LimpiarLayout();
            panel9.BackgroundImage = Properties.Resources.Yellow_Background_down1;
            panel2.BackgroundImage = Properties.Resources.Yellow_Background_down1;


            decimal sWODuracionVieja = (decimal)dgwWO[5, 0].Value;
            double sWODuracionNueva = Math.Round((iCycleTimeLine * int.Parse(dgwWO[4, 0].Value.ToString())) / 60);
            dgwWO[5, 0].Value = sWODuracionNueva;
            int iOperadoresTotal = 0, iMesasTotal = 0;

            //Actualizar tabla dgwTables
            string ComponentesSubensamble = "0";
            string ComponentesWrapSub = "0";
            int iOutFolderM = 0, iOutFolderO = 0, iWrapSubO = 0, iWrapSubM = 0;

            for (int i = 0; i < dgwTables.RowCount; i++)
            {
                if (dgwTables[0, i].Value.ToString() == "Out")
                {
                    iOutFolderM = Int32.Parse(dgwTables[3, i].Value.ToString()); // agarrando valores para llenar layout
                    iOutFolderO = Int32.Parse(dgwTables[4, i].Value.ToString());
                }

                if (dgwTables[0, i].Value.ToString() == "Sub-Ensamble" || dgwTables[0, i].Value.ToString() == "Sub-Assembly")
                {
                    ComponentesSubensamble = dgwTables[2, i].Value.ToString(); //agarrando componentes para sumarlos a ensamble
                    dgwTables.Rows.Remove(dgwTables.Rows[i]); //eliminando subensamble
                }

                if (dgwTables[0, i].Value.ToString() == "Ensamble" || dgwTables[0, i].Value.ToString() == "Assembly")
                {

                    _iPosAssyTexto = i;
                    _sAssyTextoE = "Ensamble (" + dgwTables[2, i].Value.ToString() + ") & Subensamble (" + ComponentesSubensamble.ToString() + ")";
                    _sAssyTextoI = "Assy (" + dgwTables[2, i].Value.ToString() + ") & Subassy (" + ComponentesSubensamble.ToString() + ")";

                    //separando componentes en descripcion
                    if (Globals._gsLang == "SP")
                        dgwTables[1, i].Value = _sAssyTextoE;

                    else
                        dgwTables[1, i].Value = _sAssyTextoI;


                    //actulizando el total de componentes sub+assy
                    dgwTables[2, i].Value = Int32.Parse(dgwTables[2, i].Value.ToString()) + Int32.Parse(ComponentesSubensamble);
                    dgwTables[3, i].Value = (iAssyO <= 5) ? iAssyO : 5; // si los operadores son mas 5 se tienen que acomodar en 5 mesas
                    dgwTables[4, i].Value = iAssyO; 

                }

                if (dgwTables[0, i].Value.ToString() == "Wrap")
                {
                    //si el #operadores calculado anteriormente es el doble del #mesas el nuevo generado tambien sera el doble
                    dgwTables[3, i].Value = (Int32.Parse(dgwTables[4, i].Value.ToString()) / Int32.Parse(dgwTables[3, i].Value.ToString()) == 2) ? Math.Ceiling(iWrapO / 2.0) : iWrapO;
                    dgwTables[4, i].Value = iWrapO;
                }

                if (dgwTables[0, i].Value.ToString() == "Wrap Sub")
                {
                    ComponentesWrapSub = dgwTables[2, i].Value.ToString();
                    dgwTables.Rows.Remove(dgwTables.Rows[i]);
                }
            }

            for (int i = 0; i < dgwTables.RowCount; i++)
            {
                if (dgwTables[0, i].Value.ToString() == "Wrap")
                {
                    //sumando los componentes de sub previamente optenidos
                    dgwTables[2, i].Value = Int32.Parse(dgwTables[2, i].Value.ToString()) + Int32.Parse(ComponentesWrapSub);
                    //si se pasa el numero de wrap total dentro de wrap llenar las mesas de wrap y poner los sobrantes en wrapsub
                    if (Int32.Parse(dgwTables[3, i].Value.ToString()) > 6)
                    {
                        llenarwrap(6, 12);
                        iWrapSubM = Int32.Parse(dgwTables[3, i].Value.ToString()) - 6;
                        iWrapSubO = iWrapO - 12;
                    }
                    else
                        llenarwrap(Int32.Parse(dgwTables[3, i].Value.ToString()), iWrapO);
                }

                    iOperadoresTotal = iOperadoresTotal + Int32.Parse(dgwTables[4, i].Value.ToString());
                iMesasTotal = iMesasTotal + Int32.Parse(dgwTables[3, i].Value.ToString());
            }

            lblOper.Text = iOperadoresTotal.ToString();
            lblMesas.Text = iMesasTotal.ToString();


            llenarensamble(iAssyO, iAssyO, 0, 0, true);

            llenarOFWS(iOutFolderM, iOutFolderO, iWrapSubM, iWrapSubO);
            lblCycleTime.Text = Math.Round(iCycleTimeLine, 3).ToString();
            if(_sIndBoxHr == "1")
            {
                decimal dKits = decimal.Parse(dgwWO["kits",0].Value.ToString());
                decimal dBox = (decimal)iCycleTimeLine * dKits;
                dBox = Math.Round( 3600 / dBox, 2);
                dgwWO["boxhr", 0].Value = dBox.ToString();
            }
            if (iCycleTimeLine > (int)_dTackTime)
                lblCycleTime.ForeColor = System.Drawing.Color.Red;
            else
                lblCycleTime.ForeColor = System.Drawing.Color.ForestGreen;

           //wfLayoutManual nform = new wfLayoutManual(Globals._gsLang, txtWO.Text, dgwWO.DataSource, dgwItem.DataSource, dgwTables.DataSource, iAssyO, iWrapO, iCycleTimeLine);
           //nform.Show();
           

        }

        private void btnTimer_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            try
            {
                bool bForm = false;
                foreach (Form frm in fc)
                {
                    //iterate through
                    if (frm.Name == "wfTimer")
                        bForm = true;
                }

                if(!bForm)
                {
                    wfTimer Timer = new wfTimer();
                    Timer._dCycle = decimal.Parse(lblCycleTime.Text.ToString());
                    Timer.Show();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
    #endregion

  
}
