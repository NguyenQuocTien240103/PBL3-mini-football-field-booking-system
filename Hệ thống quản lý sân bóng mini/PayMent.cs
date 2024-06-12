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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Hệ_thống_quản_lý_sân_bóng_mini
{
    public partial class PayMent : Form
    {
        public PayMent()
        {
            InitializeComponent();
            setCombox();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void abc(Field field)
        {
            // Gán dữ liệu từ Form1 cho textField2 của Form2
            txtID.Text = field.Id.ToString();
            txtFieldName.Text = field.Name.ToString();
            // lấy ra trương FieldType bằng cách lấy theo ID
            FieldType fieldType = FieldTypeDAL.Instance.getFieldTypeById(field.IdFieldType);
            txtType.Text = fieldType.TypeName.ToString(); 
            cbPrice.Items.Add(fieldType.NormalPrice.ToString());
            cbPrice.Items.Add(fieldType.SpecialPrice.ToString());
            showInformationFromField();
        }
        void setCombox()
        {
            for (int i = 0; i < 24; i++)
            {
                cb1.Items.Add("0" + i);
                cb3.Items.Add("0" + i);
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
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            int idField = int.Parse(txtID.Text.ToString());
            float price = float.Parse(txtTotal.Text);
            if (txtTotal.Text != "")
            {
                int idCustomerBooking = CustomerBookingDAL.Instance.GetIdCustomerBooking(idField,"truc tiep");
                BillDAL.Instance.insertBill(idCustomerBooking, price);
                CustomerBookingDAL.Instance.updateCustomerBookingDone(idField);
                FieldDAL.Instance.updateFieldState(idField, "empty");
                this.Close();   
            }
        }
        private void cbPrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string s = cb.SelectedItem.ToString();
            float price = float.Parse(s);
            showTotalPrice(price);
        }
        void showTotalPrice(float price)
        {
            string giobatdau = cb1.SelectedItem.ToString();
            float giobegin = float.Parse(giobatdau);
            string phutbatdau = cb2.SelectedItem.ToString();
            float phutbegin = float.Parse(phutbatdau);
            string giokethuc = cb3.SelectedItem.ToString();
            float gioend = float.Parse(giokethuc);
            string phutkethuc = cb4.SelectedItem.ToString();
            float phutend = float.Parse(phutkethuc);
            float hieu = (gioend * 60 + phutend) - (giobegin * 60 + phutbegin);
            float a = hieu / 60;
            txtTotal.Text = (a * price).ToString();
        }
        void showInformationFromField()
        {
            List<CustomerBooking> customerBookings = CustomerBookingDAL.Instance.LoadCustomerBooking();
            int idField = int.Parse(txtID.Text);
            string tgbatdau = "";
            string tgkethuc = "";
            int idCustomer = 0;
            foreach ( CustomerBooking customerBooking in customerBookings )
            {
                if(customerBooking.IdFieldName == idField && customerBooking.Status=="truc tiep")
                {
                    tgbatdau = customerBooking.StartTime.ToString("HH:mm");
                    tgkethuc = customerBooking.EndTime.ToString("HH:mm");
                    idCustomer = customerBooking.IdCustomer;
                    txtPriceBooking.Text = customerBooking.PriceBooking.ToString();
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
            foreach( Customer customer in ListCustomers )
            {
                if (customer.Id == idCustomer)
                {
                    txtName.Text=customer.Name;
                    txtPhone.Text=customer.Phone;
                }
            }
        }
    }
}
