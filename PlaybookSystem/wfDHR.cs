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
    public partial class wfDHR : Form
    {
        private long _llFolio;
        private decimal _ldMeta;
        private decimal _ldMetaHr;
        private decimal _ldMetaAc;
        private decimal _ldActual;
        private decimal _ldActualAc;
        private decimal _ldDefectos;
        private decimal _ldDefectosAc;
        public string _lsOrden;
        public string _lsProceso;
        
        Globals _gs = new Globals();

        public wfDHR()
        {
            InitializeComponent();
            
        }
        private void wfDHR_Activated(object sender, EventArgs e)
        {
            
        }
        #region regInicio
        private void wfDHR_Load(object sender, EventArgs e)
        {
            Inicio();
            tssHrVersion.Text = "DHR Tracker v. 1.0.0.1";
            
        }
        private void Inicio()
        {
            //load data
            try
            {
                _lsProceso = "QA010";
                dgwLine.DataSource = null;
                CargarColumnas();

                //1. find last folio from date
                //1.1 Load data from folio
                //1.2 Cursor on next hour row
                lblFecha.Text = DateTime.Today.ToShortDateString();
                string sIndBoxing = "0";
                if(!string.IsNullOrEmpty(Globals._gsStation))
                {
                    LineaRampeoLogica lineR = new LineaRampeoLogica();
                    lineR.Estacion = Globals._gsStation;
                    DataTable dtLR = LineaRampeoLogica.ConsultarEstacion(lineR);
                    if (dtLR.Rows.Count > 0)
                    {
                        Globals._gsCompany = dtLR.Rows[0]["company"].ToString();
                        Globals._gsLineHr = dtLR.Rows[0]["linehr"].ToString();
                        sIndBoxing = dtLR.Rows[0]["ind_hourly"].ToString();
                    }
                }

                lblLine.Text = Globals._gsLineHr;
                tssLine.Text = Globals._gsLineHr;

                if(sIndBoxing == "1")
                {
                    MessageBox.Show("La captura manual del Hora por Hora se ha desactivado para esta linea.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Close();
                    return;
                }

                LineaHoraLogica line = new LineaHoraLogica();
                line.Linea = Globals._gsLineHr;
                DataTable data = LineaHoraLogica.ConsultarActual(line);
                if (data.Rows.Count > 0)
                {
                    _llFolio = long.Parse(data.Rows[0]["folio"].ToString());
                   
                    //horario
                    LineaHoraDetLogica lined = new LineaHoraDetLogica();
                    lined.Folio = _llFolio;
                    DataTable dtH = LineaHoraDetLogica.VistaHorario(lined);
                    dgwLine.DataSource = dtH;

                    
                }
                else
                {
                    LineaConfigLogica con = new LineaConfigLogica();
                    con.Linea = Globals._gsLineHr;
                    //2. Load line data
                    //2.1 Load line setup
                    DataTable confL = LineaConfigLogica.ConsultarConfLinea(con);
                    if (confL.Rows.Count > 0)
                    {
                        
                    }
                 
                }

                if(dgwLine.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgwLine.Rows)
                    {
                         
                    }
                }
                else
                {
                    MessageBox.Show("La linea no se encuentra configurada en los parámetros del sistema.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Close();
                    return;
                }

                if (!string.IsNullOrEmpty(_lsOrden))
                {
                    lblOrden.Text = _lsOrden;
                    CargaWO();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void CargarColumnas()
        {
            int iRows = dgwLine.Rows.Count;
            if (iRows == 0)
            {
                DataTable dtNew = new DataTable("Linea");
                dtNew.Columns.Add("consec", typeof(int));
                dtNew.Columns.Add("Hora", typeof(string));
                dtNew.Columns.Add("Meta", typeof(decimal));
                dtNew.Columns.Add("Acum", typeof(decimal));
                dtNew.Columns.Add("Actual", typeof(decimal));
                dtNew.Columns.Add("Actual Acum", typeof(decimal));
                dtNew.Columns.Add("Defectos", typeof(decimal));
                dtNew.Columns.Add("Work Order", typeof(string));
                dtNew.Columns.Add("Comentarios", typeof(string));
                dtNew.Columns.Add("cumple_meta", typeof(string));
                dgwLine.DataSource = dtNew;
            }

            dgwLine.Columns[0].Visible = false;
            dgwLine.Columns[9].Visible = false;

            dgwLine.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgwLine.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dgwLine.Columns[1].Width = ColumnWith(dgwLine, 15);//hora
            dgwLine.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwLine.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwLine.Columns[1].ReadOnly = true;

            dgwLine.Columns[2].Width = ColumnWith(dgwLine, 7);//meta
            dgwLine.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwLine.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwLine.Columns[2].ReadOnly = true;

            dgwLine.Columns[3].Width = ColumnWith(dgwLine, 7);//meta acu
            dgwLine.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwLine.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwLine.Columns[3].ReadOnly = true;

            dgwLine.Columns[4].Width = ColumnWith(dgwLine, 7);//actual
            dgwLine.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwLine.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            

            dgwLine.Columns[5].Width = ColumnWith(dgwLine, 7);//actual acu
            dgwLine.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwLine.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwLine.Columns[5].ReadOnly = true;

            dgwLine.Columns[6].Width = ColumnWith(dgwLine, 10);//defectos
            dgwLine.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwLine.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgwLine.Columns[7].Width = ColumnWith(dgwLine, 21);//work order
            dgwLine.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwLine.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            dgwLine.Columns[8].Width = ColumnWith(dgwLine, 27);//comentarios
            dgwLine.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwLine.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            
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

        #region regSave
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private bool Valida()
        {
            bool bValida = true;

            if (string.IsNullOrEmpty(lblLine.Text) || string.IsNullOrWhiteSpace(lblLine.Text) || lblLine.Text == "0")
                return false;

          
            if (dgwLine.Rows.Count == 0)
                return false;

            
             

            return bValida;
        }
        private void CargaWO()
        {
            
            int iHora = DateTime.Now.Hour;
            int iHr = 0;
            string sValAnt = string.Empty;

            foreach (DataGridViewRow row in dgwLine.Rows)
            {
                //string sActual = dgwLine[4, row.Index].Value.ToString();
                //int iActual = 0;
                //if (!int.TryParse(sActual, out iActual))
                //    iActual = 0;
               
                string sWO = dgwLine[7, row.Index].Value.ToString();
                string sHorario = dgwLine[1, row.Index].Value.ToString();
                sHorario = sHorario.Substring(0, 2);
                iHr = int.Parse(sHorario);

                if (iHora > iHr && (string.IsNullOrEmpty(sWO)))
                    dgwLine[7, row.Index].Value = _lsOrden;
                
            }
        }
        private bool ValidaHoraCaptura()
        {
            bool bValida = true;
            int iHora = DateTime.Now.Hour;
            int iHr = 0;
            string sValAnt = string.Empty;

            foreach (DataGridViewRow row in dgwLine.Rows)
            {
                string sHorario = dgwLine[1, row.Index].Value.ToString();
                sHorario = sHorario.Substring(0, 2);
                iHr = int.Parse(sHorario);
                string sActual = dgwLine[4, row.Index].Value.ToString();

                //if (row.Index > 0 && iHr < iHora && (string.IsNullOrEmpty(sActual) || sActual == "0"))
                //{ 
                //    MessageBox.Show("Favor de capturar en las Horas faltantes", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    dgwLine.CurrentCell = dgwLine[4, row.Index];
                //    dgwLine.Focus();
                //    bValida = false;
                //    break;
                //}

                //adelantar captura de hora
                int iActual = 0;
                if (!int.TryParse(sActual, out iActual))
                    iActual = 0;
                if (row.Index > 0 && iHr >= iHora && iActual > 0)
                {
                    MessageBox.Show("No se permite adelantar la captura del Hora por Hora (" + sHorario + ")", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    dgwLine.CurrentCell = dgwLine[4, row.Index];
                    dgwLine.Focus();
                    bValida = false;
                    break;
                }

                //entre horas
                //if (row.Index > 0 && !string.IsNullOrEmpty(sActual) && sActual != "0")
                //{
                //    string sHora = dgwLine[1, row.Index - 1].Value.ToString();
                //    if (string.IsNullOrEmpty(sValAnt) || sValAnt == "0")
                //    {
                //        bValida = false;
                //        MessageBox.Show("No se ha capturado la cantidad de la hora " + sHora, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //        dgwLine.CurrentCell = dgwLine[4, row.Index - 1];
                //        dgwLine.Focus();
                //        break;
                //    }
                //}

                sValAnt = sActual;

            }

            return bValida;
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Valida())
                    return;

                if (!ValidaHoraCaptura())
                    return;

                LineaHoraLogica line = new LineaHoraLogica();

                if (_llFolio > 0)
                    line.Folio = _llFolio;
                else
                    line.Folio = AccesoDatos.Consec(_lsProceso);

                line.Linea = lblLine.Text.ToString();
                line.Fecha = DateTime.Parse(lblFecha.Text.ToString());  
                
                line.Usuario = Globals._gsStation;

                if(LineaHoraLogica.GuardarSP(line) > 0)
                {
                    LineaHoraDetLogica det = new LineaHoraDetLogica();
                    det.Folio = line.Folio;
                    foreach(DataGridViewRow row in dgwLine.Rows)
                    {
                        int iCons = 0;
                        if (!string.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                            iCons = int.Parse(row.Cells[0].Value.ToString());
                        string sHora = row.Cells[1].Value.ToString();
                        int iMeta = int.Parse(row.Cells[2].Value.ToString());
                        int iMetaAc = int.Parse(row.Cells[3].Value.ToString());
                        int iActual = int.Parse(row.Cells[4].Value.ToString());
                        int iActualAc = int.Parse(row.Cells[5].Value.ToString());
                        string sCumple = row.Cells[9].Value.ToString();
                        int iDefectos = 0;
                        if(!int.TryParse(row.Cells[6].Value.ToString(),out iDefectos))
                            iDefectos = 0;
                        string sOrden = row.Cells[7].Value.ToString();
                        string sNota = row.Cells[8].Value.ToString();

                        det.Consec = iCons;
                        det.Hora = sHora;
                        det.Meta = iMeta;
                        det.MetaAc = iMetaAc;
                        det.Actual = iActual;
                        det.ActualAc = iActualAc;
                        det.Defectos = iDefectos;
                        det.Orden = sOrden;
                        det.Nota = sNota;
                        det.Cumple = sCumple;

                        LineaHoraDetLogica.GuardarSP(det);

                    }

                    MessageBox.Show("Hora por Hora se ha registrado exitosamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        #endregion

        #region regGrids
        private void dgwLine_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int iRow = e.RowIndex;
            string sVal = dgwLine[9, e.RowIndex].Value.ToString();
            if ((iRow % 2) == 0)
                e.CellStyle.BackColor = Color.LightSkyBlue;
            else
                e.CellStyle.BackColor = Color.White;
               
            if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
                e.CellStyle.ForeColor = Color.DarkSlateGray;

            if(sVal == "0" && (  e.ColumnIndex == 5))
                e.CellStyle.BackColor = Color.Red;

            if (sVal == "2" )
                e.CellStyle.BackColor = Color.LightGreen;

        }
        private void dgwLine_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
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
                        
                    }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Favor de Notificar al Administrador" + Environment.NewLine + "dgwLine_CellValueChanged(6)" + Environment.NewLine + ex.ToString(), "Error en " + Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
        }
        
       
        #endregion

        #region regBotones
        private void btnNew_Click(object sender, EventArgs e)
        {
            Inicio();
        }

        

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgwLine.Rows.Count == 0)
                return;

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

                   
                    string sHoraSig = string.Empty;

                    int iHrS1 = iHr + 1;
                    int iHrS2 = iHr + 2;
                    sHoraSig = iHrS1.ToString().PadLeft(2, '0') + ":00 - " + iHrS2.ToString().PadLeft(2, '0') + ":00";

                    
                }
                 
                dgwLine.CurrentCell = dgwLine[4, iRow];
                dgwLine.Focus();
            }
        }

        private void lblTextra_Click(object sender, EventArgs e)
        {
            if (dgwLine.Rows.Count == 0)
                return;

            int iRow = dgwLine.RowCount - 1;

            string sTipo = dgwLine[9, iRow].Value.ToString();
            if (sTipo == "2")
                return;

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
                for (int x = 0; x < 3; x++)
                {
                    string sHora = dgwLine[1, iRow].Value.ToString();
                    sHora = sHora.Substring(0, 2);
                    int iHr = int.Parse(sHora);
                    int ixRow = 0;
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
                 
                dgwLine.CurrentCell = dgwLine[4, iRow];
                dgwLine.Focus();
            }
        }
        #endregion

       

        private void dgwLine_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dgwLine.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
    }
}
