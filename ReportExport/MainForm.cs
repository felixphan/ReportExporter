using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ReportExport
{
    public partial class MainForm : Form
    {
        private Service service;
        public MainForm()
        {
            InitializeComponent();
            // Initialize Service
            service = new Service();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Clear all data
            txtURL.Clear();
            txtUser.Clear();
            txtPassword.Clear();
            txtDBName.Clear();
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
            // Clear Resource
            dgvData.DataSource = null;
            dgvData.Rows.Clear();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                // Set up connection
                String sqlConnectionURL = "server=" + txtURL.Text + ";database=" + txtDBName.Text + ";UID=" + txtUser.Text + ";password=" + txtPassword.Text;
                //String sqlConnectionURL = "server=localhost;database=showroomhonda;UID=sa;password=1234";
                String sqlCommand = "SELECT * FROM ( SELECT *, ROW_NUMBER() OVER (ORDER BY FRAME_NO) as row FROm v_SCTT) a WHERE a.row > 5 and a.row <= 10";

                // Get Data
                List<Data> datas = service.getDatas(sqlConnectionURL, sqlCommand);

                // Binding Data to Data Grid View
                var bindingList = new BindingList<Data>(datas);
                var source = new BindingSource(bindingList, null);
                dgvData.DataSource = source;
                MessageBox.Show("Connect Succesful");
            }
            catch (Exception es)
            {
                MessageBox.Show(es.ToString());
            }
        }



        private void btnExportClick(object sender, EventArgs e)
        {
            service.export();
            MessageBox.Show("Export Succesfully");
        }
    }
}
