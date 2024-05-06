using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hệ_thống_quản_lý_sân_bóng_mini.DTO
{
    public class CustomerBookingDetail
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
        private string _TypeName;
        public string TypeName
        {
            get
            {
                return _TypeName;
            }
            set
            {
                _TypeName = value;
            }
        }
        private String _FieldName;
        public String FieldName
        {
            get
            {
                return _FieldName;
            }
            set
            {
                _FieldName = value;
            }
        }

        private String _CustomerName;
        public String CustomerName
        {
            get
            {
                return _CustomerName;
            }
            set
            {
                _CustomerName = value;
            }
        }

        private String _CustomerPhone;
        public String CustomerPhone
        {
            get
            {
                return _CustomerPhone;
            }
            set
            {
                _CustomerPhone = value;
            }
        }

        private String _startTime;
        public String startTime
        {
            get
            {
                return _startTime;
            }
            set
            {
                _startTime = value;
            }
        }

        private String _endTime;
        public String endTime
        {
            get
            {
                return _endTime;
            }
            set
            {
                _endTime = value;
            }
        }

        private float _priceBooking;
        public float priceBooking
        {
            get
            {
                return _priceBooking;
            }
            set
            {
                _priceBooking = value;
            }
        }

        private String _status;
        public String status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }


        public CustomerBookingDetail(int Id, string TypeName, string FieldName,
            string CustomerName, string CustomerPhone, string startTime, string endTime, float priceBooking,string status)
        {
            this.Id = Id;
            this.TypeName = TypeName;
            this.FieldName = FieldName;
            this.CustomerName = CustomerName;
            this.CustomerPhone = CustomerPhone;
            this.startTime = startTime;
            this.endTime = endTime;
            this.priceBooking = priceBooking;
            this.status = status;
        }

        public CustomerBookingDetail(DataRow row)
        {
            this.Id = (int)row["id"];
            this.TypeName = (string)row["TypeName"];
            this.FieldName = (string)row["FieldName"];
            this.CustomerName= (string)row["CustomerName"];
            this.CustomerPhone = (string)row["CustomerPhone"]; 
            this.startTime = (string)row["startTime"];
            this.endTime = (string)row["endTime"];
            this.priceBooking = Convert.ToSingle(row["priceBooking"]);
            this.status = (string)row["status"];

        }
    }
}
