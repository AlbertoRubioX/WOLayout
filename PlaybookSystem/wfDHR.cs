using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Logica;
using Datos;

namespace PlaybookSystem
{
    public partial class wfDHR : Form
    {
        private long _llFolio;
        public string _lsOrden;
        public string _lsProceso = "QA010";
        public string _lsEmployee;
        private int _liStep;
        private int _liStepNew;
        private int _liDetenido = 0;
        private string _lsLote;
        private bool _lbLine;
        private string _lsIndNC = "0";

        Globals _gs = new Globals();

        public wfDHR()
        {
            InitializeComponent();

        }
        private void wfDHR_Activated(object sender, EventArgs e)
        {
            txtOrden.Focus();
        }
        #region regInicio
        private void wfDHR_Load(object sender, EventArgs e)
        {
            wfCN wfENum = new wfCN();
            wfENum._lsCode = _lsProceso;
            wfENum.ShowDialog();

            string sNombre = wfENum._lsReturn;
            if (string.IsNullOrEmpty(sNombre))
                Close();
            else
            {
                Inicio();
                tssHrVersion.Text = "DHR Tracker v. 1.0.1.1";
                
                tssName.Text = sNombre;
                _lsEmployee = wfENum._lsCode;

                if(!string.IsNullOrEmpty(_lsOrden))
                {
                    txtOrden.Text = _lsOrden;
                    ChangeWO();
                }
                else
                    LoadPending();
            }
        }
        private void Inicio()
        {
            try
            {

                txtOrden.Text = "0000000";

                lblJob.Text = "__ |";
                lblDivision.Text = "_|";
                _lsLote = string.Empty;

                lblEstatus.Text = "";
                lblTimeActual.Text = "00:00:00";
                lblTimeActual.ForeColor = Color.Blue;
                _liStep = 0;
                _liStepNew = 0;
                _liDetenido = 0;
                _llFolio = 0;
                lblTimer1.Visible = false;
                lblTimer2.Visible = false;
                lblTimer3.Visible = false;
                lblTimerFinal.Visible = false;
                lblTimeActual.Visible = false;

                bt1.BackgroundImage = Properties.Resources.circle_o;
                bt2.BackgroundImage = Properties.Resources.circle_o;
                bt3.BackgroundImage = Properties.Resources.circle_o;
                bt4.BackgroundImage = Properties.Resources.circle_o;

                dgwAccion.DataSource = null;
                CargarColumnas1();
                dgwLine.DataSource = null;
                CargarColumnas();

                if (!string.IsNullOrEmpty(Globals._gsStation))
                {
                    LineaRampeoLogica lineR = new LineaRampeoLogica();
                    lineR.Estacion = Globals._gsStation;
                    DataTable dtLR = LineaRampeoLogica.ConsultarEstacion(lineR);
                    if (dtLR.Rows.Count > 0)
                    {
                        Globals._gsCompany = dtLR.Rows[0]["company"].ToString();
                        Globals._gsLine = dtLR.Rows[0]["line"].ToString();

                    }
                }

                cbbFallas.DataSource = DhTrackerLogica.DhrFallas();
                cbbFallas.ValueMember = "falla";
                cbbFallas.DisplayMember = "descrip";
                cbbFallas.SelectedIndex = -1;

                txtNotaFalla.Clear();

                cbbAccion.DataSource = DhTrackerLogica.DhrAccion();
                cbbAccion.ValueMember = "falla";
                cbbAccion.DisplayMember = "descrip";
                cbbAccion.SelectedIndex = -1;
                
                txtAccionDesc.Clear();

                int iLine = 0;
                _lbLine = false;
                if (int.TryParse(Globals._gsLine.Substring(0,1), out iLine))
                    _lbLine = true;

                lblLine.Text = Globals._gsLine;

                tssLine.Text = Globals._gsLine;

                gbxAccion.Visible = true;
                gbxAccion.Enabled = false;
                groupBox1.Visible = false;

                ChangeStatus(0);
                txtOrden.SelectAll();
                txtOrden.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void LoadPending()
        {
            DataTable dt = DhTrackerLogica.ConsultarPending();
            if (dt.Rows.Count > 0)
            {
                wfDhPending wfPending = new wfDhPending();
                wfPending._lsFiltro = "D";
                wfPending.ShowDialog();
            }
        }
        private void CargarColumnas()
        {
            int iRows = dgwLine.Rows.Count;
            if (iRows == 0)
            {
                DataTable dtNew = new DataTable("Linea");
                dtNew.Columns.Add("consec", typeof(int));
                dtNew.Columns.Add("falla", typeof(int));
                dtNew.Columns.Add("Correcciones", typeof(string));
                dtNew.Columns.Add("Comentarios", typeof(string));
                dtNew.Columns.Add("Inspector", typeof(bool));
                dtNew.Columns.Add("Corregido", typeof(bool));
                dtNew.Columns.Add("estatus", typeof(int));
                dtNew.Columns.Add("ind_inspector", typeof(int));
                dgwLine.DataSource = dtNew;
            }

            dgwLine.Columns[0].Visible = false;
            dgwLine.Columns[1].Visible = false;
            dgwLine.Columns[6].Visible = false;
            dgwLine.Columns[7].Visible = false;

            dgwLine.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgwLine.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dgwLine.Columns[2].Width = ColumnWith(dgwLine, 50);//Descrip
            dgwLine.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwLine.Columns[2].ReadOnly = true;

            dgwLine.Columns[3].Width = ColumnWith(dgwLine, 25);//coment
            dgwLine.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwLine.Columns[3].ReadOnly = true;

            dgwLine.Columns[4].Width = ColumnWith(dgwLine, 10);//Inspector
            dgwLine.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwLine.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwLine.Columns[4].ReadOnly = true;

            dgwLine.Columns[5].Width = ColumnWith(dgwLine, 10);//corregido box
            dgwLine.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwLine.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwLine.Columns[5].ReadOnly = true;

            iRows = dgwInsp.Rows.Count;
            if (iRows == 0)
            {
                DataTable dtNew2 = new DataTable("Falla");
                dtNew2.Columns.Add("folio", typeof(int));
                dtNew2.Columns.Add("consec", typeof(int));
                dtNew2.Columns.Add("Inspector", typeof(int));
                dtNew2.Columns.Add("Nombre", typeof(string));
                dgwInsp.DataSource = dtNew2;
            }
        }

        private void CargarColumnas1()
        {
            int iRows = dgwAccion.Rows.Count;
            if (iRows == 0)
            {
                DataTable dtNew = new DataTable("Accion");
                dtNew.Columns.Add("consec", typeof(int));
                dtNew.Columns.Add("accion", typeof(int));
                dtNew.Columns.Add("Acción Inmediata", typeof(string));
                dtNew.Columns.Add("Descripción", typeof(string));
                dtNew.Columns.Add("Inspector", typeof(string));
                dgwAccion.DataSource = dtNew;
            }

            dgwAccion.Columns[0].Visible = false;
            dgwAccion.Columns[1].Visible = false;

            dgwAccion.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgwAccion.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dgwAccion.Columns[2].Width = ColumnWith(dgwAccion, 25);//accion inemdiata
            dgwAccion.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwAccion.Columns[2].ReadOnly = true;

            dgwAccion.Columns[3].Width = ColumnWith(dgwAccion, 60);//descrip
            dgwAccion.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwAccion.Columns[3].ReadOnly = true;

            dgwAccion.Columns[4].Width = ColumnWith(dgwAccion, 10);//Inspector
            dgwAccion.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwAccion.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwAccion.Columns[4].ReadOnly = true;

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

            if (string.IsNullOrEmpty(txtOrden.Text) || string.IsNullOrWhiteSpace(txtOrden.Text) || txtOrden.Text == "000000")
                return false;

            if (_liStep == 0)
            {
                if (_liStepNew == 0)
                    return false;
                else
                {
                    if (_liStepNew > 1)
                    {
                        MessageBox.Show("No puede omitir pasos en el proceso.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            else
            {
                int iDif = _liStepNew - _liStep;
                if (iDif > 1)
                {
                    MessageBox.Show("No puede omitir pasos en el proceso.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return bValida;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Valida())
                    return;

                _liStep = _liStepNew;
                
                if (_liStep == 1)
                {
                    _lsIndNC = "0";
                    foreach (DataGridViewRow row in dgwAccion.Rows)
                    {
                        int iCons = int.Parse(row.Cells[0].Value.ToString());
                        string sValue = row.Cells[1].Value.ToString();
                        foreach (DataGridViewRow row2 in dgwAccion.Rows)
                        {
                            int iCons2 = int.Parse(row2.Cells[0].Value.ToString());
                            string sValue2 = row2.Cells[1].Value.ToString();
                            if (sValue2 == sValue && iCons != iCons2)
                            {
                                _lsIndNC = "1";
                                break;
                            }   
                        }
                        if (_lsIndNC == "1")
                            break;
                    }
                }
                

                if (_liStep == 3)
                {
                    int iCheck = 0;

                    foreach (DataGridViewRow row in dgwLine.Rows)
                    {
                        string sEstatus = dgwLine[6, row.Index].Value.ToString();
                        if (sEstatus == "0")
                        {
                            iCheck++;
                        }
                    }
                    if (iCheck == 0)
                        _liDetenido = 0;
                    else
                    {
                        if (_liDetenido == 0)
                        {
                            if (MessageBox.Show("Deseas detener la Revisión hasta la Corrección?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                _liDetenido = 1;
                        }
                    }
                }

                DhTrackerLogica dhr = new DhTrackerLogica();

                if (_llFolio > 0)
                    dhr.Folio = _llFolio;
                else
                    dhr.Folio = AccesoDatos.Consec(_lsProceso);
                dhr.Orden = txtOrden.Text.ToString();
                dhr.Linea = lblLine.Text.ToString();
                dhr.Fecha = DateTime.Now;

                int iPos = lblJob.Text.ToString().IndexOf(" - ");
                string sParte = lblJob.Text.ToString().Substring(0, iPos);
                string sDescrip = lblJob.Text.ToString().Substring(iPos + 3);

                dhr.Parte = sParte.TrimEnd();
                dhr.Descrip = sDescrip;
                dhr.Division = lblDivision.Text.ToString();

                dhr.Inspector = _lsEmployee;
                dhr.Nombre = tssName.Text.ToString();
                dhr.Estatus = _liStep;                                  
                dhr.Detenido = _liDetenido;
                dhr.Lote = _lsLote.TrimEnd();
                dhr.IndNC = _lsIndNC;
                dhr.Usuario = _lsEmployee;

                if (DhTrackerLogica.GuardarSP(dhr) > 0)
                {
                    DhTracdetaLogica deta = new DhTracdetaLogica();
                    deta.Folio = dhr.Folio;

                    //ACciones
                    foreach (DataGridViewRow row in dgwAccion.Rows)
                    {
                        int iCons = 0;
                        if (!string.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                            iCons = int.Parse(row.Cells[0].Value.ToString());

                        deta.Accion = int.Parse(row.Cells[1].Value.ToString());
                        deta.Nota = row.Cells[3].Value.ToString();
                        deta.Inspector = row.Cells[4].Value.ToString();
                        deta.Consec = iCons;
                        deta.Usuario = _lsEmployee;

                        DhTracdetaLogica.GuardarSP(deta);
                    }
                    //Correcciones
                    DhTracdetLogica det = new DhTracdetLogica();
                    det.Folio = dhr.Folio;
                    foreach (DataGridViewRow row in dgwLine.Rows)
                    {
                        int iCons = 0;
                        if (!string.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                            iCons = int.Parse(row.Cells[0].Value.ToString());

                        det.Falla = int.Parse(row.Cells[1].Value.ToString());
                        det.Nota = row.Cells[3].Value.ToString();
                        if (bool.Parse(row.Cells[4].Value.ToString()))
                            det.IndInspector = "1";// int.Parse(row.Cells[5].Value.ToString());
                        else
                            det.IndInspector = "0";

                        if (bool.Parse(row.Cells[5].Value.ToString()))
                            det.Estatus = 1;// int.Parse(row.Cells[5].Value.ToString());
                        else
                            det.Estatus = 0;

                        det.Consec = iCons;
                        det.Usuario = _lsEmployee;

                        DhTracdetLogica.GuardarSP(det);
                        
                        foreach(DataGridViewRow irow in dgwInsp.Rows)
                        {
                            int iCode = int.Parse(irow.Cells[1].Value.ToString());
                            if (iCode != det.Consec)
                                continue;

                            int iInspector = int.Parse(irow.Cells[2].Value.ToString());
                            string sNombre = irow.Cells[3].Value.ToString();

                            DhTracinsLogica ins = new DhTracinsLogica();
                            ins.Folio = dhr.Folio;
                            ins.Falla = det.Consec;
                            ins.Inspector = iInspector;
                            ins.Nombre = sNombre;
                            ins.Usuario = _lsEmployee;
                            DhTracinsLogica.GuardarSP(ins);
                        }

                    }

                    _llFolio = dhr.Folio;

                    if (_liStep == 1)
                        MessageBox.Show("DHR Tracker se ha registrado exitosamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        MessageBox.Show("DHR Tracker se ha actualizado exitosamente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        _lsOrden = txtOrden.Text.ToString();

                        Inicio();
                        txtOrden.Text = _lsOrden;
                        ChangeWO();
                    }
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
            if ((iRow % 2) == 0)
                e.CellStyle.BackColor = Color.LightSkyBlue;
            else
                e.CellStyle.BackColor = Color.White;

        }
        private void dgwLine_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
            //if (e.ColumnIndex == dgwLine.Columns[4].Index)
            //{
            //    if (bool.Parse(dgwLine[4, e.RowIndex].Value.ToString()))
            //        dgwLine[5, e.RowIndex].Value = 1;
            //    else
            //        dgwLine[5, e.RowIndex].Value = 0;
            //}

            //bool bCheck = bool.Parse(dgwLine[4, e.RowIndex].Value.ToString());
            //int iEstatus = int.Parse(dgwLine[5, e.RowIndex].Value.ToString());
            //if(bCheck)
            //    dgwLine[5, e.RowIndex].Value = 1;
            //else
            //    dgwLine[5, e.RowIndex].Value = 0;
        }
        private void dgwLine_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == dgwLine.Columns[4].Index)
            {
                dgwLine.EndEdit();
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
            if (txtOrden.Text == "0000000" || string.IsNullOrEmpty(txtOrden.Text))
                return;

            if (cbbFallas.SelectedIndex == -1)
                return;

            string sFalla = cbbFallas.SelectedValue.ToString();
            string sDescrip = cbbFallas.Text.ToString();
            string sNota = txtNotaFalla.Text.ToString();


            int iRow = dgwLine.RowCount;
            int iCons = 0;
            if(iRow > 0)
            {
                iRow--;
                string sCons = dgwLine[0, iRow].Value.ToString();
                if (!int.TryParse(sCons, out iCons))
                    iCons = 0;
            }
            iCons++;
            bool bInsp = false;
            

            if(chbInspector.Checked)
            {
                wfInspectores wfInsp = new wfInspectores();
                wfInsp._liFolio = int.Parse(txtOrden.Text.ToString());
                wfInsp._liCode = iCons;
                wfInsp.ShowDialog();

                DataTable dtIns = wfInsp._dt;
                if(dtIns.Rows.Count > 0)
                {
                    bInsp = true;
                    DataTable dt2 = dgwInsp.DataSource as DataTable;
                    for(int i = 0; i < dtIns.Rows.Count; i++)
                    {
                        int iFolio = int.Parse(dtIns.Rows[i][0].ToString());
                        int iCode = int.Parse(dtIns.Rows[i][1].ToString());
                        int iNumber = int.Parse(dtIns.Rows[i][2].ToString());
                        string sNombre = dtIns.Rows[i][3].ToString();
                        
                        dt2.Rows.Add(iFolio, iCode, iNumber, sNombre);
                    }
                }
            }

            DataTable dt = dgwLine.DataSource as DataTable;
            dt.Rows.Add(iCons, int.Parse(sFalla), sDescrip, sNota,bInsp, false, 0);

            cbbFallas.SelectedIndex = -1;
            txtNotaFalla.Clear();
            chbInspector.Checked = false;

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

        private void bt1_Click(object sender, EventArgs e)
        {
            ChangeStatus(1);
            
        }

        private void bt2_Click(object sender, EventArgs e)
        {
            ChangeStatus(2);
            
        }

        private void bt3_Click(object sender, EventArgs e)
        {
            ChangeStatus(3);
            
        }

        private void bt4_Click(object sender, EventArgs e)
        {
            
            ChangeStatus(4);
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            if (_liStep < 4)
                ChangeStatus(_liStep + 1);


        }

        private void btBack_Click(object sender, EventArgs e)
        {
            if (_liStep > 1)
                ChangeStatus(_liStep - 1);
        }
        #endregion

        #region Captura
        private void dgwLine_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            dgwLine.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        private bool ValidaStep(int _aiEstatus)
        {
            if (_liStep == 0)
            {
                if (_aiEstatus == 0)
                    return false;
                else
                {
                    if (_aiEstatus > 1)
                    {
                        MessageBox.Show("No puede omitir pasos en el proceso.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            else
            {
                int iDif = _aiEstatus - _liStep;
                if (iDif > 1)
                {
                    MessageBox.Show("No puede omitir pasos en el proceso.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (_liStep == 3 && _aiEstatus == 4)
                {
                    if (_liDetenido == 1)
                    {
                        MessageBox.Show("Favor resolver las correcciones pendientes en el paso 3", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    else
                    {
                        string sDirectorio = "\\\\Mxcprdqafs1\\QA Images";
                        string sFolder = "\\DHR SCAN STERILE";
                        string sDiv = "\\SPT";
                        if (lblDivision.Text == "DYN")
                            sDiv = "\\DYNACOR";
                        string sAxo = "\\" + DateTime.Today.Year.ToString();
                        int iPos = lblJob.Text.ToString().IndexOf(" - ");
                        string sJob = lblJob.Text.ToString().Substring(0, iPos);
                        string sDHR = _lsLote + "," + sJob + "," + txtOrden.Text.ToString() + ".pdf";
                        string sFile = sDirectorio + sFolder + sDiv + sAxo + "\\" + sDHR;
                        if (File.Exists(sFile))
                        {
                            MessageBox.Show("Documento " + sDHR + " ha sido encontrado", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        else
                        {
                            sFolder = "\\DHR SCAN NO STERILE";
                            sFile = sDirectorio + sFolder + sDiv + sAxo + "\\" + sDHR;
                            if (!File.Exists(sFile))
                            {
                                MessageBox.Show("El documento " + sDHR + " no fue encontrado", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                            else
                            {
                                MessageBox.Show("Documento " + sDHR + " ha sido encontrado", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return true;
                            }
                        }
                    }
                }
            }
            return true;
        }
        private void ChangeStatus(int _aiEstatus)
        {
            if (!ValidaStep(_aiEstatus))
                return;

            bt1.BackgroundImage = Properties.Resources.circle_gray;
            bt2.BackgroundImage = Properties.Resources.circle_gray;
            bt3.BackgroundImage = Properties.Resources.circle_gray;
            bt4.BackgroundImage = Properties.Resources.circle_gray;

            lblCR.Font = new Font(Label.DefaultFont, FontStyle.Regular);
            lblBoxing.Font = new Font(Label.DefaultFont, FontStyle.Regular);
            lblDHR.Font = new Font(Label.DefaultFont, FontStyle.Regular);
            lblEscaneo.Font = new Font(Label.DefaultFont, FontStyle.Regular);

            switch (_aiEstatus)
            {
                case 1:
                    groupBox1.Visible = false;
                    gbxAccion.Visible = true;
                    gbxAccion.Enabled = true;

                    bt1.BackgroundImage = Properties.Resources.circle_blue;
                    lblEstatus.Text = "CLEAN ROOM";
                    lblCR.Font = new Font(Label.DefaultFont, FontStyle.Bold);
                    break;
                case 2:
                    groupBox1.Visible = false;
                    gbxAccion.Enabled = false;

                    bt1.BackgroundImage = Properties.Resources.circle_blue;
                    bt2.BackgroundImage = Properties.Resources.circle_blue;
                    lblEstatus.Text = "BOXING";
                    lblBoxing.Font = new Font(Label.DefaultFont, FontStyle.Bold);
                    if (_aiEstatus != _liStep)
                    {
                        lblTimer1.Text = lblTimeActual.Text.ToString().Substring(0, 5);
                        lblTimeActual.Text = "00:00:00";
                    }
                    lblTimer1.Visible = true;
                    break;
                case 3:

                    groupBox1.Visible = true;
                    gbxAccion.Visible = false;

                    bt1.BackgroundImage = Properties.Resources.circle_blue;
                    bt2.BackgroundImage = Properties.Resources.circle_blue;
                    lblDHR.Font = new Font(Label.DefaultFont, FontStyle.Bold);
                    if (_liDetenido == 1)
                        bt3.BackgroundImage = Properties.Resources.circle_red;
                    else
                        bt3.BackgroundImage = Properties.Resources.circle_blue;
                    if (_aiEstatus != _liStep) //edit
                    {
                        lblTimer2.Text = lblTimeActual.Text.ToString().Substring(0, 5);
                        lblTimeActual.Text = "00:00:00";
                    }
                    lblEstatus.Text = "DHR";
                    lblTimer1.Visible = true;
                    lblTimer2.Visible = true;
                    groupBox1.Enabled = true;
                    break;
                case 4:
                    bt1.BackgroundImage = Properties.Resources.circle_blue;
                    bt2.BackgroundImage = Properties.Resources.circle_blue;
                    bt3.BackgroundImage = Properties.Resources.circle_blue;
                    lblEscaneo.Font = new Font(Label.DefaultFont, FontStyle.Bold);
                    bt4.BackgroundImage = Properties.Resources.circle_blue;
                    lblEstatus.Text = "ESCANEO";
                    if (_aiEstatus != _liStep) //edit
                    {
                        lblTimer3.Text = lblTimeActual.Text.ToString().Substring(0, 5);
                        lblTimeActual.Visible = false;
                    }
                    else
                        lblTimeActual.Visible = true;

                    lblTimer1.Visible = true;
                    lblTimer2.Visible = true;
                    lblTimer3.Visible = true;
                    groupBox1.Enabled = false;
                    gbxAccion.Visible = false;
                    break;
                default:
                    lblEstatus.Text = "";
                    lblTimer1.Visible = false;
                    lblTimer2.Visible = false;
                    break;
            }

            int iX = lblEstatus.Location.X;
            int iW = lblEstatus.Size.Width;
            iW = iW / 2;

            int iWp = panel4.Size.Width;
            iWp = iWp / 2;

            iX = iWp - iW;
            lblEstatus.Location = new Point(iX, lblEstatus.Location.Y);

            _liStepNew = _aiEstatus;

            if (_aiEstatus < 4)
                timer1.Start();
        }
        private string GetCurrentDuration(DateTime _adtFecha)
        {

            TimeSpan time = DateTime.Now - _adtFecha;
            var dateDiff = DateTime.Now.Subtract(_adtFecha);

            double dDay = time.Days;
            double dHrs = time.Hours;
            if (dDay > 0)
                dHrs = dHrs + (dDay * 24);
            double dMin = time.Minutes;
            string sDuracion = dHrs.ToString().PadLeft(2, '0') + ":" + dMin.ToString().PadLeft(2, '0') + ":00";

            return sDuracion;
        }
        private string GetDuration(DateTime _adtFechaIni, DateTime _adtFechaFin)
        {

            TimeSpan time = _adtFechaFin - _adtFechaIni;
            double dDay = time.Days;
            double dHrs = time.Hours;
            if (dDay > 0)
                dHrs = dHrs + (dDay * 24);

            double dMin = time.Minutes;
            string sDuracion = dHrs.ToString().PadLeft(2, '0') + ":" + dMin.ToString().PadLeft(2, '0');

            return sDuracion;
        }
        private void txtOrden_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Escape)
                Close();

            if (e.KeyCode != Keys.Enter)
                return;

            string sValue = txtOrden.Text.ToString().ToUpper();

            if (sValue.Substring(0, 3) == "686")
                sValue = sValue.Substring(3);

            if (sValue.Length < 7)
            {
                sValue = sValue.PadLeft(7, '0');
                txtOrden.Text = sValue;
            }

            Inicio();
            txtOrden.Text = sValue;
            lblTimeActual.Visible = true;

            ChangeWO();
            
        }
        private void ChangeWO()
        {
            try
            {
                string sValue = txtOrden.Text.ToString().ToUpper();

                DhTrackerLogica dhr = new DhTrackerLogica();
                dhr.Orden = sValue;
                DataTable dt = DhTrackerLogica.ConsultarOrden(dhr);
                if (dt.Rows.Count > 0)
                {
                    _llFolio = long.Parse(dt.Rows[0][0].ToString());
                    string sItem = dt.Rows[0][4].ToString().Trim();
                    string sDesc = dt.Rows[0][5].ToString().Trim();
                    string sEstatus = dt.Rows[0][6].ToString();
                    string sDivision = dt.Rows[0]["division"].ToString();
                    _liDetenido = int.Parse(dt.Rows[0]["detenido"].ToString());
                    _lsLote = dt.Rows[0]["lote"].ToString();
                    lblLine.Text = dt.Rows[0]["linea"].ToString();
                    _lsIndNC = dt.Rows[0]["ind_nc"].ToString();

                    _liStep = int.Parse(sEstatus);
                    switch (_liStep)
                    {
                        case 1:
                            lblTimeActual.Text = GetCurrentDuration(DateTime.Parse(dt.Rows[0]["f_clean"].ToString()));
                            break;
                        case 2:
                            lblTimer1.Text = GetDuration(DateTime.Parse(dt.Rows[0]["f_clean"].ToString()), DateTime.Parse(dt.Rows[0]["f_boxing"].ToString()));
                            lblTimeActual.Text = GetCurrentDuration(DateTime.Parse(dt.Rows[0]["f_boxing"].ToString()));
                            break;
                        case 3:
                            lblTimer1.Text = GetDuration(DateTime.Parse(dt.Rows[0]["f_clean"].ToString()), DateTime.Parse(dt.Rows[0]["f_boxing"].ToString()));
                            lblTimer2.Text = GetDuration(DateTime.Parse(dt.Rows[0]["f_boxing"].ToString()), DateTime.Parse(dt.Rows[0]["f_dhr"].ToString()));
                            lblTimeActual.Text = GetCurrentDuration(DateTime.Parse(dt.Rows[0]["f_dhr"].ToString()));
                            groupBox1.Enabled = true;
                            break;
                        case 4:
                            lblTimer1.Text = GetDuration(DateTime.Parse(dt.Rows[0]["f_clean"].ToString()), DateTime.Parse(dt.Rows[0]["f_boxing"].ToString()));
                            lblTimer2.Text = GetDuration(DateTime.Parse(dt.Rows[0]["f_boxing"].ToString()), DateTime.Parse(dt.Rows[0]["f_dhr"].ToString()));
                            lblTimer3.Text = GetDuration(DateTime.Parse(dt.Rows[0]["f_dhr"].ToString()), DateTime.Parse(dt.Rows[0]["f_escaneo"].ToString()));
                            lblTimeActual.Text = "Total: " + GetDuration(DateTime.Parse(dt.Rows[0]["f_clean"].ToString()), DateTime.Parse(dt.Rows[0]["f_escaneo"].ToString()));
                            lblTimeActual.ForeColor = Color.Black;
                            timer1.Stop();
                            break;
                    }

                    lblJob.Text = sItem + " - " + sDesc;
                    //iEstatus++;
                    ChangeStatus(_liStep);
                    
                    lblDivision.Text = sDivision;

                    _llFolio = long.Parse(dt.Rows[0][0].ToString());
                    dhr.Folio = _llFolio;
                    
                    DhTracdetaLogica dhra = new DhTracdetaLogica();
                    dhra.Folio = _llFolio;
                    dgwAccion.DataSource = DhTracdetaLogica.ConsultarAccionView(dhra);

                    //dwLine.DataSource = DhTrackerLogica.ConsultarFallasView(dhr);
                    DataTable dtS = dgwLine.DataSource as DataTable;
                    DataTable det = DhTrackerLogica.ConsultarFallasView(dhr);
                    for (int i = 0; i < det.Rows.Count; i++)
                    {
                        int iEstatus = int.Parse(det.Rows[i][6].ToString());
                        bool bCheck = false;
                        if (iEstatus == 1)
                            bCheck = true;

                        int iInsp = int.Parse(det.Rows[i][7].ToString());
                        bool bCheckI = false;
                        if (iInsp == 1)
                            bCheckI = true;

                        string sCons = det.Rows[i][0].ToString();
                        int iFalla = int.Parse(det.Rows[i][1].ToString());
                        string sFDesc = det.Rows[i][2].ToString();
                        string sNota = det.Rows[i][3].ToString();
                        dtS.Rows.Add(sCons, iFalla, sFDesc, sNota,bCheckI, bCheck, iEstatus,iInsp);
                    }

                    DhTracinsLogica ins = new DhTracinsLogica();
                    ins.Folio = _llFolio;
                    DataTable dtI = DhTracinsLogica.ConsultarOrden(ins);
                    dgwInsp.DataSource = dtI;

                    CargarColumnas();
                    CargarColumnas1();

                    if(_liStep == 1)
                    {
                        DateTime dtToday = DateTime.Now;
                        if(!DateTime.TryParse(dt.Rows[0]["f_clean_out"].ToString(),out dtToday))
                        {
                            DialogResult Result = MessageBox.Show("Desea Registrar la Salida del DHR de Clean Room?", Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (Result == DialogResult.Yes)
                            {

                                DhTrackerLogica dtU = new DhTrackerLogica();
                                dtU.Folio = _llFolio;
                                if (DhTrackerLogica.UpdateCleanOut(dtU) > 0)
                                {
                                    bt2.BackgroundImage = Properties.Resources.circle_green;
                                    timer1.Stop();
                                }
                            }
                        }
                        else
                        {
                            lblTimeActual.Text = GetDuration(DateTime.Parse(dt.Rows[0]["f_clean"].ToString()), DateTime.Parse(dt.Rows[0]["f_clean_out"].ToString()));
                            bt2.BackgroundImage = Properties.Resources.circle_green;
                            timer1.Stop();
                        }
                    }

                }
                else
                {
                    AS4Logica AS4 = new AS4Logica();
                    AS4.CN = Globals._gsCompany;
                    AS4.WO = sValue;

                    dt = AS4Logica.WorkOrder(AS4);
                    if (dt.Rows.Count > 0)
                    {
                        string sItem = dt.Rows[0][0].ToString().Trim();
                        string sDesc = dt.Rows[0][1].ToString().Trim();
                        string sDiv = dt.Rows[0]["IMCONV"].ToString();
                        _lsLote = dt.Rows[0]["WOLOT"].ToString();

                        string sDivision = "SPT";    
                        if (sDiv == "20")
                            sDivision = "DYN";

                        lblJob.Text = sItem + " - " + sDesc;
                        lblDivision.Text = sDivision;
                    }
                    else
                    {
                        MessageBox.Show("Work Order not Found!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtOrden.Clear();
                        txtOrden.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void txtOrden_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int ipos = lblTimeActual.Text.IndexOf(":");
            string sTimeH = lblTimeActual.Text.Substring(0, ipos);
            string sTimeM = lblTimeActual.Text.Substring(ipos+1, 2);
            string sTimeS = lblTimeActual.Text.Substring(ipos+4, 2);

            int iContM = int.Parse(sTimeM);
            int iContH = int.Parse(sTimeH);
            int iContS = int.Parse(sTimeS);

            if (iContS == 59)
            {
                iContS = 0;
                if (iContM == 59)
                {
                    iContM = 0;
                    iContH++;
                }
                else
                    iContM++;
            }
            else
                iContS++;

            lblTimeActual.Text = iContH.ToString().PadLeft(2, '0') + ":" + iContM.ToString().PadLeft(2, '0') + ":" + iContS.ToString().PadLeft(2, '0');
        }
        #endregion

      

        private void btnReport_Click(object sender, EventArgs e)
        {
            wfOptionRep Rep = new wfOptionRep();
            Rep.ShowDialog();
            string sVal = Rep._lsOption;
            wfDhPending wfPending = new wfDhPending();
            wfPending._lsFiltro = sVal;
            wfPending.Show();
            
        }

        private void btnInspector_Click(object sender, EventArgs e)
        {

        }

        private void chbInspector_CheckedChanged(object sender, EventArgs e)
        {

        }
         
        private void dgwLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                if (dgwLine.Rows.Count <= 0) return;

                DialogResult Result = MessageBox.Show("Desea eliminar la Corrección seleccionada?", Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (Result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dgwLine.SelectedRows)
                    {
                        if (_llFolio > 0)
                        {
                            int iCons = int.Parse(dgwLine[0, row.Index].Value.ToString());
                            string sInsp = dgwLine[7, row.Index].Value.ToString();
                            if (sInsp == "1")
                            {
                                DhTracinsLogica ins = new DhTracinsLogica();
                                ins.Folio = _llFolio;
                                ins.Falla = iCons;
                                DhTracinsLogica.Eliminar(ins);
                            }

                            DhTracdetLogica det = new DhTracdetLogica();
                            det.Folio = _llFolio;
                            det.Consec = iCons;
                            DhTracdetLogica.Eliminar(det);

                        }

                        dgwLine.Rows.Remove(row);

                    }
                }
            }
        }

        private void btFix_Click(object sender, EventArgs e)
        {
            if (dgwLine.SelectedRows.Count <= 0)
                return;

            if (dgwLine[6, dgwLine.SelectedRows[0].Index].Value.ToString() == "1")
                return;

            if (MessageBox.Show("Desea marcar la corrección "+ dgwLine[2, dgwLine.SelectedRows[0].Index].Value.ToString() + " como solucionada?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DhTracdetLogica det = new DhTracdetLogica();
                det.Folio = _llFolio;
                det.Consec = int.Parse(dgwLine[0, dgwLine.SelectedRows[0].Index].Value.ToString());
                if (DhTracdetLogica.Correccion(det) > 0)
                {
                    MessageBox.Show("Corrección solucionada.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgwLine[5, dgwLine.SelectedRows[0].Index].Value = true;
                    // Cambiar valor del encabezado
                    _lsOrden = txtOrden.Text.ToString();

                    Inicio();
                    txtOrden.Text = _lsOrden;
                    ChangeWO();
                }
            }

        }

        private void btnAdd1_Click(object sender, EventArgs e)
        {
            //accion inmediata

            if (cbbAccion.SelectedIndex == -1)
            {
                MessageBox.Show("Se Requiere una acción", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int iLcc = 0;
            if (string.IsNullOrEmpty(txtInspector.Text))
            {
                MessageBox.Show("Se Requiere el # de Empleado del LCC", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if(!int.TryParse(txtInspector.Text.ToString(),out iLcc))
                {
                    MessageBox.Show("El # de Empleado del LCC es invalido", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (Globals._gsCompany == "686")
                {
                    TressLogica tress = new TressLogica();
                    tress.Empleado = iLcc;
                    DataTable dt3 = TressLogica.ConsultarEmpleado(tress);
                    if (dt3.Rows.Count == 0)
                    {
                        MessageBox.Show("Numero de Empleado no encontrado", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtInspector.Clear();
                        return;
                    }
                }
            }

            //add row
            string sAccion = cbbAccion.SelectedValue.ToString();
            string sNota = txtAccionDesc.Text.ToString().TrimEnd();
            string sDesc = cbbAccion.Text.ToString();
            int iCons = 0;
            foreach (DataGridViewRow row in dgwAccion.Rows)
            {
                iCons = int.Parse(row.Cells[0].Value.ToString());
                string sValue = row.Cells[1].Value.ToString();
                if (sAccion == sValue)
                {
                    MessageBox.Show("Se Requiere generar NC debido a la repetición de la Accion Inmediata", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            DataTable dt = dgwAccion.DataSource as DataTable;

            iCons += 1;
            dt.Rows.Add(iCons, int.Parse(sAccion), sDesc, sNota,iLcc);

            cbbAccion.SelectedIndex = -1;
            txtAccionDesc.Clear();
            txtInspector.Clear();
        }

        private void dgwAccion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                if (dgwAccion.Rows.Count <= 0) return;

                DialogResult Result = MessageBox.Show("Desea eliminar la Acción Inmediata?", Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (Result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dgwAccion.SelectedRows)
                    {
                        if (_llFolio > 0)
                        {
                            int iCons = int.Parse(dgwAccion[0, row.Index].Value.ToString());
                            
                            DhTracdetLogica det = new DhTracdetLogica();
                            det.Folio = _llFolio;
                            det.Consec = iCons;
                            DhTracdetLogica.Eliminar(det);

                        }

                        dgwAccion.Rows.Remove(row);

                    }
                }
            }
        }
    }
}