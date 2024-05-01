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
        private string saveStatusField = "";
        private int saveIDField = 0;
        private int temp = 0;
        public delegate void Mydel(Field field);
        private Field SaveField;
        public Mydel d { get; set; }
        public void LoadField()
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();

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
                            case "busy":
                                btn.BackColor = Color.Red;
                                break;
                            case "booked":
                                btn.BackColor = Color.Orange;
                                break;


                        }
                    }                

                }
            }
        }
         void ShowBill(int id)
        {
            //string sql = "select * from dbo.fieldname where id = " + id.ToString();
            //DataTable data = DataProvider.Instance.ExcuteQuery(sql);
            //dtgvBill.DataSource = data;

            //string sql = "Select * FROM dbo.Customer ";
            //DataTable data = CustomerDAL.Instance.LoadFieldList(sql);
            //dtgvBill.DataSource = data;


            MessageBox.Show(id.ToString());


            //String sql = "Select * FROM dbo.CustomerBooking where idFieldName=" + id.ToString();


            //DataTable data = DataProvider.Instance.ExcuteQuery(sql);
            DataTable data = CustomerBookingDAL.Instance.GetCustomerBookingByIDField(id);
            if(data!=null)
            {
                dtgvBill.DataSource = data;
            }
                
            
          
        }
        private void btn_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("tien");
            int fieldID = ((sender as Button).Tag as Field).Id;
            saveStatusField = ((sender as Button).Tag as Field).Status;
            //Field getField = ((sender as Button).Tag as Field);
            
            SaveField = ((sender as Button).Tag as Field);
            saveIDField = ((sender as Button).Tag as Field).Id;
            temp = saveIDField;
            ShowBill(fieldID);

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        // click button Booking on form BookingManager
        private void btnBooking_Click(object sender, EventArgs e)
        {
            // call form FormInformationBooking
            if (saveStatusField == "empty")
            {
                FormInformationBooking formInforBooking = new FormInformationBooking();
                this.Hide();
                formInforBooking.ShowDialog();
                this.Show();
               
            }
            else if (saveStatusField == "")
            {
                MessageBox.Show("Please Select Fields");
            }
            else
            {
                MessageBox.Show("reservation");
            }
            saveStatusField = "";

        }
        // click button Edit on form BookingManager
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // call form FormInformationBooking
            if (saveStatusField == "busy" || saveStatusField == "booking")
            {
                FormInformationBooking formInforBooking = new FormInformationBooking();
                this.Hide();
                formInforBooking.ShowDialog();
                this.Show();
            }
            else if (saveStatusField == "")
            {
                MessageBox.Show("Please Select Fields");
            }
            else
            {
                MessageBox.Show("empty");
            }
            saveStatusField = "";

        }

        // click button Pay on form BookingManager
        private void btnPay_Click(object sender, EventArgs e)
        {
            // call form FormInformationBooking
            if (saveStatusField == "busy" || saveStatusField == "booking")
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
            if (saveStatusField == "empty")
            {
                FormInformationBookingNow formInforBooking = new FormInformationBookingNow();
                // d tham chiếu tới hàm abc của đối tượng formInforBooking
                d = new Mydel(formInforBooking.abc);
                d(SaveField);
                this.Hide();
                formInforBooking.ShowDialog();
                LoadField();
                this.Show();
            }
            else if (saveStatusField == "")
            {
                MessageBox.Show("Please Select Fields");
            }
            else
            {
                MessageBox.Show("reservation");
            }
            saveStatusField = "";

        }

        public int getIdField_Choose()
        {
            return temp;
        }
    }
}
