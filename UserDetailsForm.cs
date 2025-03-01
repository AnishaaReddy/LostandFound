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

namespace LostandFound
{
    public partial class UserDetailsForm : Form
    {

        MySQLHelper mySQLHelper = new MySQLHelper();

        string Id = "";
        public UserDetailsForm(string _Id)
        {
            InitializeComponent();
            Id = _Id;
        }

        private void UserDetailsForm_Load(object sender, EventArgs e)
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


                }
            }
        }
    }
}
