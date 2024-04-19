using Hệ_thống_quản_lý_sân_bóng_mini.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hệ_thống_quản_lý_sân_bóng_mini
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            string username = userNametxt.Text.ToString();
            string password = passWordtxt.Text.ToString();
           //MessageBox.Show(username + password);
            if (login(username, password))
            {
                BookingManager bookingmanager = new BookingManager();
                this.Hide();
                bookingmanager.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("error");
            }
        }

        bool login(string username,string password)
        {
            return AccountDAL.Instance.Login(username,password);
        }

    }
}
