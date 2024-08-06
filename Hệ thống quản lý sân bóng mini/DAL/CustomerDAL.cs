using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hệ_thống_quản_lý_sân_bóng_mini.demo;
using Hệ_thống_quản_lý_sân_bóng_mini.DTO;

namespace Hệ_thống_quản_lý_sân_bóng_mini.DAL
{
    public class CustomerDAL
    {
        private static CustomerDAL _Instance;

        public static CustomerDAL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CustomerDAL();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        private CustomerDAL() { }

        public List<Customer> LoadCustomerList()
        {
            string sql = "Select * FROM dbo.Customer ";
            List<Customer> ListCustomer = new List<Customer>();

            DataTable dataTable = DataProvider.Instance.ExecuteQuery(sql);

            foreach (DataRow row in dataTable.Rows)
            {
                Customer customer = new Customer(row);
                ListCustomer.Add(customer);
            }
            return ListCustomer;
        }
        public int getIdCustomerLast()
        {
            string sql = "SELECT TOP 1 * FROM dbo.Customer  ORDER BY id DESC";
            DataTable dataTable = DataProvider.Instance.ExecuteQuery(sql);
            if (dataTable.Rows.Count > 0)
            {
                Customer customer = new Customer(dataTable.Rows[0]);

                return customer.Id;
            }
            return -1;
        }
        public Customer GetCustomerById(int IdCustomer)
        {

            string sql = "SELECT * FROM dbo.Customer WHERE id = @IdCustomer";
            SqlParameter[] parameters = new SqlParameter[]
            {
                 new SqlParameter("@IdCustomer", IdCustomer)

            };
            // Thực thi truy vấn với tham số
            DataTable data = DataProvider.Instance.ExecuteQuery(sql, parameters);

            // Kiểm tra xem có bản ghi nào được trả về hay không
            if (data.Rows.Count > 0)
            {
                // Mỗi FieldType chỉ có một id duy nhất, nên chỉ trả về một đối tượng FieldType
                Customer customer = new Customer(data.Rows[0]);
                return customer;
            }
            else
            {
                return null; // Trả về null nếu không tìm thấy FieldType nào có id cụ thể
            }
        }
        public void InsertCustomer(string name,string phone)
        {
            string sql = "INSERT INTO dbo.Customer (name, phone) VALUES (@name, @phone)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@name", name),
                new SqlParameter("@phone", phone)
            };

            DataProvider.Instance.ExecuteNonQuery(sql, parameters);
        }
        public void updateCustomer(int idCustomer,string name,string phone)
        {
            string sql = "UPDATE dbo.Customer SET name = @name, phone = @phone WHERE id = @idCustomer";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@name", name),
                new SqlParameter("@phone", phone),
                new SqlParameter("@idCustomer", idCustomer)
            };

            DataProvider.Instance.ExecuteNonQuery(sql, parameters);
        }
    }
}
