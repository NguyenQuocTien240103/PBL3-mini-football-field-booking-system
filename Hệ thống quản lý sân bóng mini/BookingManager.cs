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
    
        public delegate void Mydel(Field field);
        public delegate void Mydel1();
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
                            
                            flowLayoutPanel1.Controls.Add(btn);
                        }

                        else if (item2.Id == 2)
                        {
                            btn.Text = item2.TypeName + item1.Name + Environment.NewLine + item1.Status;
                            
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
         void ShowBill(int idField)
        {
                dtgvBill.Controls.Clear();
                DataTable dataTable = new DataTable();
                dataTable.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn {ColumnName = "id", DataType = typeof(int)},
                    new DataColumn {ColumnName = "idField", DataType = typeof(int)},
                    new DataColumn {ColumnName = "TypeName", DataType = typeof(string)},
                    new DataColumn {ColumnName = "FieldName", DataType = typeof(string)},
                    new DataColumn {ColumnName = "CustomerName", DataType = typeof(string)},
                    new DataColumn {ColumnName = "CustomerPhone", DataType = typeof(string)},
                    new DataColumn {ColumnName = "startTime", DataType = typeof(string)},
                    new DataColumn {ColumnName = "endTime", DataType = typeof(string)},
                    new DataColumn {ColumnName = "priceBooking", DataType = typeof(float)},
                    new DataColumn {ColumnName = "status", DataType = typeof(string)}
                });
            List<CustomerBookingDetail> customerBookingDetails = CustomerBookingDetailDAL.
                Instance.LoadCustomerBookingById(idField);
            foreach (CustomerBookingDetail customerbooking in customerBookingDetails)
            {
                dataTable.Rows.Add(customerbooking.Id,customerbooking.IdField,
                customerbooking.TypeName, customerbooking.FieldName, customerbooking.CustomerName,
                customerbooking.CustomerPhone, customerbooking.startTime, customerbooking.endTime,
                customerbooking.priceBooking, customerbooking.status);
            }
            dtgvBill.DataSource = dataTable;

        }
        private void btn_Click(object sender, EventArgs e)
        {
        
            int fieldID = ((sender as Button).Tag as Field).Id;

            saveStatusField = ((sender as Button).Tag as Field).Status;
            
            SaveField = ((sender as Button).Tag as Field);

            saveIDField = ((sender as Button).Tag as Field).Id;

            ShowBill(fieldID);

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        // click button Booking on form BookingManager
        private void btnBooking_Click(object sender, EventArgs e)
        {
            // call form FormInformationBooking
              FormInformationBooking formInforBooking = new FormInformationBooking();
            // d tham chiếu tới hàm abc của đối tượng formInforBooking
        
                this.Hide();
                formInforBooking.ShowDialog();
                LoadField();
                this.Show();
     

        }
        // click button Edit on form BookingManager
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // call form FormInformationBooking
            if (saveStatusField == "busy")
            {
                
                EditForm editForm = new EditForm();
                d = new Mydel(editForm.abc);
                d(SaveField);
                
                editForm.ShowDialog();
                LoadField();
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
            if (saveStatusField == "busy" )
            {
                //PayMent pay = new PayMent ();
                // d tham chiếu tới hàm abc của đối tượng formInforBooking
                
                PayMent payMent = new PayMent();
                this.Hide();
                d = new Mydel(payMent.abc);
                d(SaveField);
                payMent.ShowDialog();
                LoadField();
                this.Show();
            }
            else if(saveStatusField == "empty")
            {
                MessageBox.Show("empty");
            }
            else
            {
                MessageBox.Show("Please Select Fields");
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
    }
}
