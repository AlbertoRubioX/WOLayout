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

        private void btnTIME_Click(object sender, EventArgs e)
        {
            _lsOption = "T";
            Close();
        }
        /*
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
                AS4.CN = Globals._gsCompany;
                AS4.WO = sValue;
                AS4.Takt = _dTackTime;
                SetupLogica set = new SetupLogica();
                set.WorkOrder = sValue;
                set.Takt = _dTackTime;

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
                        if (dKits > 0)
                            dBox = Math.Round(3600 / dBox, 2);

                        dgwWO["boxhr", 0].Value = dBox.ToString();

                    }

                    CargarColumnas();

                    string sItem = dt.Rows[0][0].ToString(); 

                    string sName = dt.Rows[0][1].ToString();
                    AS4.Item = sItem;
                    int iMaxComp = _iMaxTable * _iMesaEns;
                    AS4.MaxComp = iMaxComp;

                    lblProduct.Text = sItem.Trim();

                    if (!string.IsNullOrEmpty(sName))
                        lblProduct.Text = sItem.Trim() + " - " + sName.ToUpper().TrimEnd();

                    _lsItem = sItem.Trim();
                    _lsItemDesc = sName.Trim();

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
                        dt3.Rows.Add(iComp, sPre, sWCodeM, sWrapMain, sDuraW1, sWCodeS, sWrapSub, sDuraW2, null);

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

                            if (iOut > 0)
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

                            iOper = iMesas * _iEstSub;
                            string sCol1 = _gs.ControlGridRows(this.Name, dgwTables, "sub");
                            string sCol2 = _gs.ControlGridRows(this.Name, dgwTables, "sub_desc");
                            dtN.Rows.Add(sCol1, sCol2, iSub, iMesas, iOper, "sub");
                            _iSub = iSub;
                            iTotalMes += iMesas;
                            iTotalOps += iOper;

                            int subassym = iMesas;
                            int subassyo = iOper;

                            iMesas = (int)Math.Ceiling((decimal)iMain / _iMaxTable);
                            iOper = iMesas;
                            sCol1 = _gs.ControlGridRows(this.Name, dgwTables, "assy");
                            sCol2 = _gs.ControlGridRows(this.Name, dgwTables, "assy_desc");
                            dtN.Rows.Add(sCol1, sCol2, iMain, iMesas, iOper, "assy");
                            _iMain = iMain;
                            iTotalMes += iMesas;

                            iTotalOps += iOper;

                            int assym = iMesas;
                            int assyo = iOper;

                            double dMax = (double)_iMaxTable;
                            double dW = Math.Ceiling(dWrapTime / (dMax * _dAssyTime));
                            double dWR = Math.Round(dWrapTime / (dMax * _dAssyTime));
                            iMesas = (int)dW;
                            if (sWCodeM == "1" || sWCodeM == "8")
                                iOper = iMesas;
                            else
                                iOper = (iMesas * 2);
                            sCol2 = _gs.ControlGridRows(this.Name, dgwTables, "wrap1_desc");
                            dtN.Rows.Add("Wrap", sCol2, iWrap, iMesas, iOper, "wrap1");

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

                                sCol2 = _gs.ControlGridRows(this.Name, dgwTables, "wrap2_desc");
                                dtN.Rows.Add("Wrap Sub", sCol2, iWrap, iMesas, iOper, "wrap2");


                                iTotalMes += iMesas;
                                iTotalOps += iOper;

                                iWrapSm = iMesas;
                                iWrapSo = iOper;

                            }

                            int wrap2m = 0; 

                            sCol1 = _gs.ControlGridRows(this.Name, dgwTables, "other");
                            sCol2 = _gs.ControlGridRows(this.Name, dgwTables, "deliver");

                            if (_iSurtidor > 0)
                                dtN.Rows.Add(sCol1, sCol2, 0, 0, _iSurtidor, "deliver");

                            sCol2 = _gs.ControlGridRows(this.Name, dgwTables, "insp_sealer");
                            if (_iInspSell > 0)
                                dtN.Rows.Add(sCol1, sCol2, 0, 0, _iInspSell, "insp_sealer");

                            sCol2 = _gs.ControlGridRows(this.Name, dgwTables, "sealer");
                            if (_iSellador > 0)
                                dtN.Rows.Add(sCol1, sCol2, 0, 0, _iSellador, "sealer");

                            sCol2 = _gs.ControlGridRows(this.Name, dgwTables, "inspection");
                            if (_iInspeccion > 0)
                                dtN.Rows.Add(sCol1, sCol2, 0, 0, _iInspeccion, "inspection");


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

                        //Rampeo
                        if (_dRampeo > 0 && _dRampeo < 100)
                            CalculaRampeo();

                        //Conveyor Speed
                        CalculaConveyorSpeed();

                        //sugestion
                        ItemSugLogica sug = new ItemSugLogica();
                        sug.Item = _lsItem;
                        DataTable dtS = ItemSugLogica.ConsultarItem(sug);
                        if (dtS.Rows.Count > 0)
                        {
                            wfSugeComp Suge = new wfSugeComp();
                            Suge._lsItem = _lsItem;
                            Suge._lsDesc = _lsItemDesc;
                            Suge.Show();
                        }

                        if (_sTimer == "1")
                            ShowTimer();

                        timer1.Start();
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


        */

            /*
        public void ModoManual(int ipODisponibles)
        {
            _bModoManual = true;
            //Balance permitido = 20 (Estatico)
            double[,] ite = new double[40, 9];
            double dFactor = 0; //cambiar dAssyTime / iMazTable reemplazar por Cantidad de componentes
            dFactor = Math.Ceiling((_iSub + _iMain + _iOut) * _dAssyTime / (double)_iMaxTable);
            //dFactor = Math.Ceiling((_iSub + _iMain + _iOut) / 20);
            ite[0, 0] = dFactor;
            ite[0, 1] = ipODisponibles - ite[0, 0] - (_iOperNA + _iOutO);
            ite[0, 2] = (_dAssyTime * (double)_iMaxTable);
            ite[0, 3] = Double.IsInfinity((_sDuraW1 + _sDuraW2) / (ite[0, 1] / (_iOWrap1))) ? 0 : (_sDuraW1 + _sDuraW2) / (ite[0, 1] / (_iOWrap1)); //mas duracion wrab sub,  mas operadores por mesa
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
                ite[i, 2] = ((_iSub + _iMain) * _dAssyTime / ite[i, 0]);
                ite[i, 3] = ((_sDuraW1 + _sDuraW2) / (ite[i, 1] / (_iOWrap1)));  //mas duracion wrab sub,  mas operadores por mesa
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
                    Console.Write(ite[i, j] + ", ");
                }
                Console.WriteLine();
            }

            double dDeltaMenor = 1000;
            int iAssyO = 0; //Operadores en Ensamble total
            int iWrapO = 0;  //Operadores en Wrap total
            double iCycleTimeLine = 0;

            for (int i = 1; i < 40; i++)
            {
                if (ite[i, 8] > 0 && ite[i, 8] < dDeltaMenor && ite[i, 0] > 0 && ite[i, 1] > 0)
                {
                    dDeltaMenor = ite[i, 8];
                    iAssyO = (int)Math.Ceiling(ite[i, 0]);
                    iWrapO = (int)Math.Ceiling(ite[i, 1]);
                    iCycleTimeLine = ite[i, 7];
                }
            }

            //  MessageBox.Show("Ensamble: "+iAssyO + " Wrap: " + iWrapO + " Cycle Time: " + iCycleTimeLine);

            LimpiarLayout();
            panel9.BackgroundImage = Properties.Resources.Yellow_Background_down1;
            panel2.BackgroundImage = Properties.Resources.Yellow_Background_down1;

            int iVal = int.Parse(dgwWO[4, 0].Value.ToString());
            double sWODuracionNueva = Math.Round((iCycleTimeLine * iVal) / 60) / 60;

            dgwWO[5, 0].Value = sWODuracionNueva;

            int iOperadoresTotal = 0, iMesasTotal = 0;
            int iSubAssyOp = 0;
            //Actualizar tabla dgwTables
            string ComponentesSubensamble = "0";
            int iOutFolderM = 0, iOutFolderO = 0, iWrapSubO = 0, iWrapSubM = 0, iWrapMainM = 0;


            int iWrapMainO = (int)Math.Round((_sDuraW1 / (_sDuraW1 + _sDuraW2)) * iWrapO, MidpointRounding.AwayFromZero); //operadores wrapmain
            iWrapSubO = iWrapO - iWrapMainO;
            int iCox = dgwTables.RowCount;
            for (int i = 0; i < dgwTables.RowCount; i++)
            {
                string sValue = dgwTables[0, i].Value.ToString();
                if (dgwTables[0, i].Value.ToString() == "Out")
                {
                    iOutFolderM = Int32.Parse(dgwTables[3, i].Value.ToString()); // agarrando valores para llenar layout
                    iOutFolderO = Int32.Parse(dgwTables[4, i].Value.ToString());
                }

                if (dgwTables[0, i].Value.ToString() == "Sub-Ensamble" || dgwTables[0, i].Value.ToString() == "Sub-Assembly")
                {
                    ComponentesSubensamble = dgwTables[2, i].Value.ToString(); //agarrando componentes para sumarlos a ensamble
                    if (int.TryParse(dgwTables[4, i].Value.ToString(), out iSubAssyOp))
                    {
                        iAssyO += 0;//iSubAssyOp;
                    }

                    dgwTables.Rows.Remove(dgwTables.Rows[i]); //eliminando subensamble
                    i--;
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

                    iWrapMainM = (Int32.Parse(dgwTables[4, i].Value.ToString()) / Int32.Parse(dgwTables[3, i].Value.ToString()) == 2) ? (int)Math.Ceiling(iWrapMainO / 2.0) : iWrapMainO;
                    dgwTables[3, i].Value = iWrapMainM;
                    dgwTables[4, i].Value = iWrapMainO;
                }

                if (dgwTables[0, i].Value.ToString() == "Wrap Sub")
                {
                    //ComponentesWrapSub = dgwTables[2, i].Value.ToString();
                    // dgwTables.Rows.Remove(dgwTables.Rows[i]);
                    iWrapSubM = (Int32.Parse(dgwTables[4, i].Value.ToString()) / Int32.Parse(dgwTables[3, i].Value.ToString()) == 2) ? (int)Math.Ceiling(iWrapSubO / 2.0) : iWrapSubO;
                    dgwTables[3, i].Value = iWrapSubM;
                    dgwTables[4, i].Value = iWrapSubO;
                }
            }

            for (int i = 0; i < dgwTables.RowCount; i++)
            {
                iOperadoresTotal = iOperadoresTotal + Int32.Parse(dgwTables[4, i].Value.ToString());
                iMesasTotal = iMesasTotal + Int32.Parse(dgwTables[3, i].Value.ToString());

              
            }


            lblOper.Text = iOperadoresTotal.ToString();
            lblMesas.Text = iMesasTotal.ToString();

            //Dibujar Layout con resultados finales
            llenarensamble(iAssyO, iAssyO, 0, 0, true);
            llenarwrap(iWrapMainM, iWrapMainO);
            llenarOFWS(iOutFolderM, iOutFolderO, iWrapSubM, iWrapSubO);

            lblCycleTime.Text = Math.Round(iCycleTimeLine, 3).ToString();
            if (_sIndBoxHr == "1")
            {
                decimal dKits = decimal.Parse(dgwWO["kits", 0].Value.ToString());
                decimal dBox = (decimal)iCycleTimeLine * dKits;
                dBox = Math.Round(3600 / dBox, 2);
                dgwWO["boxhr", 0].Value = dBox.ToString();
            }
            if (iCycleTimeLine > (int)_dTackTime)
                lblCycleTime.ForeColor = System.Drawing.Color.Red;
            else
                lblCycleTime.ForeColor = System.Drawing.Color.ForestGreen;

            //wfLayoutManual nform = new wfLayoutManual(Globals._gsLang, txtWO.Text, dgwWO.DataSource, dgwItem.DataSource, dgwTables.DataSource, iAssyO, iWrapO, iCycleTimeLine);
            //nform.Show();


        }
    */

    }
}
