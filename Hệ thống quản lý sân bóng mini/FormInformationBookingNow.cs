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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void setCombox()
        {
            for(int i = 0; i < 24; i++)
            {
                cb1.Items.Add(i);
                cb3.Items.Add(i);
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

        private void label6_Click(object sender, EventArgs e)
        {

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
            String startTime = cb1.SelectedItem.ToString()+'h'+cb2.SelectedItem.ToString();
            String endTime = cb3.SelectedItem.ToString() + 'h' + cb4.SelectedItem.ToString();
            float PriceBookinng = float.Parse(txtPrice.Text.ToString());
            String Name = txtName.Text;
            String Phone = txtPhone.Text;
            CustomerDAL.Instance.InsertCustomer(Name, Phone); // insert Customer

            // lấy idCustomer mới insert
            int idCustomer = CustomerDAL.Instance.getIdCustomerLast(); // đã giải quyết
            // lấy idField
            String idfieldChoose = txtFieldID.Text.ToString();
            int idFieldChoose = int.Parse(idfieldChoose);
            CustomerBookingDAL.Instance.InSertCustomerBooking(idCustomer, idFieldChoose, startTime, endTime, PriceBookinng,"1");
            // cập  nhật trạng thái
            BookingManager bookingManager = new BookingManager();
            if (PriceBookinng > 0)
            {
                FieldDAL.Instance.updateFieldById(idFieldChoose, "booked");
            }
            else
            {
                FieldDAL.Instance.updateFieldById(idFieldChoose, "busy");
            }
           
            this.Close();
        }
    }
}
