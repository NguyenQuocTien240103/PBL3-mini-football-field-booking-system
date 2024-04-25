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
                foreach(FieldType item2 in fieldTypeList)
                {
                    if(item1.IdFieldType == item2.Id)
                    {
                        if(item2.Id == 1)
                        {
                            btn.Text = item2.TypeName + item1.Name + Environment.NewLine + item1.Status;
                            btn.TabStop= false;
                            flowLayoutPanel1.Controls.Add(btn);
                        }

                        else if (item2.Id == 2)
                        {
                            btn.Text = item2.TypeName + item1.Name + Environment.NewLine + item1.Status;
                            btn.TabStop = true;
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

        private void label3_Click(object sender, EventArgs e)
        {

        }
        // click button Booking on form BookingManager
        private void btnBooking_Click(object sender, EventArgs e)
        {
            // call form FormInformationBooking
            FormInformationBooking formInforBooking = new FormInformationBooking();
            this.Hide();
            formInforBooking.ShowDialog();
            this.Show();
        }
        // click button Edit on form BookingManager
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // call form FormInformationBooking
            FormInformationBooking formInforBooking = new FormInformationBooking();
            this.Hide();
            formInforBooking.ShowDialog();
            this.Show();
        }

        // click button Pay on form BookingManager
        private void btnPay_Click(object sender, EventArgs e)
        {
            // call form FormInformationBooking
            PayMent payMent = new PayMent();
            this.Hide();
            payMent.ShowDialog();
            this.Show();
        }

        // click toolMenuItems on form BookingManager
        private void MenuItem_Click(object sender, EventArgs e)
        {
            Manager manager = new Manager();
            this.Hide();
            manager.ShowDialog();
            this.Show();
        }


    }
}
