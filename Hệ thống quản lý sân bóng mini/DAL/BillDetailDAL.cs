using Hệ_thống_quản_lý_sân_bóng_mini.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hệ_thống_quản_lý_sân_bóng_mini.DAL
{
    public class BillDetailDAL
    {

        private static BillDetailDAL _Instance;
        public static BillDetailDAL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BillDetailDAL();
                }
                return _Instance;
            }
            private set
            {

            }

        }

        private BillDetailDAL() { }

        public List<BillDetail> LoadBill()
        {

                string sql = "\r\nSELECT \r\n    Customer.name as CustomerName ,\r\n   " +
                    " Customer.phone,\r\n    " +
                    "FieldType.TypeName AS FieldType, \r\n    " +
                    "FieldName.name AS FieldName, \r\n   " +
                    "CustomerBooking.startTime,\r\n    " +
                    "CustomerBooking.endTime,\r\n   " +
                    "CustomerBooking.priceBooking,\r\n   " +
                    "CustomerBooking.status,\r\n  " +
                    "Bill.totalPrice,\r\n   " +
                    "Bill.paymentDay\r\nFROM \r\n  " +
                    "FieldType\r\n" +
                    "INNER JOIN \r\n   " +
                    "FieldName ON FieldType.id = FieldName.idFieldType\r\n" +
                    "INNER JOIN \r\n   " +
                    "CustomerBooking ON FieldName.id = CustomerBooking.idFieldName\r\n" +
                    "INNER JOIN \r\n    " +
                    "Customer ON CustomerBooking.idCustomer = Customer.id \r\n" +
                    "INNER JOIN \r\n    " +
                    "Bill ON CustomerBooking.id = Bill.idCustomerBooking";
            
            DataTable data = DataProvider.Instance.ExcuteQuery(sql);
            List<BillDetail>   listBill = new List<BillDetail>();
            foreach (DataRow row in data.Rows)
            {
                BillDetail bill = new BillDetail(row);
               // MessageBox.Show(bill.Name.ToString());
                listBill.Add(bill);

            }
            return listBill;

        }
    }
}
