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

namespace qlsb_mini
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

        public void LoadField()
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();
            List<Field> fieldList = FieldBLL.Instance.GetAllField();
            List<FieldType> fieldTypeList = FieldTypeBLL.Instance.GetAllFieldType();

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
                Image resizedImage = originalImage.GetThumbnailImage(btn.Width / 2, btn.Height / 2, null, IntPtr.Zero);
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
                            btn.Text = fieldType.TypeName + field.Name;

                            flowLayoutPanel1.Controls.Add(btn);
                        }

                        else if (fieldType.Id == 2)
                        {
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
                string State = "truc tiep";
                dtgvBill.DataSource = CustomerBookingBLL.Instance.ShowCustomerBooking(idField, State);
                dtgvBill.Columns["id"].Visible = false;
                dtgvBill.Columns["idField"].Visible = false;
         }
        int fieldID;
        private void btn_Click(object sender, EventArgs e)
        {
            fieldID = ((sender as Button).Tag as Field).Id;

            // lưu lại trạng thái sân
            saveStatusField = ((sender as Button).Tag as Field).Status;
            
            //lưu lại Field
            SaveField = ((sender as Button).Tag as Field);

            // show thông tin đặt sân từ id sân đó
            ShowField(fieldID);
        }
        private void btnBooking_Click(object sender, EventArgs e)
        {
                FormInformationBooking formInforBooking = new FormInformationBooking();
                this.Hide();
                formInforBooking.ShowDialog();
                LoadField();
                this.Show();
        }
        private void btnBookNow_Click(object sender, EventArgs e)
        {
            if (saveStatusField == "empty")
            {
                EditForm editForm = new EditForm(fieldID);
                editForm.d += new EditForm.Mydel(LoadField);
                this.Hide();
                editForm.ShowDialog();
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
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (saveStatusField == "busy")
            {
                EditForm editForm = new EditForm(fieldID);
                editForm.d += new EditForm.Mydel(LoadField);
                this.Hide();
                editForm.ShowDialog();
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
            if (saveStatusField == "busy" )
            {
                PayMent payMent = new PayMent(fieldID);
                payMent.d += new PayMent.Mydel(LoadField);
                this.Hide();
                payMent.ShowDialog();
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
    }
}
