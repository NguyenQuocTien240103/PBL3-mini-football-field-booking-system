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
    public partial class FormInformationBookingNow : Form
    {
       
        public FormInformationBookingNow()
        {
            InitializeComponent();
            setCombox();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void setCombox()
        {
            for(int i = 0; i < 24; i++)
            {
                cb1.Items.Add("0"+i);
                cb3.Items.Add("0"+i);
            }
            for (int i = 0; i < 60; i++)
            {
                if (i < 10)
                {
                    String index = i.ToString();
                    cb2.Items.Add("0"+i);
                    cb4.Items.Add("0" + i);

                }
                else
                {
                    cb2.Items.Add(i);
                    cb4.Items.Add(i);
                }
            }

        }

        public void abc(Field field)
        {
            // Gán dữ liệu từ Form1 cho textField2 của Form2
            txtFieldID.Text = field.Id.ToString();
            txtNameField.Text = field.Name.ToString();
            // lấy ra id của TypeField có trong table Field
            int idTypeField = field.IdFieldType;
            // lấy ra trương FieldType bằng cách lấy theo ID
            FieldType fieldType = FieldTypeDAL.Instance.getFieldTypeById(idTypeField);
            txtTypeField.Text = fieldType.TypeName.ToString(); // lấy ra tên loại sân từ id
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            float PriceBookinng = float.Parse(txtPrice.Text.ToString());
            string Name = txtName.Text;
            string Phone = txtPhone.Text;
            // insert Customer
            CustomerDAL.Instance.InsertCustomer(Name, Phone);
            // lấy idCustomer mới insert
            int idCustomer = CustomerDAL.Instance.getIdCustomerLast(); // đã giải quyết
            DateTime startTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 
                int.Parse(cb1.SelectedItem.ToString()), int.Parse(cb2.SelectedItem.ToString()), 0);

            DateTime endTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 
                int.Parse(cb3.SelectedItem.ToString()), int.Parse(cb4.SelectedItem.ToString()), 0);

            CustomerBookingDAL.Instance.InSertCustomerBooking(idCustomer, int.Parse(txtFieldID.Text), startTime,
                endTime, PriceBookinng, "truc tiep", DateTime.Now.Date);
            //update state field by idField
            FieldDAL.Instance.updateFieldById(int.Parse(txtFieldID.Text), "busy");
            this.Close();
        }
    }
}
