using qlsb_mini.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qlsb_mini.DAL
{
    public class BillDAL
    {
        private static BillDAL _Instance;
        public static BillDAL Instance
        {
            get
            {
                if(_Instance == null)
                {
                    _Instance = new BillDAL();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        private BillDAL() { }
        
        public void insertBill(int idCustomerBooking,float totalPrice)
        {
            // Câu truy vấn SQL với tham số
            string sql = "INSERT INTO dbo.Bill (idCustomerBooking, totalPrice) VALUES (@idCustomerBooking, @totalPrice)";
            // Tạo mảng các SqlParameter
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@idCustomerBooking", idCustomerBooking),
                new SqlParameter("@totalPrice", totalPrice)
            };
            // Thực thi truy vấn với tham số
            DataProvider.Instance.ExecuteNonQuery(sql, parameters);
        }
    }
}
