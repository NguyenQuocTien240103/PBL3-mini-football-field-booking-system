using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hệ_thống_quản_lý_sân_bóng_mini.DTO
{
    public class Bill
    {
        private int _Id;
        public int Id {
            get {
                return _Id; 
            } 
            set {
                _Id = value;
            } 
        }

        private int _IdCustomerBooking;
        public int IdCustomerBooking
        {
            get
            {
                return _IdCustomerBooking;
            }
            set
            {
                _IdCustomerBooking = value;
            }
        }

        private float _TotalPrice;
        public float TotalPrice
        {
            get
            {
                return _TotalPrice;
            }
            set
            {
                _TotalPrice = value;
            }
        }
        private DateTime _DatePayment;
        public DateTime DatePayment
        {
            get
            {
                return _DatePayment;
            }
            set
            {
                _DatePayment = value;
            }
        }
        public Bill(DataRow row)
        {
            this.Id = (int)row["id"];
            this.IdCustomerBooking = (int)row["idCustomerBooking"];
            this.TotalPrice = Convert.ToSingle(row["totalPrice"]);
            this.DatePayment = (DateTime)row["paymentDay"];
        }

    }
}
