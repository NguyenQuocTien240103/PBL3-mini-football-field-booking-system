using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using qlsb_mini.DTO;

namespace qlsb_mini.DAL
{
    public class CustomerBookingDetailDAL
    {
        private static CustomerBookingDetailDAL _Instance;
        public static CustomerBookingDetailDAL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CustomerBookingDetailDAL();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        private CustomerBookingDetailDAL() { }
        public List<CustomerBookingDetail> LoadCustomerBookingById(string state,int idField = 0)
        {
            string sql = "SELECT CustomerBooking.id, FieldName.id AS idField, " +
                  "FieldType.TypeName, FieldName.name AS FieldName, " +
                  "Customer.name AS CustomerName, Customer.phone AS CustomerPhone, " +
                  "CustomerBooking.startTime, CustomerBooking.endTime, " +
                  "CustomerBooking.priceBooking, CustomerBooking.status, " +
                  "CustomerBooking.ngaydat " +
                  "FROM FieldType " +
                  "INNER JOIN FieldName ON FieldType.id = FieldName.idFieldType " +
                  "INNER JOIN CustomerBooking ON FieldName.id = CustomerBooking.idFieldName " +
                  "INNER JOIN Customer ON CustomerBooking.idCustomer = Customer.id " + 
                  "WHERE CustomerBooking.status = @state " +
                  (idField != 0 ? "AND FieldName.id = @idField" : "");
            List<CustomerBookingDetail> ListCustomerBookingDetail = new List<CustomerBookingDetail>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                    new SqlParameter("@state", state),
                    new SqlParameter("@idField", idField)
            };

            DataTable dataTable = DataProvider.Instance.ExecuteQuery(sql,parameters); 

            foreach (DataRow row in dataTable.Rows)
            {
                CustomerBookingDetail customerBookingDetail = new CustomerBookingDetail(row);
                ListCustomerBookingDetail.Add(customerBookingDetail);
            }
            return ListCustomerBookingDetail;
        }
    }
}
