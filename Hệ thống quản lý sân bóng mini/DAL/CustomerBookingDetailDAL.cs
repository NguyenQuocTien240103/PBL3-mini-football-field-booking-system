using System;
using System.Collections.Generic;
using System.Data;
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
            String sql =
                "SELECT FieldName.id," +
                "FieldType.TypeName," +
                "FieldName.name AS FieldName," +
                " Customer.name AS CustomerName," +
                "Customer.phone AS CustomerPhone," +
                "CustomerBooking.startTime," +
                " CustomerBooking.endTime," +
                "CustomerBooking.priceBooking, " +
                "CustomerBooking.status" +
                "FROM FieldType " +
                "INNER JOIN FieldName ON FieldType.id = FieldName.idFieldType" + "and  FieldName.id ="+ idField.ToString()+
                "INNER JOIN CustomerBooking ON FieldName.id = CustomerBooking.idFieldName" + "CustomerBooking.status='chua thanh toan'" +
                "INNER JOIN Customer ON CustomerBooking.idCustomer = Customer.id ";


            String sql1 = "SELECT FieldName.id," +
                " \r\nFieldType.TypeName," +
                " \r\nFieldName.name AS FieldName," +
                " \r\n Customer.name AS CustomerName, " +
                "\r\nCustomer.phone AS CustomerPhone," +
                "\r\nCustomerBooking.startTime," +
                "\r\n CustomerBooking.endTime," +
                "\r\nCustomerBooking.priceBooking, " +
                "\r\nCustomerBooking.status \r\nFROM FieldType" +
                " \r\nINNER JOIN FieldName ON FieldType.id = FieldName.idFieldType and  FieldName.id = " + idField.ToString()+
                "\r\nINNER JOIN CustomerBooking ON FieldName.id = CustomerBooking.idFieldName and " +
                "CustomerBooking.status='chua thanh toan'\r\n" +
                "INNER JOIN Customer ON CustomerBooking.idCustomer = Customer.id ";

            List<CustomerBookingDetail> ListCustomerBookingDetail = new List<CustomerBookingDetail>();

            DataTable dataTable = DataProvider.Instance.ExcuteQuery(sql1);

            foreach (DataRow row in dataTable.Rows)
            {
                CustomerBookingDetail customerBookingDetail = new CustomerBookingDetail(row);
                ListCustomerBookingDetail.Add(customerBookingDetail);
            }
            return ListCustomerBookingDetail;
        }

    }
}
