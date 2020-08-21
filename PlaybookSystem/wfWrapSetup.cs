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
    public partial class wfWrapSetup : Form
    {
        public string _lsWrap;
        public string _lsSize;
        public string _lsFold;
        public double _ldTime;

        public wfWrapSetup()
        {
            InitializeComponent();
        }
        private string getWrapDesc(string _asWrap)
        {
            string sWrapDesc = string.Empty;
            if (_asWrap == "1") sWrapDesc = "ENVELOP";
            if (_asWrap == "2") sWrapDesc = "VERTICAL";
            if (_asWrap == "3") sWrapDesc = "HORIZONTAL";

            return sWrapDesc;
        }
        private void wfWrapSetup_Load(object sender, EventArgs e)
        {
            try
            {
                string sWrapTile = getWrapDesc(_lsWrap);

                if (string.IsNullOrEmpty(sWrapTile))
                {
                    MessageBox.Show("Wrap type not found",Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
                    Close();
                    return;
                }
                    

                btWrap.Text = sWrapTile;

                ConfigWrapLogica data = new ConfigWrapLogica();
                data.CN = Globals._gsCompany;
                data.Wrap = _lsWrap;

                //load size & fold type
                DataTable dt = ConfigWrapLogica.ConsultarWrap(data);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Wrap configuration not found", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                    return;
                }

                rbtL.Visible =false;
                rbtM.Visible = false;
                rbtS.Visible = false;
                
                cbbFold.DataSource = null;

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    string sSize = dt.Rows[x][2].ToString();
                    if (sSize == "L")
                        rbtL.Visible = true;
                
                    if (sSize == "M")
                        rbtM.Visible = true;
                        
                    
                    if (sSize == "S")
                        rbtS.Visible = true;
                    
                    
                }
                if (!rbtL.Visible && !rbtM.Visible && !rbtS.Visible)
                {
                    panel3.Visible = false;
                    panel4.Location = new Point(19, 66);
                    panel1.Height = 160;
                    this.Height = 220;
                    //laod cbbFold
                    _lsSize = "N";
                    FillType();
                }

                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

       
        private void FillType()
        {

            ConfigWrapLogica data = new ConfigWrapLogica();
            data.CN = Globals._gsCompany;
            data.Wrap = _lsWrap;
            data.Size = _lsSize;
            //load size & fold type
            DataTable dt = ConfigWrapLogica.VistaSize(data);
            cbbFold.DataSource = dt;
            cbbFold.ValueMember = "fold";
            cbbFold.DisplayMember = "fold";
            cbbFold.SelectedIndex = -1;

            if(dt.Rows.Count == 1)
            {
                _lsFold = dt.Rows[0][0].ToString();
                _ldTime = double.Parse(dt.Rows[0][1].ToString());
                Close();
            }
            
        }
        private void rbtS_Click(object sender, EventArgs e)
        {
            _lsSize = "S";
            FillType();
        }

        private void cbbFold_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(cbbFold.SelectedIndex >= 0)
            {
                _lsFold = cbbFold.SelectedValue.ToString();
                ConfigWrapLogica data = new ConfigWrapLogica();
                data.CN = Globals._gsCompany;
                data.Wrap = _lsWrap;
                data.Size = _lsSize;
                data.Fold = _lsFold;
                //load size & fold type
                DataTable dt = ConfigWrapLogica.ConsultaFold(data);
                if(dt.Rows.Count > 0)
                {
                    _ldTime = double.Parse(dt.Rows[0][0].ToString());
                    Close();
                }
                else
                {
                    MessageBox.Show("No fold time configured", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Close();
                }
            }
        }

        private void rbtM_CheckedChanged(object sender, EventArgs e)
        {
            _lsSize = "M";
            FillType();
        }

        private void rbtL_CheckedChanged(object sender, EventArgs e)
        {
            _lsSize = "L";
            FillType();
        }
    }
}
