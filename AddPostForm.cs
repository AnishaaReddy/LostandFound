using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace LostandFound
{
    public partial class AddPostForm : Form
    {
        MySQLHelper mySQLHelper = new MySQLHelper();

        public AddPostForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Using the OpenFileDialog control to select an image file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image file|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedImagePath = openFileDialog.FileName;
                pictureBox1.Image = new Bitmap(selectedImagePath);

                string saveImagePath = AppDomain.CurrentDomain.BaseDirectory + "\\File";
                if (!Directory.Exists(saveImagePath))
                {
                    Directory.CreateDirectory(saveImagePath);
                }

                string saveFileName = Path.GetFileName(selectedImagePath);
                string savePath = Path.Combine(saveImagePath, saveFileName);

                try
                {
                    File.Copy(selectedImagePath, savePath, true);

                    ItemImageTb.Text = Path.Combine("\\File", saveFileName);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("system error");
                }
            }
        }

        private void AddPostForm_Load(object sender, EventArgs e)
        {
            this.PostTypeTb.SelectedIndex = 0;
            this.CategoryTb.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ItemName = this.ItemNameTb.Text;
            string Category = this.CategoryTb.Text;
            string Location = this.LocationTb.Text;
            string Time = this.TimeTb.Text;
            string PostType = this.PostTypeTb.Text;
            string ItemImage = this.ItemImageTb.Text;


            if (ItemName == "" || Location == "" || Time == "" || ItemImage == "")
            {
                MessageBox.Show("Please enter complete information"); return;
            }


            try
            {
                MySqlParameter[] SqlPar =
                {
                    new MySqlParameter("@ItemName",ItemName),
                    new MySqlParameter("@Category",Category),
                    new MySqlParameter("@Location",Location),
                    new MySqlParameter("@Time",Time),
                    new MySqlParameter("@PostType",PostType),
                    new MySqlParameter("@ItemImage",ItemImage),
                    new MySqlParameter("@UserID",Common.UsersID),
                    new MySqlParameter("@Status",""),
                };


                //Entering the database
                string sql = $@"insert into posts(ItemName,Category,Location,Time,PostType,ItemImage,Status,UserID) values(@ItemName,@Category,@Location,@Time,@PostType,@ItemImage,@Status,@UserID)";

                int re = mySQLHelper.ExecuteNonQuery(sql,SqlPar);

                if (re > 0)
                {
                    MessageBox.Show("Added successfully");

                    MyPostForm myPostForm=(MyPostForm)this.Owner;
                    myPostForm.GetData();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Add failed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("system error");
            }
             
        }
    }
}
