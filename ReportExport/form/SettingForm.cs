using Microsoft.Win32;
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
                var url = Registry.GetValue(@"HKEY_CURRENT_USER\ReportExport", "url", "");
                if (url ==null)
                {
                    this.txtURL.Text = resource.DB_URL;
                } else
                {
                    this.txtURL.Text = url.ToString();
                }
            }
            // Initial User
            if (this.txtUser.Text == "")
            {
                var user = Registry.GetValue(@"HKEY_CURRENT_USER\ReportExport", "user", "");
                if (user == null)
                {
                    this.txtUser.Text = resource.USER;
                }
                else
                {
                    this.txtUser.Text = user.ToString();
                }
            }
            // Initial DB Name
            if(this.txtDBName.Text == "")
            {
                var dbName = Registry.GetValue(@"HKEY_CURRENT_USER\ReportExport", "dbName", "");
                if (dbName == null)
                {
                    this.txtDBName.Text = resource.DB_NAME;
                }
                else
                {
                    this.txtDBName.Text = dbName.ToString();
                }
            }
            // Initial Password
            if(this.txtPassword.Text == "")
            {
                var password = Registry.GetValue(@"HKEY_CURRENT_USER\ReportExport", "password", "");
                if (password == null)
                {
                    this.txtPassword.Text = resource.PASSWORD;
                }
                else
                {
                    this.txtPassword.Text = password.ToString();
                }
            }
            // Initial PathExport
            if (this.txtPathExport.Text == "")
            {
                var pathExport = Registry.GetValue(@"HKEY_CURRENT_USER\ReportExport", "pathExport", "");
                if (pathExport == null)
                {
                    this.txtPathExport.Text = resource.PATH_EXPORT;
                }
                else
                {
                    this.txtPathExport.Text = pathExport.ToString();
                }
            }
            // Initial txtSQL
            if (this.txtSQL.Text == "")
            {
                var sql = Registry.GetValue(@"HKEY_CURRENT_USER\ReportExport", "sql", "");
                if (sql == null)
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
                } else
                {
                    this.txtSQL.Text = sql.ToString();
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
                Registry.SetValue(@"HKEY_CURRENT_USER\ReportExport", "url", txtURL.Text);
                Registry.SetValue(@"HKEY_CURRENT_USER\ReportExport", "user", txtUser.Text);
                Registry.SetValue(@"HKEY_CURRENT_USER\ReportExport", "dbName", txtDBName.Text);
                Registry.SetValue(@"HKEY_CURRENT_USER\ReportExport", "password", txtPassword.Text);
                Registry.SetValue(@"HKEY_CURRENT_USER\ReportExport", "pathExport", txtPathExport.Text);
                Registry.SetValue(@"HKEY_CURRENT_USER\ReportExport", "sql", txtSQL.Text);
                this.Close();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.StackTrace);
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
