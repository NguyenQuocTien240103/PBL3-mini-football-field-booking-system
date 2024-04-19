using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hệ_thống_quản_lý_sân_bóng_mini.DAL
{
    public class AccountDAL
    {
        private static AccountDAL _instance;
        public static AccountDAL Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new AccountDAL();
                }
                return _instance;
            }
            private set
            {

            }
        }

        public bool Login(string username, string password)
        {
            string query =  "select * from dbo.Account where username='" + username + "' and password='" + password+"'";
            ;
            DataTable rs = DataProvider.Instance.ExcuteQuery(query);
            return rs.Rows.Count == 0;
            
        }
    }
}
