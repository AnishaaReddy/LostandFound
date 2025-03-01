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
    public partial class AdminsForm : Form
    {
        public AdminsForm()
        {
            InitializeComponent();
        }

        private void homePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin_PostsListForm admin_PostsListForm = new Admin_PostsListForm();
            admin_PostsListForm.ShowDialog();
        }

        private void myPostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin_UsersListForm admin_UsersListForm = new Admin_UsersListForm();
            admin_UsersListForm.ShowDialog();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
