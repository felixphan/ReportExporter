using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportExport
{
    public partial class SettingForm : Form
    {
        public String sqlConnectionURL;
        public SettingForm()
        {
            InitializeComponent();
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

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                sqlConnectionURL = "server=" + txtURL.Text + ";database=" + txtDBName.Text + ";UID=" + txtUser.Text + ";password=" + txtPassword.Text;
                //String sqlConnectionURL = "server=localhost;database=showroomhonda;UID=sa;password=1234";
                SqlConnection sqlConnection = new SqlConnection(sqlConnectionURL);
                sqlConnection.Open();
                sqlConnection.Close();
            }
            catch (Exception es)
            {
                MessageBox.Show("Connect Failed");
            }
            finally
            {
                this.Close();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.txtDBName.Clear();
            this.txtPassword.Clear();
            this.txtURL.Clear();
            this.txtUser.Clear();
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
    }
}
