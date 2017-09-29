using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OGCSOSCopier
{
    public partial class SelectProcedures : Form
    {

        private static List<string> seledtedProcedures;

        public SelectProcedures(List<string> procedureList)
        {
            InitializeComponent();
            seledtedProcedures = new List<string>();

            DataTable dt = new DataTable("Procedures");
            DataColumn identifierColumn = new DataColumn("procedure");
            DataColumn formatColumn = new DataColumn("format");

            dt.Columns.Add(identifierColumn);
            //dt.Columns.Add(formatColumn);

            DataRow row;
            foreach (var item in procedureList)
            {
                row = dt.NewRow();
                row["procedure"] = item;

                //ComboBox cmb = new ComboBox();
                //var list11 = new List<string>() { "10", "30", "80", "100" };
                //cmb.DataSource = list11;
                //row["format"] = cmb;
                //var format = row["format"];

                dt.Rows.Add(row);
            }

            dataGridView1.DataSource = dt;

            dataGridView1.Columns["procedure"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["procedure"].ReadOnly = true;
            dataGridView1.CellClick += DataGridView1_CellClick;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv == null) return;

            if (e.RowIndex >= 0 && dgv.Rows[e.RowIndex] != null)
            {
                if (dgv.Rows[e.RowIndex].Cells[0].Value == null || (bool)dgv.Rows[e.RowIndex].Cells[0].Value == false)
                {
                    dgv.Rows[e.RowIndex].Cells[0].Value = true;
                }
                else
                {
                    dgv.Rows[e.RowIndex].Cells[0].Value = false;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public List<string> SeledtedProcedures
        {
            get
            {
                return seledtedProcedures;
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            seledtedProcedures = new List<string>();
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if (item.Cells[0] != null && item.Cells[0].Value != null && (bool)item.Cells[0].Value == true)
                {
                    seledtedProcedures.Add(item.Cells[1].Value.ToString());
                }
            }


            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button_CANCEL_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button_selectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                item.Cells[0].Value = true;
            }
        }

        private void button_selectNone_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                item.Cells[0].Value = false;
            }
        }
    }
}
