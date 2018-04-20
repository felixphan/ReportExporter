using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ReportExport
{
    public partial class SettingForm : Form
    {
        public String sqlConnectionURL;
        public Boolean isConnected;
        public SettingForm()
        {
            InitializeComponent();
            // Initial DB_URL
            if(this.txtURL.Text == "")
            {
                this.txtURL.Text = resource.DB_URL;
            }
            // Initial User
            if (this.txtUser.Text == "")
            {
                this.txtUser.Text = resource.USER;
            }
            // Initial DB Name
            if(this.txtDBName.Text == "")
            {
                this.txtDBName.Text = resource.DB_NAME;
            }
            // Initial Password
            if(this.txtPassword.Text == "")
            {
                this.txtPassword.Text = resource.PASSWORD;
            }
            // Initial PathExport
            if (this.txtPathExport.Text == "")
            {
                this.txtPathExport.Text = resource.PATH_EXPORT;
            }
            // Initial txtSQL
            if (this.txtSQL.Text == "")
            {
                try
                {
                    StreamReader sr = new StreamReader(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\lib\\sql\\sql.txt");
                    var line = "";
                    //Read it line-by-line
                    while ((line = sr.ReadLine()) != null)
                    {
                        this.txtSQL.Text = this.txtSQL.Text + line + "\n";
                    }
                    sr.Close();
                }
                catch (Exception es)
                {
                    this.txtSQL.Clear();
                }
            }
            this.isConnected = false;
            this.pnSQL.Height = 0;
            this.pnButton.Location = new Point(this.pnButton.Location.X, this.pnButton.Location.Y - 350);
            this.Height = this.Height - 350;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                sqlConnectionURL = "server=" + txtURL.Text + ";database=" + txtDBName.Text + ";UID=" + txtUser.Text + ";password=" + txtPassword.Text;
                SqlConnection sqlConnection = new SqlConnection(sqlConnectionURL);
                sqlConnection.Open();
                sqlConnection.Close();
                this.isConnected = true;
                this.Close();
            }
            catch (Exception es)
            {
                MessageBox.Show("[FAILURE] Can not connect to database !");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.txtDBName.Clear();
            this.txtPassword.Clear();
            this.txtURL.Clear();
            this.txtUser.Clear();
            this.Close();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                String sqlConnectionURLTest = "server=" + txtURL.Text + ";database=" + txtDBName.Text + ";UID=" + txtUser.Text + ";password=" + txtPassword.Text;
                //String sqlConnectionURL = "server=localhost;database=showroomhonda;UID=sa;password=1234";
                SqlConnection sqlConnection = new SqlConnection(sqlConnectionURLTest);
                sqlConnection.Open();
                sqlConnection.Close();
                MessageBox.Show("[SUCCESS] Connect successful to database !");
            }
            catch (Exception es)
            {
                MessageBox.Show("[FAILURE] Can not connect to database !");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.pnSQL.Height == 0)
            {
                this.pnSQL.Height = 350;
                this.pnButton.Location = new Point(this.pnButton.Location.X, this.pnButton.Location.Y + 350);
                this.Height = this.Height + 350;
            } else
            {
                this.pnSQL.Height = 0;
                this.pnButton.Location = new Point(this.pnButton.Location.X, this.pnButton.Location.Y - 350);
                this.Height = this.Height - 350;
            }
        }
    }
}
