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

namespace LostandFound
{
    public partial class EditPostForm : Form
    {
        MySQLHelper mySQLHelper = new MySQLHelper();


        string Id = "";
        public EditPostForm(string _Id)
        {
            InitializeComponent();
            Id = _Id;
        }

        private void EditPostForm_Load(object sender, EventArgs e)
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
                    this.ItemImageTb.Text = dt.Rows[0]["ItemImage"].ToString();

                    pictureBox1.Image = new Bitmap(AppDomain.CurrentDomain.BaseDirectory+ this.ItemImageTb.Text);

                }

            }
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
                    new MySqlParameter("@PostID",Id),
                    new MySqlParameter("@ItemName",ItemName),
                    new MySqlParameter("@Category",Category),
                    new MySqlParameter("@Location",Location),
                    new MySqlParameter("@Time",Time),
                    new MySqlParameter("@PostType",PostType),
                    new MySqlParameter("@ItemImage",ItemImage),
                    new MySqlParameter("@Status","Audit"),
                };


                //Entering the database
                string sql = $@"update posts set  ItemName=@ItemName
,ItemName=@ItemName
,Category=@Category
,Location=@Location
,Time=@Time
,PostType=@PostType
,ItemImage=@ItemImage
,Status=@Status
where PostID=@PostID
";
                int re = mySQLHelper.ExecuteNonQuery(sql, SqlPar);

                if (re > 0)
                {
                    MessageBox.Show("Successfully modified");

                    MyPostForm myPostForm = (MyPostForm)this.Owner;
                    myPostForm.GetData();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Modification failed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("system error");
            }
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
    }
}
