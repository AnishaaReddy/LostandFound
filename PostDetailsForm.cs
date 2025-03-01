using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LostandFound
{
    public partial class PostDetailsForm : Form
    {

        MySQLHelper mySQLHelper = new MySQLHelper();


        string Id = "";
        public PostDetailsForm(string _Id)
        {
            InitializeComponent();
            Id = _Id;
        }

        private void PostDetailsForm_Load(object sender, EventArgs e)
        {
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
                    this.StatusTb.Text = dt.Rows[0]["Status"].ToString();
                    
                    pictureBox1.Image = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + dt.Rows[0]["ItemImage"].ToString());

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.StatusTb.Text== "Completed")
            {
                MessageBox.Show("This post has ended");
                return;
            }

            UserDetailsForm userDetailsForm = new UserDetailsForm(this.UserIDTb.Text);
            userDetailsForm.ShowDialog();
        }
    }
}
