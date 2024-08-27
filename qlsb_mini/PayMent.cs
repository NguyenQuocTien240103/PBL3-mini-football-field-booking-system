using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using qlsb_mini.BLL;
using qlsb_mini.DAL;
using qlsb_mini.demo;
using qlsb_mini.DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace qlsb_mini
{
    public partial class PayMent : Form
    {
        public delegate void Mydel();
        public Mydel d { get; set; }
        int IdField;
        public PayMent(int id=0)
        {
            InitializeComponent();
            setCombox();
            IdField = id;
            GUI();
        }
        public void GUI()
        {
            if(IdField != 0)
            {
                string Status = "truc tiep";
                CustomerBooking Cus = CustomerBookingBLL.Instance.GetCustomerBooking(IdField, Status);
                Field field = FieldBLL.Instance.GetFieldById(IdField);
                if(Cus!= null && field !=null)
                {
                    Customer customer = CustomerBLL.Instance.GetCustomerById(Cus.IdCustomer);
                    FieldType fieldType = FieldTypeBLL.Instance.GetFieldTypeById(field.IdFieldType);
                    if (fieldType != null && customer != null)
                    {
                        // field
                        txtID.Text = field.Id.ToString();
                        txtFieldName.Text = field.Name.ToString();
                        txtType.Text = fieldType.TypeName.ToString();
                        // customer
                        txtName.Text = customer.Name;
                        txtPhone.Text = customer.Phone;
                        // time
                        string tgbatdau = Cus.StartTime.ToString("HH:mm");
                        string tgkethuc = Cus.EndTime.ToString("HH:mm");
                        string[] parts1 = tgbatdau.Split(':');
                        string[] parts2 = tgkethuc.Split(':');
                        cb1.Text = parts1[0];
                        cb2.Text = parts1[1];
                        cb3.Text = parts2[0];
                        cb4.Text = parts2[1];
                        // price
                        txtPriceBooking.Text = Cus.PriceBooking.ToString();
                        cbPrice.Items.Add(fieldType.NormalPrice.ToString());
                        cbPrice.Items.Add(fieldType.SpecialPrice.ToString());

                    }
                }
            }
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
        private void cbPrice_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = cbPrice.SelectedItem.ToString();
            float price = float.Parse(s);
            showTotalPrice(price);
        }
        void showTotalPrice(float price)
        {
            float StartHours = float.Parse(cb1.SelectedItem.ToString());
            float StartMinutes = float.Parse(cb2.SelectedItem.ToString());
            float EndHours = float.Parse(cb3.SelectedItem.ToString());
            float EndMinutes = float.Parse(cb4.SelectedItem.ToString());
            float Handle = (EndHours * 60 + EndMinutes) - (StartHours * 60 + StartMinutes);
            float Result = Handle / 60;
            txtTotal.Text = (Result * price).ToString();
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            int idField = int.Parse(txtID.Text);
            float price = float.Parse(txtTotal.Text);
            if (txtTotal.Text != "")
            {
                BillBLL.Instance.AddBill(idField, price);
                d();
                this.Close();
            }
            else
            {
                MessageBox.Show("please select price!");
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
