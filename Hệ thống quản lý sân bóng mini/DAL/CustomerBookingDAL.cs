using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Hệ_thống_quản_lý_sân_bóng_mini.DTO;

namespace Hệ_thống_quản_lý_sân_bóng_mini.DAL
{
    public class CustomerBookingDAL
    {
        private static CustomerBookingDAL _Instance;

        public static CustomerBookingDAL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CustomerBookingDAL();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        private CustomerBookingDAL() { }

       public void InSertCustomerBooking(int idCustomer,int idFieldName,
           String startTime, String endTime, float priceBooking, String status )
        {
            String sql = "INSERT INTO dbo.CustomerBooking(idCustomer,idFieldName,startTime,endTime,priceBooking,status)" +
              "VALUES " + "(" + idCustomer.ToString() + "," + idFieldName.ToString() + "," +

              "'" + startTime.ToString() + "'" + "," + "'" + endTime.ToString() + "'" + "," +

              priceBooking.ToString() + "," + "'" + status.ToString() + "')";

            DataProvider.Instance.ExcuteNonQuery(sql);
        }

        
        public DataTable GetCustomerBookingDepositMoney()
        {
            String sql = "SELECT * FROM dbo.CustomerBooking WHERE priceBooking > 0";


            DataTable data = DataProvider.Instance.ExcuteQuery(sql);
            return data;
        }
        public List<CustomerBooking> LoadCustomerBooking()
        {
            List<CustomerBooking> listCustomerBooking = new List<CustomerBooking>();
            string sql = "Select * FROM dbo.CustomerBooking";
            DataTable data = DataProvider.Instance.ExcuteQuery(sql);
            foreach (DataRow row in data.Rows)
            {
                CustomerBooking customerBooking = new CustomerBooking(row);
                listCustomerBooking.Add(customerBooking);

            }
            return listCustomerBooking;
        }

        public void updateCustomerBooking(int idField)
        {
            String sql = "update dbo.CustomerBooking" +
                "\r\nset status='da thanh toan'\r\n" +
                "where idFieldName= " + idField.ToString()+
                "and status = 'chua thanh toan'";

            DataProvider.Instance.ExcuteNonQuery(sql);

        }
    }
}
