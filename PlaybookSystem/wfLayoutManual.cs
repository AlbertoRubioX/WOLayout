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
    public partial class wfLayoutManual : Form
    {
        FormWindowState _WindowStateAnt;
        private int _iWidthAnt;
        private int _iHeightAnt;
        private string _lsLen;

        public wfLayoutManual(string _plsLen, string pWO, object poWODataSource, object poItemDataSource, object poTablesDataSource, int AssyO, int WrapO, double CycleTime)
        {
            InitializeComponent();

            _lsLen = _plsLen;
            txtWO.Text = pWO;
            dgwWO.DataSource = poWODataSource;
            dgwItem.DataSource = poItemDataSource;
            dgwTables2.DataSource = poTablesDataSource;

            _iWidthAnt = Width;
            _iHeightAnt = Height;
            _WindowStateAnt = WindowState;

            ActualizarLayout(CycleTime, AssyO, WrapO);

        }

        private void WOLayoutManual_Load(object sender, EventArgs e)
        {
            Inicio();
        }

        private void Inicio()
        {
            CargarColumnas();
            LimpiarLayout();
            ChangeLen();
        }

        private void ActualizarLayout(double CycleTime, int AssyO, int WrapO)
        {
            decimal sWODuracionVieja = (decimal)dgwWO[5, 0].Value;
            double sWODuracionNueva = Math.Round((CycleTime * int.Parse(dgwWO[4, 0].Value.ToString())) / 60);
            dgwWO[5, 0].Value = sWODuracionNueva;
            int iOperadoresTotal = 0, iMesasTotal = 0;

            //Actualizar tabla dgwTables
            string ComponentesSubensamble = "0";
            int iOutFolderM = 0, iOutFolderO = 0, iWrapSubO = 0, iWrapSubM = 0;

            for (int i = 0; i < dgwTables2.RowCount; i++)
            {
                if (dgwTables2[0, i].Value.ToString() == "Out")
                {
                    iOutFolderM = Int32.Parse(dgwTables2[3, i].Value.ToString());
                    iOutFolderO = Int32.Parse(dgwTables2[4, i].Value.ToString());
                }

                if (dgwTables2[0, i].Value.ToString() == "Sub-Ensamble" || dgwTables2[0, i].Value.ToString() == "Sub-Assembly")
                {
                    ComponentesSubensamble = dgwTables2[2, i].Value.ToString();
                    dgwTables2[2, i + 1].Value = Int32.Parse(dgwTables2[2, i + 1].Value.ToString());
                    dgwTables2.Rows.Remove(dgwTables2.Rows[i]);
                }

                if (dgwTables2[0, i].Value.ToString() == "Ensamble" || dgwTables2[0, i].Value.ToString() == "Assembly")
                {
                    dgwTables2[1, i].Value = "Ensamble (" + dgwTables2[2, i].Value.ToString() + ") & Subensamble (" + ComponentesSubensamble.ToString() + ")";
                    dgwTables2[2, i].Value = Int32.Parse(dgwTables2[2, i].Value.ToString()) + Int32.Parse(ComponentesSubensamble);
                    dgwTables2[3, i].Value = AssyO;
                    dgwTables2[4, i].Value = AssyO;

                    
                }

                if (dgwTables2[0, i].Value.ToString() == "Wrap")
                {
                    if (Int32.Parse(dgwTables2[4, i].Value.ToString()) / Int32.Parse(dgwTables2[3, i].Value.ToString()) == 2)
                    {
                        dgwTables2[4, i].Value = WrapO;
                        dgwTables2[3, i].Value = Math.Ceiling(WrapO / 2.0);
                    }
                    else
                    {
                        dgwTables2[4, i].Value = WrapO;
                        dgwTables2[3, i].Value = WrapO;
                    }

                    llenarmesa(WrapO, Int32.Parse(dgwTables2[3, i].Value.ToString()), false);

                }

                if (dgwTables2[0, i].Value.ToString() == "Wrap Sub")
                {
                    iWrapSubM = Int32.Parse(dgwTables2[3, i].Value.ToString());
                    iWrapSubO = Int32.Parse(dgwTables2[4, i].Value.ToString());
                }
            }

            for (int i = 0; i < dgwTables2.RowCount; i++)
            {
                iOperadoresTotal = iOperadoresTotal + Int32.Parse(dgwTables2[4, i].Value.ToString());
                iMesasTotal = iMesasTotal + Int32.Parse(dgwTables2[3, i].Value.ToString());
            }

            lblOper.Text = iOperadoresTotal.ToString();
            lblMesas.Text = iMesasTotal.ToString();

            llenarOFWS(iOutFolderM, iOutFolderO, iWrapSubM, iWrapSubM);
            lblCycleTime.Text = Math.Round(CycleTime, 3).ToString();

            if (CycleTime > 20)
                lblCycleTime.ForeColor = System.Drawing.Color.Red;
            else
                lblCycleTime.ForeColor = System.Drawing.Color.Green;

        }

        private void dgwTables_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int iRow = e.RowIndex;
            string sValue = e.Value.ToString();


            sValue = dgwTables2[0, e.RowIndex].Value.ToString();
            if (sValue == "Sub-Ensamble" || sValue == "Sub-Assembly")
            {
                e.CellStyle.BackColor = Color.Gold;
            }
            if (sValue == "Ensamble" || sValue == "Assembly")
            {
                e.CellStyle.BackColor = Color.SkyBlue;
                //e.CellStyle.ForeColor = Color.White;
            }
            if (sValue == "Wrap Sub")
            {
                e.CellStyle.BackColor = Color.LightPink;
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
                dgwWO.DataSource = dtNew;
            }

            dgwWO.Columns[1].Visible = false;
            dgwWO.Columns[0].Width = ColumnWith(dgwWO, 20);
            dgwWO.Columns[2].Width = ColumnWith(dgwWO, 15);
            dgwWO.Columns[3].Width = ColumnWith(dgwWO, 20);
            dgwWO.Columns[4].Width = ColumnWith(dgwWO, 20);
            dgwWO.Columns[5].Width = ColumnWith(dgwWO, 30);

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
            dgwItem.Columns[5].Visible = false;
            dgwItem.Columns[6].Width = ColumnWith(dgwItem, 15);
            dgwItem.Columns[7].Width = ColumnWith(dgwItem, 16);
            dgwItem.Columns[8].Visible = false;
            if (dgwTables2.Rows.Count == 0)
            {
                DataTable dtNew = new DataTable("Tables");
                dtNew.Columns.Add("area", typeof(string));
                dtNew.Columns.Add("descrip", typeof(string));
                dtNew.Columns.Add("comp", typeof(string));
                dtNew.Columns.Add("tables", typeof(int));
                dtNew.Columns.Add("headcount", typeof(int));
                dtNew.Columns.Add("code", typeof(string));
                dgwTables2.DataSource = dtNew;
            }

            dgwTables2.Columns[5].Visible = false;
            dgwTables2.Columns[0].Width = ColumnWith(dgwTables2, 25);
            dgwTables2.Columns[1].Width = ColumnWith(dgwTables2, 48);
            dgwTables2.Columns[2].Width = ColumnWith(dgwTables2, 10);
            dgwTables2.Columns[3].Width = ColumnWith(dgwTables2, 10);
            dgwTables2.Columns[4].Width = ColumnWith(dgwTables2, 10);
            dgwTables2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwTables2.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwTables2.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwTables2.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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

        private void ChangeLen()
        {

            ControlText(this);
            ControlText(this.panel8);
            ControlGridText(dgwWO);
            ControlGridText(dgwItem);
            ControlGridText(dgwTables2);
            ControlGridRows2(dgwItem);
            ControlGridRows2(dgwTables2);

        }
        private void ControlText(Control _control)
        {
            ConfigLogica con = new ConfigLogica();
            con.Language = _lsLen;
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
            con.Language = _lsLen;
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
            con.Language = _lsLen;
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
            con.Language = _lsLen;
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
                ResizeControl(dgwTables2, 1, ref _iWidthAnt, ref _iHeightAnt, 1);
                int iH = panel9.Height;
                int iY = iH - ptbLogo.Height - 30;
                ptbLogo.Location = new Point(ptbLogo.Location.X, iY);

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

        public void llenarmesa(int nummesas, int nummoperadores, bool ensamable)
        {
            PictureBox[] mesas = getmesas();
            PictureBox[] operadores = getoperadores();

            Boolean libre = true;
            int posicionlibre;

            if (ensamable)
                posicionlibre = 0;
            else
                posicionlibre = 9;

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

        public void llenarensamble(int emesas, int eoperadores, int smesas, int soperadores)
        {
            PictureBox[] mesas = getmesas();
            PictureBox[] operadores = getoperadores();
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

        public void llenarOFWS(int piOFMesas, int piOFOperadores, int piWSMesas, int piWSOperadores)
        {
            PictureBox[] mesas = getmesas();
            PictureBox[] operadores = getoperadores();

            for (int i = 15; i < 15 + piOFMesas; i++)
            {
                mesas[i].Visible = true;
                operadores[i * 2].Visible = true;
                if (piWSOperadores == piOFMesas * 2)
                    operadores[(i * 2) + 1].Visible = true;
            }

            for (int i = 20 + piOFMesas; i < 20 + piWSMesas; i++)
            {
                mesas[i].Visible = true;
                operadores[i * 2].Visible = true;
                if (piWSOperadores == piOFMesas * 2)
                    operadores[(i * 2) + 1].Visible = true;
            }

        }


        #endregion

    }
}
