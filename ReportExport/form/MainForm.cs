using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ReportExport
{
    public partial class MainForm : Form
    {
        List<Data> datas;
        private Service service;
        private SettingForm settingForm;
        private String sqlQuery;
        public MainForm()
        {
            InitializeComponent();
            pbLoading.Hide();
            dtpFrom.Value = DateTime.Now.AddDays(-30);
            dtpTo.Value = DateTime.Now;
            // Initialize Service
            service = new Service();
            //Disable Button
            this.btnView.Enabled = true;
            this.btnExport.Enabled = false;
            //Initial Setting Form
            settingForm = new SettingForm();
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

        private async void btnView_Click(object sender, EventArgs e)
        {
            btnView.Enabled = false;
            // Get Data
            pbLoading.Show();
            pbLoading.Update();
            // Query
            sqlQuery = settingForm.txtSQL.Text.Replace("{head_code}", settingForm.txtHeadCode.Text)
                .Replace("{from_date}", dtpFrom.Value.ToString("yyyy/MM/dd HH:mm:ss"))
                .Replace("{to_date}", dtpTo.Value.ToString("yyyy/MM/dd HH:mm:ss"));
            datas = await service.getDatas(settingForm.sqlConnectionURL, sqlQuery);
            pbLoading.Hide();
            // Binding Data to Data Grid View
            var bindingList = new BindingList<Data>(datas);
            var source = new BindingSource(bindingList, null);
            dgvData.DataSource = source;
            btnView.Enabled = true;
        }

        private async void btnExportClick(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            // Get data
            // Query
            sqlQuery = settingForm.txtSQL.Text.Replace("{head_code}", settingForm.txtHeadCode.Text)
                .Replace("{from_date}", dtpFrom.Value.ToString("yyyy/MM/dd HH:mm:ss"))
                .Replace("{to_date}", dtpTo.Value.ToString("yyyy/MM/dd HH:mm:ss"));
            datas = await service.getDatas(settingForm.sqlConnectionURL, sqlQuery);
            // Export
            string pathExport = settingForm.txtPathExport.Text;
            service.export(pathExport);
            MessageBox.Show("[SUCCESS] Export file: " + pathExport + "/ExportSCTT_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
            btnExport.Enabled = true;
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            settingForm.FormClosed += new FormClosedEventHandler(settingFormClosed);
            settingForm.StartPosition = FormStartPosition.CenterParent;
            settingForm.ShowDialog(this);
        }

        void settingFormClosed(object sender, FormClosedEventArgs e)
        {
            if (settingForm.isConnected)
            {
                this.btnView.Enabled = true;
                this.btnExport.Enabled = true;
            } else
            {
                this.btnView.Enabled = false;
                this.btnExport.Enabled = false;
            }
        }
    }
}
