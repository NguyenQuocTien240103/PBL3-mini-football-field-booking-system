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
        public void updateCustomerBooking(int idCustomerBooking,int idCustomer, int idFieldName,
           String startTime, String endTime, float priceBooking, String status)
        {

            String sql = "update dbo.CustomerBooking " +
                "set idCustomer = '" + idCustomer + "', idFieldName = '" + idFieldName + "', startTime = '" + startTime + "', endTime = '" + endTime + "', priceBooking = '" +priceBooking + "', status = '" +status+ "' " +
                "where id = " + idCustomerBooking;
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
        public void updateCustomerBookingById(int idCustomerBooking)
        {
            String sql = "update dbo.CustomerBooking set status= 'chua thanh toan' Where id=" + idCustomerBooking;
            DataProvider.Instance.ExcuteNonQuery(sql);
        }
        public void updatestatusCustomerBookingById(int idCustomerBooking)
        {
            string sql = "update dbo.CustomerBooking set status = 'da huy' where id = " + idCustomerBooking;
            DataProvider.Instance.ExcuteNonQuery(sql);
        }

        public CustomerBooking getCustomerBookingById(int idCustomerBooking)
        {
            String sql = "Select * FROM dbo.CustomerBooking Where id=" + idCustomerBooking;
            DataTable data = DataProvider.Instance.ExcuteQuery(sql);
            CustomerBooking customerBooking = new CustomerBooking(data.Rows[0]);
            return customerBooking;

        }

        public int getIdCustomerByidField(int idField)
        {
            String sql = "Select * from dbo.CustomerBooking Where status='chua thanh toan' and idFieldName=" + idField;
            DataTable data = DataProvider.Instance.ExcuteQuery(sql);
            CustomerBooking customerBooking = new CustomerBooking(data.Rows[0]);
            return customerBooking.Id;
        }

        public void delCustomerBookingById(int idCustomerBooking)
        {
            string sql = "Delete from dbo.CustomerBooking where id = " + idCustomerBooking;
            DataProvider.Instance.ExcuteNonQuery(sql);
        }
    }
}
