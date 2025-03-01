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
    public partial class Admin_PostsListForm : Form
    {
        MySQLHelper mySQLHelper = new MySQLHelper();

        public Admin_PostsListForm()
        {
            InitializeComponent();
        }

        private void Admin_PostsListForm_Load(object sender, EventArgs e)
        {
            GetData();
        }

        public void GetData(string status = "All", string keys = "")
        {
            string sqls = $@"
SELECT 
PostID
,ItemName
,Category
,Location
,Time
,PostType

,CreateTime
,'Examine and Approve' as `Op1`
,'Delete' as `Op2`
FROM `posts` 
where ('{status}'='All' or PostType='{status}')
and ('{keys}'='' or ItemName like '%{keys}%')
";
            DataTable dt = mySQLHelper.ExecuteQuery(sqls);

            this.dgvData.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetData(this.comboBox1.Text, this.textBox1.Text);
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) { return; }
                if (dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Examine and Approve")
                {
                    string Id = dgvData.SelectedRows[0].Cells["id"].Value.ToString();
                    string status = dgvData.SelectedRows[0].Cells[6].Value.ToString();

                    if (status== "Completed")
                    {
                        MessageBox.Show("This post has ended and cannot be modified");
                        return;
                    }

                    Admin_EAForm admin_EAForm = new Admin_EAForm(Id);
                    admin_EAForm.Owner = this;
                    admin_EAForm.ShowDialog();
                }
                else
                 if (dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
                {
                    string Id = dgvData.SelectedRows[0].Cells["id"].Value.ToString();
                    DeleteMsgForm deleteMsgForm = new DeleteMsgForm(Id);
                    deleteMsgForm.ShowDialog();
                    GetData();
                }
            }
            catch
            {
                MessageBox.Show("system error");
            }
        }
    }
}
