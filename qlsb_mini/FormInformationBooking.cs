using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using qlsb_mini.BLL;
using qlsb_mini.DAL;
using qlsb_mini.demo;
using qlsb_mini.DTO;

namespace qlsb_mini
{
    public partial class FormInformationBooking : Form
    {
        public FormInformationBooking()
        {
            InitializeComponent();
            setComboxTime();
            LoadCustomerBooking();
            loadFieldType();
        }
        void loadFieldType()
        {
            if(cbTypeField.Items.Count == 0)
            {
                cbTypeField.Items.Add(new FieldType(0,"All"));
                List<FieldType> listFieldType = FieldTypeBLL.Instance.GetAllFieldType();
                foreach (FieldType type in listFieldType)
                {
                    cbTypeField.Items.Add(type.TypeName);
                }
            }
            cbTypeField.SelectedIndex = 0;
        }
        int idType = 0;
        private void cbTypeField_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<FieldType> listFieldType = FieldTypeBLL.Instance.GetAllFieldType();
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem.ToString() == "All")
            {
                LoadFieldByIdTypeField();
            }
            else
            {   
                foreach (FieldType fieldType in listFieldType)
                {
                    if (fieldType.TypeName == cb.SelectedItem.ToString())
                    {
                        idType = fieldType.Id;
                        LoadFieldByIdTypeField(fieldType.Id);
                        break;
                    }
                }
            }
        }
        void LoadFieldByIdTypeField(int idFieldType=0)
        {
            cbField.Items.Clear();
            if (cbField.Items.Count == 0)
            {
                cbField.Items.Add(new Field(0, "All"));
                if (idFieldType != 0)
                {
                    List<Field> listField = FieldBLL.Instance.GetAllField(idFieldType);
                    foreach (Field field in listField)
                    {
                        cbField.Items.Add(field.Name);
                    }
                }
            }
            cbField.SelectedIndex = 0;
        }
        private void cbField_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            txtFieldType.Text = cbTypeField.SelectedItem.ToString();
            txtFieldName.Text = cb.SelectedItem.ToString();
            List<Field> listField = FieldBLL.Instance.GetAllField(idType);
            foreach (Field field in listField)
            {
                if (field.IdFieldType == idType && field.Name == txtFieldName.Text)
                {
                    txtIdField.Text = field.Id.ToString();
                }
                if(cbTypeField.SelectedItem.ToString() == "All" || cb.SelectedItem.ToString() == "All")
                {
                    txtIdField.Text = "";
                }
            }
        }
        void setComboxTime()
        {
            for (int i = 0; i < 24; i++)
            {
                if (i < 10)
                {
                    string index = i.ToString();
                    cb1.Items.Add("0" + i);
                    cb3.Items.Add("0" + i);
                }
                else
                {
                    cb1.Items.Add(i);
                    cb3.Items.Add(i);
                }
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
        void LoadCustomerBooking()
        {
            int IdField = 0; // cho IdField = 0 để lấy ra tất cả các Field bằng cách xử lí logic ở câu truy vấn
            string State = "dat truoc";
            dataGridView1.DataSource = CustomerBookingBLL.Instance.ShowCustomerBooking(IdField,State);
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["idField"].Visible = false;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Lấy hàng đã click
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                // Tiếp theo, bạn có thể lấy giá trị từ các ô trong hàng đã chọn
                txtIdCustomerBooking.Text= selectedRow.Cells[0].Value.ToString();
                txtIdField.Text = selectedRow.Cells[1].Value.ToString();
                txtFieldType.Text = selectedRow.Cells[2].Value.ToString();
                cbTypeField.Text = selectedRow.Cells[2].Value.ToString();
                txtFieldName.Text = selectedRow.Cells[3].Value.ToString();
                cbField.Text = selectedRow.Cells[3].Value.ToString();
                txtCustomerName.Text = selectedRow.Cells[4].Value.ToString();
                txtCustomerPhone.Text = selectedRow.Cells[5].Value.ToString();
                txtPriceBooking.Text = selectedRow.Cells[8].Value.ToString();
                txtStatus.Text = selectedRow.Cells[9].Value.ToString();
                dateTimePicker1.Text = selectedRow.Cells[10].Value.ToString();
                // lấy time
                string tgbatdau = selectedRow.Cells[6].Value.ToString();
                string tgkethuc = selectedRow.Cells[7].Value.ToString();
                string[] parts1 = tgbatdau.Split(':');
                string[] parts2 = tgkethuc.Split(':');
                cb1.Text = parts1[0];
                cb2.Text = parts1[1];
                cb3.Text = parts2[0];
                cb4.Text = parts2[1];
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            float PriceBookinng = float.Parse(txtPriceBooking.Text);
            int Id = 1; // id bất kì
            string Name = txtCustomerName.Text;
            string Phone = txtCustomerPhone.Text;
            Customer Cus = new Customer(Id, Name, Phone);
            DateTime StartTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, int.Parse(cb1.SelectedItem.ToString()), int.Parse(cb2.SelectedItem.ToString()), 0);
            DateTime EndTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, int.Parse(cb3.SelectedItem.ToString()), int.Parse(cb4.SelectedItem.ToString()), 0);
            DateTime Date = dateTimePicker1.Value.Date;
            string State = "dat truoc";
            if (txtIdField.Text != "")
            {
                int IdField = int.Parse(txtIdField.Text);
                CustomerBookingBLL.Instance.AddCustomerBooking(IdField, Cus, PriceBookinng, StartTime, EndTime, Date, State);
            }
            else
            {
                MessageBox.Show("Sân bạn chọn không hợp lệ");
            }
            LoadCustomerBooking();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string s1 = txtIdCustomerBooking.Text;
            if (s1 == "")
            {
                MessageBox.Show("vui lòng chọn đơn đặt sân");
            }
            else
            {
                int IdCustomerBooking = int.Parse(s1);
                int IdField = int.Parse(txtIdField.Text);
                DateTime StartTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, int.Parse(cb1.SelectedItem.ToString()), int.Parse(cb2.SelectedItem.ToString()), 0);
                DateTime EndTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, int.Parse(cb3.SelectedItem.ToString()), int.Parse(cb4.SelectedItem.ToString()), 0);
                DateTime Date = dateTimePicker1.Value.Date;
                int Id = 1; // id bất kì
                string Name = txtCustomerName.Text;
                string Phone = txtCustomerPhone.Text;
                Customer Cus = new Customer(Id, Name, Phone);
                float PriceBooking = float.Parse(txtPriceBooking.Text);
                string State = txtStatus.Text;
                CustomerBookingBLL.Instance.UpdateCustomerBooking(IdCustomerBooking, IdField, StartTime, EndTime, Date, Cus, PriceBooking, State);
            }
            LoadCustomerBooking();
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txtIdCustomerBooking.Text == "")
            {
                MessageBox.Show("vui lòng chọn đơn đặt sân");
            }
            else
            {
                int IdCustomerBooking = int.Parse(txtIdCustomerBooking.Text);
                float PriceBooking = float.Parse(txtPriceBooking.Text);
                CustomerBookingBLL.Instance.DelCustomerBooking(IdCustomerBooking, PriceBooking);
                this.Close();
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            int IdField = 0; // cho IdField = 0 để lấy ra tất cả các Field bằng cách xử lí logic ở câu truy vấn
            string State = "dat truoc";
            string TypeName = cbTypeField.SelectedItem.ToString();
            string FieldName = cbField.SelectedItem.ToString();
            string Search = txtSearch.Text.ToLower();
            dataGridView1.DataSource = CustomerBookingBLL.Instance.ShowCustomerBooking(IdField,State,TypeName,FieldName,Search);
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["idField"].Visible = false;
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string s1 = txtIdCustomerBooking.Text;
            if (s1 == "")
            {
                MessageBox.Show("vui lòng chọn đơn đặt sân");
            }
            else
            {
                    if (dateTimePicker1.Value.Date.ToString("MM/dd/yyyy") == DateTime.Today.ToString("MM/dd/yyyy"))
                    {
                        int IdCustomerBooking = int.Parse(s1);
                        int IdField = int.Parse(txtIdField.Text);
                        string State = "busy";
                        CustomerBookingBLL.Instance.ConfirmCustomerBooking(IdCustomerBooking,IdField,State);
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Chưa đến thời điểm ");
                    }   
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}


