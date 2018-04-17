using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportExport 
{
    class Data
    {
        public String createDate { get; set; }
        public String dealerCode { get; set; }
        public String repairNo { get; set; }
        public String typeCD { get; set; }
        [System.ComponentModel.Browsable(false)]
        public String wrNo { get; set; }
        public String frameNo { get; set; }
        public String eNo { get; set; }
        public String plateNo { get; set; }
        [System.ComponentModel.Browsable(false)]
        public int km { get; set; }
        public String dop { get; set; }
        public String cusName { get; set; }
        public String cusAddress { get; set; }
        public String phone { get; set; }
        public String pCode { get; set; }
        public String pName { get; set; }
        public String dCode { get; set; }
        public String dName { get; set; }
        public String empReceive { get; set; }
        public String empRepair { get; set; }
        [System.ComponentModel.Browsable(false)]
        public String fuel { get; set; }
        // Annotation to hide column in Data Grid View
        [System.ComponentModel.Browsable(false)]
        public String printed { get; set; }
        public String dateIn { get; set; }
        public String dateOut { get; set; }
        public String modelName { get; set; }
        public String cusRequest { get; set; }
        public String insResult { get; set; }
        public String beforeRepair { get; set; }
        [System.ComponentModel.Browsable(false)]
        public List<Item> items { get; set; }
        public class Item
        {
            public int discountP { get; set; }
            public int discountW { get; set; }
            public String repairDesc { get; set; }
            public int rate0 { get; set; }
            public String partNo { get; set; }
            public String partName { get; set; }
            public int rate1 { get; set; }
            public int qty { get; set; }
            public String asNo { get; set; }
            public String checkedStatus { get; set; }
            public String typeNo { get; set; }

            public Item(SqlDataReader sqlDataReader)
            {
                this.discountP = NullToInt(sqlDataReader["DISCOUNT_P"]);
                this.discountW = NullToInt(sqlDataReader["DISCOUNT_W"]);
                //TODO: Repair Desc not in SQL
               this.repairDesc = "Repair Desc";
               this.rate0 = NullToInt(sqlDataReader["RATE0"]);
               this.partNo = NullToString(sqlDataReader["PART_NO"]);
               this.partName = NullToString(sqlDataReader["PART_NAME"]);
               this.rate1 = NullToInt(sqlDataReader["RATE1"]);
               this.qty = NullToInt(sqlDataReader["QTY"]);
               this.asNo = NullToString(sqlDataReader["AS_NO"]);
               this.checkedStatus = NullToString(sqlDataReader["CHECKED"]);
                this.typeNo = NullToString(sqlDataReader["TYPE_NO"]);
            }
        }

        public Data(SqlDataReader sqlDataReader)
        {
            this.createDate = NullToString(sqlDataReader["CREATEDATE"]);
            this.dealerCode = NullToString(sqlDataReader["DEALER_CODE"]);
            this.repairNo = NullToString(sqlDataReader["REPAIR_NO"]);
            this.typeCD = NullToString(sqlDataReader["TYPE_CD"]);
            this.wrNo = NullToString(sqlDataReader["WR_NO"]);
            this.frameNo = NullToString(sqlDataReader["FRAME_NO"]);
            this.eNo = NullToString(sqlDataReader["E_NO"]);
            this.plateNo = NullToString(sqlDataReader["PLATE_NO"]);
            this.km = NullToInt(sqlDataReader["KM"]);
            this.dop = NullToString(sqlDataReader["DOP"]);
            this.cusName = NullToString(sqlDataReader["CUS_NAME"]);
            this.cusAddress = NullToString(sqlDataReader["CUS_ADDRESS"]);
            this.phone = NullToString(sqlDataReader["PHONE"]);
            this.pCode = NullToString(sqlDataReader["P_CODE"]);
            this.pName = NullToString(sqlDataReader["P_NAME"]);
            this.dCode = NullToString(sqlDataReader["D_CODE"]);
            this.dName = NullToString(sqlDataReader["D_NAME"]);
            this.empReceive = NullToString(sqlDataReader["EMP_RECEIVE"]);
            this.empRepair = NullToString(sqlDataReader["EMP_REPAIR"]);
            this.fuel = NullToString(sqlDataReader["FUEL"]);
            this.printed = NullToString(sqlDataReader["PRINTED"]);
            this.dateIn = NullToString(sqlDataReader["DATE_IN"]);
            this.dateOut = NullToString(sqlDataReader["DATE_OUT"]);
            this.modelName = NullToString(sqlDataReader["MODEL_NAME"]);
            this.cusRequest = NullToString(sqlDataReader["CUS_REQUEST"]);
            this.insResult = NullToString(sqlDataReader["INS_RESULT"]);
            this.beforeRepair = NullToString(sqlDataReader["BEFORE_REPAIR"]);
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
