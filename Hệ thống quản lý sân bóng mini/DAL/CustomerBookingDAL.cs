using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public DataTable GetCustomerBookingByIDField(int id)
        {
            //String sql =
            //    "SELECT * FROM dbo.CustomerBooking" +
            //    "WHERE idFieldName = " + id.ToString() +
            //    "AND id = (SELECT MAX(id) FROM dbo.CustomerBooking  WHERE idFieldName = " + id.ToString() + ")";

            String sql = "SELECT * FROM dbo.CustomerBooking WHERE " +
                "idFieldName =  "+id.ToString() + "AND id = (   " +
                " SELECT MAX(id) FROM dbo.CustomerBooking   " +
                " WHERE idFieldName = "+ id.ToString()    +   ")";
            DataTable data = DataProvider.Instance.ExcuteQuery(sql);
            return data;
        }
    }
}
