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
    public partial class HomePageForm : Form
    {
        MySQLHelper mySQLHelper = new MySQLHelper();

        public HomePageForm()
        {
            InitializeComponent();
        }

        private void HomePageForm_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = 0;
        }


        public void GetData(string status = "All")
        {
            flowLayoutPanel1.Controls.Clear();

            string sqls = $@"
SELECT 
PostID
,ItemName
,Category
,Location
,Time
,PostType
,Status
,CreateTime
,ItemImage
FROM `posts` 
where ('{status}'='All' or PostType='{status}')
and (Status='Processing' or Status='Completed' )
order by PostID desc
";
            DataTable dt = mySQLHelper.ExecuteQuery(sqls);

            if (dt.Rows.Count == 0)
            {
                this.Tiplb.Text = "* No posts yet";
            }
            else
            {
                this.Tiplb.Text = "";
            }

            foreach (DataRow dr in dt.Rows)
            {
                
                Panel clonedPanel = new Panel();
                clonedPanel.BorderStyle = BorderStyle.FixedSingle;
                clonedPanel.Size = new Size(248, 289);

                #region PictureBox
                PictureBox pictureBox = new PictureBox();
                pictureBox.Location = new Point(40, 3);
                pictureBox.Size = new Size(159, 165);
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Image = new Bitmap(AppDomain.CurrentDomain.BaseDirectory + dr["ItemImage"].ToString());
                clonedPanel.Controls.Add(pictureBox);
                #endregion


                #region Item Name:

                Label label = new Label();
                label.Text = label6.Text;
                label.Size = label6.Size;
                label.Location = label6.Location;
                label.Font = label6.Font;
                clonedPanel.Controls.Add(label);



                string itemName = dr["ItemName"].ToString();
                if (itemName.Length > 21)
                {
                    itemName = itemName.Substring(0, 18) + "...";
                }
                Label label2 = new Label();
                label2.Text = itemName;
                label2.Size = label7.Size;
                label2.Location = label7.Location;
                clonedPanel.Controls.Add(label2);
                #endregion

                #region Post Type:
                Label label3 = new Label();
                label3.Text = label5.Text;
                label3.Size = label5.Size;
                label3.Location = label5.Location;
                label3.Font = label5.Font;
                clonedPanel.Controls.Add(label3);

                Label label_4 = new Label();
                label_4.Text = dr["PostType"].ToString(); ;
                label_4.Size = label8.Size;
                label_4.Location = label8.Location;
                clonedPanel.Controls.Add(label_4);
                #endregion

                #region Post Type:
                Label label_5 = new Label();
                label_5.Text = label4.Text;
                label_5.Size = label4.Size;
                label_5.Location = label4.Location;
                label_5.Font = label4.Font;
                clonedPanel.Controls.Add(label_5);

                string location = dr["location"].ToString();
                if (location.Length > 21)
                {
                    location = location.Substring(0, 18) + "...";
                }

                Label label_6 = new Label();
                label_6.Text = location;
                label_6.Size = label9.Size;
                label_6.Location = label9.Location;
                clonedPanel.Controls.Add(label_6);
                #endregion

                #region Button
                Button button = new Button();
                button.Text = button1.Text;
                button.Location = button1.Location;
                button.Size = button1.Size;
                button.Tag = dr["PostID"].ToString();
                button.Click += EnterDetails_Click;
                clonedPanel.Controls.Add(button);

                #endregion


                flowLayoutPanel1.Controls.Add(clonedPanel);

            }
        }

        private void EnterDetails_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                string id = clickedButton.Tag.ToString();
                PostDetailsForm postDetailsForm = new PostDetailsForm(id);
                postDetailsForm.ShowDialog();


            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData(this.comboBox1.Text);
        }
    }
}
