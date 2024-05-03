using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hệ_thống_quản_lý_sân_bóng_mini.DTO
{
    public class detailBill
    {
        private string _Name;
        public string Name {
            get 
            { 
                return _Name;
 
            } 
            set
            { 
                _Name = value; 
            } 
        }

        private string _Phone;
        public string Phone
        {
            get
            {
                return _Phone;

            }
            set
            {
                _Name = value;
            }
        }

        private string _TypeField;
        public string TypeField
        {
            get
            {
                return _TypeField;

            }
            set
            {
                _TypeField = value;
            }
        }

        private string _NameField;
        public string NameField
        {
            get
            {
                return _NameField;

            }
            set
            {
                _NameField = value;
            }
        }

        private float _PriceBooking;
        public float PriceBooking
        {
            get
            {
                return _PriceBooking;

            }
            set
            {
                _PriceBooking = value;
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

        private string _Status;
        public string Status
        {
            get
            {
                return _Status;

            }
            set
            {
                _Status = value;
            }
        }
    }
}
