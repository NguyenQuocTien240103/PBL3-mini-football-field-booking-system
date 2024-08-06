using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hệ_thống_quản_lý_sân_bóng_mini.DTO
{
    public class CustomerBooking
    {
        private int _Id;
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        private int _IdCustomer;
        public int IdCustomer
        {
            get
            {
                return _IdCustomer;
            }
            set
            {
                _IdCustomer = value;
            }
        }

        private int _IdFieldName;
        public int IdFieldName
        {
            get
            {
                return _IdFieldName;
            }
            set
            {
                _IdFieldName = value;
            }
        }

        private DateTime _StartTime;
        public DateTime StartTime
        {
            get
            {
                return _StartTime;
            }
            set
            {
                _StartTime = value;
            }
        }

        private DateTime _EndTime;
        public DateTime EndTime
        {
            get
            {
                return _EndTime;
            }
            set
            {
                _EndTime = value;
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

        private DateTime _Ngaydat;
        public DateTime Ngaydat
        {
            get
            {
                return _Ngaydat;
            }
            set
            {
                _Ngaydat = value;
            }
        }



        public CustomerBooking(DataRow row)
        {
            this.Id = (int)row["id"];
            this.IdCustomer = (int)row["idCustomer"];
            this.IdFieldName = (int)row["idFieldName"]; 
            this.StartTime = (DateTime)row["startTime"];
            this.EndTime =  (DateTime)row["endTime"];
            this.PriceBooking = Convert.ToSingle(row["priceBooking"]);
            this.Status = (string)row["status"];
            this.Ngaydat = (DateTime)row["ngaydat"];
        }
    }
}
