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
            setComboxTime();
            LoadCustomerBooking();
            loadType();
        }
        // loadFiled,FieldType
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void loadType()
        {
            if(cbTypeField.Items.Count == 0)
            {
                cbTypeField.Items.Add(new FieldType(0,"All"));

                List<FieldType> listFieldType = FieldTypeDAL.Instance.LoadFieldType();
                foreach(FieldType type in listFieldType)
                {
                    cbTypeField.Items.Add(type.TypeName);
                }
            }
            cbTypeField.SelectedIndex = 0;
        }
        int idType = 0;
        private void cbTypeField_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<FieldType> listFieldType = FieldTypeDAL.Instance.LoadFieldType();
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem.ToString() == "All")
            {
                LoadFieldByIdTypeField();
            }
            else
            {
                foreach (FieldType fieldType in listFieldType)
                {
                    if (fieldType.TypeName == cb.SelectedItem.ToString())
                    {
                        idType = fieldType.Id;
                        LoadFieldByIdTypeField(fieldType.Id);
                        break;
                    }
                }
            }
        }
        void LoadFieldByIdTypeField(int id=0)
        {
            cbField.Items.Clear();
            if (cbField.Items.Count == 0)
            {
                cbField.Items.Add(new Field(0, "All"));
                if (id != 0)
                {
                    List<Field> listField = FieldDAL.Instance.GetFieldByIdFieldType(id);
                    foreach (Field field in listField)
                    {
                        cbField.Items.Add(field.Name);
                    }
                }
            }
            cbField.SelectedIndex = 0;
        }
        private void cbField_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            Show(cbTypeField.SelectedItem.ToString(), cb.SelectedItem.ToString(), idType);
        }
        void Show(string Type, string Field, int idType)
        {
            txtFieldType.Text = Type;
            txtFieldName.Text = Field;
            int id = idType;
            List<Field> listField = FieldDAL.Instance.GetFieldByIdFieldType(id);
            foreach (Field field in listField)
            {
                if(field.IdFieldType==id && field.Name == Field)
                {
                    txtIdField.Text = field.Id.ToString();
                }
            }
        }
        // loadTime
        void setComboxTime()
        {
            for (int i = 0; i < 24; i++)
            {
                if (i < 10)
                {
                    string index = i.ToString();
                    cb1.Items.Add("0" + i);
                    cb3.Items.Add("0" + i);
                }
                else
                {
                    cb1.Items.Add(i);
                    cb3.Items.Add(i);
                }
            }
            for (int i = 0; i < 60; i++)
            {
                if (i < 10)
                {
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
        void LoadCustomerBooking(string TypeName=null, string FieldName = null, string search = null)
        {
            //MessageBox.Show(TypeName + FieldName);
            if(TypeName == null && FieldName == null && search == null)
            {
                TypeName = "";
                FieldName = "";
                search = "";
            }
            if (TypeName == "All" && FieldName == "All")
            {
                TypeName = "";
                FieldName = "";
            }
            if (FieldName == "All")
            {
                FieldName = "";
            }
            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[]
            {
                   // new DataColumn {ColumnName = "STT", DataType = typeof(int)},
                    new DataColumn {ColumnName = "id", DataType = typeof(int)},
                    new DataColumn {ColumnName = "idField", DataType = typeof(int)},
                    new DataColumn {ColumnName = "TypeName", DataType = typeof(string)},
                    new DataColumn {ColumnName = "FieldName", DataType = typeof(string)},
                    new DataColumn {ColumnName = "CustomerName", DataType = typeof(string)},
                    new DataColumn {ColumnName = "CustomerPhone", DataType = typeof(string)},
                    new DataColumn {ColumnName = "startTime", DataType = typeof(string)},
                    new DataColumn {ColumnName = "endTime", DataType = typeof(string)},
                    new DataColumn {ColumnName = "priceBooking", DataType = typeof(float)},
                    new DataColumn {ColumnName = "status", DataType = typeof(string)},
                    new DataColumn {ColumnName = "bookingDay", DataType = typeof(string)}
            });
            List<CustomerBookingDetail> customerBookingDetails = CustomerBookingDetailDAL.
                Instance.LoadCustomerBookingById1();
           
            foreach (CustomerBookingDetail customerbooking in customerBookingDetails) {

                //dataTable.Rows.Add(customerbooking.Id, customerbooking.IdField,
                //customerbooking.TypeName, customerbooking.FieldName, customerbooking.CustomerName,
                //customerbooking.CustomerPhone, customerbooking.startTime.ToString("HH:mm"), customerbooking.endTime.ToString("HH:mm"),
                //customerbooking.priceBooking, customerbooking.status, customerbooking.Ngaydat.ToString("dd/MM/yyyy"));
                if (customerbooking.ToString().Contains(TypeName) && customerbooking.ToString().Contains(FieldName) && customerbooking.ToString().ToLower().Contains(search))
                {
                    dataTable.Rows.Add(customerbooking.Id, customerbooking.IdField,
                    customerbooking.TypeName, customerbooking.FieldName, customerbooking.CustomerName,
                    customerbooking.CustomerPhone, customerbooking.startTime.ToString("HH:mm"), customerbooking.endTime.ToString("HH:mm"),
                    customerbooking.priceBooking, customerbooking.status, customerbooking.Ngaydat.ToString("MM/dd/yyyy"));
                }
            }
            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["idField"].Visible = false;
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
                cbTypeField.Text = selectedRow.Cells[2].Value.ToString();
                txtFieldName.Text = selectedRow.Cells[3].Value.ToString();
                cbField.Text = selectedRow.Cells[3].Value.ToString();
                txtCustomerName.Text = selectedRow.Cells[4].Value.ToString();
                saveCustomerName = selectedRow.Cells[4].Value.ToString();

                txtCustomerPhone.Text = selectedRow.Cells[5].Value.ToString();
                saveCustomerPhone= selectedRow.Cells[5].Value.ToString();


                txtPriceBooking.Text = selectedRow.Cells[8].Value.ToString();
                savePriceBooking = selectedRow.Cells[8].Value.ToString();

                txtStatus.Text = selectedRow.Cells[9].Value.ToString();
                dateTimePicker1.Text = selectedRow.Cells[10].Value.ToString();

                // Và tiếp tục lấy giá trị từ các ô khác nếu cần
                // lấy time
                string tgbatdau = selectedRow.Cells[6].Value.ToString();
                string tgkethuc = selectedRow.Cells[7].Value.ToString();
                string[] parts1 = tgbatdau.Split(':');
                string[] parts2 = tgkethuc.Split(':');
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
                    if (dateTimePicker1.Value.Date.ToString("MM/dd/yyyy") == DateTime.Today.ToString("MM/dd/yyyy"))
                    {
                        int idCustomerBooking = int.Parse(s1);
                        string s2 = txtIdField.Text;
                        int idField = int.Parse(s2);
                        if (checkFieldStatus(idField))
                        {
                            FieldDAL.Instance.updateFieldById(idField, "busy");
                            CustomerBookingDAL.Instance.updateCustomerBookingById(idCustomerBooking);
                            this.Close();
                        }
                        else
                        {
                            
                            MessageBox.Show("Sân đang có người đá");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Chưa đến thời điểm ");
                    }
                }
                else
                {
                    MessageBox.Show("trường này không có đơn đặt sân");
                }
            }
        }
        public bool checkFieldStatus(int idField)
        {
            string status = "";
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
            float PriceBookinng = float.Parse(txtPriceBooking.Text.ToString());
            string Name = txtCustomerName.Text;
            string Phone = txtCustomerPhone.Text;
            CustomerDAL.Instance.InsertCustomer(Name, Phone); // insert Customer
            // lấy idCustomer mới insert
            int idCustomer = CustomerDAL.Instance.getIdCustomerLast(); // đã giải quyết

            if (txtIdField.Text != "")
            {
                int idFieldChoose = int.Parse(txtIdField.Text.ToString());

                // cập  nhật trạng thái
                DateTime startTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month,
                    DateTime.Today.Day, int.Parse(cb1.SelectedItem.ToString()), int.Parse(cb2.SelectedItem.ToString()), 0);

                DateTime endTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month,
                    DateTime.Today.Day, int.Parse(cb3.SelectedItem.ToString()), int.Parse(cb4.SelectedItem.ToString()), 0);

                DateTime ngaydat = dateTimePicker1.Value.Date;

                if (PriceBookinng > 0)
                {
                    if (checktimeFieldNeedAdd(idFieldChoose, startTime.ToString("HH:mm"), endTime.ToString("HH:mm"), ngaydat))
                    {
                        CustomerBookingDAL.Instance.InSertCustomerBooking(idCustomer, idFieldChoose, startTime,
                           endTime, PriceBookinng, "dat truoc", ngaydat);
                    }
                    else
                    {
                        MessageBox.Show("Sân đã có người đặt");
                    }
                }
            }
            else
            {
                MessageBox.Show("Sân bạn chọn không hợp lệ");
            }
            LoadCustomerBooking();
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
                DateTime startTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month,
              DateTime.Today.Day, int.Parse(cb1.SelectedItem.ToString()), int.Parse(cb2.SelectedItem.ToString()), 0);

                DateTime endTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month,
                    DateTime.Today.Day, int.Parse(cb3.SelectedItem.ToString()), int.Parse(cb4.SelectedItem.ToString()), 0);
                DateTime ngaydat = dateTimePicker1.Value.Date;

                CustomerBooking customerBooking = CustomerBookingDAL.Instance.getCustomerByCustomerBooking(int.Parse(s1));

                if (checktimeFieldNeedUpdate(int.Parse(txtIdField.Text), startTime.ToString("HH:mm"), endTime.ToString("HH:mm"),int.Parse(s1),ngaydat))
                {
                    CustomerDAL.Instance.updateCustomer(customerBooking.IdCustomer, txtCustomerName.Text, txtCustomerPhone.Text);
                    CustomerBookingDAL.Instance.updateCustomerBooking(int.Parse(s1), customerBooking.IdCustomer, int.Parse(txtIdField.Text), startTime, endTime, float.Parse(txtPriceBooking.Text.ToString()), txtStatus.Text,ngaydat);
                    
                }
                else
                    {
                        MessageBox.Show("Sân đã có người đặt");
                    }
                }
            LoadCustomerBooking();
        }
        //public void updateDatagridViewAfterSearch(string TypeName, string FieldName, string search)
        //{
        //    //MessageBox.Show(TypeName + FieldName);
        //    if (TypeName == "All" && FieldName == "All")
        //    {
        //        TypeName = "";
        //        FieldName = "";
        //    }
        //    if (FieldName == "All")
        //    {
        //        FieldName = "";
        //    }
        //    DataTable dataTable = new DataTable();
        //    dataTable.Columns.AddRange(new DataColumn[]
        //    {
        //            new DataColumn {ColumnName = "id", DataType = typeof(int)},
        //            new DataColumn {ColumnName = "idField", DataType = typeof(int)},
        //            new DataColumn {ColumnName = "TypeName", DataType = typeof(string)},
        //            new DataColumn {ColumnName = "FieldName", DataType = typeof(string)},
        //            new DataColumn {ColumnName = "CustomerName", DataType = typeof(string)},
        //            new DataColumn {ColumnName = "CustomerPhone", DataType = typeof(string)},
        //            new DataColumn {ColumnName = "startTime", DataType = typeof(string)},
        //            new DataColumn {ColumnName = "endTime", DataType = typeof(string)},
        //            new DataColumn {ColumnName = "priceBooking", DataType = typeof(float)},
        //            new DataColumn {ColumnName = "status", DataType = typeof(string)},
        //            new DataColumn {ColumnName = "bookingDay", DataType = typeof(string)}
        //    });

        //    List<CustomerBookingDetail> customerBookingDetails = CustomerBookingDetailDAL.
        //        Instance.LoadCustomerBookingById1();
        //    foreach (CustomerBookingDetail customerbooking in customerBookingDetails)
        //    {
        //        if (customerbooking.ToString().Contains(TypeName) && customerbooking.ToString().Contains(FieldName) && customerbooking.ToString().ToLower().Contains(search))
        //        {
        //            dataTable.Rows.Add(customerbooking.Id, customerbooking.IdField,
        //            customerbooking.TypeName, customerbooking.FieldName, customerbooking.CustomerName,
        //            customerbooking.CustomerPhone, customerbooking.startTime.ToString("HH:mm"), customerbooking.endTime.ToString("HH:mm"),
        //            customerbooking.priceBooking, customerbooking.status, customerbooking.Ngaydat.ToString("dd/MM/yyyy"));
        //        }
        //    }
        //    dataGridView1.DataSource = dataTable;
        //}
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadCustomerBooking(cbTypeField.SelectedItem.ToString(), cbField.SelectedItem.ToString(), txtSearch.Text.ToLower());
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
        public bool checktimeFieldNeedAdd(int idField, string startTime, string endTime, DateTime ngaydat)
        {
            bool check = true;
            List<CustomerBooking> customerbookings = CustomerBookingDAL.Instance.LoadCustomerBooking();
            foreach (CustomerBooking customerbooking in customerbookings)
            {
                if ((customerbooking.Status == "dat truoc" || customerbooking.Status == "truc tiep") && customerbooking.IdFieldName == idField && customerbooking.Ngaydat.ToString("MM/dd/yyyy") == ngaydat.ToString("MM/dd/yyyy"))
                {
                    if (sosanhthoigian(endTime, customerbooking.StartTime.ToString("HH:mm")) == true &&
                        sosanhthoigian(endTime, customerbooking.EndTime.ToString("HH:mm")) == false)
                    {
                        check = false;
                        break;
                    }
                    if (sosanhthoigian(startTime, customerbooking.EndTime.ToString("hh:mm")) == false &&
                        sosanhthoigian(startTime, customerbooking.StartTime.ToString("hh:mm")) == true)
                    {
                        check = false;
                        break;
                    }
                    if (sosanhthoigian(startTime, customerbooking.StartTime.ToString("hh:mm")) == false &&
                        sosanhthoigian(endTime, customerbooking.EndTime.ToString("hh:mm")) == true)
                    {
                        check = false;
                        break;
                    }
                }
            }
            return check;
        }
        public bool checktimeFieldNeedUpdate(int idField, string startTime, string endTime, int idCustomerBooking, DateTime ngaydat)
        {
            bool check = true;
            List<CustomerBooking> customerbookings = CustomerBookingDAL.Instance.LoadCustomerBooking();
            foreach (CustomerBooking customerbooking in customerbookings)
            {
                if ((customerbooking.Status == "dat truoc" || customerbooking.Status == "truc tiep") && customerbooking.IdFieldName == idField && customerbooking.Id != idCustomerBooking && customerbooking.Ngaydat == ngaydat)
                {
                    if (sosanhthoigian(endTime, customerbooking.StartTime.ToString("HH:mm")) == true &&
                        sosanhthoigian(endTime, customerbooking.EndTime.ToString("HH:mm")) == false)
                    {
                        check = false;
                        break;
                    }
                    if (sosanhthoigian(startTime, customerbooking.EndTime.ToString("hh:mm")) == false &&
                        sosanhthoigian(startTime, customerbooking.StartTime.ToString("hh:mm")) == true)
                    {
                        check = false;
                        break;
                    }
                    if (sosanhthoigian(startTime, customerbooking.StartTime.ToString("hh:mm")) == false &&
                        sosanhthoigian(endTime, customerbooking.EndTime.ToString("hh:mm")) == true)
                    {
                        check = false;
                        break;
                    }
                }
            }
            return check;
        }
        public bool sosanhthoigian(string time1, string time2)
        {
            int minutes1 = TimeToMinutes(time1);
            int minutes2 = TimeToMinutes(time2);
            if (minutes1 > minutes2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int TimeToMinutes(string time)
        {
            string[] parts = time.Split(':');
            int hours = int.Parse(parts[0]);
            int minutes = int.Parse(parts[1]);
            return hours * 60 + minutes;
        }
    }
}


