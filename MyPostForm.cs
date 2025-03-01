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
    public partial class MyPostForm : Form
    {
        MySQLHelper mySQLHelper = new MySQLHelper();

        public MyPostForm()
        {
            InitializeComponent();
        }

        private void MyPostForm_Load(object sender, EventArgs e)
        {
            GetData();
        }

        public void GetData(string status= "All", string keys = "")
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
,'Edit' as `Op1`
,'Delete' as `Op2`
,'End_Post' as `Op3`
FROM `posts` 
where ('{status}'='All' or Status='{status}')
and ('{keys}'='' or ItemName like '%{keys}%')
order by PostID desc
";
            DataTable dt = mySQLHelper.ExecuteQuery(sqls);

            this.dgvData.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddPostForm addPostForm = new AddPostForm();
            addPostForm.Owner = this;
            addPostForm.ShowDialog();
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
                if (dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Edit")
                {
                    string Id = dgvData.SelectedRows[0].Cells["id"].Value.ToString();
                    EditPostForm editPostForm = new EditPostForm(Id);
                    editPostForm.Owner = this;
                    editPostForm.ShowDialog();
                }
                else
                 if (dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete")
                {
                    string Id = dgvData.SelectedRows[0].Cells["id"].Value.ToString();
                    DeleteMsgForm deleteMsgForm = new DeleteMsgForm(Id);
                    deleteMsgForm.ShowDialog();
                    GetData();
                }
                else if (dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "End_Post")
                {
                    string Id = dgvData.SelectedRows[0].Cells["id"].Value.ToString();
                    string status = dgvData.SelectedRows[0].Cells[6].Value.ToString();


                    if (status == "Audit")
                    {
                        MessageBox.Show("Post pending review");
                        return;
                    }

                    string sqls = $@"update posts set Status='Completed' where postid='{Id}'";
                    int re = mySQLHelper.ExecuteNonQuery(sqls);
                    if (re > 0)
                    {
                        MessageBox.Show("Successfully Operation");
                        GetData();
                    }
                    else
                    {
                        MessageBox.Show("Operation failed");
                    }
                }

            }
            catch
            {
                MessageBox.Show("system error");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
    