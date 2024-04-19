using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hệ_thống_quản_lý_sân_bóng_mini.DAL;

namespace Hệ_thống_quản_lý_sân_bóng_mini
{
    public partial class Manager : Form
    {
        public Manager()
        {
            InitializeComponent();
            loadAccountList();
        }

        void loadAccountList()
        {
            //string query = "SElECT * FROM dbo.Account";
            string query = "EXEC dbo.GetAccountByUserName @userName";
            // DataProvider dataProvider = new DataProvider();
            dtgvAccount.DataSource = DataProvider.Instance.ExcuteQuery(query,new object[] {"nguyentien"});
            

        }
    }
}
