using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Logica;

namespace WOLayout
{
    public class Globals
    {
        public static string _gsLocation;
        public static string _gsUser;
        public static string _gsLang;

        public void ControlText(string _asFormName,Control _control)
        {
            ConfigLogica con = new ConfigLogica();
            con.Language = _gsLang;
            con.Form = _asFormName;

            foreach (Control c in _control.Controls)
            {
                if (c is GroupBox)
                {
                    con.Control = c.Name;
                    string sValue = ConfigLogica.ChangeLanguageCont(con);
                    if (!string.IsNullOrEmpty(sValue))
                        c.Text = sValue;
                }

                foreach (Control cs in c.Controls)
                {
                    if (cs is Label || cs is GroupBox || cs is CheckBox)
                    {
                        con.Control = cs.Name;
                        string sValue = ConfigLogica.ChangeLanguageCont(con);
                        if (!string.IsNullOrEmpty(sValue))
                            cs.Text = sValue;
                    }
                }
            }
        }
        public void ControlGridText(string _asFormName,DataGridView _control)
        {
            ConfigLogica con = new ConfigLogica();
            con.Language = _gsLang;
            con.Form = _asFormName;
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
        public string ControlGridRows(string _asFormName,Control _control, string _asRow)
        {
            ConfigLogica con = new ConfigLogica();
            con.Language = _gsLang;
            con.Form = _asFormName;
            con.Control = _control.Name;
            con.SubControl = _asRow;
            string sValue = ConfigLogica.ChangeLanguageGrid(con);
            if (!string.IsNullOrEmpty(sValue))
                return sValue;

            return null;
        }
        public void ControlGridRows2(string _asFormName,DataGridView _control)
        {
            ConfigLogica con = new ConfigLogica();
            con.Language = _gsLang;
            con.Form = _asFormName;
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
        public string MessageText(string _asFormName, string _control, string _asMessage)
        {
            ConfigLogica con = new ConfigLogica();
            con.Language = _gsLang;
            con.Form = _asFormName;
            con.Control = _control;
            con.SubControl = _asMessage;
            string sValue = ConfigLogica.ChangeLanguageGrid(con);
            if (!string.IsNullOrEmpty(sValue))
                return sValue;

            return null;
        }
    }
}
