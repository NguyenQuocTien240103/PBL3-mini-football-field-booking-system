using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
           string startTime, string endTime, float priceBooking, string status ,DateTime ngaydat)
        {
            //String sql = "INSERT INTO dbo.CustomerBooking(idCustomer,idFieldName,startTime,endTime,priceBooking,status,ngaydat)" +
            //  "VALUES " + "(" + idCustomer.ToString() + "," + idFieldName.ToString() + "," +

            //  "'" + startTime.ToString() + "'" + "," + "'" + endTime.ToString() + "'" + "," +

            //  priceBooking.ToString() + "," + "'" + status.ToString() + "','"+ngaydat+"')";

            //DataProvider.Instance.ExcuteNonQuery(sql);

            // Parameterized SQL query to enhance security
            string sql = "INSERT INTO dbo.CustomerBooking (idCustomer, idFieldName, startTime, endTime, priceBooking, status, ngaydat) " +
                         "VALUES (@idCustomer, @idFieldName, @startTime, @endTime, @priceBooking, @status, @ngaydat)";

            // Create array of SqlParameter
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@idCustomer", idCustomer),
                new SqlParameter("@idFieldName", idFieldName),
                new SqlParameter("@startTime", startTime),
                new SqlParameter("@endTime", endTime),
                new SqlParameter("@priceBooking", priceBooking),
                new SqlParameter("@status", status),
                new SqlParameter("@ngaydat", ngaydat)
            };

            // Execute non-query with parameters
            DataProvider.Instance.ExecuteNonQuery(sql, parameters);

        }

        
        public void InSertCustomerBooking(int idCustomer, int idFieldName,
           DateTime startTime, DateTime endTime, float priceBooking, string status, DateTime ngaydat)
        {
            //String sql = "INSERT INTO dbo.CustomerBooking(idCustomer,idFieldName,startTime,endTime,priceBooking,status,ngaydat)" +
            //  "VALUES " + "(" + idCustomer.ToString() + "," + idFieldName.ToString() + "," +

            //  "'" + startTime.ToString() + "'" + "," + "'" + endTime.ToString() + "'" + "," +

            //  priceBooking.ToString() + "," + "'" + status.ToString() + "','" + ngaydat + "')";

            //DataProvider.Instance.ExcuteNonQuery(sql);

            // Câu lệnh SQL với tham số
            string sql = "INSERT INTO dbo.CustomerBooking(idCustomer, idFieldName, startTime, endTime, priceBooking, status, ngaydat) " +
                         "VALUES (@idCustomer, @idFieldName, @startTime, @endTime, @priceBooking, @status, @ngaydat)";

            // Tạo mảng các SqlParameter
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@idCustomer", idCustomer),
        new SqlParameter("@idFieldName", idFieldName),
        new SqlParameter("@startTime", startTime),
        new SqlParameter("@endTime", endTime),
        new SqlParameter("@priceBooking", priceBooking),
        new SqlParameter("@status", status),
        new SqlParameter("@ngaydat", ngaydat)
            };
            DataProvider.Instance.ExecuteNonQuery(sql, parameters);




        }
        public void InSertCustomerBooking1(int idCustomer, int idFieldName,
           string startTime, string endTime, float priceBooking, string status)
        {
            //String sql = "INSERT INTO dbo.CustomerBooking(idCustomer,idFieldName,startTime,endTime,priceBooking,status)" +
            //  "VALUES " + "(" + idCustomer.ToString() + "," + idFieldName.ToString() + "," +

            //  "'" + startTime.ToString() + "'" + "," + "'" + endTime.ToString() + "'" + "," +

            //  priceBooking.ToString() + "," + "'" + status.ToString() +  "')";

            //DataProvider.Instance.ExcuteNonQuery(sql);
            // Parameterized SQL query to enhance security
            string sql = "INSERT INTO dbo.CustomerBooking (idCustomer, idFieldName, startTime, endTime, priceBooking, status) " +
                         "VALUES (@idCustomer, @idFieldName, @startTime, @endTime, @priceBooking, @status)";

            // Create array of SqlParameter
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@idCustomer", idCustomer),
                new SqlParameter("@idFieldName", idFieldName),
                new SqlParameter("@startTime", startTime),
                new SqlParameter("@endTime", endTime),
                new SqlParameter("@priceBooking", priceBooking),
                new SqlParameter("@status", status)
            };

            // Execute non-query with parameters
            DataProvider.Instance.ExecuteNonQuery(sql, parameters);
        }
        public void updateCustomerBooking(int idCustomerBooking,int idCustomer, int idFieldName,
           String startTime, String endTime, float priceBooking, String status)
        {

            //String sql = "update dbo.CustomerBooking " +
            //    "set idCustomer = '" + idCustomer + "', idFieldName = '" + idFieldName + "', startTime = '" + startTime + "', endTime = '" + endTime + "', priceBooking = '" +priceBooking + "', status = '" +status+ "' " +
            //    "where id = " + idCustomerBooking;
            //DataProvider.Instance.ExcuteNonQuery(sql);

            // Parameterized SQL query to enhance security
            string sql = "UPDATE dbo.CustomerBooking " +
                         "SET idCustomer = @idCustomer, idFieldName = @idFieldName, startTime = @startTime, endTime = @endTime, priceBooking = @priceBooking, status = @status " +
                         "WHERE id = @idCustomerBooking";

            // Create array of SqlParameter
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@idCustomerBooking", idCustomerBooking),
        new SqlParameter("@idCustomer", idCustomer),
        new SqlParameter("@idFieldName", idFieldName),
        new SqlParameter("@startTime", startTime),
        new SqlParameter("@endTime", endTime),
        new SqlParameter("@priceBooking", priceBooking),
        new SqlParameter("@status", status)
            };

            // Execute non-query with parameters
            DataProvider.Instance.ExecuteNonQuery(sql, parameters);
        }
        public void updateCustomerBooking(int idCustomerBooking, int idCustomer, int idFieldName,
           DateTime startTime, DateTime endTime, float priceBooking, String status)
        {

            //String sql = "update dbo.CustomerBooking " +
            //    "set idCustomer = '" + idCustomer + "', idFieldName = '" + idFieldName + "', startTime = '" + startTime + "', endTime = '" + endTime + "', priceBooking = '" + priceBooking + "', status = '" + status + "' " +
            //    "where id = " + idCustomerBooking;
            //DataProvider.Instance.ExcuteNonQuery(sql);

            // Parameterized SQL query to enhance security
            string sql = "UPDATE dbo.CustomerBooking " +
                         "SET idCustomer = @idCustomer, idFieldName = @idFieldName, startTime = @startTime, endTime = @endTime, priceBooking = @priceBooking, status = @status " +
                         "WHERE id = @idCustomerBooking";

            // Create array of SqlParameter
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@idCustomerBooking", idCustomerBooking),
        new SqlParameter("@idCustomer", idCustomer),
        new SqlParameter("@idFieldName", idFieldName),
        new SqlParameter("@startTime", startTime),
        new SqlParameter("@endTime", endTime),
        new SqlParameter("@priceBooking", priceBooking),
        new SqlParameter("@status", status)
            };

            // Execute non-query with parameters
            DataProvider.Instance.ExecuteNonQuery(sql, parameters);
        }
        public DataTable GetCustomerBookingDepositMoney()
        {
            String sql = "SELECT * FROM dbo.CustomerBooking WHERE priceBooking > 0";


            DataTable data = DataProvider.Instance.ExecuteQuery(sql);
            return data;
        }
        public List<CustomerBooking> LoadCustomerBooking()
        {
            List<CustomerBooking> listCustomerBooking = new List<CustomerBooking>();
            string sql = "Select * FROM dbo.CustomerBooking";
            DataTable data = DataProvider.Instance.ExecuteQuery(sql);
            foreach (DataRow row in data.Rows)
            {
                CustomerBooking customerBooking = new CustomerBooking(row);
                listCustomerBooking.Add(customerBooking);

            }
            return listCustomerBooking;
        }

        public void updateCustomerBooking(int idField)
        {
            //String sql = "update dbo.CustomerBooking" +
            //    "\r\nset status='da thanh toan'\r\n" +
            //    "where idFieldName= " + idField.ToString()+
            //    "and status = 'chua thanh toan'";

            //DataProvider.Instance.ExcuteNonQuery(sql);
            string sql = "UPDATE dbo.CustomerBooking " +
                "SET status = 'da thanh toan' " +
                "WHERE idFieldName = @idField AND status = 'chua thanh toan'";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@idField", idField)
            };

            DataProvider.Instance.ExecuteNonQuery(sql, parameters);

        }
        public void updateCustomerBookingById(int idCustomerBooking)
        {
            //String sql = "update dbo.CustomerBooking set status= 'chua thanh toan' Where id=" + idCustomerBooking;
            //DataProvider.Instance.ExcuteNonQuery(sql);
            string sql = "UPDATE dbo.CustomerBooking SET status = 'chua thanh toan' WHERE id = @idCustomerBooking";

            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@idCustomerBooking", idCustomerBooking)
            };

            DataProvider.Instance.ExecuteNonQuery(sql, parameters);
        }
        public void updatestatusCustomerBookingById(int idCustomerBooking)
        {
            string sql = "update dbo.CustomerBooking set status = 'da huy' where id =@idCustomerBooking ";
            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@idCustomerBooking", idCustomerBooking)
            };
            DataProvider.Instance.ExecuteNonQuery(sql,parameters);
        }

        public CustomerBooking getCustomerByCustomerBooking(int idCustomerBooking)
        {
            //String sql = "Select * FROM dbo.CustomerBooking Where id=" + idCustomerBooking;
            //DataTable data = DataProvider.Instance.ExcuteQuery(sql);
            //CustomerBooking customerBooking = new CustomerBooking(data.Rows[0]);
            //return customerBooking;

            string sql = "SELECT * FROM dbo.CustomerBooking WHERE id = @idCustomerBooking";

            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@idCustomerBooking", idCustomerBooking)
            };


            DataTable data = DataProvider.Instance.ExecuteQuery(sql, parameters);

            if (data.Rows.Count == 0)
            {
                // Handle no matching record scenario (e.g., return null or throw an exception)
                return null; // Example: return null if no matching customer booking found
            }

            CustomerBooking customerBooking = new CustomerBooking(data.Rows[0]);
            return customerBooking;


        }

        public int getIdCustomerByidField(int idField)
        {
            String sql = "Select * from dbo.CustomerBooking Where status='truc tiep' and idFieldName=@idField" ;
            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@idField", idField)
            };
            DataTable data = DataProvider.Instance.ExecuteQuery(sql,parameters);
            CustomerBooking customerBooking = new CustomerBooking(data.Rows[0]);
            return customerBooking.Id;
        }

        public void delCustomerBookingById(int idCustomerBooking)
        {
           // string sql = "Delete from dbo.CustomerBooking where id = " + idCustomerBooking;
         //   DataProvider.Instance.ExcuteNonQuery(sql);




            string sql = "Delete from dbo.CustomerBooking where id = @idCustomerBooking";

            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@idCustomerBooking", idCustomerBooking)
            };
            DataProvider.Instance.ExecuteNonQuery(sql,parameters);
        }
    }
}
