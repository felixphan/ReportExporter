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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(MainForm));
        public MainForm()
        {
            try
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
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                // Clear all data
                dtpFrom.Value = DateTime.Now.AddDays(-30);
                dtpTo.Value = DateTime.Now;
                // Clear Resource
                dgvData.DataSource = null;
                dgvData.Rows.Clear();
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnView_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnExportClick(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            try
            {
                settingForm.FormClosed += new FormClosedEventHandler(settingFormClosed);
                settingForm.StartPosition = FormStartPosition.CenterParent;
                settingForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                MessageBox.Show(ex.Message);
            }
        }

        void settingFormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (settingForm.isConnected)
                {
                    this.btnView.Enabled = true;
                    this.btnExport.Enabled = true;
                }
                else
                {
                    this.btnView.Enabled = false;
                    this.btnExport.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.StackTrace);
                MessageBox.Show(ex.Message);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
