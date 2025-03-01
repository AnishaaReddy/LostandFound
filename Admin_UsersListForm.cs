using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LostandFound
{
    public partial class Admin_UsersListForm : Form
    {
        MySQLHelper mySQLHelper = new MySQLHelper();

        public Admin_UsersListForm()
        {
            InitializeComponent();
        }

        private void Admin_UsersListForm_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = 0;
            GetData();
        }


        public void GetData(string status = "All", string keys = "")
        {
            string sqls = $@"
select *
,'modify state' as Op1
from users where ('{status}'='All' or status='{status}') and ('{keys}'='' or Name like '%{keys}%')";
            DataTable dt = mySQLHelper.ExecuteQuery(sqls);

            this.dgvData.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetData(this.comboBox1.Text,this.textBox1.Text);
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) { return; }
                if (dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "modify state")
                {
                    string Id = dgvData.SelectedRows[0].Cells["id"].Value.ToString();

                    Admin_UpdateUserForm Admin_UpdateUserForm = new Admin_UpdateUserForm(Id);
                    Admin_UpdateUserForm.Owner = this;
                    Admin_UpdateUserForm.ShowDialog();
                }
            }
            catch
            {
                MessageBox.Show("system error");
            }
        }
    }
}
