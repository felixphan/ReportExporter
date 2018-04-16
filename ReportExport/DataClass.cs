using System;
using System.Collections.Generic;
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
        public String wrNo { get; set; }
        public String frameNo { get; set; }
        public String eNo { get; set; }
        public String plateNo { get; set; }
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
        public String fuel { get; set; }
        public String printed { get; set; }
        public String dateIn { get; set; }
        public String dateOut { get; set; }
        public String modelName { get; set; }
        public String cusRequest { get; set; }
        public String insResult { get; set; }
        public String beforeRepair { get; set; }
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
        }

    }
}
