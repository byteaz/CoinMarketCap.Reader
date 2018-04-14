using Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QueryEx
{
    public partial class frmSelectData : Form
    {
        DBUtil DB = new DBUtil();

        public string table;
        public string id;
        public string value;
        public string condition;
        public string order;

        public bool selected = false;
        public string selected_id;
        public string selected_value;

        public frmSelectData(string caption, string _selected_value, string _table, string _id, string _value, string _condition, string _order)
        {
            InitializeComponent();

            this.Text = caption;

            selected = false;

            table = _table;
            id = _id;
            value = _value;
            condition = _condition;
            order = _order;

            txtKeyword.Text = _selected_value;
        }

        public DataTable GetData(string keyword)
        {
            try
            {
                return DB.GetData("SELECT " + 
                                  id + " AS id, " + 
                                  value + " AS [value] "+
                                  "FROM " + table + 
                                  " WHERE " + condition + " AND "+
                                  value + " LIKE '%"+keyword.Replace("'",String.Empty)+"%'" +
                                  " ORDER BY " + order, null);
            }
            catch
            {
                return null;
            }
        }

        private void txtKeyword_TextChanged(object sender, EventArgs e)
        {
            gvMain.DataSource = GetData(txtKeyword.Text);
        }

        private void gvMain_DoubleClick(object sender, EventArgs e)
        {
            btnSelect_Click(sender, e);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (gvMain.SelectedRows.Count == 1)
            {
                DataGridViewRow row = gvMain.SelectedRows[0];

                selected = true;
                selected_id = row.Cells[0].Value.ToString();
                selected_value = row.Cells[1].Value.ToString();

                Close();
            }
        }

        private void txtKeyword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13)
            {
                gvMain.Focus();
            }
        }

        private void gvMain_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSelect_Click(sender, e);
            }
        }
    }
}
