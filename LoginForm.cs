using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LostandFound
{
    public partial class LoginForm : Form
    {
        MySQLHelper mySQLHelper = new MySQLHelper();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.UserType.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //验证
            string UsersName = this.UsersName.Text;
            string Password = this.Password.Text;
            string UserType = this.UserType.Text;


            //判断
            if (UsersName == "" || Password == "")
            {
                MessageBox.Show("Please enter username and password"); return;
            }

            try
            {
                MySqlParameter[] SqlPar =
                {
                    new MySqlParameter("@UsersName",UsersName),
                    new MySqlParameter("@Password",Password),
                };

                //Determine user roles
                string sql = $@"select * from users where Name=@UsersName and Password=@Password";
                if (UserType == "Admin")
                {
                    sql = $@"select * from admins where adminName=@UsersName and adminpwd=@Password";
                }

                DataTable dt = mySQLHelper.ExecuteQuery(sql, SqlPar);

                //Determine if it exists
                if (dt.Rows.Count > 0)
                {
                    if (UserType == "Admin")
                    {
                        MessageBox.Show("Login succeeded");

                        AdminsForm adminsForm = new AdminsForm();
                        adminsForm.ShowDialog();
                    }
                    else
                    {
                        if (dt.Rows[0]["Status"].ToString().Trim() != "normal")
                        {
                            MessageBox.Show("The account has been disabled. Please contact the administrator");
                            return;
                        }

                        MessageBox.Show("Login succeeded");


                        //Store data
                        Common.UsersID = dt.Rows[0]["UserID"].ToString();
                        UsersForm usersForm = new UsersForm();
                        usersForm.ShowDialog();

                    }

                }
                else
                {
                    MessageBox.Show("There is currently no such user available");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("system error");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }
    }
}
