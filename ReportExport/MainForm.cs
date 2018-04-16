using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
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
    public partial class MainForm : Form
    {
        private List<Data> datas;
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
                this.datas = new List<Data>();
                //                String sqlConnectionURL = "server=" + txtURL.Text + ";database=" + txtDBName.Text + ";UID=" + txtUser.Text + ";password=" + txtPassword.Text;
                String sqlConnectionURL = "server=localhost;database=showroomhonda;UID=sa;password=1234";
                SqlConnection sqlConnection = new SqlConnection(sqlConnectionURL);
                SqlCommand cmd = new SqlCommand("SELECT * FROM ( SELECT *, ROW_NUMBER() OVER (ORDER BY FRAME_NO) as row FROm v_SCTT) a WHERE a.row > 5 and a.row <= 10", sqlConnection);
                SqlDataReader sqlDataReader = null;
                try
                {
                    sqlConnection.Open();
                    sqlDataReader = cmd.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Data data = datas.Find(x => x.repairNo == NullToString(sqlDataReader["REPAIR_NO"]));
                        if (data == null)
                        {
                            data = new Data();
                            data.createDate = NullToString(sqlDataReader["CREATEDATE"]);
                            data.dealerCode = NullToString(sqlDataReader["DEALER_CODE"]);
                            data.repairNo = NullToString(sqlDataReader["REPAIR_NO"]);
                            data.typeCD = NullToString(sqlDataReader["TYPE_CD"]);
                            data.wrNo = NullToString(sqlDataReader["WR_NO"]);
                            data.frameNo = NullToString(sqlDataReader["FRAME_NO"]);
                            data.eNo = NullToString(sqlDataReader["E_NO"]);
                            data.plateNo = NullToString(sqlDataReader["PLATE_NO"]);
                            data.km = NullToInt(sqlDataReader["KM"]);
                            data.dop = NullToString(sqlDataReader["DOP"]);
                            data.cusName = NullToString(sqlDataReader["CUS_NAME"]);
                            data.cusAddress = NullToString(sqlDataReader["CUS_ADDRESS"]);
                            data.phone = NullToString(sqlDataReader["PHONE"]);
                            data.pCode = NullToString(sqlDataReader["P_CODE"]);
                            data.pName = NullToString(sqlDataReader["P_NAME"]);
                            data.dCode = NullToString(sqlDataReader["D_CODE"]);
                            data.dName = NullToString(sqlDataReader["D_NAME"]);
                            data.empReceive = NullToString(sqlDataReader["EMP_RECEIVE"]);
                            data.empRepair = NullToString(sqlDataReader["EMP_REPAIR"]);
                            data.fuel = NullToString(sqlDataReader["FUEL"]);
                            data.printed = NullToString(sqlDataReader["PRINTED"]);
                            data.dateIn = NullToString(sqlDataReader["DATE_IN"]);
                            data.dateOut = NullToString(sqlDataReader["DATE_OUT"]);
                            data.modelName = NullToString(sqlDataReader["MODEL_NAME"]);
                            data.cusRequest = NullToString(sqlDataReader["CUS_REQUEST"]);
                            data.insResult = NullToString(sqlDataReader["INS_RESULT"]);
                            data.beforeRepair = NullToString(sqlDataReader["BEFORE_REPAIR"]);
                            datas.Add(data);
                        }
                        List<Data.Item> items = data.items;
                        if (items == null)
                        {
                            items = new List<Data.Item>();
                            data.items = items;
                        }
                        if (sqlDataReader["DISCOUNT_P"] != null)
                        {
                            Data.Item item = new Data.Item();
                            item.discountP = NullToInt(sqlDataReader["DISCOUNT_P"]);
                            item.discountW = NullToInt(sqlDataReader["DISCOUNT_W"]);
                            //TODO: Repair Desc not in SQL
                            item.repairDesc = "Repair Desc";
                            item.rate0 = NullToInt(sqlDataReader["RATE0"]);
                            item.partNo = NullToString(sqlDataReader["PART_NO"]);
                            item.partName = NullToString(sqlDataReader["PART_NAME"]);
                            item.rate1 = NullToInt(sqlDataReader["RATE1"]);
                            item.qty = NullToInt(sqlDataReader["QTY"]);
                            item.asNo = NullToString(sqlDataReader["AS_NO"]);
                            item.checkedStatus = NullToString(sqlDataReader["CHECKED"]);
                            item.typeNo = NullToString(sqlDataReader["TYPE_NO"]);
                            items.Add(item);
                        }
                    }
                    var bindingList = new BindingList<Data>(datas);
                    var source = new BindingSource(bindingList, null);
                    dgvData.DataSource = source;
                }
                finally
                {
                    // 3. close the reader
                    if (sqlDataReader != null)
                    {
                        sqlDataReader.Close();
                    }

                    // close the connection
                    if (sqlConnection != null)
                    {
                        sqlConnection.Close();
                    }
                }
                MessageBox.Show("Connect Succesful");
            }
            catch (Exception es)
            {
                MessageBox.Show(es.ToString());
            }
        }



        private void btnExportClick(object sender, EventArgs e)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Export Sheet");
            //Row 0
            IRow row = sheet.CreateRow(0);
            // Create column
            row.CreateCell(0).SetCellValue("CREATEDATE");
            row.CreateCell(1).SetCellValue("DEALER_CODE");
            row.CreateCell(2).SetCellValue("REPAIR_NO");
            row.CreateCell(3).SetCellValue("TYPE_CD");
            row.CreateCell(4).SetCellValue("WR_NO");
            row.CreateCell(5).SetCellValue("FRAME_NO");
            row.CreateCell(6).SetCellValue("E_NO");
            row.CreateCell(7).SetCellValue("PLATE_NO");
            row.CreateCell(8).SetCellValue("KM");
            row.CreateCell(9).SetCellValue("DOP");
            row.CreateCell(10).SetCellValue("CUS_NAME");
            row.CreateCell(11).SetCellValue("CUS_ADDRESS");
            row.CreateCell(12).SetCellValue("PHONE");
            row.CreateCell(13).SetCellValue("P_CODE");
            row.CreateCell(14).SetCellValue("P_NAME");
            row.CreateCell(15).SetCellValue("D_CODE");
            row.CreateCell(16).SetCellValue("D_NAME");
            row.CreateCell(17).SetCellValue("EMP_RECEIVE");
            row.CreateCell(18).SetCellValue("EMP_REPAIR");
            row.CreateCell(19).SetCellValue("FUEL");
            row.CreateCell(20).SetCellValue("PRINTED");
            row.CreateCell(21).SetCellValue("DATE_IN");
            row.CreateCell(22).SetCellValue("DATE_OUT");
            row.CreateCell(23).SetCellValue("MODEL_NAME");
            row.CreateCell(24).SetCellValue("CUS_REQUEST");
            row.CreateCell(25).SetCellValue("INS_RESULT");
            row.CreateCell(26).SetCellValue("BEFORE_REPAIR");
            row.CreateCell(27).SetCellValue("DISCOUNT_P");
            row.CreateCell(28).SetCellValue("DISCOUNT_W");
            row.CreateCell(29).SetCellValue("REPAIR_DESC");
            row.CreateCell(30).SetCellValue("RATE0");
            row.CreateCell(31).SetCellValue("PART_NO");
            row.CreateCell(32).SetCellValue("PART_NAME");
            row.CreateCell(33).SetCellValue("RATE1");
            row.CreateCell(34).SetCellValue("QTY");
            row.CreateCell(35).SetCellValue("AS_NO");
            row.CreateCell(36).SetCellValue("CHECKED");
            row.CreateCell(37).SetCellValue("TYPE_NO");
            // Write Data
            int i = 0;
            foreach (Data data in datas)
            {
                i++;
                IRow firstRow = sheet.CreateRow(i);
                firstRow.CreateCell(0).SetCellValue(data.createDate);
                firstRow.CreateCell(1).SetCellValue(data.dealerCode);
                firstRow.CreateCell(2).SetCellValue(data.repairNo);
                firstRow.CreateCell(3).SetCellValue(data.typeCD);
                firstRow.CreateCell(4).SetCellValue(data.wrNo);
                firstRow.CreateCell(5).SetCellValue(data.frameNo);
                firstRow.CreateCell(6).SetCellValue(data.eNo);
                firstRow.CreateCell(7).SetCellValue(data.plateNo);
                firstRow.CreateCell(8).SetCellValue(data.km);
                firstRow.CreateCell(9).SetCellValue(data.dop);
                firstRow.CreateCell(10).SetCellValue(data.cusName);
                firstRow.CreateCell(11).SetCellValue(data.cusAddress);
                firstRow.CreateCell(12).SetCellValue(data.phone);
                firstRow.CreateCell(13).SetCellValue(data.pCode);
                firstRow.CreateCell(14).SetCellValue(data.pName);
                firstRow.CreateCell(15).SetCellValue(data.dCode);
                firstRow.CreateCell(16).SetCellValue(data.dName);
                firstRow.CreateCell(17).SetCellValue(data.empReceive);
                firstRow.CreateCell(18).SetCellValue(data.empRepair);
                firstRow.CreateCell(19).SetCellValue(data.fuel);
                firstRow.CreateCell(20).SetCellValue(data.printed);
                firstRow.CreateCell(21).SetCellValue(data.dateIn);
                firstRow.CreateCell(22).SetCellValue(data.dateOut);
                firstRow.CreateCell(23).SetCellValue(data.modelName);
                firstRow.CreateCell(24).SetCellValue(data.cusRequest);
                firstRow.CreateCell(25).SetCellValue(data.insResult);
                firstRow.CreateCell(26).SetCellValue(data.beforeRepair);
                if (data.items != null && data.items.Count > 0)
                {
                    foreach (Data.Item item in data.items)
                    {
                        if (data.items.Count == 1)
                        {
                            IRow childRow = sheet.GetRow(i);
                            childRow.CreateCell(27).SetCellValue(item.discountP);
                            childRow.CreateCell(28).SetCellValue(item.discountW);
                            childRow.CreateCell(29).SetCellValue(item.repairDesc);
                            childRow.CreateCell(30).SetCellValue(item.partNo);
                            childRow.CreateCell(31).SetCellValue(item.partName);
                            childRow.CreateCell(32).SetCellValue(item.rate0);
                            childRow.CreateCell(33).SetCellValue(item.rate1);
                            childRow.CreateCell(34).SetCellValue(item.qty);
                            childRow.CreateCell(35).SetCellValue(item.asNo);
                            childRow.CreateCell(36).SetCellValue(item.checkedStatus);
                            childRow.CreateCell(37).SetCellValue(item.typeNo);
                        } else
                        {
                            i++;
                            IRow childRow = sheet.CreateRow(i);
                            childRow.CreateCell(27).SetCellValue(item.discountP);
                            childRow.CreateCell(28).SetCellValue(item.discountW);
                            childRow.CreateCell(29).SetCellValue(item.repairDesc);
                            childRow.CreateCell(30).SetCellValue(item.partNo);
                            childRow.CreateCell(31).SetCellValue(item.partName);
                            childRow.CreateCell(32).SetCellValue(item.rate0);
                            childRow.CreateCell(33).SetCellValue(item.rate1);
                            childRow.CreateCell(34).SetCellValue(item.qty);
                            childRow.CreateCell(35).SetCellValue(item.asNo);
                            childRow.CreateCell(36).SetCellValue(item.checkedStatus);
                            childRow.CreateCell(37).SetCellValue(item.typeNo);
                        }
                    }
                }
            }
            using (var file = new FileStream("D:\\Workbook.xlsx", FileMode.CreateNew, FileAccess.ReadWrite))
            {
                workbook.Write(file);
                MessageBox.Show("Create File Succesfully");
            }
        }

        static string NullToString(object Value)
        {

            // Value.ToString() allows for Value being DBNull, but will also convert int, double, etc.
            return Value == null ? "" : Value.ToString();

            // If this is not what you want then this form may suit you better, handles 'Null' and DBNull otherwise tries a straight cast
            // which will throw if Value isn't actually a string object.
            //return Value == null || Value == DBNull.Value ? "" : NullToString(Value;


        }

        static int NullToInt(object Value)
        {
            if (Value == null)
            {
                return 0;
            }
            else
            {
                try
                {
                    return Int32.Parse(Value.ToString());
                }
                catch (Exception e)
                {
                    return 0;
                }
            }

        }
    }
}
