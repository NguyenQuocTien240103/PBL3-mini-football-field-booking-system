using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using qlsb_mini.BLL;
using qlsb_mini.DAL;
using qlsb_mini.demo;
using qlsb_mini.DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace qlsb_mini
{
    public partial class EditForm : Form
    {
        public delegate void Mydel();
        public Mydel d { get; set; }
        int IdField;
        public EditForm(int id = 0)
        {
            InitializeComponent();
            setCombox();
            LoadFieldType();
            IdField = id;
            GUI();
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
        void LoadFieldType()
        {
            List<FieldType> listFieldType = FieldTypeBLL.Instance.GetAllFieldType();
            cbFieldType.DataSource = listFieldType;
            cbFieldType.DisplayMember = "TypeName";
        }
        private int IdFieldType; // lưu Id của TypeField
        private void cbFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FieldType choosed = cbFieldType.SelectedItem as FieldType;
            List<FieldType> listFieldType = FieldTypeDAL.Instance.LoadFieldType();
            if (choosed != null)
            {
                // lấy ra idFieldType 
                foreach (FieldType fieldType in listFieldType) {
                    if(fieldType.Id == choosed.Id)
                    {
                        IdFieldType = choosed.Id;  // đã lưu được Id của TypeField
                        break;
                    }
                }
                LoadFieldByIdFieldType(IdFieldType); 
            }
        }
        void LoadFieldByIdFieldType(int IdFieldType)
        {
            List<Field> listField = FieldBLL.Instance.GetAllField(IdFieldType);
            cbNameField.DataSource = listField;
            cbNameField.DisplayMember = "name";
        }
        private void cbNameField_SelectedIndexChanged(object sender, EventArgs e)
        {
            Field choosed = cbNameField.SelectedItem as Field;
            List<Field> listField = FieldBLL.Instance.GetAllField();
            if (choosed != null)
            {
                string FieldName = choosed.Name; // lấy ra được tên sân
                foreach (Field field in listField)
                {
                    if (field.IdFieldType == IdFieldType && field.Name == FieldName)
                    {
                        txtFieldID.Text = field.Id.ToString();
                    }
                }
            }
        }
        public void GUI()
        {
            if (IdField != 0)
            {
                string Status = "truc tiep";
                CustomerBooking customerBooking = CustomerBookingBLL.Instance.GetCustomerBooking(IdField, Status);
                Field field = FieldBLL.Instance.GetFieldById(IdField);
                if (customerBooking != null && field != null)
                {
                    Customer Cus = CustomerBLL.Instance.GetCustomerById(customerBooking.IdCustomer);
                    FieldType fieldType = FieldTypeBLL.Instance.GetFieldTypeById(field.IdFieldType);
                    if (fieldType != null && Cus != null)
                    {
                        // field
                        txtFieldID.Text = field.Id.ToString();
                        cbFieldType.Text = fieldType.TypeName.ToString();
                        cbNameField.Text = field.Name.ToString();
                        // customer
                        txtName.Text = Cus.Name;
                        txtPhone.Text = Cus.Phone;
                        // time
                        string tgbatdau = customerBooking.StartTime.ToString("HH:mm");
                        string tgkethuc = customerBooking.EndTime.ToString("HH:mm");
                        string[] parts1 = tgbatdau.Split(':');
                        string[] parts2 = tgkethuc.Split(':');
                        cb1.Text = parts1[0];
                        cb2.Text = parts1[1];
                        cb3.Text = parts2[0];
                        cb4.Text = parts2[1];
                        // price
                        txtPriceBooking.Text = customerBooking.PriceBooking.ToString();
                    }
                }
                if(customerBooking == null && field != null)
                {
                    FieldType fieldType = FieldTypeBLL.Instance.GetFieldTypeById(field.IdFieldType);
                    if(fieldType != null)
                    {
                        // field
                        txtFieldID.Text = field.Id.ToString();
                        cbFieldType.Text = fieldType.TypeName.ToString();
                        cbNameField.Text = field.Name.ToString();
                        txtPriceBooking.Text = "0";
                        cbFieldType.Enabled = false;
                        cbNameField.Enabled = false;
                    }
                }
            }
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DateTime StartTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, int.Parse(cb1.SelectedItem.ToString()), int.Parse(cb2.SelectedItem.ToString()), 0);
            DateTime EndTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, int.Parse(cb3.SelectedItem.ToString()), int.Parse(cb4.SelectedItem.ToString()), 0);
            DateTime Date = DateTime.Now.Date;
            int Id = 1; // id bất kì
            string Name = txtName.Text;
            string Phone = txtPhone.Text;
            Customer Cus = new Customer(Id, Name, Phone);
            float PriceBooking = float.Parse(txtPriceBooking.Text);
            string State = "truc tiep";
            // nếu sân và loại sân bị khóa chứng tỏ ta đang thục hiện chức năng đặt sân trực tiếp
            if (cbFieldType.Enabled== false && cbNameField.Enabled == false)
            {
                CustomerBookingBLL.Instance.AddCustomerBooking(IdField, Cus, PriceBooking, StartTime, EndTime, Date, State);
            }
            if(cbFieldType.Enabled != false && cbNameField.Enabled != false)
            {
                CustomerBooking customerBooking = CustomerBookingBLL.Instance.GetCustomerBooking(IdField, State); // IdField khhi ta click vào
                int IdCustomerBooking = customerBooking.Id;
                int idField = int.Parse(txtFieldID.Text); // idField hiển thị trong đơn trước khi ta confirm (không phải idField khi ta click vào)
                CustomerBookingBLL.Instance.UpdateCustomerBooking(IdCustomerBooking, idField, StartTime, EndTime, Date, Cus, PriceBooking, State);
            }
            d();
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
