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
    public partial class wfTableSetup : Form
    {
        private bool _lbCambio;
        private string _lsLayout;
        public string _lsOrden;
        public string _lsProceso;
        FormWindowState _WindowStateAnt;
        private int _iWidthAnt;
        private int _iHeightAnt;

        Globals _gs = new Globals();

        public wfTableSetup()
        {
            InitializeComponent();
            _iWidthAnt = Width;
            _iHeightAnt = Height;
            _WindowStateAnt = WindowState;
        }
        
        #region regInicio

        private void wfTableSetup_Load(object sender, EventArgs e)
        {
            Globals._gsCompany = "686";

            Inicio();
            tssHrVersion.Text = "1.0.0.1";
            
        }
        private void wfTableSetup_Activated(object sender, EventArgs e)
        {
            txtItem.Focus();
        }
        private void Inicio()
        {
            //load data
            try
            {
                _lbCambio = false;
                _lsProceso = "PRO040";
                _lsLayout = null;
                txtItem.Clear();
                lblDescrip.Text = "";
                txtLayout.Clear();
                dgwComp.DataSource = null;
                CargarColumnas();

                dgwSuge.DataSource = null;
                CargarColumnasSug();

                btTable1.FlatAppearance.BorderColor = Color.Cyan;
                btTable1.ForeColor = Color.DarkTurquoise;
                btTable2.FlatAppearance.BorderColor = Color.Cyan;
                btTable2.ForeColor = Color.DarkTurquoise;
                btTable3.FlatAppearance.BorderColor = Color.Cyan;
                btTable3.ForeColor = Color.DarkTurquoise;
                btTable4.FlatAppearance.BorderColor = Color.Cyan;
                btTable4.ForeColor = Color.DarkTurquoise;
                btTable5.FlatAppearance.BorderColor = Color.Cyan;
                btTable5.ForeColor = Color.DarkTurquoise;
                btTable6.FlatAppearance.BorderColor = Color.Cyan;
                btTable6.ForeColor = Color.DarkTurquoise;
                btTable7.FlatAppearance.BorderColor = Color.Cyan;
                btTable7.ForeColor = Color.DarkTurquoise;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void CargarColumnas()
        {
            int iRows = dgwComp.Rows.Count;
            if (iRows == 0)
            {
                DataTable dtNew = new DataTable("Comp");
                dtNew.Columns.Add("NIVEL", typeof(string));
                dtNew.Columns.Add("NO. PARTE", typeof(string));
                dtNew.Columns.Add("DESCRIPCION", typeof(string));
                dtNew.Columns.Add("CANT", typeof(decimal));
                dtNew.Columns.Add("UM", typeof(string));
                dtNew.Columns.Add("REV", typeof(string));
                dgwComp.DataSource = dtNew;
            }

            dgwComp.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgwComp.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dgwComp.Columns[5].Visible = false;

            dgwComp.Columns[0].Width = ColumnWith(dgwComp, 10);
            dgwComp.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwComp.Columns[0].ReadOnly = true;

            dgwComp.Columns[1].Width = ColumnWith(dgwComp, 20);
            dgwComp.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwComp.Columns[1].ReadOnly = true;

            dgwComp.Columns[2].Width = ColumnWith(dgwComp, 50);
            dgwComp.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwComp.Columns[2].ReadOnly = true;

            dgwComp.Columns[3].Width = ColumnWith(dgwComp, 10);//qty
            dgwComp.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwComp.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgwComp.Columns[3].DefaultCellStyle.Format = "0";
            dgwComp.Columns[3].ReadOnly = true;

            dgwComp.Columns[4].Width = ColumnWith(dgwComp, 10);//um
            dgwComp.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwComp.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwComp.Columns[4].ReadOnly = true;

            foreach(DataGridViewRow row in dgwComp.Rows)
            {
                bool bExist = false;
                string sValue = dgwComp[1, row.Index].Value.ToString();
                foreach (DataGridViewRow row2 in dgwSuge.Rows)
                {
                    string sComp = dgwSuge[3, row2.Index].Value.ToString();
                    if (sComp == sValue)
                    {
                        bExist = true;
                        break;
                    }
                }
                if(bExist)
                    row.DefaultCellStyle.BackColor = Color.Orange;
                else
                    row.DefaultCellStyle.BackColor = Color.White;
            }
            
        }
        private void CargarColumnasSug()
        {
            int iRows = dgwSuge.Rows.Count;
            if (iRows == 0)
            {
                DataTable dtNew = new DataTable("Suge");
                dtNew.Columns.Add("item", typeof(string));
                dtNew.Columns.Add("MESA", typeof(string));
                dtNew.Columns.Add("#", typeof(decimal));
                dtNew.Columns.Add("NO. PARTE", typeof(string));
                dtNew.Columns.Add("DESCRIPCION", typeof(string));
                dtNew.Columns.Add("CANT", typeof(decimal));
                dtNew.Columns.Add("UM", typeof(string));
                dgwSuge.DataSource = dtNew;
            }

            dgwSuge.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgwSuge.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dgwSuge.Columns[0].Visible = false;

            dgwSuge.Columns[1].Width = ColumnWith(dgwSuge, 10);
            dgwSuge.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwSuge.Columns[1].ReadOnly = true;

            dgwSuge.Columns[2].Width = ColumnWith(dgwSuge, 5);
            dgwSuge.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwSuge.Columns[2].ReadOnly = true;

            dgwSuge.Columns[3].Width = ColumnWith(dgwSuge, 20);
            dgwSuge.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwSuge.Columns[3].ReadOnly = true;
        
            dgwSuge.Columns[4].Width = ColumnWith(dgwSuge, 50);
            dgwSuge.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwSuge.Columns[4].ReadOnly = true;

            dgwSuge.Columns[5].Width = ColumnWith(dgwSuge, 6);//qty
            dgwSuge.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwSuge.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgwSuge.Columns[5].DefaultCellStyle.Format = "0";
            dgwSuge.Columns[5].ReadOnly = true;

            dgwSuge.Columns[6].Width = ColumnWith(dgwSuge, 8);//um
            dgwSuge.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwSuge.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwSuge.Columns[6].ReadOnly = true;


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

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                    Close();


                if (e.KeyCode != Keys.Enter)
                    return;

                if (string.IsNullOrEmpty(txtItem.Text))
                    return;

                string sValue = txtItem.Text.ToString().ToUpper().Trim();

                Inicio();
                txtItem.Text = sValue;

                AS4Logica AS4 = new AS4Logica();
                AS4.CN = Globals._gsCompany;
                AS4.Item = sValue;

                ItemSugLogica item = new ItemSugLogica();
                item.Item = sValue;
                DataTable data = ItemSugLogica.ConsultarItem(item);
                if(data.Rows.Count > 0)
                {
                    lblDescrip.Text = data.Rows[0][1].ToString();
                    txtLayout.Text = data.Rows[0][2].ToString();

                    //sugerencia detalle
                    ItemSugdetLogica itemd = new ItemSugdetLogica();
                    itemd.Item = txtItem.Text.ToString().Trim();
                    DataTable dtS = ItemSugdetLogica.ConsultarVista(itemd);
                    dgwSuge.DataSource = dtS;
                    CargarColumnasSug();

                    if(dtS.Rows.Count > 0)
                    {
                        for(int x = 1; x <= 7; x++)
                        {
                            itemd.Mesa = x.ToString();
                            DataTable dt = ItemSugdetLogica.ConsultarVistaMesa(itemd);
                            if (dt.Rows.Count > 0)
                            {
                                switch (x.ToString())
                                {
                                    case "1":
                                        btTable1.FlatAppearance.BorderColor = Color.Orange;
                                        btTable1.ForeColor = Color.DarkOrange;
                                        break;
                                    case "2":
                                        btTable2.FlatAppearance.BorderColor = Color.Orange;
                                        btTable2.ForeColor = Color.DarkOrange;
                                        break;
                                    case "3":
                                        btTable3.FlatAppearance.BorderColor = Color.Orange;
                                        btTable3.ForeColor = Color.DarkOrange;
                                        break;
                                    case "4":
                                        btTable4.FlatAppearance.BorderColor = Color.Orange;
                                        btTable4.ForeColor = Color.DarkOrange;
                                        break;
                                    case "5":
                                        btTable5.FlatAppearance.BorderColor = Color.Orange;
                                        btTable5.ForeColor = Color.DarkOrange;
                                        break;
                                    case "6":
                                        btTable6.FlatAppearance.BorderColor = Color.Orange;
                                        btTable6.ForeColor = Color.DarkOrange;
                                        break;
                                    case "7":
                                        btTable7.FlatAppearance.BorderColor = Color.Orange;
                                        btTable7.ForeColor = Color.DarkOrange;
                                        break;
                                }
                            }
                        }
                    }
                }
                else
                {   
                    data = AS4Logica.PartKits(AS4);
                    if (data.Rows.Count == 0)
                    {
                        MessageBox.Show("El Job no se encuentra registrado en PackDraw", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Inicio();
                        return;
                    }
                    lblDescrip.Text = data.Rows[0][1].ToString();
                }
                
                DataTable dtAS = AS4Logica.ComponentsTable(AS4);
                if (dtAS.Rows.Count == 0)
                {
                    MessageBox.Show("El Job no se encuentra registrado en PackDraw", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Inicio();
                    return;
                }

                dgwComp.DataSource = dtAS;
                CargarColumnas();
                
                tssDYN.Text = txtItem.Text.ToString();
                tssTotal.Text = dtAS.Rows.Count.ToString();
                lblLayout2.Text = dtAS.Rows[0]["PACKDRAW_REV"].ToString();
                if(string.IsNullOrEmpty(txtLayout.Text))
                {
                    txtLayout.Text = dtAS.Rows[0]["PACKDRAW_REV"].ToString();
                    Guardar();
                }
                _lsLayout = txtLayout.Text.ToString();

                txtItem.SelectAll();

                dgwComp.ClearSelection();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #region regSave
       
      
        private bool Guardar()
        {
            bool bReturn = true;
            try
            {
                ItemSugLogica item = new ItemSugLogica();
                item.Item = txtItem.Text.ToString();
                if(_lbCambio)
                {
                    item.Descrip = lblDescrip.Text.ToString().Trim();
                    item.Layout = int.Parse(txtLayout.Text.ToString());
                    item.Usuario = Globals._gsUser;

                    ItemSugLogica.GuardarSP(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                bReturn = false;
            }

            return bReturn;
        }
        #endregion

        #region regGrids
        private void dgwLine_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //int iRow = e.RowIndex;
            //string sVal = dgwComp[9, e.RowIndex].Value.ToString();
            //if ((iRow % 2) == 0)
            //    e.CellStyle.BackColor = Color.LightSkyBlue;
            //else
            //    e.CellStyle.BackColor = Color.White;
               
            //if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
            //    e.CellStyle.ForeColor = Color.DarkSlateGray;

            //if(sVal == "0" && (  e.ColumnIndex == 5))
            //    e.CellStyle.BackColor = Color.Red;

            //if (sVal == "2" )
            //    e.CellStyle.BackColor = Color.LightGreen;

        }
        private void dgwLine_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            /*
            if (e.ColumnIndex == 7)
            {
                string sValor = dgwLine[e.ColumnIndex, e.RowIndex].Value.ToString();
                if (!string.IsNullOrEmpty(sValor) && !string.IsNullOrWhiteSpace(sValor))
                    lblOrden.Text = sValor;
            }

            if (e.ColumnIndex == 4 || e.ColumnIndex == 6)
            {
              
                try
                {
                    string sValor = dgwLine[e.ColumnIndex, e.RowIndex].Value.ToString();
                    if (!string.IsNullOrEmpty(sValor) && !string.IsNullOrWhiteSpace(sValor))
                    {
                        
                        int iCant = 0;
                        if (!int.TryParse(sValor, out iCant))
                        {
                            MessageBox.Show("El valor capturado no es válido", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgwLine[e.ColumnIndex, e.RowIndex].Value = 0;
                            return;
                        }
                        else
                        {
                            if (e.ColumnIndex == 4)
                            {
                                int iActual = iCant;
                                int iActualAc = 0;
                                int iMeta = int.Parse(dgwLine[2, e.RowIndex].Value.ToString());
                                int iMetaAc = int.Parse(dgwLine[3, e.RowIndex].Value.ToString());

                                if (e.RowIndex == 0)
                                    iActualAc = iActual;
                                else
                                    iActualAc = iActual + int.Parse(dgwLine[5, e.RowIndex - 1].Value.ToString());
 
                                CargarActualAcumulado();
                            }

                            //defectos

                            if (e.ColumnIndex == 6)
                            {
                                int iValor = 0;
                                foreach (DataGridViewRow row in dgwLine.Rows)
                                {
                                    if (string.IsNullOrEmpty(dgwLine[6, row.Index].Value.ToString()))
                                        continue;

                                    int iDef = int.Parse(dgwLine[6, row.Index].Value.ToString());
                                    iValor = iValor + iDef;
                                }

                                lblDefectos.Text = iValor.ToString();
                            }
                            //cargar WO anterior
                            if(e.RowIndex > 0)
                            {
                                if (string.IsNullOrEmpty(dgwLine[7, e.RowIndex].Value.ToString()))
                                    dgwLine[7, e.RowIndex].Value = dgwLine[7, e.RowIndex - 1].Value.ToString();
                            }

                        }
                    }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Favor de Notificar al Administrador" + Environment.NewLine + "dgwLine_CellValueChanged(6)" + Environment.NewLine + ex.ToString(), "Error en " + Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            */
        }

        private void CargarActualAcumulado()
        {
            int iActualAc = 0;
            foreach (DataGridViewRow row in dgwComp.Rows)
            {
                if (string.IsNullOrEmpty(dgwComp[4, row.Index].Value.ToString()))
                    continue;

                int iActual = int.Parse(dgwComp[4, row.Index].Value.ToString());
                
                if (row.Index == 0)
                    iActualAc = iActual;
                else
                    iActualAc += iActual;
                
                dgwComp[5, row.Index].Value = iActualAc.ToString();
            }
            
           
        }
        private void dgwLine_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 4)
                return;
            /*
            int iMeta = int.Parse(dgwLine[2, e.RowIndex].Value.ToString());
            int iMetaAc = int.Parse(dgwLine[3, e.RowIndex].Value.ToString());

            if (iMeta == 0)
                return;

            if (string.IsNullOrEmpty(dgwLine[4, e.RowIndex].Value.ToString()))
                return;

            int iActual = int.Parse(dgwLine[4, e.RowIndex].Value.ToString());
            int iActualAc = 0;
            if (e.RowIndex == 0)
                iActualAc = iActual;
            else
                iActualAc = iActual + int.Parse(dgwLine[5, e.RowIndex - 1].Value.ToString());


            //if (iActual > 0 && (iActualAc < iMetaAc))//iActual < iMeta || 
            if (iActualAc < iMetaAc)
            {
                dgwLine[9, e.RowIndex].Value = "0";

                if (iActualAc < iMetaAc)
                {
                    pnlActual.BackColor = Color.Red;
                    lblActual.ForeColor = Color.White;
                }
                else
                {
                    pnlActual.BackColor = Color.Green;
                    lblActual.ForeColor = Color.DarkBlue;
                }
            }
            else
            {
                dgwLine[9, e.RowIndex].Value = "1";
                if (iActualAc >= iMetaAc)
                {
                    pnlActual.BackColor = Color.Green;
                    lblActual.ForeColor = Color.DarkBlue;
                }
                
            }
            */
        }

            #endregion

        #region regBotones
        private void btnNew_Click(object sender, EventArgs e)
        {
            Inicio();
        }

        private void btnNew_Click_1(object sender, EventArgs e)
        {
            Inicio();
        }

        private void btnScaled_Click(object sender, EventArgs e)
        {
            wfLineDown ScaleDown = new wfLineDown();
            ScaleDown.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgwComp.Rows.Count == 0)
                return;
            /*
            int iRow = dgwLine.RowCount - 1;
            string sActual = dgwLine[4, iRow].Value.ToString();
            if (sActual == "0")
            {
                MessageBox.Show("Debes capturar la hora faltante antes de agregar un horario de Tiempo Extra.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgwLine.Rows[iRow].Cells[4].Selected = true;
                return;
            }

            string sWO = dgwLine[7, iRow].Value.ToString();
            int iHora = DateTime.Now.Hour;
            int iRowAnt = iRow;
            int iMetaAc = int.Parse(dgwLine[3, iRow].Value.ToString());
            if (dgwLine[4, iRow].Value.ToString() != "O")
            {
                for(int x  = 0; x < 3; x++)
                {
                    string sHora = dgwLine[1, iRow].Value.ToString();
                    sHora = sHora.Substring(0, 2);
                    int iHr = int.Parse(sHora);
                    int ixRow =  0;
                    if (x == 2)
                        ixRow = iRowAnt;
                    else
                        ixRow = x;

                    int iMeta = int.Parse(dgwLine[2, ixRow].Value.ToString());
                    iMetaAc += iMeta;
                    //if(iHora <= iHr)
                    //{
                    //    MessageBox.Show("Favor de agregar el registro del Tiempo Extra al finalizar el horario del turno regular.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}

                    
                    string sHoraSig = string.Empty;

                    int iHrS1 = iHr + 1;
                    int iHrS2 = iHr + 2;
                    sHoraSig = iHrS1.ToString().PadLeft(2, '0') + ":00 - " + iHrS2.ToString().PadLeft(2, '0') + ":00";

                    //if (iHora <= iHrS1)
                    //{
                    //    MessageBox.Show("No se permite adelantar la captura del Hora por Hora (" + sHoraSig + ")", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    return;
                    //}

                    DataTable dt = dgwLine.DataSource as DataTable;
                    dt.Rows.Add(0, sHoraSig, iMeta, iMetaAc, 0, 0, 0, sWO, "T. Extra", "2");
                    iRow++;
                }

                lblMeta.Text = iMetaAc.ToString();
                dgwLine.CurrentCell = dgwLine[4, iRow];
                dgwLine.Focus();
            }
            */
        }

       
            #endregion

      

        private void dgwLine_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dgwComp.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }


        private void OpenTable(string _asMesa)
        {
            if (string.IsNullOrEmpty(txtItem.Text))
                return;

            if (string.IsNullOrEmpty(txtLayout.Text))
            {
                MessageBox.Show("Favor de indicar el Layout", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtLayout.Focus();
                return;
            }
            else
            {
                int iL = 0;
                if (!int.TryParse(txtLayout.Text.ToString(), out iL))
                {
                    MessageBox.Show("El Layout no es válido", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtLayout.Focus();
                    return;
                }
            }


            if (!Guardar())
                return;

            wfTableComp Comp = new wfTableComp();
            Comp._lsMesa = _asMesa;
            Comp._lsItem = txtItem.Text.ToString();
            DataTable dtC = dgwComp.DataSource as DataTable;
            Comp._dt = dtC;
            Comp.ShowDialog();

            //sugerencia detalle
            ItemSugdetLogica itemd = new ItemSugdetLogica();
            itemd.Item = txtItem.Text.ToString().Trim();
            DataTable dtS = ItemSugdetLogica.ConsultarVista(itemd);
            dgwSuge.DataSource = dtS;
            CargarColumnasSug();

            itemd.Mesa = _asMesa;
            DataTable dt = ItemSugdetLogica.ConsultarVistaMesa(itemd);
            int iRows = dt.Rows.Count;
            ButtonColor(_asMesa, iRows);
            
            CargarColumnas();
        }
        private void ButtonColor(string _asMesa,int _aiCant)
        {
            if (_aiCant == 0)
            {
                switch (_asMesa)
                {
                    case "1":
                        btTable1.FlatAppearance.BorderColor = Color.Cyan;
                        btTable1.ForeColor = Color.DarkTurquoise;
                        break;
                    case "2":
                        btTable2.FlatAppearance.BorderColor = Color.Cyan;
                        btTable2.ForeColor = Color.DarkTurquoise;
                        break;
                    case "3":
                        btTable3.FlatAppearance.BorderColor = Color.Cyan;
                        btTable3.ForeColor = Color.DarkTurquoise;
                        break;
                    case "4":
                        btTable4.FlatAppearance.BorderColor = Color.Cyan;
                        btTable4.ForeColor = Color.DarkTurquoise;
                        break;
                    case "5":
                        btTable5.FlatAppearance.BorderColor = Color.Cyan;
                        btTable5.ForeColor = Color.DarkTurquoise;
                        break;
                    case "6":
                        btTable6.FlatAppearance.BorderColor = Color.Cyan;
                        btTable6.ForeColor = Color.DarkTurquoise;
                        break;
                    case "7":
                        btTable7.FlatAppearance.BorderColor = Color.Cyan;
                        btTable7.ForeColor = Color.DarkTurquoise;
                        break;
                }
            }
            else
            {
                switch (_asMesa)
                {
                    case "1":
                        btTable1.FlatAppearance.BorderColor = Color.Orange;
                        btTable1.ForeColor = Color.DarkOrange;
                        break;
                    case "2":
                        btTable2.FlatAppearance.BorderColor = Color.Orange;
                        btTable2.ForeColor = Color.DarkOrange;
                        break;
                    case "3":
                        btTable3.FlatAppearance.BorderColor = Color.Orange;
                        btTable3.ForeColor = Color.DarkOrange;
                        break;
                    case "4":
                        btTable4.FlatAppearance.BorderColor = Color.Orange;
                        btTable4.ForeColor = Color.DarkOrange;
                        break;
                    case "5":
                        btTable5.FlatAppearance.BorderColor = Color.Orange;
                        btTable5.ForeColor = Color.DarkOrange;
                        break;
                    case "6":
                        btTable6.FlatAppearance.BorderColor = Color.Orange;
                        btTable6.ForeColor = Color.DarkOrange;
                        break;
                    case "7":
                        btTable7.FlatAppearance.BorderColor = Color.Orange;
                        btTable7.ForeColor = Color.DarkOrange;
                        break;
                }
            }
        }
       

        private void dgwSuge_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int iRow = e.RowIndex;
            if ((iRow % 2) == 0)
                e.CellStyle.BackColor = Color.LightSkyBlue;
            else
                e.CellStyle.BackColor = Color.White;
        }

        private void dgwComp_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int iRow = e.RowIndex;
            string sValue = e.Value.ToString();
            
            sValue = dgwComp[1, e.RowIndex].Value.ToString();
            foreach(DataGridViewRow row in dgwSuge.Rows)
            {
                string sComp = dgwSuge[3, row.Index].Value.ToString();
                if(sComp == sValue)
                {
                    e.CellStyle.BackColor = Color.Orange;
                    //e.CellStyle.ForeColor = Color.MediumBlue;
                }
            }
        }

        private void btTable1_Click(object sender, EventArgs e)
        {

            OpenTable("1");
        }

        private void btTable3_Click(object sender, EventArgs e)
        {
            OpenTable("3");
        }
        private void btTable2_Click(object sender, EventArgs e)
        {
            OpenTable("2");
        }

        private void btTable4_Click(object sender, EventArgs e)
        {
            OpenTable("4");
        }

        private void btTable5_Click(object sender, EventArgs e)
        {
            OpenTable("5");
        }

        private void btTable6_Click(object sender, EventArgs e)
        {
            OpenTable("6");
        }

        private void btTable7_Click(object sender, EventArgs e)
        {

            OpenTable("7");
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

        private void wfTableSetup_Resize(object sender, EventArgs e)
        {
            if (WindowState != _WindowStateAnt && WindowState != FormWindowState.Minimized)
            {
                _WindowStateAnt = WindowState;
                ResizeControl(panel1, 3, ref _iWidthAnt, ref _iHeightAnt, 0);
                ResizeControl(panel2, 1, ref _iWidthAnt, ref _iHeightAnt, 0);
                ResizeControl(tabControl1, 3, ref _iWidthAnt, ref _iHeightAnt, 0);
                
                ResizeControl(dgwComp, 3, ref _iWidthAnt, ref _iHeightAnt, 0);
                ResizeControl(dgwSuge, 3, ref _iWidthAnt, ref _iHeightAnt, 1);
                
            }
        }

        private void txtLayout_TextChanged(object sender, EventArgs e)
        {
            if (txtLayout.Text.ToString() != _lsLayout)
            {
                _lsLayout = txtLayout.Text.ToString();
                _lbCambio = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItem.Text.ToString()))
                return;

            wfSugeComp Suge = new wfSugeComp();
            Suge._lsItem = txtItem.Text.ToString();
            Suge._lsDesc = lblDescrip.Text.ToString();
            Suge.ShowDialog();
               
        }

        private void dgwComp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       
    }
}
