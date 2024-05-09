using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hệ_thống_quản_lý_sân_bóng_mini.DAL;
using Hệ_thống_quản_lý_sân_bóng_mini.demo;
using Hệ_thống_quản_lý_sân_bóng_mini.DTO;

namespace Hệ_thống_quản_lý_sân_bóng_mini
{
    public partial class Manager : Form
    {
        public Manager()
        {
            InitializeComponent();
       //     loadAccountList();
            loadBill();
          //  load();
        }

        //void loadAccountList()
        //{
        //    string query = "EXEC dbo.GetFieldType";
        //    dtgvAccount.DataSource = DataProvider.Instance.ExcuteQuery(query);

        //}


        void loadBill()
        {
            dataGridView1.Controls.Clear();   
            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[]
            {
                    new DataColumn {ColumnName = "name", DataType = typeof(string)},
                    new DataColumn {ColumnName = "phone", DataType = typeof(string)},
                    new DataColumn {ColumnName = "FieldType", DataType = typeof(string)},
                    new DataColumn {ColumnName = "FieldName", DataType = typeof(string) },
                    new DataColumn {ColumnName = "startTime", DataType = typeof(string)},
                    new DataColumn {ColumnName = "endTime", DataType = typeof(string)},
                    new DataColumn {ColumnName = "priceBooking", DataType = typeof(float)},
                    new DataColumn {ColumnName = "status", DataType = typeof(string)},
                    new DataColumn {ColumnName = "totalPrice", DataType = typeof(float)},
                    new DataColumn {ColumnName = "payMentDay", DataType = typeof(DateTime)}
            });
            List<BillDetail> listBillDetal = BillDetailDAL.Instance.LoadBill();
            foreach (BillDetail billdetail in listBillDetal)
            {
                dataTable.Rows.Add(billdetail.Name,billdetail.Phone,billdetail.FieldType,billdetail.FieldName,billdetail.StartTime,billdetail.EndTime,
                    billdetail.PriceBooking,billdetail.Status,billdetail.TotalPrice,billdetail.PaymentDay);
            }
            dataGridView1.DataSource = dataTable;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = "\r\nSELECT \r\n " +
                "Customer.name  ,\r\n    " +
                "Customer.phone,\r\n   " +
                "FieldType.TypeName AS FieldType, \r\n   " +
                "FieldName.name AS FieldName, \r\n   " +
                "CustomerBooking.startTime,\r\n   " +
                "CustomerBooking.endTime,\r\n    " +
                "CustomerBooking.priceBooking,\r\n   " +
                "CustomerBooking.status,\r\n   " +
                "Bill.totalPrice,\r\n   " +
                "Bill.paymentDay\r\nFROM \r\n    " +
                "FieldType\r\nINNER JOIN \r\n   " +
                "FieldName ON FieldType.id = FieldName.idFieldType\r\n" +
                "INNER JOIN \r\n    " +
                "CustomerBooking ON FieldName.id = CustomerBooking.idFieldName\r\n" +
                "INNER JOIN \r\n    " +
                "Customer ON CustomerBooking.idCustomer = Customer.id \r\n" +
                "INNER JOIN \r\n    " +
                "Bill ON CustomerBooking.id = Bill.idCustomerBooking and Bill.paymentDay= '"+dateTimePicker1.Value.ToShortDateString().ToString()+"'";
            DataTable data = DataProvider.Instance.ExcuteQuery(sql);
            dataGridView1.Controls.Clear();
            dataGridView1.DataSource = data;
        }
    }
}
