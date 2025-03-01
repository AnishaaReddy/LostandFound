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
    public partial class Admin_UpdateUserForm : Form
    {
        MySQLHelper mySQLHelper = new MySQLHelper();

        string Id = "";
        public Admin_UpdateUserForm(string _Id)
        {
            InitializeComponent();
            Id = _Id;
        }

        private void Admin_UpdateUserForm_Load(object sender, EventArgs e)
        {
            if (Id != "")
            {
                string sqls = $@"select * from users where UserID='{Id}'";
                DataTable dt = mySQLHelper.ExecuteQuery(sqls);
                if (dt.Rows.Count > 0)
                {

                    this.UsersName.Text = dt.Rows[0]["Name"].ToString();
                    this.Email.Text = dt.Rows[0]["Email"].ToString();
                    this.Phone.Text = dt.Rows[0]["PhoneNumber"].ToString();
                    this.AddressTb.Text = dt.Rows[0]["Address"].ToString();
                    this.comboBox1.Text = dt.Rows[0]["Status"].ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Status = this.comboBox1.Text;

            string sqls = $@"update users set Status='{Status}' where userid='{Id}'";
            int re = mySQLHelper.ExecuteNonQuery(sqls);

            if (re > 0)
            {
                MessageBox.Show("Successfully Operation");
                Admin_UsersListForm Admin_UsersListForm = (Admin_UsersListForm)this.Owner;
                Admin_UsersListForm.GetData();
                this.Close();
            }
            else
            {
                MessageBox.Show("Operation failed");
            }
        }
    }
}
