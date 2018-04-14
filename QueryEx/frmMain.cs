using Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QueryEx
{
    public partial class frmMain : Form
    {
        DBUtil DB = new DBUtil();
        public string selected_value = String.Empty;
       
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            gvQuery.DataSource = DB.GetData("SELECT id_query, query_name FROM query ORDER BY order_no",null);
        }

        private void gvQuery_DoubleClick(object sender, EventArgs e)
        {
            if (gvQuery.SelectedRows.Count == 1)
            {
                DataGridViewRow row = gvQuery.SelectedRows[0];

                DataTable DT = new DataTable();
                DT = DB.GetData("SELECT query FROM query WHERE id_query=" + row.Cells[0].Value.ToString(), null);

                if (DT.Rows.Count == 1)
                {
                    txtQuery.Text = DT.Rows[0]["query"].ToString();
                }
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if ((txtQuery.Text.ToLower().IndexOf("delete") >= 0) ||
                (txtQuery.Text.ToLower().IndexOf("update") >= 0) ||
                (txtQuery.Text.ToLower().IndexOf("truncate") >= 0) || 
                ((txtQuery.Text.ToLower().IndexOf("insert") >= 0) && (txtQuery.Text.ToLower().IndexOf("into") >= 0) && (txtQuery.Text.ToLower().IndexOf("@result") > txtQuery.Text.ToLower().IndexOf("into"))))
            {
                MessageBox.Show("Error...");
                return;
            }

            try
            {
                string sql = txtQuery.Text;

                if (sql.IndexOf("{id}")>=0)
                {
                    frmSelectData frmSelect = new frmSelectData("Valyutanı seç",selected_value, "coin", "id", "name", "1=1", "rank");
                    frmSelect.ShowDialog();

                    if (frmSelect.selected)
                    {
                        selected_value = frmSelect.selected_value;
                        sql = sql.Replace("{id}", frmSelect.selected_id);
                    }
                }

                gvMain.DataSource = DB.GetData(sql, null);
                ssResultCount.Text = gvMain.Rows.Count.ToString();

                gvMain.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtQuery.Text = String.Empty;
        }

        private void txtQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnRun_Click(sender, e);
            }
        }

        private void txtQuery_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }

        private void gvQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnRun_Click(sender, e);
            }
        }

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnRun_Click(sender, e);
            }
        }
    }
}
