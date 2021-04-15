using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Net.Mail;
using System.Runtime.InteropServices;
using Outlook = Microsoft.Office.Interop.Outlook;
using Excel = Microsoft.Office.Interop.Excel;
using Logica;
using Datos;

namespace PlaybookSystem
{
    public partial class wfPickingProblem : Form
    {
        public string _lsProceso;
        private double _ldQty;
        public string _lsTicket;

        Globals _gs = new Globals();
        public wfPickingProblem()
        {
            InitializeComponent();
        }

        private void wfPickingProblem_Load(object sender, EventArgs e)
        {
            
            Inicio();
            if(!string.IsNullOrEmpty(_lsTicket))
            {
                txtFolio.Text = _lsTicket;
                CargarTicket(_lsTicket);
            }

        }
        private void Inicio()
        {
            try
            {

                _lsProceso = "PRO050";
                _ldQty = 0;

                txtFolio.Clear();
                dtpFecha.Value = DateTime.Today;
                txtRealizado.Text = Globals._gsUser;

                txtWO.Clear();
                txtItem.Clear();
                txtComp.Clear();
                txtCompDesc.Clear();                
                txtLocacion.Clear();
                txtFecha.Clear();
                txtFaltante.Clear();
                txtAutor.Text = string.Empty;
                txtBOM.Text = string.Empty;
                txtNotas.Text = string.Empty;
                

                Dictionary<string, string> Listt = new Dictionary<string, string>();
                Listt.Add("1", "1");
                Listt.Add("2", "2");
                cbbTurno.DataSource = new BindingSource(Listt, null);
                cbbTurno.DisplayMember = "Value";
                cbbTurno.ValueMember = "Key";
                cbbTurno.SelectedIndex = 0;

                Dictionary<string, string> List = new Dictionary<string, string>();
                List.Add("S", "Setup");
                List.Add("W", "Almacén");
                List.Add("P", "Producción");
                cbbArea.DataSource = new BindingSource(List, null);
                cbbArea.DisplayMember = "Value";
                cbbArea.ValueMember = "Key";
                cbbArea.SelectedIndex = -1;

                cbbLinea.DataSource = LineaRampeoLogica.LineasHr();
                cbbLinea.DisplayMember = "lineahr";
                cbbLinea.ValueMember = "lineahr";
                cbbLinea.SelectedIndex = -1;

                Dictionary<string, string> List1 = new Dictionary<string, string>();
                List1.Add("A", "A: Cantidad de Componente Corto");
                List1.Add("B", "B: Componentes Mezclados");
                List1.Add("C", "C: Componentes Sucios");
                List1.Add("D", "D: Componentes Dañados");
                List1.Add("E", "E: Componente Incorrecto");
                List1.Add("F", "F: Componente Faltante");
                List1.Add("G", "G: Componente Incompleto");
                List1.Add("H", "H: Otro (Excedente)");
                cbbMotivo.DataSource = new BindingSource(List1, null);
                cbbMotivo.DisplayMember = "Value";
                cbbMotivo.ValueMember = "Key";
                cbbMotivo.SelectedIndex = -1;

                Dictionary<string, string> List2 = new Dictionary<string, string>();
                List2.Add("1", "ERROR EN UNIDAD DE MEDIDA");
                List2.Add("2", "MAL CONTEO");
                List2.Add("3", "LOCACION ERRONEA");
                List2.Add("4", "RETORNO MAL IDENTIFICADO");
                List2.Add("5", "COMPONENTE SIMILAR");
                List2.Add("6", "PROVEEDOR");
                List2.Add("7", "MAL SURTIDO");
                List2.Add("8", "MULTIPARTE");
                List2.Add("9", "MATERIAL ESPECIAL");
                List2.Add("10", "PACK FACTOR");
                cbbCausa.DataSource = new BindingSource(List2, null);
                cbbCausa.DisplayMember = "Value";
                cbbCausa.ValueMember = "Key";
                cbbCausa.SelectedIndex = -1;

                cbbLote.DataSource = null;

                txtFolio.Focus();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        
        private void wfPickingProblem_Activated(object sender, EventArgs e)
        {
            txtFolio.Focus();
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Inicio();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!Valida())
                return;

            try
            {
                string sFolio = txtFolio.Text.ToString();
                PickingLogica pick = new PickingLogica();
                if (string.IsNullOrEmpty(sFolio))
                    pick.Folio = AccesoDatos.Consec(_lsProceso);
                else
                    pick.Folio = long.Parse(sFolio);

                pick.Fecha = dtpFecha.Value;
                pick.Area = cbbArea.SelectedValue.ToString();
                pick.WO = txtWO.Text.ToString();
                pick.QtyWO = _ldQty;
                pick.Turno = cbbTurno.SelectedValue.ToString();
                pick.Linea = cbbLinea.SelectedValue.ToString();
                pick.Item = txtItem.Text.ToString();
                pick.Componente = txtComp.Text.ToString();
                pick.CompDesc = txtCompDesc.Text.ToString();
                pick.Motivo = cbbMotivo.SelectedValue.ToString();
                pick.Concepto = cbbMotivo.Text.ToString();
                if(cbbCausa.SelectedIndex != -1)
                {
                    pick.Causa = cbbCausa.SelectedValue.ToString();
                    pick.CausaDesc = cbbCausa.Text.ToString();
                }
                pick.Comentario = txtNotas.Text.ToString();
                pick.Lote = cbbLote.Text.ToString();

                double dR = double.Parse(txtBOM.Text.ToString());
                double dF = double.Parse(txtFaltante.Text.ToString());
                double dS = dR - dF;
                double dD = dF / dR;

                pick.Requerido = dR;
                pick.Faltante = dF;
                pick.Surtido = dS;
                pick.Discrep = dD;

                int iPos = DateTime.Now.ToString().IndexOf(" ");
                int iL = 5;
                if (iPos == 9) iL = 4;
                string sHora = DateTime.Now.ToString().Substring(iPos + 1, iL);

                pick.Hora = sHora;
                pick.FechaD = txtFecha.Text.ToString();
                pick.Locacion = txtLocacion.Text.ToString();
                pick.Autor = txtAutor.Text.ToString();
                pick.Realizado = txtRealizado.Text.ToString();
                pick.Usuario = Globals._gsUser;

                if (PickingLogica.GuardarSP(pick) > 0)
                {
                    if(string.IsNullOrEmpty(sFolio))
                    {
                        ConfigLogica con = new ConfigLogica();
                        con.CN = Globals._gsCompany;
                        DataTable dt = ConfigLogica.Consultar(con);
                        string sFile = dt.Rows[0]["file_picking"].ToString();
                        //genera PDF
                        CreatePDF(sFile, pick.Folio.ToString());

                        //envia correo
                        string sTo = "";// "amontes@medline.com";
                        string sCc = "";// "Msantoyo@medline.com";
                        DataTable dtC = ConfigLogica.ConsultarDest(_lsProceso);
                        for (int x = 0; x < dtC.Rows.Count; x++)
                        {
                            string sTipo = dtC.Rows[x][2].ToString();
                            if (sTipo == "T")
                                sTo = dtC.Rows[x][1].ToString();
                            else
                                sCc = dtC.Rows[x][1].ToString();
                        }

                        if (cbbArea.SelectedValue.ToString() != "S" && cbbMotivo.SelectedValue.ToString() != "H")
                        {
                            //enviar mail
                            string sAsunto = "LINEA " + pick.Linea + " PICKING PROBLEM " + pick.Folio.ToString();
                            string sBody = "WO: " + pick.WO + Environment.NewLine + "COMPONENTE: " + pick.Componente + Environment.NewLine +
                                "CANTIDAD A SURTIR: " + pick.Faltante + Environment.NewLine +
                                "LOCACION A PICKEAR: " + pick.Locacion + Environment.NewLine +
                                "CONCEPTO: " + pick.Concepto + Environment.NewLine +
                                "COMENTARIOS: " + pick.Comentario + Environment.NewLine +
                                "---------" + Environment.NewLine;

                            AS4Logica as4 = new AS4Logica();
                            as4.CN = Globals._gsCompany;
                            as4.Comp = pick.Componente;
                            DataTable dtL = AS4Logica.FKLOCMSTData(as4);
                            if (dtL.Rows.Count > 0)
                            {
                                sBody += "Listado de locaciones sugeridas:" + Environment.NewLine +
                                "Locacion:     " + "Cantidad O/H:" + Environment.NewLine;
                                for (int x = 0; x < dtL.Rows.Count; x++)
                                {
                                    string sLocacion = dtL.Rows[x][2].ToString();
                                    string sCant = dtL.Rows[x][3].ToString();
                                    sBody += sLocacion + "      " + sCant + Environment.NewLine;
                                }
                            }

                            CreateEmailItem(sAsunto, sTo, sCc, sBody);
                        }

                        if (cbbMotivo.SelectedValue.ToString() == "H")
                        {
                            //correo excedente
                            string sAsunto = "LINEA " + pick.Linea + " PICKING PROBLEM " + pick.Folio.ToString();
                            string sBody = "MATERIAL EXCEDENTE" + Environment.NewLine +
                                "COMPONENTE: " + pick.Componente + Environment.NewLine +
                                "CANTIDAD EXCEDENTE: " + pick.Faltante + Environment.NewLine +
                                "LOCACION DE DONDE SE PICKEO: " + pick.Locacion + Environment.NewLine +
                                "---------" + Environment.NewLine;

                            CreateEmailItem(sAsunto, sTo, sCc, sBody);
                        }

                        MessageBox.Show("Picking Problem " + pick.Folio.ToString() + " registrado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtFolio.Text = pick.Folio.ToString();
                    }
                    else
                        MessageBox.Show("Picking Problem "+ pick.Folio.ToString() + " actualizado.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al intentar guardar. " + ex.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtFolio.Focus();
            }
            
        }

        private void CreateEmailItem(string subjectEmail,string toEmail, string ccEmail, string bodyEmail)
        {
            Outlook.Application app = new Outlook.Application();
            Outlook.MailItem eMail = app.CreateItem(Outlook.OlItemType.olMailItem);

            eMail.Subject = subjectEmail;
            eMail.To = toEmail;
            eMail.CC = ccEmail;
            eMail.Body = bodyEmail;
            eMail.Importance = Outlook.OlImportance.olImportanceLow;
            ((Outlook._MailItem)eMail).Send();
        }

        private bool Valida()
        {
            bool bValida = true;
            if (string.IsNullOrEmpty(txtWO.Text) || string.IsNullOrWhiteSpace(txtWO.Text))
                bValida = false;
             
             
            if (string.IsNullOrEmpty(txtComp.Text) || string.IsNullOrWhiteSpace(txtComp.Text))
                bValida = false;
            else
            {
                double iDura = 0;
                if (!double.TryParse(txtBOM.Text, out iDura))
                    bValida = false;

                double iFal = 0;
                if (!double.TryParse(txtFaltante.Text, out iFal))
                    bValida = false;

                
                
            }

            if (cbbArea.SelectedIndex == -1)
            {
                MessageBox.Show("Favor de ingresar el Area", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cbbArea.Focus();
                bValida = false;
            }

            if (cbbLinea.SelectedIndex == -1)
            {
                MessageBox.Show("Favor de ingresar la Linea", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cbbLinea.Focus();
                bValida = false;
            }

            if (cbbMotivo.SelectedIndex == -1)
            {
                MessageBox.Show("Favor de ingresar el Motivo", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cbbMotivo.Focus();
                bValida = false;
            }


            double dR = 0;
            if(!double.TryParse(txtBOM.Text.ToString(),out dR))
            {
                MessageBox.Show("Cantidad de BOM inválida", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtBOM.Focus();
                bValida = false;
            }

            double dF = 0;
            if(!double.TryParse(txtFaltante.Text.ToString(),out dF))
            {
                MessageBox.Show("Cantidad Faltante inválida", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtFaltante.Focus();
                bValida = false;
            }

            if(dF/dR < .15)
            {
                MessageBox.Show("No aplica como Picking Problem (Discrepancia menor a 15%); favor de ordenar Rush Picking.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                bValida = false;
            }

            return bValida;
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

                txtWO.Text = sValue;

                AS4Logica AS4 = new AS4Logica();
                AS4.CN = Globals._gsCompany;
                AS4.WO = sValue;
               
                DataTable dt = AS4Logica.WorkOrderData(AS4);
                if (dt.Rows.Count > 0)
                {
                    txtItem.Text = dt.Rows[0]["WOPN"].ToString();
                    _ldQty = double.Parse(dt.Rows[0]["WOQTY"].ToString());
                    cbbMotivo.Focus();
                }
                else
                {
                    MessageBox.Show("Orden no encontrada", "System Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Clear();
                    txtWO.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtWO_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        
        private void txtComp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            string sItem = txtItem.Text.ToString().TrimEnd();
            if (string.IsNullOrEmpty(sItem))
                return;

            string sValue = txtComp.Text.ToString().ToUpper();

            try
            {
                AS4Logica AS4 = new AS4Logica();
                AS4.CN = Globals._gsCompany;
                AS4.Item = sItem;
                AS4.Comp = sValue;
                //comp data
                DataTable dt = AS4Logica.FKITMSTRData(AS4);
                if (dt.Rows.Count > 0)
                {
                    txtCompDesc.Text = dt.Rows[0]["IMDSC"].ToString().TrimEnd().ToUpper();
                    txtFaltante.Focus();

                    //qty & alloc
                    AS4.CN = Globals._gsCompany;
                    AS4.Comp = sValue;
                    AS4.WO = txtWO.Text.ToString();
                    dt = AS4Logica.FMALOCATData(AS4);
                    if (dt.Rows.Count > 0)
                    {
                        txtBOM.Text = dt.Rows[0]["QTY"].ToString();
                        txtLocacion.Text = dt.Rows[0]["ALLOC"].ToString();
                        //user
                        dt = AS4Logica.FKITSAVEData(AS4);
                        if (dt.Rows.Count > 0)
                        {
                            txtFecha.Text = dt.Rows[0]["TSDATE"].ToString(); 
                            txtAutor.Text = dt.Rows[0]["USRPRF"].ToString(); 
                        }

                        dt = AS4Logica.FKLOTESData(AS4);
                        if (dt.Rows.Count > 0)
                        {
                            Dictionary<string, string> List = new Dictionary<string, string>();
                            
                            for (int x=0; x < dt.Rows.Count; x++)
                            {
                                string sLote = dt.Rows[x][0].ToString();
                                List.Add(sLote, sLote);
                            }

                            cbbLote.DataSource = new BindingSource(List, null);
                            cbbLote.DisplayMember = "Value";
                            cbbLote.ValueMember = "Key";
                            cbbLote.SelectedIndex = -1;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Componente no encontrado", "System Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCompDesc.Clear();
                    txtComp.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

       
        private void txtFolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtFolio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            string sItem = txtFolio.Text.ToString().TrimEnd();
            if (string.IsNullOrEmpty(sItem))
                return;

            CargarTicket(sItem);
        }
        private void CargarTicket(string _asFolio)
        {
            long lFolio = 0;
            if (!long.TryParse(_asFolio, out lFolio))
                lFolio = 0;

            if (lFolio > 0)
            {
                PickingLogica pick = new PickingLogica();
                pick.Folio = lFolio;
                DataTable dt = PickingLogica.Consultar(pick);
                if (dt.Rows.Count > 0)
                {
                    dtpFecha.Text = dt.Rows[0]["fecha"].ToString();
                    txtWO.Text = dt.Rows[0]["orden"].ToString();
                    txtItem.Text = dt.Rows[0]["parte"].ToString();
                    cbbArea.SelectedValue = dt.Rows[0]["area"].ToString();
                    cbbTurno.SelectedValue = dt.Rows[0]["turno"].ToString();
                    cbbLinea.SelectedValue = dt.Rows[0]["linea"].ToString();
                    cbbMotivo.SelectedValue = dt.Rows[0]["motivo"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[0]["motivo"].ToString()))
                        cbbCausa.SelectedValue = dt.Rows[0]["causa"].ToString();

                    txtNotas.Text = dt.Rows[0]["comentario"].ToString();
                    txtComp.Text = dt.Rows[0]["componente"].ToString();
                    txtCompDesc.Text = dt.Rows[0]["comp_desc"].ToString();
                    txtBOM.Text = dt.Rows[0]["cant_req"].ToString();
                    txtFaltante.Text = dt.Rows[0]["cant_rest"].ToString();
                    txtFecha.Text = dt.Rows[0]["fecha_disc"].ToString();
                    txtAutor.Text = dt.Rows[0]["capturado"].ToString();
                    txtRealizado.Text = dt.Rows[0]["realizado"].ToString();
                    txtLocacion.Text = dt.Rows[0]["locacion"].ToString();
                    cbbLote.Text = dt.Rows[0]["lote"].ToString();
                }
                else
                {
                    MessageBox.Show("Ticket no encontrado", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Folio inválido", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void CreatePDF(string _asFile,string _asFolio )
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                Excel.Application xlApp = new Excel.Application();
                xlApp.AskToUpdateLinks = false;
                Excel.Workbooks xlWorkbookS = xlApp.Workbooks;
                Excel.Workbook xlWorkbook = xlWorkbookS.Open(_asFile);

                Excel.Worksheet xlWorksheet = new Excel.Worksheet();
                
                int iSheets = xlWorkbook.Sheets.Count;
                int iSheet = 1;

                xlWorksheet = xlWorkbook.Sheets[iSheet];
                Excel.Range xlRange = xlWorksheet.UsedRange;
                
                xlWorksheet.Range["J4"].Value = _asFolio;
                
                xlWorksheet.Range["G8"].Value = cbbArea.Text.ToString();
                xlWorksheet.Range["D10"].Value = dtpFecha.Value.ToString();
                xlWorksheet.Range["G10"].Value = cbbTurno.SelectedValue.ToString();
                xlWorksheet.Range["J10"].Value = cbbLinea.SelectedValue.ToString();

                xlWorksheet.Range["F14"].Value = cbbMotivo.Text.ToString();
                xlWorksheet.Range["F15"].Value = cbbCausa.Text.ToString();
                xlWorksheet.Range["F17"].Value = txtNotas.Text.ToString();
                xlWorksheet.Range["C25"].Value = cbbMotivo.SelectedValue.ToString();
                xlWorksheet.Range["D25"].Value = txtComp.Text.ToString();
                xlWorksheet.Range["F25"].Value = txtCompDesc.Text.ToString();
                xlWorksheet.Range["I25"].Value = txtBOM.Text.ToString();
                xlWorksheet.Range["J25"].Value = txtFaltante.Text.ToString();
                xlWorksheet.Range["E26"].Value = txtFecha.Text.ToString();
                xlWorksheet.Range["G26"].Value = txtLocacion.Text.ToString();
                xlWorksheet.Range["J26"].Value = txtAutor.Text.ToString();
                xlWorksheet.Range["E27"].Value = txtRealizado.Text.ToString();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlRange);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkbook.Sheets[iSheet]);

                xlApp.DisplayAlerts = false;
                
                string sFile = DateTime.Today.Month.ToString() + "-" + DateTime.Today.Day.ToString() + "-" + DateTime.Today.Year.ToString() + "  PICKING PROBLEM " + txtWO.Text.ToString() + " " + txtComp.Text.ToString();
                string sPath = "\\\\mxcprdfp1\\Public\\MXC Picking Problem Electronic Requisition\\Picking Problem Electronic Forms\\";
                xlWorkbook.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, sPath + sFile);

                xlWorkbook.Close(false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkbookS);
                
                xlApp.DisplayAlerts = true;
                xlApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);

                Cursor = Cursors.Default;

                MessageBox.Show("Documento "+sFile+" creado!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                ex.ToString();
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            wfPickingTicketReport wfReport = new wfPickingTicketReport();
            wfReport.Show();
        }
    }
}
