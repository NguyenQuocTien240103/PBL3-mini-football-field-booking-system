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
        // lưu lại id sân đã chọn
        private int tempForSaveidField = 0;
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void setCombox()
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


        private int saveIdFieldType = 0;
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
                        saveIdFieldType = choosed.Id;
                    }
                }
                LoadFieldByIdTypeField(choosed.Id);
                
            }
        }
        private string saveFieldName = ""; 
        private void cbNameField_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            Field choosed = cb.SelectedItem as Field;

            if(choosed != null)
            {
                saveFieldName = choosed.Name; // lấy ra được tên roài
            }
            show(saveIdFieldType, saveFieldName);
            
        }

        void show(int idType,string FieldName)
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
            showInformationFromField();
        }
        void showInformationFromField()
        {
            List<CustomerBooking> customerBookings = CustomerBookingDAL.Instance.LoadCustomerBooking();

            int idField = int.Parse(txtFieldID.Text);
            // lưu lại id bàn đã chọn vào biến temp;
            tempForSaveidField = idField;
            string tgbatdau = "";
            string tgkethuc = "";
            int idCustomer = 0;
            float priceBooking = 0;
            foreach (CustomerBooking customerBooking in customerBookings)
            {
                if (customerBooking.IdFieldName == idField && customerBooking.Status == "chua thanh toan")
                {
                    tgbatdau = customerBooking.StartTime;
                    tgkethuc = customerBooking.EndTime;
                    idCustomer = customerBooking.IdCustomer;
                    priceBooking = customerBooking.PriceBooking;
                    txtPriceBooking.Text = priceBooking.ToString();
                    break;
                }
            }
            string[] parts1 = tgbatdau.Split('h');
            string[] parts2 = tgkethuc.Split('h');
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

            String startTime = cb1.SelectedItem.ToString() + 'h' + cb2.SelectedItem.ToString();
            String endTime = cb3.SelectedItem.ToString() + 'h' + cb4.SelectedItem.ToString();
            int idCustomerBooking = 0; // lưu lại idCustomerBooking
            float PriceBookinng = float.Parse(txtPriceBooking.Text.ToString()); // lấy ra text của priceBooking
            int idCustomer = 0; // lưu lại id của customer
            List<CustomerBooking> customerBookings = CustomerBookingDAL.Instance.LoadCustomerBooking();
            foreach (CustomerBooking customerBooking in customerBookings)
            {
                if (customerBooking.IdFieldName == tempForSaveidField && customerBooking.Status == "chua thanh toan")
                {
                    idCustomer = customerBooking.IdCustomer;
                    idCustomerBooking = customerBooking.Id;
                 //   MessageBox.Show(idCustomer.ToString());
                    break;
                }
            }
      //      MessageBox.Show(txtName.Text.ToString());
        //    MessageBox.Show(txtPhone.Text.ToString());
            CustomerDAL.Instance.updateCustomer(idCustomer,txtName.Text,txtPhone.Text);


            if (!int.Parse(txtFieldID.Text).Equals(tempForSaveidField))
            {
                // cập nhật lại idField mới và chuyển idField cũ về empty
                FieldDAL.Instance.updateFieldById(tempForSaveidField, "empty");

                if(PriceBookinng > 0)
                {

                }
                else
                {
                    FieldDAL.Instance.updateFieldById(int.Parse(txtFieldID.Text), "busy");
                    CustomerBookingDAL.Instance.updateCustomerBooking(idCustomerBooking, idCustomer, int.Parse(txtFieldID.Text), startTime, endTime, PriceBookinng,"chua thanh toan");
                }

            }
            

            this.Close();
        }
    }
}
