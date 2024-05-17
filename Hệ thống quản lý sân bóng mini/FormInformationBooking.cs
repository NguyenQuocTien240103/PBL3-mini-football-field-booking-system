using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Hệ_thống_quản_lý_sân_bóng_mini.DAL;
using Hệ_thống_quản_lý_sân_bóng_mini.demo;
using Hệ_thống_quản_lý_sân_bóng_mini.DTO;

namespace Hệ_thống_quản_lý_sân_bóng_mini
{
    public partial class FormInformationBooking : Form
    {
        public FormInformationBooking()
        {
            InitializeComponent();
            LoadTypeField();
            setComboxTime();
            LoadCustomerBooking();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        void LoadTypeField()
        {
            List<FieldType> listFieldType = FieldTypeDAL.Instance.LoadFieldType();
            cbTypeField.DataSource = listFieldType;
            cbTypeField.DisplayMember= "TypeName";
        }
        void LoadFieldByIdTypeField(int id)
        {
            List<Field> listField = FieldDAL.Instance.GetFieldByIdFieldType(id);
            cbField.DataSource = listField;
            cbField.DisplayMember = "name";
        }

        int saveIDFieldType = 0;
        string saveTypeName = "";
        private void cbTypeField_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<FieldType> listFieldType = FieldTypeDAL.Instance.LoadFieldType();
            ComboBox cb = sender as ComboBox;
            FieldType choosed = cb.SelectedItem as FieldType;
            if (choosed != null)
            {
                saveIDFieldType = choosed.Id;
                saveTypeName = choosed.TypeName;
                LoadFieldByIdTypeField(choosed.Id);
            }

        }
        string saveFieldName = "";
        private void cbField_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            Field choosed = cb.SelectedItem as Field;
            if(choosed != null)
            {
                saveFieldName = choosed.Name;
            }
            Show(saveIDFieldType, saveFieldName,saveTypeName);
        }

        void Show(int idType,string fieldName,string TypeName)
        {
            List<Field> listField = FieldDAL.Instance.LoadFieldList();
            foreach(Field field in listField)
            {
                if(field.IdFieldType == idType && field.Name == fieldName)
                {
                    txtIdField.Text = field.Id.ToString();
                    txtFieldName.Text = field.Name;
                    txtFieldType.Text = TypeName;
                }
            }
        }
        void setComboxTime()
        {
            for (int i = 0; i < 24; i++)
            {
                cb1.Items.Add(i);
                cb3.Items.Add(i);
            }
            for (int i = 0; i < 60; i++)
            {
                if (i < 10)
                {
                    String index = i.ToString();
                    cb2.Items.Add("0" + i);
                    cb4.Items.Add("0" + i);

                }
                else
                {
                    cb2.Items.Add(i);
                    cb4.Items.Add(i);
                }
            }

        }

       

        void LoadCustomerBooking()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[]
            {
                    new DataColumn {ColumnName = "id", DataType = typeof(int)},
                    new DataColumn {ColumnName = "idField", DataType = typeof(int)},
                    new DataColumn {ColumnName = "TypeName", DataType = typeof(string)},
                    new DataColumn {ColumnName = "FieldName", DataType = typeof(string)},
                    new DataColumn {ColumnName = "CustomerName", DataType = typeof(string)},
                    new DataColumn {ColumnName = "CustomerPhone", DataType = typeof(string)},
                    new DataColumn {ColumnName = "startTime", DataType = typeof(string)},
                    new DataColumn {ColumnName = "endTime", DataType = typeof(string)},
                    new DataColumn {ColumnName = "priceBooking", DataType = typeof(float)},
                    new DataColumn {ColumnName = "status", DataType = typeof(string)}
            });
            List<CustomerBookingDetail> customerBookingDetails = CustomerBookingDetailDAL.
                Instance.LoadCustomerBookingById1();
            foreach (CustomerBookingDetail customerbooking in customerBookingDetails)
            {
                dataTable.Rows.Add(customerbooking.Id,customerbooking.IdField,
                customerbooking.TypeName, customerbooking.FieldName, customerbooking.CustomerName,
                customerbooking.CustomerPhone, customerbooking.startTime, customerbooking.endTime,
                customerbooking.priceBooking, customerbooking.status);
            }
            dataGridView1.DataSource = dataTable;

        }
        
        
        string saveCustomerName = "";
        string saveCustomerPhone = "";
        string saveHourBegin = "";
        string saveMinuteBegin = "";
        string saveHourEnd = "";
        string saveMinuteEnd = "";
        string savePriceBooking = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Lấy hàng đã click
                
                
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Tiếp theo, bạn có thể lấy giá trị từ các ô trong hàng đã chọn
                txtIdCustomerBooking.Text= selectedRow.Cells[0].Value.ToString();
                txtIdField.Text = selectedRow.Cells[1].Value.ToString();
                txtFieldType.Text = selectedRow.Cells[2].Value.ToString();
                txtFieldName.Text = selectedRow.Cells[3].Value.ToString();

                txtCustomerName.Text = selectedRow.Cells[4].Value.ToString();
                saveCustomerName = selectedRow.Cells[4].Value.ToString();

                txtCustomerPhone.Text = selectedRow.Cells[5].Value.ToString();
                saveCustomerPhone= selectedRow.Cells[5].Value.ToString();


                txtPriceBooking.Text = selectedRow.Cells[8].Value.ToString();
                savePriceBooking = selectedRow.Cells[8].Value.ToString();

                txtStatus.Text = selectedRow.Cells[9].Value.ToString();


                // Và tiếp tục lấy giá trị từ các ô khác nếu cần
                // lấy time
                string tgbatdau = selectedRow.Cells[6].Value.ToString();
                string tgkethuc = selectedRow.Cells[7].Value.ToString();
                string[] parts1 = tgbatdau.Split('h');
                string[] parts2 = tgkethuc.Split('h');
                cb1.Text = parts1[0];
                saveHourBegin  = parts1[0];

                cb2.Text = parts1[1];
                saveMinuteBegin = parts1[1];

                cb3.Text = parts2[0];
                saveHourEnd = parts2[0];

                cb4.Text = parts2[1];
                saveMinuteEnd   = parts2[1];
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string s1 = txtIdCustomerBooking.Text;
            
            
           
            if (s1 == "")
            {
                MessageBox.Show("vui lòng chọn đơn đặt sân");
            }
            else
            {
                if(txtCustomerName.Text == saveCustomerName && txtCustomerPhone.Text == saveCustomerPhone 
                    && txtPriceBooking.Text == savePriceBooking && cb1.SelectedItem.ToString() == saveHourBegin
                    && cb2.SelectedItem.ToString() == saveMinuteBegin && cb3.SelectedItem.ToString() == saveHourEnd
                    && cb4.SelectedItem.ToString() == saveMinuteEnd)
                {

                    int idCustomerBooking = int.Parse(s1);
                    string s2 = txtIdField.Text;
                    int idField = int.Parse(s2);
                    if (checkFieldStatus(idField))
                    {
                        
                         FieldDAL.Instance.updateFieldById(idField, "busy");
                         CustomerBookingDAL.Instance.updateCustomerBookingById(idCustomerBooking);
                    }
                    else
                    {
                        MessageBox.Show("San dang co nguoi da");
                    }

                }
                else
                {
                    MessageBox.Show("truong nay k co trong don dat san");
                }
                this.Close();
            }
           
        }
        public bool checkFieldStatus(int idField)
        {
            String status = "";
            List<Field> fields = FieldDAL.Instance.LoadFieldList();
            foreach( Field field in fields)
            {
                if(field.Id == idField)
                {
                    status = field.Status;
                    break;
                }
            }
            if (status == "empty")
            {
                return true;
            }
            return false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            String startTime = cb1.SelectedItem.ToString() + 'h' + cb2.SelectedItem.ToString();
            String endTime = cb3.SelectedItem.ToString() + 'h' + cb4.SelectedItem.ToString();
            float PriceBookinng = float.Parse(txtPriceBooking.Text.ToString());
            String Name = txtCustomerName.Text;
            String Phone = txtCustomerPhone.Text;
            CustomerDAL.Instance.InsertCustomer(Name, Phone); // insert Customer
            // lấy idCustomer mới insert
            int idCustomer = CustomerDAL.Instance.getIdCustomerLast(); // đã giải quyết
            // lấy idField
            String idfieldChoose = txtIdField.Text.ToString();
            int idFieldChoose = int.Parse(idfieldChoose);
            // cập  nhật trạng thái
            BookingManager bookingManager = new BookingManager();
            if (PriceBookinng > 0)
            {
               // FieldDAL.Instance.updateFieldById(idFieldChoose, "empty");
                CustomerBookingDAL.Instance.InSertCustomerBooking(idCustomer, idFieldChoose, startTime,
                   endTime, PriceBookinng, "cho duyet");
            }
            else
            {
                CustomerBookingDAL.Instance.InSertCustomerBooking(idCustomer, idFieldChoose, startTime,
                    endTime, PriceBookinng, "chua thanh toan");
                FieldDAL.Instance.updateFieldById(idFieldChoose, "busy");
            }

            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string s1 = txtIdCustomerBooking.Text;
            if(s1 == "")
            {
                MessageBox.Show("vui lòng chọn đơn đặt sân");
            }
            else
            {
                string startTime = cb1.SelectedItem.ToString() + "h" + cb2.SelectedItem.ToString();
                string endTime = cb3.SelectedItem.ToString()+ "h"+ cb4.SelectedItem.ToString();
                CustomerBooking customerBooking = CustomerBookingDAL.Instance.getCustomerBookingById(int.Parse(s1));
                CustomerDAL.Instance.updateCustomer(customerBooking.IdCustomer, txtCustomerName.Text, txtCustomerPhone.Text);
            //    FieldDAL.Instance.updateFieldById(int.Parse(txtIdField.Text),"empty");
                CustomerBookingDAL.Instance.updateCustomerBooking(int.Parse(s1),customerBooking.IdCustomer, int.Parse(txtIdField.Text), startTime, endTime, float.Parse(txtPriceBooking.Text.ToString()),txtStatus.Text );
                this.Close();
            }
        }

        public void updateDatagridViewAfterSearch(string TypeName, string FieldName, string search)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[]
            {
                    new DataColumn {ColumnName = "id", DataType = typeof(int)},
                    new DataColumn {ColumnName = "idField", DataType = typeof(int)},
                    new DataColumn {ColumnName = "TypeName", DataType = typeof(string)},
                    new DataColumn {ColumnName = "FieldName", DataType = typeof(string)},
                    new DataColumn {ColumnName = "CustomerName", DataType = typeof(string)},
                    new DataColumn {ColumnName = "CustomerPhone", DataType = typeof(string)},
                    new DataColumn {ColumnName = "startTime", DataType = typeof(string)},
                    new DataColumn {ColumnName = "endTime", DataType = typeof(string)},
                    new DataColumn {ColumnName = "priceBooking", DataType = typeof(float)},
                    new DataColumn {ColumnName = "status", DataType = typeof(string)}
            });

            List<CustomerBookingDetail> customerBookingDetails = CustomerBookingDetailDAL.
                Instance.LoadCustomerBookingById1();
            foreach (CustomerBookingDetail customerbooking in customerBookingDetails)
            {
                if (customerbooking.ToString().Contains(TypeName) && customerbooking.ToString().Contains(FieldName) && customerbooking.ToString().ToLower().Contains(search))
                {

                    dataTable.Rows.Add(customerbooking.Id, customerbooking.IdField,
                    customerbooking.TypeName, customerbooking.FieldName, customerbooking.CustomerName,
                    customerbooking.CustomerPhone, customerbooking.startTime, customerbooking.endTime,
                    customerbooking.priceBooking, customerbooking.status);
                }
            }
            dataGridView1.DataSource = dataTable;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            updateDatagridViewAfterSearch(txtFieldType.Text, txtFieldName.Text.ToString(), txtSearch.Text.ToLower());
        }

        

        private void btnDel_Click(object sender, EventArgs e)
        {
            if(txtIdCustomerBooking.Text.ToString() == "")
            {
                MessageBox.Show("vui lòng chọn đơn đặt sân");
            }
            else
            {
                // cap nhat thanh trang thai da huy
                CustomerBookingDAL.Instance.updatestatusCustomerBookingById(int.Parse(txtIdCustomerBooking.Text));
                BillDAL.Instance.insertBill(int.Parse(txtIdCustomerBooking.Text), float.Parse(txtPriceBooking.Text));
                this.Close();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
