using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hệ_thống_quản_lý_sân_bóng_mini.DTO;

namespace Hệ_thống_quản_lý_sân_bóng_mini.DAL
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
        public List<CustomerBookingDetail> LoadCustomerBookingById(int idField)
        {


            //String sql = "SELECT CustomerBooking.id,FieldName.id AS idField," +
            //    " \r\nFieldType.TypeName," +
            //    " \r\nFieldName.name AS FieldName," +
            //    " \r\n Customer.name AS CustomerName, " +
            //    "\r\nCustomer.phone AS CustomerPhone," +
            //    "\r\nCustomerBooking.startTime," +
            //    "\r\n CustomerBooking.endTime," +
            //    "\r\nCustomerBooking.priceBooking, " +
            //    "\r\nCustomerBooking.status ," +
            //     "\r\n       CustomerBooking.ngaydat" +
            //    "\r\nFROM FieldType" +
            //    " \r\nINNER JOIN FieldName ON FieldType.id = FieldName.idFieldType and  FieldName.id = " + idField.ToString()+
            //    "\r\nINNER JOIN CustomerBooking ON FieldName.id = CustomerBooking.idFieldName and " +
            //    "CustomerBooking.status='chua thanh toan'\r\n" +
            //    "INNER JOIN Customer ON CustomerBooking.idCustomer = Customer.id ";


            string sql = "SELECT CustomerBooking.id,FieldName.id AS idField," +
                " \r\nFieldType.TypeName," +
                " \r\nFieldName.name AS FieldName," +
                " \r\n Customer.name AS CustomerName, " +
                "\r\nCustomer.phone AS CustomerPhone," +
                "\r\nCustomerBooking.startTime," +
                "\r\n CustomerBooking.endTime," +
                "\r\nCustomerBooking.priceBooking, " +
                "\r\nCustomerBooking.status ," +
                 "\r\n       CustomerBooking.ngaydat" +
                "\r\nFROM FieldType" +
                " \r\nINNER JOIN FieldName ON FieldType.id = FieldName.idFieldType and  FieldName.id = @idField" +
                "\r\nINNER JOIN CustomerBooking ON FieldName.id = CustomerBooking.idFieldName and " +
                "CustomerBooking.status='truc tiep'\r\n" +
                "INNER JOIN Customer ON CustomerBooking.idCustomer = Customer.id ";

            List<CustomerBookingDetail> ListCustomerBookingDetail = new List<CustomerBookingDetail>();
            SqlParameter[] parameters = new SqlParameter[]
            {
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

        public List<CustomerBookingDetail> LoadCustomerBookingById1()
        {
            string sql = "\r\nSELECT CustomerBooking.id,FieldName.id AS idField," +
                "\r\nFieldType.TypeName," +
                " \r\nFieldName.name AS FieldName," +
                " \r\n       Customer.name AS CustomerName," +
                " \r\n       Customer.phone AS CustomerPhone," +
                "\r\n       CustomerBooking.startTime," +
                "\r\n       CustomerBooking.endTime," +
                "\r\n       CustomerBooking.priceBooking," +
                "\r\n       CustomerBooking.status," +
                "\r\n       CustomerBooking.ngaydat" +
                "\r\nFROM FieldType" +
                "\r\nINNER JOIN FieldName ON FieldType.id = FieldName.idFieldType " +
                "\r\nINNER JOIN CustomerBooking ON FieldName.id = CustomerBooking.idFieldName and CustomerBooking.status='dat truoc'" +
                "\r\nINNER JOIN Customer ON CustomerBooking.idCustomer = Customer.id";

            List<CustomerBookingDetail> ListCustomerBookingDetail = new List<CustomerBookingDetail>();

            //  DataTable dataTable = DataProvider.Instance.ExcuteQuery(sql);

            DataTable dataTable = DataProvider.Instance.ExecuteQuery(sql);

            foreach (DataRow row in dataTable.Rows)
            {
                CustomerBookingDetail customerBookingDetail = new CustomerBookingDetail(row);
                ListCustomerBookingDetail.Add(customerBookingDetail);
            }
            return ListCustomerBookingDetail;
        }

    }
}
