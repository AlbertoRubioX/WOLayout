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
    public partial class wfLayout : Form
    {
        private bool _bNumber = false;
        private double _dWrapTime;  //14
        private double _dAssyTime;  //14
        private double _dTackIdeal;
        private double _dTackTime;
        private int _iMaxTable;     //15
        private int _iMesaEns;      //16
        private int _iMesaWrap;     //17
        private int _iEstSub;       //18
        private int _iSurtidor;
        private int _iInspSell;
        private int _iSellador;
        private int _iInspeccion;
        public wfLayout()
        {
            InitializeComponent();
        }

        private void wfLayout_Load(object sender, EventArgs e)
        {
            //Tack Data
            _dWrapTime = 84.5;
            _dTackIdeal = 25;
            _dTackTime = _dTackIdeal * 0.8;

            _dAssyTime = 2.8;
            _iMaxTable = 7;
            _iMesaEns = 5;
            _iMesaWrap = 6;
            _iEstSub = 2;
            //No assy
            _iSurtidor = 1;
            _iInspeccion = 1;
            _iInspSell = 1;
            _iSellador = 1;

            txtWO.Focus();
            CargarColumnas();
        }

        private void wfLayout_Activated(object sender, EventArgs e)
        {
            txtWO.Focus();
        }

        private void txtWO_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                _bNumber = false;

                if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
                {
                    if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                    {
                        if (e.KeyCode != Keys.Back)
                            _bNumber = true;
                    }
                }
                
                if (Control.ModifierKeys == Keys.Shift)
                    _bNumber = true;
                

                if (e.KeyCode != Keys.Enter)
                    return;

                string sValue = txtWO.Text.ToString().ToUpper();
                if (sValue.Length < 7)
                {
                    sValue = sValue.PadLeft(7, '0');
                    txtWO.Text = sValue;
                }

                AS4Logica AS4 = new AS4Logica();
                AS4.WO = sValue;
                DataTable dt = AS4Logica.WorkOrder(AS4);
                if(dt.Rows.Count > 0)
                {
                    txtItem.Clear();
                    dgwWO.DataSource = dt;
                    dgwWO.CurrentCell = null;
                    dgwWO.Columns[0].Visible = false;

                    string sItem = dt.Rows[0][0].ToString();
                    AS4.Item = sItem;
                    txtItem.Text = sItem;

                    DataTable dt2 = AS4Logica.ComponentsLayer(AS4);
                    dgwItem.DataSource = null;
                    CargarColumnas();
                    if(dt2.Rows.Count > 0)
                    {
                        string sPre = dt2.Rows[0][0].ToString();
                        int iComp = int.Parse(dt2.Rows[0][1].ToString());
                        
                        string sDuraW1 = dt2.Rows[1][0].ToString();
                        string sWrap1 = dt2.Rows[1][1].ToString();
                        string sWrapDesc = string.Empty;
                        if (sWrap1 == "1")
                            sWrapDesc = "Sobre";
                        if (sWrap1 == "2")
                            sWrapDesc = "Vertical";
                        if (sWrap1 == "3")
                            sWrapDesc = "Horizontal";
                        if (sWrap1 == "4")
                            sWrapDesc = "Detroit";
                        if (sWrap1 == "8")
                            sWrapDesc = "Truck and Tape";

                        DataTable dt3 = dgwItem.DataSource as DataTable;
                        dt3.Rows.Add(iComp,sPre,sWrapDesc,sDuraW1,null,null);

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
                                if (sLevel == "M") iMain = iCompx;
                                if (sLevel == "Y") iPiggy = iCompx;
                                
                            }

                            int iOper = 0;
                            int iMesas = 0;
                            DataTable dtN = dgwTables.DataSource as DataTable;

                            iMesas = iOut + iBasin + iPiggy;
                            iMesas =(int)Math.Ceiling((decimal)iMesas / (decimal)_iMaxTable);
                            iOper = iMesas;
                            dtN.Rows.Add("Assy", "OutFolder/Basin", iMesas, iOper);
                            iTotalMes += iMesas;
                            iTotalOps += iOper;

                            decimal cM = Math.Ceiling((decimal)iSub / (decimal)_iMaxTable);
                            iMesas = (int)cM;
                            iMesas = (int)Math.Ceiling((decimal)iMesas / (decimal)_iEstSub);
                            iOper = iMesas * _iEstSub;
                            dtN.Rows.Add("Assy", "Sub-Assy", iMesas, iOper);
                            iTotalMes += iMesas;
                            iTotalOps += iOper;

                            iMesas = (int)Math.Ceiling((decimal)iMain / (decimal)_iMaxTable);
                            iOper = iMesas;
                            dtN.Rows.Add("Assy", "Conveyor Assy", iMesas, iOper);
                            iTotalMes += iMesas;
                            iTotalOps += iOper;

                            double dMax = (double)_iMaxTable;
                            double dW = Math.Ceiling(_dWrapTime / (dMax * _dAssyTime));
                            iMesas = (int)dW;
                            if (sWrap1 == "1" || sWrap1 == "8")
                                iOper = iMesas;
                            else
                                iOper = (iMesas * 2);

                            dtN.Rows.Add("Wrap", "Wrap 1", iMesas, iOper);
                            iTotalMes += iMesas;
                            iTotalOps += iOper;

                            iMesas = 0;
                            iOper = 0;
                            dtN.Rows.Add("Wrap", "Wrap 2", iMesas, iOper);
                            iTotalMes += iMesas;
                            iTotalOps += iOper;

                            dtN.Rows.Add("Other", "Supplier", 0, _iSurtidor);
                            dtN.Rows.Add("Other", "Sealer Inspection", 0, _iInspSell);
                            dtN.Rows.Add("Other", "Sealer", 0, _iSellador);
                            dtN.Rows.Add("Other", "Inspection 100%", 0, _iInspeccion);
                            iTotalOps += iO;

                            lblMesas.Text = iTotalMes.ToString();
                            lblOper.Text = iTotalOps.ToString();

                            dgwTables.ClearSelection();
                        }
                        
                        dgwItem.ClearSelection();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtWO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_bNumber == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }

        private void CargarColumnas()
        {
            if (dgwItem.Rows.Count == 0)
            {
                DataTable dtNew = new DataTable("Item");
                dtNew.Columns.Add("COMPONENTS", typeof(int));
                dtNew.Columns.Add("PRE-ASSY", typeof(string));
                dtNew.Columns.Add("WRAP 1", typeof(string));
                dtNew.Columns.Add("DURATION", typeof(string));
                dtNew.Columns.Add("WRAP 2", typeof(string));
                dtNew.Columns.Add("DURATION 2", typeof(string));
                dgwItem.DataSource = dtNew;
            }

            dgwItem.Columns[0].Width = ColumnWith(dgwItem, 18);
            dgwItem.Columns[1].Width = ColumnWith(dgwItem, 15);
            dgwItem.Columns[2].Width = ColumnWith(dgwItem, 15);
            dgwItem.Columns[3].Width = ColumnWith(dgwItem, 20);
            dgwItem.Columns[4].Width = ColumnWith(dgwItem, 15);
            dgwItem.Columns[5].Width = ColumnWith(dgwItem, 20);

            dgwItem.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwItem.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwItem.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwItem.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgwItem.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwItem.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            if (dgwTables.Rows.Count == 0)
            {
                DataTable dtNew = new DataTable("Tables");
                dtNew.Columns.Add("TYPE", typeof(string));
                dtNew.Columns.Add("DESCRIPTION", typeof(string));
                dtNew.Columns.Add("TABLES", typeof(int));
                dtNew.Columns.Add("OPERATORS", typeof(int));
                dgwTables.DataSource = dtNew;
            }

            dgwTables.Columns[0].Width = ColumnWith(dgwTables, 15);
            dgwTables.Columns[1].Width = ColumnWith(dgwTables, 43);
            dgwTables.Columns[2].Width = ColumnWith(dgwTables, 20);
            dgwTables.Columns[3].Width = ColumnWith(dgwTables, 25);
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

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

        public void LimpiarEnsamble()
        {
            PictureBox[] mesas = getmesas();
            PictureBox[] operadores = getoperadores();

            for (int i = 0; i <= 7; i++)
            {
                mesas[i].Visible = false;
            }

            for (int i = 0; i <= 15; i++)
            {
                operadores[i].Visible = false;
            }
        }

        public void LimpiarWrap()
        {
            PictureBox[] mesas = getmesas();
            PictureBox[] operadores = getoperadores();

            for (int i = 8; i <= 13; i++)
            {
                mesas[i].Visible = false;
            }
            for (int i = 16; i <= 27; i++)
            {
                operadores[i].Visible = false;
            }
        }
    }
}
