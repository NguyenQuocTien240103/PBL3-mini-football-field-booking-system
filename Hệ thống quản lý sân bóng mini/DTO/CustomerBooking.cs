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

        private string _StartTime;
        public string StartTime
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

        private string _EndTime;
        public string EndTime
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

        private String _Status;
        public String Status
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



        public CustomerBooking(int id,int idCustomer, 
            int idFieldName, string startTime, string EndTime, int priceBooking, string status)
        {
            this.Id = id;
            this.IdCustomer = idCustomer;
            this.IdFieldName = idFieldName;
            this.StartTime= startTime;
            this.EndTime = EndTime;
            this.PriceBooking = priceBooking;
            this.Status = status;
        }

        public CustomerBooking(DataRow row)
        {
            this.Id = (int)row["id"];
            this.IdCustomer = (int)row["idCustomer"];
            this.IdFieldName = (int)row["idFieldName"]; 
            this.StartTime = (string)row["startTime"];
            this.EndTime =  (string)row["endTime"];
            this.PriceBooking = Convert.ToSingle(row["priceBooking"]);
            this.Status = (string)row["status"];
        }
    }
}
