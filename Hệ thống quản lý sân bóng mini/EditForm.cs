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
    public partial class EditForm : Form
    {
        public EditForm()
        {
            InitializeComponent();
            LoadTypeField();
            setCombox();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void setCombox()
        {
            for (int i = 0; i < 24; i++)
            {
                cb1.Items.Add(i);
                cb3.Items.Add(i);
            }
            for (int i = 0; i < 60; i++)
            {
                if (i < 10)
                {
                    String index = i.ToString();
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

        void LoadTypeField()
        {
            List<FieldType> listFieldType = FieldTypeDAL.Instance.LoadFieldType();
            cbFieldType.DataSource = listFieldType;
            cbFieldType.DisplayMember = "TypeName";
        }
        void LoadFieldByIdTypeField(int id)
        {
            List<Field> listField = FieldDAL.Instance.GetFieldByIdFieldType(id);
            cbNameField.DataSource = listField;
            cbNameField.DisplayMember = "name";
        }


        private void cbFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            FieldType choosed = cb.SelectedItem as FieldType;
            if (choosed != null)
            {
                LoadFieldByIdTypeField(choosed.Id);
            }
        }

        public void abc(Field field)
        {
            txtFieldID.Text = field.Id.ToString();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            
        }
    }
}
