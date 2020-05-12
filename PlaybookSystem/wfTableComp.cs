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
    public partial class wfTableComp : Form
    {
        public string _lsProceso;
        public string _lsMesa;
        public string _lsItem;
        public int _liPS;
        public int _liCom;
        public DataTable _dt;

        Globals _gs = new Globals();
        public wfTableComp()
        {
            InitializeComponent();
        }
        #region Inicio
        private void wfTableComp_Load(object sender, EventArgs e)
        {
            _lsProceso = "PRO040";
            this.Text = "Mesa " + _lsMesa;
            tssTable.Text = this.Text;
            tssJob.Text = _lsItem;
            tssTotal.Text = "0";

            tabControl1.TabPages[0].Text = this.Text;
            Inicio();
            
        }
        private void Inicio()
        {
            try
            {
                ConfigLogica con = new ConfigLogica();
                con.CN = Globals._gsCompany;
                DataTable dtC = ConfigLogica.Consultar(con);
                _liPS = int.Parse(dtC.Rows[0]["comp_pre"].ToString());
                _liCom = int.Parse(dtC.Rows[0]["max_comp"].ToString());

                cbComp.DataSource = null;
                CargarLista();

                dgwTable.DataSource = null;

                dgwPre.DataSource = null;
                dgwPre2.DataSource = null;
                dgwPre3.DataSource = null;
                dgwPre4.DataSource = null;
                dgwPre5.DataSource = null;

                ItemSugdetLogica item = new ItemSugdetLogica();
                item.Item = _lsItem;
                item.Mesa = _lsMesa;
                int iT = 0;
                DataTable dt = ItemSugdetLogica.ConsultarVistaMesa(item);
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    dt.Rows[x][1] = x + 1;

                    string sCodigo = dt.Rows[x][2].ToString();
                    if (!string.IsNullOrEmpty(sCodigo))
                        iT++;
                }

                dgwTable.DataSource = dt;
                DataTable dtS = ItemSugdetLogica.ListarPS(item);
                iT = iT - dtS.Rows.Count;
                int iTab = 0;
                
                for (int x = 0; x < dtS.Rows.Count; x++)
                {
                    string sCodigo = dtS.Rows[x][0].ToString();
                    
                    item.Codigo = sCodigo;
                    iTab = int.Parse(sCodigo.Substring(sCodigo.Length - 1, 1));
                    switch(iTab)
                    {
                        case 1:
                            dgwPre.DataSource = ItemSugdetLogica.ConsultarVistaPS(item);
                            break;
                        case 2:
                            dgwPre2.DataSource = ItemSugdetLogica.ConsultarVistaPS(item);
                            break;
                        case 3:
                            dgwPre3.DataSource = ItemSugdetLogica.ConsultarVistaPS(item);
                            break;
                        case 4:
                            dgwPre4.DataSource = ItemSugdetLogica.ConsultarVistaPS(item);
                            break;
                        case 5:
                            dgwPre5.DataSource = ItemSugdetLogica.ConsultarVistaPS(item);
                            break;
                    }
                }
                //set sort value to pre
                foreach (DataGridViewRow row in dgwPre.Rows)
                {
                    dgwPre[1, row.Index].Value = row.Index + 1;
                }
                foreach (DataGridViewRow row in dgwPre2.Rows)
                {
                    dgwPre2[1, row.Index].Value = row.Index + 1;
                }
                foreach (DataGridViewRow row in dgwPre3.Rows)
                {
                    dgwPre3[1, row.Index].Value = row.Index + 1;
                }
                foreach (DataGridViewRow row in dgwPre4.Rows)
                {
                    dgwPre4[1, row.Index].Value = row.Index + 1;
                }
                foreach (DataGridViewRow row in dgwPre5.Rows)
                {
                    dgwPre5[1, row.Index].Value = row.Index + 1;
                }

                iT += dgwPre.Rows.Count;
                iT += dgwPre2.Rows.Count;
                iT += dgwPre3.Rows.Count;
                iT += dgwPre4.Rows.Count;
                iT += dgwPre5.Rows.Count;

                CargarColumnas();
                CargarColumnasSP();

                tabControl1.SelectedIndex = 0;
                LastRow(0);

                tssTotal.Text = iT.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    
        private void wfTableComp_Activated(object sender, EventArgs e)
        {
            cbComp.Focus();
            
        }
        private void CargarLista()
        {
            DataTable dtNew = new DataTable("Comp");
            dtNew.Columns.Add("codigo", typeof(string));
            dtNew.Columns.Add("descrip", typeof(string));
            
            for (int x = 0; x < _dt.Rows.Count; x++)
            {
                string sCode = _dt.Rows[x][1].ToString();
                string sDesc = _dt.Rows[x][2].ToString();
                sDesc = sCode + " " + sDesc;

                dtNew.Rows.Add(sCode, sDesc);
            }

            DataTable dtSE = new DataTable("se");
            dtSE.Columns.Add("codigo", typeof(string));
            
            for (int i = 1; i <= _liPS; i++)
            {
                string sCode = "PRE-" + i.ToString().PadLeft(2,'0');
                dtSE.Rows.Add(sCode);

                dtNew.Rows.Add(sCode, sCode);
            }

            cbComp.DataSource = dtNew;
            cbComp.DisplayMember = "descrip";
            cbComp.ValueMember = "codigo";
            cbComp.SelectedIndex = -1;

            
        }
        private void CargarColumnas()
        {
            int iRows = dgwTable.Rows.Count;
            if (iRows == 0)
            {
                DataTable dtNew = new DataTable("Comp");
                dtNew.Columns.Add("mesa", typeof(string));
                dtNew.Columns.Add("#", typeof(decimal));
                dtNew.Columns.Add("CODIGO", typeof(string));
                dtNew.Columns.Add("DESCRIPCION", typeof(string));
                dtNew.Columns.Add("CANT", typeof(decimal));
                dtNew.Columns.Add("UM", typeof(string));
                dtNew.Columns.Add("consec", typeof(decimal));
                dgwTable.DataSource = dtNew;
            }

            dgwTable.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgwTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dgwTable.Columns[0].Visible = false;
            dgwTable.Columns[6].Visible = false;

            dgwTable.Columns[1].Width = ColumnWith(dgwTable, 5);
            dgwTable.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwTable.Columns[1].ReadOnly = true;

            dgwTable.Columns[2].Width = ColumnWith(dgwTable, 20);
            dgwTable.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwTable.Columns[2].ReadOnly = true;

            dgwTable.Columns[3].Width = ColumnWith(dgwTable, 55);
            dgwTable.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwTable.Columns[3].ReadOnly = true;

            dgwTable.Columns[4].Width = ColumnWith(dgwTable, 10);//qty
            dgwTable.Columns[4].DefaultCellStyle.BackColor = Color.LightYellow;
            dgwTable.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwTable.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgwTable.Columns[4].DefaultCellStyle.Format = "0";

            dgwTable.Columns[5].Width = ColumnWith(dgwTable, 10);//um
            dgwTable.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwTable.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwTable.Columns[5].ReadOnly = true;

            int iCons = 1;
            foreach(DataGridViewRow row in dgwTable.Rows)
            {
                int iConsec = int.Parse(dgwTable[1, row.Index].Value.ToString());
                if(iConsec - iCons > 0)
                {
                    for (int i = iCons; i < iConsec; i++)
                    {
                        DataTable dtA = dgwTable.DataSource as DataTable;
                        dtA.Rows.Add(_lsMesa, i, null, null, null, null,i);
                    }
                    iCons = iConsec;
                }

                iCons++;
            }

            int iR = dgwTable.Rows.Count + 1;
            DataTable dt = dgwTable.DataSource as DataTable;
            for (int i = iR; i <= _liCom; i++)
            {
                dt.Rows.Add(_lsMesa, i, null, null, null, null,i);
            }

            dgwTable.Sort(dgwTable.Columns[1], ListSortDirection.Ascending);

        }

        private void CargarColumnasSP()
        {
            int iRows = dgwPre.Rows.Count;
            if (iRows == 0)
            {
                DataTable dtNew2 = new DataTable("pre");
                dtNew2.Columns.Add("ps", typeof(string));
                dtNew2.Columns.Add("#", typeof(decimal));
                dtNew2.Columns.Add("CODIGO", typeof(string));
                dtNew2.Columns.Add("DESCRIPCION", typeof(string));
                dtNew2.Columns.Add("CANT", typeof(decimal));
                dtNew2.Columns.Add("UM", typeof(string));
                dtNew2.Columns.Add("consec", typeof(decimal));

                dgwPre.DataSource = dtNew2;
            }
            dgwPre.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgwPre.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dgwPre.Columns[0].Visible = false;
            dgwPre.Columns[6].Visible = false;

            dgwPre.Columns[1].Width = ColumnWith(dgwPre, 5);
            dgwPre.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre.Columns[1].ReadOnly = true;

            dgwPre.Columns[2].Width = ColumnWith(dgwPre, 20);
            dgwPre.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre.Columns[2].ReadOnly = true;

            dgwPre.Columns[3].Width = ColumnWith(dgwPre, 55);
            dgwPre.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre.Columns[3].ReadOnly = true;

            dgwPre.Columns[4].Width = ColumnWith(dgwPre, 10);//qty
            dgwPre.Columns[4].DefaultCellStyle.BackColor = Color.LightYellow;
            dgwPre.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgwPre.Columns[4].DefaultCellStyle.Format = "0";

            dgwPre.Columns[5].Width = ColumnWith(dgwPre, 10);//um
            dgwPre.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre.Columns[5].ReadOnly = true;

            iRows = dgwPre2.Rows.Count;
            if (iRows == 0)
            {
                DataTable dtNew2 = new DataTable("pre");
                dtNew2.Columns.Add("ps", typeof(string));
                dtNew2.Columns.Add("#", typeof(decimal));
                dtNew2.Columns.Add("CODIGO", typeof(string));
                dtNew2.Columns.Add("DESCRIPCION", typeof(string));
                dtNew2.Columns.Add("CANT", typeof(decimal));
                dtNew2.Columns.Add("UM", typeof(string));
                dtNew2.Columns.Add("consec", typeof(decimal));

                dgwPre2.DataSource = dtNew2;
            }
            dgwPre2.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgwPre2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dgwPre2.Columns[0].Visible = false;
            dgwPre2.Columns[6].Visible = false;

            dgwPre2.Columns[1].Width = ColumnWith(dgwPre, 5);
            dgwPre2.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre2.Columns[1].ReadOnly = true;

            dgwPre2.Columns[2].Width = ColumnWith(dgwPre, 20);
            dgwPre2.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre2.Columns[2].ReadOnly = true;

            dgwPre2.Columns[3].Width = ColumnWith(dgwPre, 55);
            dgwPre2.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre2.Columns[3].ReadOnly = true;

            dgwPre2.Columns[4].Width = ColumnWith(dgwPre, 10);//qty
            dgwPre2.Columns[4].DefaultCellStyle.BackColor = Color.LightYellow;
            dgwPre2.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre2.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgwPre2.Columns[4].DefaultCellStyle.Format = "0";

            dgwPre2.Columns[5].Width = ColumnWith(dgwPre, 10);//um
            dgwPre2.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre2.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre2.Columns[5].ReadOnly = true;

           

            //*3*//
            iRows = dgwPre3.Rows.Count;
            if (iRows == 0)
            {
                DataTable dtNew2 = new DataTable("pre");
                dtNew2.Columns.Add("ps", typeof(string));
                dtNew2.Columns.Add("#", typeof(decimal));
                dtNew2.Columns.Add("CODIGO", typeof(string));
                dtNew2.Columns.Add("DESCRIPCION", typeof(string));
                dtNew2.Columns.Add("CANT", typeof(decimal));
                dtNew2.Columns.Add("UM", typeof(string));
                dtNew2.Columns.Add("consec", typeof(decimal));
                dgwPre3.DataSource = dtNew2;
            }
            dgwPre3.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgwPre3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dgwPre3.Columns[0].Visible = false;
            dgwPre3.Columns[6].Visible = false;

            dgwPre3.Columns[1].Width = ColumnWith(dgwPre, 5);
            dgwPre3.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre3.Columns[1].ReadOnly = true;

            dgwPre3.Columns[2].Width = ColumnWith(dgwPre, 20);
            dgwPre3.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre3.Columns[2].ReadOnly = true;

            dgwPre3.Columns[3].Width = ColumnWith(dgwPre, 55);
            dgwPre3.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre3.Columns[3].ReadOnly = true;

            dgwPre3.Columns[4].Width = ColumnWith(dgwPre, 10);//qty
            dgwPre3.Columns[4].DefaultCellStyle.BackColor = Color.LightYellow;
            dgwPre3.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre3.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgwPre3.Columns[4].DefaultCellStyle.Format = "0";

            dgwPre3.Columns[5].Width = ColumnWith(dgwPre, 10);//um
            dgwPre3.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre3.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre3.Columns[5].ReadOnly = true;

            
            //*4*//
            iRows = dgwPre4.Rows.Count;
            if (iRows == 0)
            {
                DataTable dtNew2 = new DataTable("pre");
                dtNew2.Columns.Add("ps", typeof(string));
                dtNew2.Columns.Add("#", typeof(decimal));
                dtNew2.Columns.Add("CODIGO", typeof(string));
                dtNew2.Columns.Add("DESCRIPCION", typeof(string));
                dtNew2.Columns.Add("CANT", typeof(decimal));
                dtNew2.Columns.Add("UM", typeof(string));
                dtNew2.Columns.Add("consec", typeof(decimal));
                dgwPre4.DataSource = dtNew2;
            }
            dgwPre4.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgwPre4.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dgwPre4.Columns[0].Visible = false;
            dgwPre4.Columns[6].Visible = false;

            dgwPre4.Columns[1].Width = ColumnWith(dgwPre, 5);
            dgwPre4.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre4.Columns[1].ReadOnly = true;

            dgwPre4.Columns[2].Width = ColumnWith(dgwPre, 20);
            dgwPre4.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre4.Columns[2].ReadOnly = true;

            dgwPre4.Columns[3].Width = ColumnWith(dgwPre, 55);
            dgwPre4.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre4.Columns[3].ReadOnly = true;

            dgwPre4.Columns[4].Width = ColumnWith(dgwPre, 10);//qty
            dgwPre4.Columns[4].DefaultCellStyle.BackColor = Color.LightYellow;
            dgwPre4.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre4.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgwPre4.Columns[4].DefaultCellStyle.Format = "0";

            dgwPre4.Columns[5].Width = ColumnWith(dgwPre, 10);//um
            dgwPre4.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre4.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre4.Columns[5].ReadOnly = true;
            

            //*5*//
            iRows = dgwPre5.Rows.Count;
            if (iRows == 0)
            {
                DataTable dtNew2 = new DataTable("pre");
                dtNew2.Columns.Add("ps", typeof(string));
                dtNew2.Columns.Add("#", typeof(decimal));
                dtNew2.Columns.Add("CODIGO", typeof(string));
                dtNew2.Columns.Add("DESCRIPCION", typeof(string));
                dtNew2.Columns.Add("CANT", typeof(decimal));
                dtNew2.Columns.Add("UM", typeof(string));
                dtNew2.Columns.Add("consec", typeof(decimal));

                dgwPre5.DataSource = dtNew2;
            }
            dgwPre5.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgwPre5.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dgwPre5.Columns[0].Visible = false;
            dgwPre5.Columns[6].Visible = false;

            dgwPre5.Columns[1].Width = ColumnWith(dgwPre, 5);
            dgwPre5.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre5.Columns[1].ReadOnly = true;

            dgwPre5.Columns[2].Width = ColumnWith(dgwPre, 20);
            dgwPre5.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre5.Columns[2].ReadOnly = true;

            dgwPre5.Columns[3].Width = ColumnWith(dgwPre, 55);
            dgwPre5.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre5.Columns[3].ReadOnly = true;

            dgwPre5.Columns[4].Width = ColumnWith(dgwPre, 10);//qty
            dgwPre5.Columns[4].DefaultCellStyle.BackColor = Color.LightYellow;
            dgwPre5.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre5.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgwPre5.Columns[4].DefaultCellStyle.Format = "0";

            dgwPre5.Columns[5].Width = ColumnWith(dgwPre, 10);//um
            dgwPre5.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre5.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwPre5.Columns[5].ReadOnly = true;
            
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


        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Inicio();
        }

        #endregion

        #region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!Valida())
                return;

            try
            {
                
                ItemSugdetLogica item = new ItemSugdetLogica();
                item.Item = _lsItem;
                item.Mesa = _lsMesa;

                foreach (DataGridViewRow row in dgwTable.Rows)
                {
                    if (!string.IsNullOrEmpty(dgwTable[2, row.Index].Value.ToString()))
                    {
                        item.Consec = int.Parse(dgwTable[6, row.Index].Value.ToString());
                        item.Codigo = dgwTable[2, row.Index].Value.ToString();
                        item.Descrip = dgwTable[3, row.Index].Value.ToString();
                        item.Cant = decimal.Parse(dgwTable[4, row.Index].Value.ToString());
                        item.UM = dgwTable[5, row.Index].Value.ToString();
                        item.Tipo = "C";
                        item.Usuario = Globals._gsUser;
                        ItemSugdetLogica.GuardarSP(item);
                    }
                }

                //sub-ensambles
                foreach (DataGridViewRow row in dgwPre.Rows)
                {
                    if (!string.IsNullOrEmpty(dgwPre[2, row.Index].Value.ToString()))
                    {
                        item.Mesa = dgwPre[0, row.Index].Value.ToString();
                        item.Consec = int.Parse(dgwPre[6, row.Index].Value.ToString());
                        item.Codigo = dgwPre[2, row.Index].Value.ToString();
                        item.Descrip = dgwPre[3, row.Index].Value.ToString();
                        item.Cant = decimal.Parse(dgwPre[4, row.Index].Value.ToString());
                        item.UM = dgwPre[5, row.Index].Value.ToString();
                        item.Tipo = "S";
                        item.Usuario = Globals._gsUser;
                        ItemSugdetLogica.GuardarSP(item);
                    }
                }
                foreach (DataGridViewRow row in dgwPre2.Rows)
                {
                    if (!string.IsNullOrEmpty(dgwPre2[2, row.Index].Value.ToString()))
                    {
                        item.Mesa = dgwPre2[0, row.Index].Value.ToString();
                        item.Consec = int.Parse(dgwPre2[6, row.Index].Value.ToString());
                        item.Codigo = dgwPre2[2, row.Index].Value.ToString();
                        item.Descrip = dgwPre2[3, row.Index].Value.ToString();
                        item.Cant = decimal.Parse(dgwPre2[4, row.Index].Value.ToString());
                        item.UM = dgwPre2[5, row.Index].Value.ToString();
                        item.Tipo = "S";
                        item.Usuario = Globals._gsUser;
                        ItemSugdetLogica.GuardarSP(item);
                    }
                }
                foreach (DataGridViewRow row in dgwPre3.Rows)
                {
                    if (!string.IsNullOrEmpty(dgwPre3[2, row.Index].Value.ToString()))
                    {
                        item.Mesa = dgwPre3[0, row.Index].Value.ToString();
                        item.Consec = int.Parse(dgwPre3[6, row.Index].Value.ToString());
                        item.Codigo = dgwPre3[2, row.Index].Value.ToString();
                        item.Descrip = dgwPre3[3, row.Index].Value.ToString();
                        item.Cant = decimal.Parse(dgwPre3[4, row.Index].Value.ToString());
                        item.UM = dgwPre3[5, row.Index].Value.ToString();
                        item.Tipo = "S";
                        item.Usuario = Globals._gsUser;
                        ItemSugdetLogica.GuardarSP(item);
                    }
                }
                foreach (DataGridViewRow row in dgwPre4.Rows)
                {
                    if (!string.IsNullOrEmpty(dgwPre4[2, row.Index].Value.ToString()))
                    {
                        item.Mesa = dgwPre4[0, row.Index].Value.ToString();
                        item.Consec = int.Parse(dgwPre4[6, row.Index].Value.ToString());
                        item.Codigo = dgwPre4[2, row.Index].Value.ToString();
                        item.Descrip = dgwPre4[3, row.Index].Value.ToString();
                        item.Cant = decimal.Parse(dgwPre4[4, row.Index].Value.ToString());
                        item.UM = dgwPre4[5, row.Index].Value.ToString();
                        item.Tipo = "S";
                        item.Usuario = Globals._gsUser;
                        ItemSugdetLogica.GuardarSP(item);
                    }
                }
                foreach (DataGridViewRow row in dgwPre5.Rows)
                {
                    if (!string.IsNullOrEmpty(dgwPre5[2, row.Index].Value.ToString()))
                    {
                        item.Mesa = dgwPre5[0, row.Index].Value.ToString();
                        item.Consec = int.Parse(dgwPre5[6, row.Index].Value.ToString());
                        item.Codigo = dgwPre5[2, row.Index].Value.ToString();
                        item.Descrip = dgwPre5[3, row.Index].Value.ToString();
                        item.Cant = decimal.Parse(dgwPre5[4, row.Index].Value.ToString());
                        item.UM = dgwPre5[5, row.Index].Value.ToString();
                        item.Tipo = "S";
                        item.Usuario = Globals._gsUser;
                        ItemSugdetLogica.GuardarSP(item);
                    }
                }

                ItemSugLogica sug = new ItemSugLogica();
                sug.Item = _lsItem;
                sug.Usuario = Globals._gsUser;
                ItemSugLogica.UpdateItem(sug);

                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al intentar guardar. " + ex.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cbComp.Focus();
            }
        }
        private bool ValidaCant(string _asComp,int _aiCant)
        {
            bool bValida = true;
            int iCx = 0;

            if(_asComp.IndexOf("PRE") == -1)
            {
                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    string sCodigo = _dt.Rows[i][1].ToString();
                    if (sCodigo == _asComp)
                    {
                        decimal iC = decimal.Parse(_dt.Rows[i][3].ToString());
                        iCx += (int)iC;
                    }
                }

                if (_aiCant > iCx)
                {
                    MessageBox.Show("La cantidad capturada (" + _aiCant.ToString() + ") para el componente " + _asComp + " supera la cantidad en PackDraw (" + iCx.ToString() + ")", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    bValida = false;
                }
            }
            
            return bValida;
        }
        private bool Valida()
        {
            bool bValida = true;

            if (dgwTable.Rows.Count == 0)
                return false;

            int iComp = 0;
            foreach (DataGridViewRow row in dgwTable.Rows)
            {
                if (!string.IsNullOrEmpty(dgwTable[2, row.Index].Value.ToString()))
                {
                    string sComp = dgwTable[2, row.Index].Value.ToString();
                    decimal iCant = 0;
                    if (!decimal.TryParse(dgwTable[4, row.Index].Value.ToString(), out iCant))
                        iCant = 0;

                    if(iCant == 0)
                    {
                        MessageBox.Show("No se ha capturado la cantidad en el componente" + sComp, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tabControl1.SelectedIndex = 0;
                        return false;
                    }
                    //comparar cantidad en packdraw
                    int iCanti = (int)iCant;
                    if (!ValidaCant(sComp, iCanti))
                    {
                        tabControl1.SelectedIndex = 0;
                        dgwTable.Rows[row.Index].Cells[4].Selected = true;
                        return false;
                    }


                    iComp++;
                    
                    if(sComp.IndexOf("PRE-") != -1)
                    {
                        bool bExiste = false;
                        if(sComp == "PRE-01")
                        {
                            foreach (DataGridViewRow row2 in dgwPre.Rows)
                            {
                                string sPre = dgwPre[0, row2.Index].Value.ToString();
                                if (sComp == sPre)
                                {
                                    bExiste = true;
                                    break;
                                }
                            }
                        }
                        if (sComp == "PRE-02")
                        {
                            foreach (DataGridViewRow row2 in dgwPre2.Rows)
                            {
                                string sPre = dgwPre2[0, row2.Index].Value.ToString();
                                if (sComp == sPre)
                                {
                                    bExiste = true;
                                    break;
                                }
                            }
                        }

                        if (sComp == "PRE-03")
                        {
                            foreach (DataGridViewRow row2 in dgwPre3.Rows)
                            {
                                string sPre = dgwPre3[0, row2.Index].Value.ToString();
                                if (sComp == sPre)
                                {
                                    bExiste = true;
                                    break;
                                }
                            }
                        }
                        if (sComp == "PRE-04")
                        {
                            foreach (DataGridViewRow row2 in dgwPre4.Rows)
                            {
                                string sPre = dgwPre4[0, row2.Index].Value.ToString();
                                if (sComp == sPre)
                                {
                                    bExiste = true;
                                    break;
                                }
                            }
                        }

                        if (sComp == "PRE-05")
                        {
                            foreach (DataGridViewRow row2 in dgwPre5.Rows)
                            {
                                string sPre = dgwPre5[0, row2.Index].Value.ToString();
                                if (sComp == sPre)
                                {
                                    bExiste = true;
                                    break;
                                }
                            }
                        }

                        if (!bExiste)
                        {
                            MessageBox.Show("No se ha regsitrado los componentes del Pre-Ensamble " + sComp, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            tabControl1.SelectedIndex = 0;
                            return false;
                        }
                    }
                }   
            }
           
            if(iComp == 0)
            {
                MessageBox.Show("No se ha regsitrado los componentes", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedIndex = 0;
                return false;
            }

            foreach (DataGridViewRow row in dgwPre.Rows)
            {
                if (!string.IsNullOrEmpty(dgwPre[2, row.Index].Value.ToString()))
                {
                    string sComp = dgwPre[2, row.Index].Value.ToString();
                    decimal iCant = 0;
                    if (!decimal.TryParse(dgwPre[4, row.Index].Value.ToString(), out iCant))
                        iCant = 0;

                    if (iCant == 0)
                    {
                        MessageBox.Show("No se ha capturado la cantidad en el componente " + sComp, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tabControl1.SelectedIndex = 1;
                        return false;
                    }

                    //comparar cantidad en packdraw
                    int iCanti = (int)iCant;
                    if (!ValidaCant(sComp, iCanti))
                    {
                        tabControl1.SelectedIndex = 1;
                        dgwPre.Rows[row.Index].Cells[4].Selected = true;
                        return false;
                    }
                }
            }

            //PRE02
            foreach (DataGridViewRow row in dgwPre2.Rows)
            {
                if (!string.IsNullOrEmpty(dgwPre2[2, row.Index].Value.ToString()))
                {
                    string sComp = dgwPre2[2, row.Index].Value.ToString();
                    decimal iCant = 0;
                    if (!decimal.TryParse(dgwPre2[4, row.Index].Value.ToString(), out iCant))
                        iCant = 0;

                    if (iCant == 0)
                    {
                        MessageBox.Show("No se ha capturado la cantidad en el componente " + sComp, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tabControl1.SelectedIndex = 2;
                        return false;
                    }

                    //comparar cantidad en packdraw
                    int iCanti = (int)iCant;
                    if (!ValidaCant(sComp, iCanti))    
                    {
                        tabControl1.SelectedIndex = 2;
                        dgwPre2.Rows[row.Index].Cells[4].Selected = true;
                        return false;
                    }
                }
            }

            //PRE03
            foreach (DataGridViewRow row in dgwPre3.Rows)
            {
                if (!string.IsNullOrEmpty(dgwPre3[2, row.Index].Value.ToString()))
                {
                    string sComp = dgwPre3[2, row.Index].Value.ToString();
                    decimal iCant = 0;
                    if (!decimal.TryParse(dgwPre3[4, row.Index].Value.ToString(), out iCant))
                        iCant = 0;

                    if (iCant == 0)
                    {
                        MessageBox.Show("No se ha capturado la cantidad en el componente " + sComp, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tabControl1.SelectedIndex = 1;
                        return false;
                    }

                    //comparar cantidad en packdraw
                    int iCanti = (int)iCant;
                    if (!ValidaCant(sComp, iCanti))
                    {
                        tabControl1.SelectedIndex = 3;
                        dgwPre3.Rows[row.Index].Cells[4].Selected = true;
                        return false;
                    }
                }
            }
            //PRE04
            foreach (DataGridViewRow row in dgwPre4.Rows)
            {
                if (!string.IsNullOrEmpty(dgwPre4[2, row.Index].Value.ToString()))
                {
                    string sComp = dgwPre4[2, row.Index].Value.ToString();
                    decimal iCant = 0;
                    if (!decimal.TryParse(dgwPre4[4, row.Index].Value.ToString(), out iCant))
                        iCant = 0;

                    if (iCant == 0)
                    {
                        MessageBox.Show("No se ha capturado la cantidad en el componente " + sComp, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tabControl1.SelectedIndex = 1;
                        return false;
                    }

                    //comparar cantidad en packdraw
                    int iCanti = (int)iCant;
                    if (!ValidaCant(sComp, iCanti))
                    {
                        tabControl1.SelectedIndex = 4;
                        dgwPre4.Rows[row.Index].Cells[4].Selected = true;
                        return false;
                    }
                }
            }
            //PRE05
            foreach (DataGridViewRow row in dgwPre5.Rows)
            {
                if (!string.IsNullOrEmpty(dgwPre5[2, row.Index].Value.ToString()))
                {
                    string sComp = dgwPre5[2, row.Index].Value.ToString();
                    decimal iCant = 0;
                    if (!decimal.TryParse(dgwPre5[4, row.Index].Value.ToString(), out iCant))
                        iCant = 0;

                    if (iCant == 0)
                    {
                        MessageBox.Show("No se ha capturado la cantidad en el componente " + sComp, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tabControl1.SelectedIndex = 4;
                        return false;
                    }

                    //comparar cantidad en packdraw
                    int iCanti = (int)iCant;
                    if (!ValidaCant(sComp, iCanti))
                    {
                        tabControl1.SelectedIndex = 5;
                        dgwPre5.Rows[row.Index].Cells[4].Selected = true;
                        return false;
                    }
                }
            }


            return bValida;
        }

        #endregion

        #region MyRegion

        #endregion
        private void btAdd_Click(object sender, EventArgs e)
        {
            if (cbComp.SelectedIndex == -1)
                return;

            string sCodigo = cbComp.SelectedValue.ToString();

            string[] array = new string[4];
            array = Components(sCodigo);

            //LastRow(tabControl1.SelectedIndex);

            int iTab = tabControl1.SelectedIndex;
            if (iTab == 0)
            {
                //mesa
                if (dgwTable.SelectedCells.Count == 0)
                    return;
                  
                int iIdx = 0;
                if (!int.TryParse(dgwTable.SelectedCells[0].RowIndex.ToString(), out iIdx))
                    iIdx = -1;
                 
                if (iIdx == -1)
                    return;

                if(!string.IsNullOrEmpty(dgwTable[2, iIdx].Value.ToString()))
                {
                    string sValue = dgwTable[2, iIdx].Value.ToString();
                    DialogResult result = MessageBox.Show("Desea reemplazar el componente " + sValue + "?",Text,MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
                    if(result != DialogResult.Yes)
                        return;
                }

                if(sCodigo.IndexOf("PRE-") == -1)
                {
                    dgwTable[2, iIdx].Value = array[0].ToString();
                    dgwTable[3, iIdx].Value = array[1].ToString();
                    dgwTable[4, iIdx].Value = array[2].ToString();
                    dgwTable[5, iIdx].Value = array[3].ToString();

                    int iT = int.Parse(tssTotal.Text.ToString());
                    iT++;
                    tssTotal.Text = iT.ToString();

                }
                else
                {
                    foreach (DataGridViewRow row in dgwTable.Rows)
                    {
                        string sPcode = dgwTable[2, row.Index].Value.ToString();
                        if (sCodigo == sPcode)
                            return;
                    }

                    dgwTable[2, iIdx].Value = sCodigo;
                    dgwTable[3, iIdx].Value = sCodigo;
                    dgwTable[4, iIdx].Value = "1";
                    dgwTable[5, iIdx].Value = "EA";

                    //load data from pre
                    ItemSugdetLogica item = new ItemSugdetLogica();
                    item.Item = _lsItem;
                    item.Codigo = sCodigo;
                    iTab = int.Parse(sCodigo.Substring(sCodigo.Length - 1, 1));
                    switch (iTab)
                    {
                        case 1:
                            dgwPre.DataSource = ItemSugdetLogica.ConsultarVistaPS(item);
                            break;
                        case 2:
                            dgwPre2.DataSource = ItemSugdetLogica.ConsultarVistaPS(item);
                            break;
                        case 3:
                            dgwPre3.DataSource = ItemSugdetLogica.ConsultarVistaPS(item);
                            break;
                        case 4:
                            dgwPre4.DataSource = ItemSugdetLogica.ConsultarVistaPS(item);
                            break;
                        case 5:
                            dgwPre5.DataSource = ItemSugdetLogica.ConsultarVistaPS(item);
                            break;
                    }
                    CargarColumnasSP();
                }
            }
            else
            {
                bool bExiste = false;
                string sPS = "PRE-0" + iTab.ToString();
                foreach(DataGridViewRow row in dgwTable.Rows)
                {
                    string sPcode = dgwTable[2,row.Index].Value.ToString();
                    if (sPS == sPcode)
                        bExiste = true;
                }
                if(!bExiste)
                {
                    MessageBox.Show("No se ha cargado el Pre-Ensamble ("+sPS+") en la mesa", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //not sub-PRE allowed
                if (sCodigo.IndexOf("PRE-") != -1)
                    return;

                if (iTab == 1)
                {
                    int iRow = dgwPre.Rows.Count;
                    iRow++;
                    DataTable dt = dgwPre.DataSource as DataTable;
                    dt.Rows.Add(sPS, iRow, sCodigo, array[1].ToString(), array[2].ToString(), array[3].ToString(),iRow);
                        
                }
                if (iTab == 2)
                {
                    int iRow = dgwPre2.Rows.Count;
                    iRow++;
                    DataTable dt = dgwPre2.DataSource as DataTable;
                    dt.Rows.Add(sPS, iRow, sCodigo, array[1].ToString(), array[2].ToString(), array[3].ToString(),iRow);
                }
                if (iTab == 3)
                {
                    int iRow = dgwPre3.Rows.Count;
                    iRow++;
                    DataTable dt = dgwPre3.DataSource as DataTable;
                    dt.Rows.Add(sPS, iRow, sCodigo, array[1].ToString(), array[2].ToString(), array[3].ToString(),iRow);
                }
                if (iTab == 4)
                {
                    int iRow = dgwPre4.Rows.Count;
                    iRow++;
                    DataTable dt = dgwPre4.DataSource as DataTable;
                    dt.Rows.Add(sPS, iRow, sCodigo, array[1].ToString(), array[2].ToString(), array[3].ToString(),iRow);
                }
                if (iTab == 5)
                {
                    int iRow = dgwPre5.Rows.Count;
                    iRow++;
                    DataTable dt = dgwPre5.DataSource as DataTable;
                    dt.Rows.Add(sPS, iRow, sCodigo, array[1].ToString(), array[2].ToString(), array[3].ToString(),iRow);
                }

                int iT = int.Parse(tssTotal.Text.ToString());
                iT++;
                tssTotal.Text = iT.ToString();
            }
            LastRow(tabControl1.SelectedIndex);
        }
        private string[] Components(string _sCode)
        {
            string[] array = new string[4];
            for (int x = 0; x < _dt.Rows.Count; x++)
            {
                string sCodigo = _dt.Rows[x][1].ToString();
                if(sCodigo == _sCode)
                {
                    array[0] = _dt.Rows[x][1].ToString();//code
                    array[1] = _dt.Rows[x][2].ToString();//descrip
                    array[2] = _dt.Rows[x][3].ToString();//qty
                    array[3] = _dt.Rows[x][4].ToString();//um
                }
            }

            return array;
        }
        
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iT = tabControl1.SelectedIndex;
            if(iT > 0)
            {
                if (dgwTable.Rows.Count == 0)
                    tabControl1.SelectedIndex = 0;


                bool bExiste = false;
                string sPS = "PRE-0" + iT.ToString();
                foreach (DataGridViewRow row in dgwTable.Rows)
                {
                    if (!string.IsNullOrEmpty(dgwTable[2, row.Index].Value.ToString()))
                    {
                        string sCodigo = dgwTable[2, row.Index].Value.ToString();
                        if(sCodigo == sPS)
                        {
                            bExiste = true;
                            break;
                        }
                    }
                }

                if (!bExiste)
                {
                    MessageBox.Show("No se han capturado Pre-Ensambles en la Mesa", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabControl1.SelectedIndex = 0;
                    return;
                }
                
            }
        }

        #region gridControl

        private void LastRow(int iTab)
        {
            if (iTab == 0)
            {
                foreach (DataGridViewRow row in dgwTable.Rows)
                {
                    if (string.IsNullOrEmpty(dgwTable[2, row.Index].Value.ToString()))
                    {
                        row.Selected = true;
                        break;
                    }
                    dgwTable.ClearSelection();
                }
            }
            else
            {
                foreach (DataGridViewRow row in dgwPre.Rows)
                {
                    if (string.IsNullOrEmpty(dgwPre[1, row.Index].Value.ToString()))
                        row.Selected = true;
                }
            }
        }
        private void dgwTable_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dgwTable.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void dgwPre_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dgwPre.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void dgwPre2_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dgwPre2.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void dgwTable_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int iRow = e.RowIndex;
            string sValue = e.Value.ToString();

            sValue = dgwTable[2, e.RowIndex].Value.ToString();
            if (sValue.IndexOf("PRE-") == -1)
                return;

            int iTab = int.Parse(sValue.Substring(sValue.Length - 1, 1));
            e.CellStyle.BackColor = tabControl1.TabPages[iTab].BackColor;
            
        }

        private void wfTableComp_Resize(object sender, EventArgs e)
        {
            panel1.Height = this.Height - 120;
            tabControl1.Height = this.Height - 200;
            dgwTable.Height = this.Height - 250;
            dgwPre.Height = this.Height - 250;
            dgwPre2.Height = this.Height - 250;
            dgwPre3.Height = this.Height - 250;
            dgwPre4.Height = this.Height - 250;
            dgwPre5.Height = this.Height - 250;
        }

        #endregion

        #region regRemove
        private void btnRemove_Click(object sender, EventArgs e)
        {
            int iTab = tabControl1.SelectedIndex;
            if (iTab == 0)
            {
                //mesa
                if (dgwTable.SelectedCells.Count == 0)
                    return;

                int iIdx = -1;
                if (!int.TryParse(dgwTable.SelectedCells[0].RowIndex.ToString(), out iIdx))
                    return;

                if (iIdx == -1)
                    return;

                string sCodigo = dgwTable[2, iIdx].Value.ToString();
                if (string.IsNullOrEmpty(sCodigo))
                    return;

                int iConsec = int.Parse(dgwTable[6, iIdx].Value.ToString());

                ItemSugdetLogica item = new ItemSugdetLogica();
                item.Item = _lsItem;
                item.Mesa = _lsMesa;
                item.Consec = iConsec;

                DialogResult Result = MessageBox.Show("Desea eliminar el componente " + sCodigo + "?",Text,MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
                if(Result == DialogResult.Yes)
                {
                    if (sCodigo.IndexOf("PRE-") != -1)
                    {
                        int iRowS = 0;
                        iTab = int.Parse(sCodigo.Substring(sCodigo.Length - 1, 1));
                        switch (iTab)
                        {
                            case 1:
                                iRowS = dgwPre.Rows.Count;
                                break;
                            case 2:
                                iRowS = dgwPre2.Rows.Count;
                                break;
                            case 3:
                                iRowS = dgwPre3.Rows.Count;
                                break;
                            case 4:
                                iRowS = dgwPre4.Rows.Count;
                                break;
                            case 5:
                                iRowS = dgwPre5.Rows.Count;
                                break;
                        }
                        if(iRowS > 0)
                        {
                            Result = MessageBox.Show("Desea eliminar los componentes asignados al pre-ensamble " + sCodigo + " ?", Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (Result == DialogResult.Yes)
                            {
                                item.Mesa = sCodigo;
                                ItemSugdetLogica.EliminarSP(item);
                            }
                            else
                            {
                                if (Result == DialogResult.Cancel)
                                    return;
                            }

                            iTab = int.Parse(sCodigo.Substring(sCodigo.Length - 1, 1));
                            switch (iTab)
                            {
                                case 1:
                                    dgwPre.DataSource = null;
                                    break;
                                case 2:
                                    dgwPre2.DataSource = null;
                                    break;
                                case 3:
                                    dgwPre3.DataSource = null;
                                    break;
                                case 4:
                                    dgwPre4.DataSource = null;
                                    break;
                                case 5:
                                    dgwPre5.DataSource = null;
                                    break;
                            }
                        }
                    }

                    item.Mesa = _lsMesa;
                    ItemSugdetLogica.Eliminar(item);
                    
                    dgwTable[2, iIdx].Value = string.Empty;
                    dgwTable[3, iIdx].Value = string.Empty;
                    dgwTable[4, iIdx].Value = 0;
                    dgwTable[5, iIdx].Value = string.Empty;

                    CargarColumnas();
                    CargarColumnasSP();
                }
            }
            else
            {
                if (iTab == 1)
                {
                    //mesa
                    if (dgwPre.SelectedCells.Count == 0)
                        return;

                    int iIdx = -1;
                    if (!int.TryParse(dgwPre.SelectedCells[0].RowIndex.ToString(), out iIdx))
                        return;

                    if (iIdx == -1)
                        return;

                    string sCodigo = dgwPre[2, iIdx].Value.ToString();
                    if (string.IsNullOrEmpty(sCodigo))
                        return;

                    string sMesa = dgwPre[0, iIdx].Value.ToString();
                    int iConsec = int.Parse(dgwPre[6, iIdx].Value.ToString());
                    DialogResult Result = MessageBox.Show("Desea eliminar el componente " + sCodigo + "?", Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (Result == DialogResult.Yes)
                    {
                        ItemSugdetLogica item = new ItemSugdetLogica();
                        item.Item = _lsItem;
                        item.Mesa = sMesa;
                        item.Consec = iConsec;
                        ItemSugdetLogica.Eliminar(item);
                        dgwPre.Rows.RemoveAt(iIdx);
                    }
                }
                if (iTab == 2)
                {
                    //mesa
                    if (dgwPre2.SelectedCells.Count == 0)
                        return;

                    int iIdx = -1;
                    if (!int.TryParse(dgwPre2.SelectedCells[0].RowIndex.ToString(), out iIdx))
                        return;

                    if (iIdx == -1)
                        return;

                    string sCodigo = dgwPre2[2, iIdx].Value.ToString();
                    if (string.IsNullOrEmpty(sCodigo))
                        return;

                    string sMesa = dgwPre2[0, iIdx].Value.ToString();
                    int iConsec = int.Parse(dgwPre2[6, iIdx].Value.ToString());
                    DialogResult Result = MessageBox.Show("Desea eliminar el componente " + sCodigo + "?", Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (Result == DialogResult.Yes)
                    {
                        ItemSugdetLogica item = new ItemSugdetLogica();
                        item.Item = _lsItem;
                        item.Mesa = sMesa;
                        item.Consec = iConsec;
                        ItemSugdetLogica.Eliminar(item);
                        dgwPre2.Rows.RemoveAt(iIdx);

                    }
                }
                if (iTab == 3)
                {
                    //mesa
                    if (dgwPre3.SelectedCells.Count == 0)
                        return;

                    int iIdx = -1;
                    if (!int.TryParse(dgwPre3.SelectedCells[0].RowIndex.ToString(), out iIdx))
                        return;

                    if (iIdx == -1)
                        return;

                    string sCodigo = dgwPre3[2, iIdx].Value.ToString();
                    if (string.IsNullOrEmpty(sCodigo))
                        return;

                    string sMesa = dgwPre3[0, iIdx].Value.ToString();
                    int iConsec = int.Parse(dgwPre3[6, iIdx].Value.ToString());
                    DialogResult Result = MessageBox.Show("Desea eliminar el componente " + sCodigo + "?", Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (Result == DialogResult.Yes)
                    {
                        ItemSugdetLogica item = new ItemSugdetLogica();
                        item.Item = _lsItem;
                        item.Mesa = sMesa;
                        item.Consec = iConsec;
                        ItemSugdetLogica.Eliminar(item);
                        dgwPre3.Rows.RemoveAt(iIdx);

                    }
                }
                if (iTab == 4)
                {
                    //mesa
                    if (dgwPre4.SelectedCells.Count == 0)
                        return;

                    int iIdx = -1;
                    if (!int.TryParse(dgwPre4.SelectedCells[0].RowIndex.ToString(), out iIdx))
                        return;

                    if (iIdx == -1)
                        return;

                    string sCodigo = dgwPre4[2, iIdx].Value.ToString();
                    if (string.IsNullOrEmpty(sCodigo))
                        return;

                    string sMesa = dgwPre4[0, iIdx].Value.ToString();
                    int iConsec = int.Parse(dgwPre4[6, iIdx].Value.ToString());
                    DialogResult Result = MessageBox.Show("Desea eliminar el componente " + sCodigo + "?", Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (Result == DialogResult.Yes)
                    {
                        ItemSugdetLogica item = new ItemSugdetLogica();
                        item.Item = _lsItem;
                        item.Mesa = sMesa;
                        item.Consec = iConsec;
                        ItemSugdetLogica.Eliminar(item);
                        dgwPre4.Rows.RemoveAt(iIdx);

                    }
                }
                if (iTab == 5)
                {
                    //mesa
                    if (dgwPre5.SelectedCells.Count == 0)
                        return;

                    int iIdx = -1;
                    if (!int.TryParse(dgwPre5.SelectedCells[0].RowIndex.ToString(), out iIdx))
                        return;

                    if (iIdx == -1)
                        return;

                    string sCodigo = dgwPre5[2, iIdx].Value.ToString();
                    if (string.IsNullOrEmpty(sCodigo))
                        return;

                    int iConsec = int.Parse(dgwPre5[6, iIdx].Value.ToString());
                    string sMesa = dgwPre5[0, iIdx].Value.ToString();
                    DialogResult Result = MessageBox.Show("Desea eliminar el componente " + sCodigo + "?", Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (Result == DialogResult.Yes)
                    {
                        ItemSugdetLogica item = new ItemSugdetLogica();
                        item.Item = _lsItem;
                        item.Mesa = sMesa;
                        item.Consec = iConsec;
                        ItemSugdetLogica.Eliminar(item);
                        dgwPre5.Rows.RemoveAt(iIdx);

                    }
                }

                CargarColumnasSP();
            }
        }
        #endregion
    }
}
