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
    public partial class DeleteMsgForm : Form
    {
        MySQLHelper mySQLHelper = new MySQLHelper();

        string id = "";
        public DeleteMsgForm(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string sqls = $@"delete from posts where postid='{id}'";
            int re = mySQLHelper.ExecuteNonQuery(sqls);
            if (re > 0)
            {
                MessageBox.Show("Successfully deleted");
                this.Close();
            }
            else
            {
                MessageBox.Show("Delete failed");
            }
        }
    }
}
