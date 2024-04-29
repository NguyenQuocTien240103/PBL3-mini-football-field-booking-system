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
    public partial class BookingManager : Form
    {
        public BookingManager()
        {
            InitializeComponent();
            LoadField();
        }
        private string buttonAValue;
        void LoadField()
        {
            List<Field> fieldList = FieldDAL.Instance.LoadFieldList();

            List<FieldType> fieldTypeList = FieldTypeDAL.Instance.LoadFieldType();

            foreach(Field item1 in fieldList)
            {
                Button btn = new Button()
                {
                    Width = 50,
                    Height = 50
                };
                btn.Click += btn_Click;
                btn.Tag = item1;
                btn.TabStop = false;
                foreach (FieldType item2 in fieldTypeList)
                {

                    if (item1.IdFieldType == item2.Id)
                    {
                        if(item2.Id == 1)
                        {
                            btn.Text = item2.TypeName + item1.Name + Environment.NewLine + item1.Status;
                            //btn.TabStop= false;
                            //btn.Click += btn_Click;
                            //btn.Tag = item1;
                            flowLayoutPanel1.Controls.Add(btn);
                        }

                        else if (item2.Id == 2)
                        {
                            btn.Text = item2.TypeName + item1.Name + Environment.NewLine + item1.Status;
                            //btn.TabStop = true;
                            //btn.Click += btn_Click;
                            //btn.Tag = item1;
                            flowLayoutPanel2.Controls.Add(btn);
                        }
                        
                        switch(item1.Status )
                        {
                            case "empty":
                                btn.BackColor = Color.Green;
                                break;


                        }
                    }                

                }
            }
        }
        void ShowBill(int id)
        {
                String sql = "Select * FROM dbo.FieldName Where id = " + id.ToString();
                DataTable data = DataProvider.Instance.ExcuteQuery(sql);
                dtgvBill.DataSource = data;
        }
        private void btn_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("tien");
            int fieldID = ((sender as Button).Tag as Field).Id;
            buttonAValue = ((sender as Button).Tag as Field).Status;
            ShowBill(fieldID);
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        // click button Booking on form BookingManager
        private void btnBooking_Click(object sender, EventArgs e)
        {
            // call form FormInformationBooking
            if (buttonAValue == "empty")
            {
                FormInformationBooking formInforBooking = new FormInformationBooking();
                this.Hide();
                formInforBooking.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("reservation");
            }
                
        }
        // click button Edit on form BookingManager
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // call form FormInformationBooking
            if (buttonAValue == "busy" || buttonAValue == "booking")
            {
                FormInformationBooking formInforBooking = new FormInformationBooking();
                this.Hide();
                formInforBooking.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("empty");
            }

        }

        // click button Pay on form BookingManager
        private void btnPay_Click(object sender, EventArgs e)
        {
            // call form FormInformationBooking
            if (buttonAValue == "busy" || buttonAValue=="booking")
            {
                PayMent payMent = new PayMent();
                this.Hide();
                payMent.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("empty");
            }
                
        }

        // click toolMenuItems on form BookingManager
        private void MenuItem_Click(object sender, EventArgs e)
        {
            Manager manager = new Manager();
            this.Hide();
            manager.ShowDialog();
            this.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnBookNow_Click(object sender, EventArgs e)
        {
            if (buttonAValue=="empty")
            {
                FormInformationBookingNow formInforBooking = new FormInformationBookingNow();
                this.Hide();
                formInforBooking.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("reservation");
            }
            
            
        }
    }
}
