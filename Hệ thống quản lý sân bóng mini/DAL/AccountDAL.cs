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
        private static AccountDAL _Instance;
        public static AccountDAL Instance
        {
            get
            {
                if(_Instance == null)
                {
                    _Instance = new AccountDAL();
                }
                return _Instance;
            }
            private set
            {

            }
        }

        public bool Login(string username, string password)
        {
            string query =  "select * from dbo.Account where username='" + username + "' and password='" + password+"'";
            ;
            DataTable rs = DataProvider.Instance.ExecuteQuery(query);
            return rs.Rows.Count == 0;
            
        }
    }
}
