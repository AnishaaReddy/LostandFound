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
    public partial class RegisterForm : Form
    {

        MySQLHelper mySQLHelper = new MySQLHelper();

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string UsersName = this.UsersName.Text;
            string Password = this.Password.Text;
            string rePassword = this.rePassword.Text;
            string Email = this.Email.Text;
            string Phone = this.Phone.Text;
            string Address = this.AddressTb.Text;

            //test and verify
            if (UsersName == "" || Password == ""|| rePassword == ""|| Email == ""|| Phone == ""|| Address == "")
            {
                MessageBox.Show("Please enter complete information"); return;
            }

            if (Password!= rePassword)
            {
                MessageBox.Show("Two different passwords"); return;
            }


            try
            {
                //Entering the database
                string sql = $@"insert into users values(Null,'{UsersName}','{Email}','{Address}','{Phone}','{Password}','normal')";

                int re = mySQLHelper.ExecuteNonQuery(sql);

                if (re > 0)
                {
                    MessageBox.Show("login was successful,Please log in");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("login has failed");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("system error");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
