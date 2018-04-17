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
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtURL.Clear();
            txtUser.Clear();
            txtPassword.Clear();
            txtDBName.Clear();
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
            dgvData.DataSource = null;
            dgvData.Rows.Clear();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dataTable = new DataTable();

                String sqlConnectionURL = "server=" + txtURL.Text + ";database=" + txtDBName.Text + ";UID=" + txtUser.Text + ";password=" + txtPassword.Text;
                //String sqlConnectionURL = "server=localhost;database=showroomhonda;UID=sa;password=1234";
                String sqlCommand = "SELECT * FROM ( SELECT *, ROW_NUMBER() OVER (ORDER BY FRAME_NO) as row FROm v_SCTT) a WHERE a.row > 5 and a.row <= 10";
                Service service = new Service();
                List<Data> datas = service.getDatas(sqlConnectionURL, sqlCommand);
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
            Service service = new Service();
            service.export();
            MessageBox.Show("Export Succesfully");
        }
    }
}
