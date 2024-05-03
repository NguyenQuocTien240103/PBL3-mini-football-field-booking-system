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
    public partial class PayMent : Form
    {
        public PayMent()
        {
            InitializeComponent();
            LoadTypeField();
            setCombox();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        void LoadTypeField()
        {
            List<FieldType> listFieldType = FieldTypeDAL.Instance.LoadFieldType();
            cbtype.DataSource = listFieldType;
            cbtype.DisplayMember = "TypeName";
        }
        void LoadFieldByIdTypeField(int id)
        {
            List<Field> listField = FieldDAL.Instance.GetFieldByIdFieldType(id);
            cbName.DataSource = listField;
            cbName.DisplayMember = "name";
        }

        private void cbtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            FieldType choosed = cb.SelectedItem as FieldType;
            if (choosed != null)
            {
                LoadFieldByIdTypeField(choosed.Id);
            }
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
    }
}
