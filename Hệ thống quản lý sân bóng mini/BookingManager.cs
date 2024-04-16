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
    public partial class BookingManager : Form
    {
        public BookingManager()
        {
            InitializeComponent();
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
