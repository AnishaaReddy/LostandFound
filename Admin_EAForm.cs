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
    public partial class Admin_EAForm : Form
    {
        MySQLHelper mySQLHelper = new MySQLHelper();

        string Id = "";

        public Admin_EAForm(string id)
        {
            InitializeComponent();
            this.Id = id;
        }

        private void Admin_EAForm_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = 0;

            if (Id != "")
            {
                string sqls = $@"select * from posts where PostID='{Id}'";
                DataTable dt = mySQLHelper.ExecuteQuery(sqls);
                if (dt.Rows.Count > 0)
                {

                    this.ItemNameTb.Text = dt.Rows[0]["ItemName"].ToString();
                    this.CategoryTb.Text = dt.Rows[0]["Category"].ToString();
                    this.LocationTb.Text = dt.Rows[0]["Location"].ToString();
                    this.TimeTb.Text = dt.Rows[0]["Time"].ToString();
                    this.PostTypeTb.Text = dt.Rows[0]["PostType"].ToString();
                    this.UserIDTb.Text = dt.Rows[0]["UserID"].ToString();

                    pictureBox1.Image = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + dt.Rows[0]["ItemImage"].ToString());

                    string Status = dt.Rows[0]["Status"].ToString();
                    if (dt.Rows[0]["Status"].ToString() == "Processing")
                    {
                        Status = "Approve";
                    }
                    this.comboBox1.Text = Status;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Status = "Processing";
            if (this.comboBox1.Text == "Decline")
            {
                Status = "Decline";
            }

            string sqls = $@"update posts set Status='{Status}' where postid='{Id}'";
            int re = mySQLHelper.ExecuteNonQuery(sqls);

            if (re > 0)
            {
                MessageBox.Show("Successfully Operation");
                Admin_PostsListForm admin_PostsListForm = (Admin_PostsListForm)this.Owner;
                admin_PostsListForm.GetData();
                this.Close();
            }
            else
            {
                MessageBox.Show("Operation failed");
            }
        }
    }
}
