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
        public delegate void Mydel(Field field);
        public Mydel d { get; set; }

        private Field SaveField;
        private string saveStatusField = "";
        private int saveIDField = -1;

        public void LoadField()
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();

            List<Field> fieldList = FieldDAL.Instance.LoadFieldList();

            List<FieldType> fieldTypeList = FieldTypeDAL.Instance.LoadFieldType();

            foreach(Field field in fieldList)
            {
                Button btn = new Button()
                {
                    Width = 120,
                    Height = 70,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0 },
                    Cursor = Cursors.Hand,
                    TextAlign = ContentAlignment.TopCenter, // Căn chỉnh văn bản bên trái
                    ImageAlign = ContentAlignment.BottomCenter, // Căn chỉnh hình ảnh bên phải
                    TextImageRelation = TextImageRelation.TextAboveImage, // Văn bản trước hình ảnh
                };

                Image originalImage = Image.FromFile("image\\sanbong.png");
                Image resizedImage = originalImage.GetThumbnailImage(btn.Width/2, btn.Height /2, null, IntPtr.Zero);
                btn.Image = resizedImage;
                btn.Click += btn_Click;
                btn.Tag = field;
                btn.TabStop = false;

                foreach (FieldType fieldType in fieldTypeList)
                {

                    if (field.IdFieldType == fieldType.Id)
                    {
                        if(fieldType.Id == 1)
                        {
                            // btn.Text = item2.TypeName + item1.Name + Environment.NewLine + item1.Status;
                            btn.Text = fieldType.TypeName + field.Name;

                            flowLayoutPanel1.Controls.Add(btn);
                        }

                        else if (fieldType.Id == 2)
                        {
                            // btn.Text = item2.TypeName + item1.Name + Environment.NewLine + item1.Status;
                            btn.Text = fieldType.TypeName + field.Name;

                            flowLayoutPanel2.Controls.Add(btn);
                        }
                        
                        switch(field.Status)
                        {
                            case "empty":
                                btn.BackColor = Color.FromArgb(75, 181, 67);  // Sử dụng mã màu RGB cho xanh
                                btn.ForeColor = Color.White;
                                break;
                            case "busy":
                                btn.BackColor = Color.FromArgb(204, 37, 41);  // Sử dụng mã màu RGB cho đỏ
                                btn.ForeColor = Color.White;
                                break;
                        }
                    }                
                }
            }
        }
         void ShowField(int idField)
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
                    new DataColumn {ColumnName = "status", DataType = typeof(string)},
                    new DataColumn {ColumnName = "bookingDay", DataType = typeof(string)}
                });
                List<CustomerBookingDetail> customerBookingDetails = CustomerBookingDetailDAL.
                Instance.LoadCustomerBookingById(idField);
            
                foreach (CustomerBookingDetail customerbooking in customerBookingDetails)
                {
                    dataTable.Rows.Add(customerbooking.Id,customerbooking.IdField,
                    customerbooking.TypeName, customerbooking.FieldName, customerbooking.CustomerName,
                    customerbooking.CustomerPhone, customerbooking.startTime.ToString("HH:mm"), customerbooking.endTime.ToString("HH:mm"),
                    customerbooking.priceBooking, customerbooking.status,customerbooking.Ngaydat.ToString("MM/dd/yyyy"));
                
                }
                dtgvBill.DataSource = dataTable;
                dtgvBill.Columns["id"].Visible = false;
                dtgvBill.Columns["idField"].Visible = false;
         }
        private void btn_Click(object sender, EventArgs e)
        {
            
            int fieldID = ((sender as Button).Tag as Field).Id;

            // lưu lại trạng thái sân
            saveStatusField = ((sender as Button).Tag as Field).Status;
            
            //lưu lại Field
            SaveField = ((sender as Button).Tag as Field);

            // lưu lại id của sân đó
            saveIDField = ((sender as Button).Tag as Field).Id;

            // show thông tin đặt sân từ id sân đó
            ShowField(fieldID);

        }
        private void btnBooking_Click(object sender, EventArgs e)
        {
              FormInformationBooking formInforBooking = new FormInformationBooking();
            // d tham chiếu tới hàm abc của đối tượng formInforBooking
                this.Hide();
                formInforBooking.ShowDialog();
                LoadField();
                this.Show();
        }
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
                MessageBox.Show("Vui lòng chọn sân");
            }
            else
            {
                MessageBox.Show("empty");
            }
            saveStatusField = "";
        }
        private void btnPay_Click(object sender, EventArgs e)
        {
            // call form FormInformationBooking
            if (saveStatusField == "busy" )
            {
                PayMent payMent = new PayMent();
                // d tham chiếu tới hàm abc của đối tượng formInforBooking
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
                MessageBox.Show("Vui lòng chọn sân");
            }
                
        }
        private void MenuItem_Click(object sender, EventArgs e)
        {
            Manager manager = new Manager();
            manager.d += new Manager.Mydel(LoadField);
            this.Hide();
            manager.ShowDialog();
            this.Show();
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
                MessageBox.Show("Vui lòng chọn sân");
            }
            else
            {
                MessageBox.Show("Đã được đặt");
            }
            saveStatusField = "";
        }
    }
}
