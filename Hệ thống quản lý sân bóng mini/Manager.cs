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
            LoadField();
            LoadTypeField();
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

        void LoadField()
        {
            
                DataTable dataTable = new DataTable();
                dataTable.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn {ColumnName = "id", DataType = typeof(int)},
                    new DataColumn {ColumnName = "Name", DataType = typeof(string)},
                    new DataColumn {ColumnName = "idType", DataType = typeof(int)},
                  //  new DataColumn {ColumnName = "status", DataType = typeof(string)}
                });
                List<Field> listField = FieldDAL.
                    Instance.LoadFieldList();
                foreach (Field field in listField)
                {
                    dataTable.Rows.Add(field.Id,field.Name,field.IdFieldType);
                }
                dataGridView2.DataSource = dataTable;

            

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Lấy hàng đã click


                DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];

                // Tiếp theo, bạn có thể lấy giá trị từ các ô trong hàng đã chọn
                txtIdField.Text = selectedRow.Cells[0].Value.ToString();
                txtNameField.Text = selectedRow.Cells[1].Value.ToString();
                cbIdType.Text = selectedRow.Cells[2].Value.ToString(); 
                int id = int.Parse(cbIdType.Text);
                showTypeName(id);
            }

        }

        void showTypeName(int id)
        {
            List<FieldType> listFieldType = FieldTypeDAL.Instance.LoadFieldType();
            foreach (FieldType fieldType in listFieldType)
            {
                if (fieldType.Id == id)
                {
                    txtTypeName.Text = fieldType.TypeName;
                }
            }
        }

        void LoadTypeField()
        {
            List<FieldType> listFieldType = FieldTypeDAL.Instance.LoadFieldType();
            cbIdType.DataSource = listFieldType;
            cbIdType.DisplayMember = "id";
        }

        private void cbIdType_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  List<FieldType> listFieldType = FieldTypeDAL.Instance.LoadFieldType();
            ComboBox cb = sender as ComboBox;
            FieldType choosed = cb.SelectedItem as FieldType;
            if (choosed != null)
            {
                int id = choosed.Id;
                showTypeName(id);
            }
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int id = int.Parse(cbIdType.Text);
            bool check = true;
            List<Field> listField = FieldDAL.Instance.GetFieldByIdFieldType(id);
            foreach(Field field in listField)
            {
                if(field.Name == txtNameField.Text)
                {
                    check = false;
                    break;
                }
            }
            if (check)
            {
                // insert vào
            }
        }

        private void panel19_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
