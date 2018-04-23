using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace ReportExport
{
    class Service
    {
        List<Data> datas;
        public void export(string pathExport)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("SCTT Report");
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
            foreach (Data data in this.datas)
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
                // Write Item Data
                if (data.items != null && data.items.Count > 0)
                {
                    foreach (Data.Item item in data.items)
                    {
                        // Have 1 item only
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
                        }
                        else
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

            using (var file = new FileStream(pathExport + "/ExportSCTT_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx", FileMode.CreateNew, FileAccess.ReadWrite))
            {
                workbook.Write(file);
            }
        }

        public async Task<List<Data>> getDatas(String sqlConnectionURL, String sqlCommand)
        {
            SqlDataReader sqlDataReader = null;
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionURL);
            SqlCommand cmd = new SqlCommand(sqlCommand, sqlConnection);
            this.datas = new List<Data>();
            try
            {
                sqlConnection.Open();
                sqlDataReader = cmd.ExecuteReader();
                await Task.Run(() =>
                {
                    while (sqlDataReader.Read())
                    {
                        // Loop to check existed data
                        Data data = datas.Find(x => x.repairNo == (String)sqlDataReader["REPAIR_NO"]);
                        if (data == null)
                        {
                            data = new Data(sqlDataReader);
                            datas.Add(data);
                        }
                        // Get Item data
                        List<Data.Item> items = data.items;
                        if (items == null)
                        {
                            items = new List<Data.Item>();
                            data.items = items;
                        }
                        if (sqlDataReader["DISCOUNT_P"] != null)
                        {
                            Data.Item item = new Data.Item(sqlDataReader);
                            items.Add(item);
                        }
                    }
                });

                return datas;
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
        }
    }
}
