using Hệ_thống_quản_lý_sân_bóng_mini.DAL;
using Hệ_thống_quản_lý_sân_bóng_mini.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hệ_thống_quản_lý_sân_bóng_mini.BLL
{
    public class CustomerBLL
    {
        private static CustomerBLL _Instance;
        public static CustomerBLL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CustomerBLL();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        private CustomerBLL() { }
        public Customer GetCustomerById(int IdCustomer)
        {
            return CustomerDAL.Instance.GetCustomerById(IdCustomer);
        }
    }
}
