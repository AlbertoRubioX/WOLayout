﻿using System;
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
using Datos;

namespace PlaybookSystem
{
    public partial class wfDhPending : Form
    {
        public string _lsProceso;
        public string _lsFiltro;
        Globals _gs = new Globals();
        public wfDhPending()
        {
            InitializeComponent();
        }

        private void wfDhPending_Load(object sender, EventArgs e)
        {
            _lsProceso = "PRO050";
             
            Inicio();
            lbl01.Text = "Reporte de DHR";
            if (_lsFiltro == "D")
            {
                lbl01.Text = "Reporte de DHR Detenidos";
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;

                DhTrackerLogica dh = new DhTrackerLogica();
                if(chLinea.Checked)
                {
                    dh.TipoLinea = "U";
                    dh.Linea = cbLinea.SelectedValue.ToString();
                }
                else
                    dh.TipoLinea = "A";


                //dh.Estatus = cbEstatus.SelectedValue.ToString();
                chEstatus.Checked = true;
                cbEstatus.SelectedIndex = 4;
                dgwData.DataSource = DhTrackerLogica.VistaReporteDet(dh);
                CargarColumnasDet();
            }
            
        }
        private void Inicio()
        {
            try
            {
                
                dgwData.DataSource = null;

                chFecha.Checked = false;
                dtFechaFin.Enabled = false;
                dtFechaIni.Enabled = false;
                dtFechaFin.ResetText();
                dtFechaIni.ResetText();

                chLinea.Checked = false;
                cbLinea.Enabled = false;
                cbLinea.DataSource = LineaRampeoLogica.ListarDrop();
                cbLinea.ValueMember = "linehr";
                cbLinea.DisplayMember = "linehr";
                cbLinea.SelectedIndex = -1;

                chEstatus.Checked = false;
                cbEstatus.Enabled = false;
                Dictionary<string, string> List = new Dictionary<string, string>();
                List.Add("1", "Clean Room");
                List.Add("2", "Boxing");
                List.Add("3", "DHR");
                List.Add("4", "Escaneo");
                List.Add("0", "Detenidos");
                cbEstatus.DataSource = new BindingSource(List, null);
                cbEstatus.DisplayMember = "Value";
                cbEstatus.ValueMember = "Key";
                cbEstatus.SelectedIndex = -1;

                CargarColumnas();

                tssTotal.Text = "0";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    
        private void wfDhPending_Activated(object sender, EventArgs e)
        {
            groupBox1.Focus();
            
        }
        
        private void CargarColumnas()
        {
            int iRows = dgwData.Rows.Count;
            if (iRows == 0)
            {
                DataTable dtNew = new DataTable("Comp");
                dtNew.Columns.Add("Linea", typeof(string));
                dtNew.Columns.Add("Work Order", typeof(string));
                dtNew.Columns.Add("Descripción", typeof(string));
                dtNew.Columns.Add("Estatus", typeof(string));
                dtNew.Columns.Add("Fecha C.R.", typeof(DateTime));
                dtNew.Columns.Add("Fecha Escaneo", typeof(DateTime));
                dtNew.Columns.Add("Duracion Hrs", typeof(int));
                dtNew.Columns.Add("Inspector", typeof(string));
                dtNew.Columns.Add("Detenido", typeof(bool));
                dgwData.DataSource = dtNew;
            }

            dgwData.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgwData.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dgwData.Columns[0].Width = ColumnWith(dgwData, 5);

            dgwData.Columns[1].Width = ColumnWith(dgwData, 10);
            dgwData.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgwData.Columns[2].Width = ColumnWith(dgwData, 25);//descrip
            dgwData.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            dgwData.Columns[3].Width = ColumnWith(dgwData, 10);//estatus
            dgwData.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgwData.Columns[4].Width = ColumnWith(dgwData, 10);//facha ini
            dgwData.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgwData.Columns[5].Width = ColumnWith(dgwData, 10);//fechafin
            dgwData.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgwData.Columns[6].Width = ColumnWith(dgwData, 10);//duracion
            dgwData.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgwData.Columns[6].DefaultCellStyle.Format = "0";

            dgwData.Columns[7].Width = ColumnWith(dgwData, 10);//inspector
            dgwData.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            

            dgwData.Columns[8].Width = ColumnWith(dgwData, 10);//detenido
            dgwData.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void CargarColumnasDet()
        {
            int iRows = dgwData.Rows.Count;
            if (iRows == 0)
            {
                DataTable dtNew = new DataTable("Comp");
                dtNew.Columns.Add("Linea", typeof(string));
                dtNew.Columns.Add("Work Order", typeof(string));
                dtNew.Columns.Add("Descripción", typeof(string));
                dtNew.Columns.Add("Estatus", typeof(string));
                dtNew.Columns.Add("En DHR Desde", typeof(DateTime));
                dtNew.Columns.Add("Duracion Hrs", typeof(int));
                dtNew.Columns.Add("Inspector", typeof(string));
                dtNew.Columns.Add("Detenido", typeof(bool));
                dgwData.DataSource = dtNew;
            }

            dgwData.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgwData.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            dgwData.Columns[0].Width = ColumnWith(dgwData, 5);

            dgwData.Columns[1].Width = ColumnWith(dgwData, 10);
            dgwData.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgwData.Columns[2].Width = ColumnWith(dgwData, 30);//descrip
            dgwData.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgwData.Columns[3].Width = ColumnWith(dgwData, 10);//estatus
            dgwData.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgwData.Columns[4].Width = ColumnWith(dgwData, 15);//desde
            dgwData.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgwData.Columns[5].Width = ColumnWith(dgwData, 10);//duracion
            dgwData.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgwData.Columns[5].DefaultCellStyle.Format = "0";

            dgwData.Columns[6].Width = ColumnWith(dgwData, 10);//inspector
            dgwData.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            dgwData.Columns[7].Width = ColumnWith(dgwData, 10);//detenido
            dgwData.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgwData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
        
        private void dgwData_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
        
        }

      
        private void dgwData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int iRow = e.RowIndex;
            string sValue = e.Value.ToString();

            sValue = dgwData[4, e.RowIndex].Value.ToString();
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

            //sValue = dgwData[2, e.RowIndex].Value.ToString();
            //if (sValue.IndexOf("PRE-") != -1)
            //{
            //    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Italic);
            //    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
            //    //e.CellStyle.BackColor = Color.WhiteSmoke;
            //}
        }

        private void wfDhPending_Resize(object sender, EventArgs e)
        {

            panel1.Height = this.Height - 130;
            panel1.Width = this.Width - 30;

            groupBox3.Height = this.Height - 300;
            groupBox3.Width = this.Width - 70;

            dgwData.Height = this.Height - 320;
            dgwData.Width = this.Width - 80;

            CargarColumnas();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Inicio();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chFecha_CheckedChanged(object sender, EventArgs e)
        {
            dtFechaIni.Enabled = chFecha.Checked;
            dtFechaFin.Enabled = chFecha.Checked;
        }

        private void chLinea_CheckedChanged(object sender, EventArgs e)
        {
            cbLinea.Enabled = chLinea.Checked;
        }

        private void chEstatus_CheckedChanged(object sender, EventArgs e)
        {
            cbEstatus.Enabled = chEstatus.Checked;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btLoad_Click(object sender, EventArgs e)
        {
            DhTrackerLogica dh = new DhTrackerLogica();
            
            if(_lsFiltro=="D")
            {
                dgwData.DataSource = DhTrackerLogica.VistaReporteDet(dh);
                CargarColumnasDet();
            }
            else
            {
                if (chFecha.Checked)
                {
                    dh.TipoFecha = "1";
                    dh.FechaIni = dtFechaIni.Value;
                    dh.FechaFin = dtFechaFin.Value;
                }
                else
                    dh.TipoFecha = "0";

                if (chLinea.Checked)
                {
                    dh.TipoLinea = "1";
                    dh.Linea = cbLinea.SelectedValue.ToString();
                }
                else
                    dh.TipoLinea = "0";

                if (chEstatus.Checked)
                {
                    dh.TipoEstatus = "1";
                    dh.Estatus = int.Parse(cbEstatus.SelectedValue.ToString());
                }
                else
                    dh.TipoEstatus = "0";

                dgwData.DataSource = DhTrackerLogica.VistaReporte(dh);
                tssTotal.Text = dgwData.Rows.Count.ToString();
                CargarColumnas();
            }

            tssTotal.Text = dgwData.Rows.Count.ToString();
        }

        private void copyAlltoClipboard()
        {
            dgwData.SelectAll();
            DataObject dataObj = dgwData.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            copyAlltoClipboard();
            Excel.Application xlexcel;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

            Cursor = Cursors.Default;
        }
    }
}
