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
            dtpFrom.Value = DateTime.Now.AddDays(-30);
            dtpTo.Value = DateTime.Now;
            // Initialize Service
            service = new Service();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Clear all data
            dtpFrom.Value = DateTime.Now.AddDays(-30);
            dtpTo.Value = DateTime.Now;
            // Clear Resource
            dgvData.DataSource = null;
            dgvData.Rows.Clear();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            //TODO: Append data from header no and date to settingForm.txtSQL;
            String defaultSql = settingForm.txtSQL.Text;
            String sqlCommand = defaultSql.Replace("{head_code}", txtRepairNo.Text)
                .Replace("{from_date}", dtpFrom.Value.ToString("yyyy/MM/dd HH:mm:ss"))
                .Replace("{to_date}", dtpTo.Value.ToString("yyyy/MM/dd HH:mm:ss"));

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
