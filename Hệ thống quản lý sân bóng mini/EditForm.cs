using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class EditForm : Form
    {
        public EditForm()
        {
            InitializeComponent();
            LoadTypeField();
            setCombox();
        }
        void setCombox()
        {
            for (int i = 0; i < 24; i++)
            {
                cb1.Items.Add("0"+i);
                cb3.Items.Add("0"+i);
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
        void LoadTypeField()
        {
            List<FieldType> listFieldType = FieldTypeDAL.Instance.LoadFieldType();
            cbFieldType.DataSource = listFieldType;
            cbFieldType.DisplayMember = "TypeName";
        }
        void LoadFieldByIdTypeField(int id)
        {
            List<Field> listField = FieldDAL.Instance.GetFieldByIdFieldType(id);
            cbNameField.DataSource = listField;
            cbNameField.DisplayMember = "name";
        }
        private int IdType = 0; // lưu Id của TypeField
        private void cbFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            FieldType choosed = cb.SelectedItem as FieldType;
            List<FieldType> listFieldType = FieldTypeDAL.Instance.LoadFieldType();
            if (choosed != null)
            {
                // lấy ra idFieldType 
                foreach (FieldType fieldType in listFieldType) {
                    if(fieldType.Id == choosed.Id)
                    {
                        IdType = choosed.Id;  // đã lưu được Id của TypeField
                        break;
                    }
                }
                LoadFieldByIdTypeField(choosed.Id); 
            }
        }
        private string FieldName = ""; // lưu tên của Field
        private void cbNameField_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            Field choosed = cb.SelectedItem as Field;

            if(choosed != null)
            {
                FieldName = choosed.Name; // lấy ra được tên sân
            }
            showIdFieldBy(IdType, FieldName);
        }
        void showIdFieldBy(int idType,string FieldName)
        {
            List<Field> listField = FieldDAL.Instance.LoadFieldList();
            foreach (Field field in listField)
            {
                if(field.IdFieldType == idType && field.Name==FieldName) {
                    txtFieldID.Text = field.Id.ToString(); 
                }
            }  
        }
        public void abc(Field field)
        {
            txtFieldID.Text = field.Id.ToString();
            FieldType fieldType = FieldTypeDAL.Instance.getFieldTypeById(field.IdFieldType);
            cbFieldType.Text = fieldType.TypeName.ToString();
            cbNameField.Text = field.Name.ToString();
            showInformationFromField();
        }

        // lưu lại id sân đã chọn ban đầu
        private int tempForSaveidField = 0;
        void showInformationFromField()
        {
            List<CustomerBooking> customerBookings = CustomerBookingDAL.Instance.LoadCustomerBooking();
            int idField = int.Parse(txtFieldID.Text);
            // lưu lại id Field đã chọn vào biến temp;
            tempForSaveidField = idField;
            string tgbatdau = ""; //lưu tg bất đầu trong đơn
            string tgkethuc = ""; //lưu tg kết thúc trong đơn
            int idCustomer = 0;     // lưu id Customer trong đơn
            float priceBooking = 0; // lưu giá đặt cọc trong đơn
            foreach (CustomerBooking customerBooking in customerBookings)
            {
                if (customerBooking.IdFieldName == idField && customerBooking.Status == "truc tiep")
                {
                    tgbatdau = customerBooking.StartTime.ToString("HH:mm");
                    tgkethuc = customerBooking.EndTime.ToString("HH:mm");
                    idCustomer = customerBooking.IdCustomer;
                    priceBooking = customerBooking.PriceBooking;
                    txtPriceBooking.Text = priceBooking.ToString();
                    break;
                }
            }
            string[] parts1 = tgbatdau.Split(':');
            string[] parts2 = tgkethuc.Split(':');
            cb1.Text = parts1[0];
            cb2.Text = parts1[1];
            cb3.Text = parts2[0];
            cb4.Text = parts2[1];

            List<Customer> ListCustomers = CustomerDAL.Instance.LoadCustomerList();
            foreach (Customer customer in ListCustomers)
            {
                if (customer.Id == idCustomer)
                {
                    txtName.Text = customer.Name; 
                    txtPhone.Text = customer.Phone;
                }
            }
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DateTime startTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 
                DateTime.Today.Day, int.Parse(cb1.SelectedItem.ToString()), int.Parse(cb2.SelectedItem.ToString()), 0);
           
            DateTime endTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 
                DateTime.Today.Day, int.Parse(cb3.SelectedItem.ToString()), int.Parse(cb4.SelectedItem.ToString()), 0);

            int idCustomerBooking = 0; // lưu lại idCustomerBooking
            float PriceBookinng = float.Parse(txtPriceBooking.Text.ToString()); // lấy ra text của priceBooking
            int idCustomer = 0; // lưu lại id của customer

            // lấy ra đơn đặt CustomerBooking, id cần update
            List<CustomerBooking> customerBookings = CustomerBookingDAL.Instance.LoadCustomerBooking();
            foreach (CustomerBooking customerBooking in customerBookings)
            {
                if (customerBooking.IdFieldName == tempForSaveidField && customerBooking.Status == "truc tiep") //tempForSaveidField là idField sân mà mình click vào
                {
                    idCustomer = customerBooking.IdCustomer;
                    idCustomerBooking = customerBooking.Id;
                    break;
                }
            }

            // update Customer
            CustomerDAL.Instance.updateCustomer(idCustomer,txtName.Text,txtPhone.Text);

            // nếu id sân hiện tại khác với id sân mình đã click vào
            if (!int.Parse(txtFieldID.Text).Equals(tempForSaveidField))
            {
                // kiểm tra trạng thái của sân mình muốn chuyển qua
                if (checkFieldBusyBy(int.Parse(txtFieldID.Text))){
                    MessageBox.Show("sân này đang có người đá vui lòng chọn sân khác");
                }
                else
                {
                    if (ktragiodat(int.Parse(txtFieldID.Text), startTime.ToString("HH:mm"), endTime.ToString("HH:mm"),DateTime.Now.ToString("dd/MM/yyyy")))
                    {
                        FieldDAL.Instance.updateFieldById(tempForSaveidField, "empty");
                        FieldDAL.Instance.updateFieldById(int.Parse(txtFieldID.Text), "busy");
                        CustomerBookingDAL.Instance.updateCustomerBooking(idCustomerBooking, idCustomer, int.Parse(txtFieldID.Text), startTime, endTime, PriceBookinng, "truc tiep", DateTime.Now);
                    }
                    else
                    {
                        MessageBox.Show("Sân đã có người đặt");
                    }
                }
            }
            else
            {
                if (ktragiodat(int.Parse(txtFieldID.Text), startTime.ToString("HH:mm"), endTime.ToString("HH:mm"), DateTime.Now.ToString("dd/MM/yyyy")))
                {
                    CustomerBookingDAL.Instance.updateCustomerBooking(idCustomerBooking, idCustomer, int.Parse(txtFieldID.Text), startTime, endTime, PriceBookinng, "truc tiep", DateTime.Now);
                }
                else
                {
                    MessageBox.Show("Sân đã có người đặt");
                }
            }

            this.Close();
        }
        bool checkFieldBusyBy(int idField)
        {
            string status = "";
            List<Field> listField = FieldDAL.Instance.LoadFieldList();
            foreach (Field field in listField)
            {
                if (field.Id == idField)
                {
                    status = field.Status;
                }
            }
            if (status == "empty")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public bool ktragiodat(int idField, string startTime, string endTime, string ngaydat)
        {
            bool check = true;
            List<CustomerBooking> customerbookings = CustomerBookingDAL.Instance.LoadCustomerBooking();
            foreach (CustomerBooking customerbooking in customerbookings)
            {
                if (customerbooking.Status == "dat truoc" && customerbooking.IdFieldName == idField && customerbooking.Ngaydat.ToString("dd/MM/yyyy") == ngaydat)
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
