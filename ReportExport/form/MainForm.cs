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
        List<Data> datas;
        private Service service;
        private SettingForm settingForm;
        public MainForm()
        {
            InitializeComponent();
            // Initialize Service
            service = new Service();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Clear all data
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
            // Clear Resource
            dgvData.DataSource = null;
            dgvData.Rows.Clear();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            //TODO: Append data from header no and date to settingForm.txtSQL;
            String sqlCommand = "SELECT * FROM ( SELECT *, ROW_NUMBER() OVER (ORDER BY FRAME_NO) as row FROm v_SCTT) a WHERE a.row > 5 and a.row <= 10";

            // Get Data
            datas = service.getDatas(settingForm.sqlConnectionURL, sqlCommand);

            // Binding Data to Data Grid View
            var bindingList = new BindingList<Data>(datas);
            var source = new BindingSource(bindingList, null);
            dgvData.DataSource = source;
        }

        private void btnExportClick(object sender, EventArgs e)
        {
            service.export();
            MessageBox.Show("Export Succesfully");
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            settingForm = new SettingForm();
            settingForm.StartPosition = FormStartPosition.CenterParent;
            settingForm.ShowDialog(this);
        }
    }
}
