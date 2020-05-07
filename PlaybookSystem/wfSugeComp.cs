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
    public partial class wfSugeComp : Form
    {
        public string _lsProceso;
        public string _lsItem;
        public string _lsDesc;
        Globals _gs = new Globals();
        public wfSugeComp()
        {
            InitializeComponent();
        }

        private void wfSugeComp_Load(object sender, EventArgs e)
        {
            _lsProceso = "PRO050";
            lblItem.Text = _lsItem + " " + _lsDesc;
            tssJob.Text = _lsItem;
            tssTotal.Text = "0";
            
            Inicio();
            
        }
        private void Inicio()
        {
            try
            {
                
                dgwTable.DataSource = null;

                dgwPre.DataSource = null;
                dgwPre2.DataSource = null;
                dgwPre3.DataSource = null;
                dgwPre4.DataSource = null;
                dgwPre5.DataSource = null;

                ItemSugdetLogica item = new ItemSugdetLogica();
                item.Item = _lsItem;
                int iT = 0;
                DataTable dt = ItemSugdetLogica.ConsultarVistaSug(item);
                dgwTable.DataSource = dt;
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    string sCodigo = dt.Rows[x][2].ToString();
                    if (!string.IsNullOrEmpty(sCodigo))
                        iT++;
                }
                
                DataTable dtS = ItemSugdetLogica.ListarSugePS(item);
                iT = iT - dtS.Rows.Count;
                int iTab = 0;
                
                for (int x = 0; x < dtS.Rows.Count; x++)
                {
                    string sCodigo = dtS.Rows[x][0].ToString();
                    
                    item.Mesa = sCodigo;
                    iTab = int.Parse(sCodigo.Substring(sCodigo.Length - 1, 1));
                    switch(iTab)
                    {
                        case 1:
                            dgwPre.DataSource = ItemSugdetLogica.ConsultarVistaSugPS(item);
                            break;
                        case 2:
                            dgwPre2.DataSource = ItemSugdetLogica.ConsultarVistaSugPS(item);
                            break;
                        case 3:
                            dgwPre3.DataSource = ItemSugdetLogica.ConsultarVistaSugPS(item);
                            break;
                        case 4:
                            dgwPre4.DataSource = ItemSugdetLogica.ConsultarVistaSugPS(item);
                            break;
                        case 5:
                            dgwPre5.DataSource = ItemSugdetLogica.ConsultarVistaSugPS(item);
                            break;
                    }
                }

                iT += dgwPre.Rows.Count;
                iT += dgwPre2.Rows.Count;
                iT += dgwPre3.Rows.Count;
                iT += dgwPre4.Rows.Count;
                iT += dgwPre5.Rows.Count;

                CargarColumnas();
                
                tabControl1.SelectedIndex = 0;
                dgwTable.ClearSelection();

                tssTotal.Text = iT.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    
        private void wfSugeComp_Activated(object sender, EventArgs e)
        {
            tabControl1.Focus();
            
        }
        
        private void CargarColumnas()
        {
            int iRows = dgwTable.Rows.Count;
            if (iRows == 0)
            {
                DataTable dtNew = new DataTable("Comp");
                dtNew.Columns.Add("MESA", typeof(string));
                dtNew.Columns.Add("#", typeof(decimal));
                dtNew.Columns.Add("CODIGO", typeof(string));
                dtNew.Columns.Add("DESCRIPCION", typeof(string));
                dtNew.Columns.Add("CANT", typeof(decimal));
                dtNew.Columns.Add("UM", typeof(string));

                dgwTable.DataSource = dtNew;
            }

            dgwTable.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgwTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dgwTable.Columns[0].Width = ColumnWith(dgwTable, 10);

            dgwTable.Columns[1].Width = ColumnWith(dgwTable, 5);
            dgwTable.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwTable.Columns[1].ReadOnly = true;

            dgwTable.Columns[2].Width = ColumnWith(dgwTable, 20);
            dgwTable.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwTable.Columns[2].ReadOnly = true;

            dgwTable.Columns[3].Width = ColumnWith(dgwTable, 45);
            dgwTable.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwTable.Columns[3].ReadOnly = true;

            dgwTable.Columns[4].Width = ColumnWith(dgwTable, 10);//qty
            dgwTable.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwTable.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgwTable.Columns[4].DefaultCellStyle.Format = "0";
            dgwTable.Columns[4].ReadOnly = true;

            dgwTable.Columns[5].Width = ColumnWith(dgwTable, 10);//um
            dgwTable.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwTable.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwTable.Columns[5].ReadOnly = true;

            
            #region regPS
            iRows = dgwPre.Rows.Count;
            if (iRows == 0)
            {
                DataTable dtNew2 = new DataTable("pre");
                dtNew2.Columns.Add("PS", typeof(string));
                dtNew2.Columns.Add("#", typeof(decimal));
                dtNew2.Columns.Add("CODIGO", typeof(string));
                dtNew2.Columns.Add("DESCRIPCION", typeof(string));
                dtNew2.Columns.Add("CANT", typeof(decimal));
                dtNew2.Columns.Add("UM", typeof(string));

                
                dgwPre.DataSource = dtNew2;
            }
            dgwPre.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgwPre.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dgwPre.Columns[0].Visible = false;

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


                dgwPre2.DataSource = dtNew2;
            }
            dgwPre2.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgwPre2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dgwPre2.Columns[0].Visible = false;

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
                
                dgwPre3.DataSource = dtNew2;
            }
            dgwPre3.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgwPre3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dgwPre3.Columns[0].Visible = false;

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
                
                dgwPre4.DataSource = dtNew2;
            }
            dgwPre4.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgwPre4.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dgwPre4.Columns[0].Visible = false;

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


                dgwPre5.DataSource = dtNew2;
            }
            dgwPre5.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgwPre5.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dgwPre5.Columns[0].Visible = false;

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
            #endregion
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

                    iComp++;
                    
                    if(sComp.IndexOf("PS-") != -1)
                    {
                        bool bExiste = false;
                        if(sComp == "PS-01")
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
                        if (sComp == "PS-02")
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

                        if (sComp == "PS-03")
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
                        if (sComp == "PS-04")
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

                        if (sComp == "PS-05")
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
                            MessageBox.Show("No se ha regsitrado los componentes del Sub-Ensamble " + sComp, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                }
            }
            return bValida;
        }

      
        
        private void dgwTable_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dgwTable.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

      
        private void dgwTable_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int iRow = e.RowIndex;
            string sValue = e.Value.ToString();

            sValue = dgwTable[0, e.RowIndex].Value.ToString();
            switch (sValue)
            {
                case "1":
                    e.CellStyle.BackColor = Color.DodgerBlue;
                    break;
                case "2":
                    e.CellStyle.BackColor = Color.Yellow;
                    break;
                case "3":
                    e.CellStyle.BackColor = Color.LightGreen;
                    break;
                case "4":
                    e.CellStyle.BackColor = Color.Orange;
                    break;
                case "5":
                    e.CellStyle.BackColor = Color.LightGray;
                    break;
                case "6":
                    e.CellStyle.BackColor = Color.LightBlue;
                    break;
                case "7":
                    e.CellStyle.BackColor = Color.MediumPurple;
                    break;
            }

            sValue = dgwTable[2, e.RowIndex].Value.ToString();
            if (sValue.IndexOf("PRE-") != -1)
            {
                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Italic);
                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                //e.CellStyle.BackColor = Color.WhiteSmoke;
            }
        }

        private void wfSugeComp_Resize(object sender, EventArgs e)
        {
            panel1.Height = this.Height - 90;
            tabControl1.Height = this.Height - 150;
            dgwTable.Height = this.Height - 200;
            dgwPre.Height = this.Height - 200;
            dgwPre2.Height = this.Height - 200;
            dgwPre3.Height = this.Height - 200;
            dgwPre4.Height = this.Height - 200;
            dgwPre5.Height = this.Height - 200;
        }
        
    }
}
