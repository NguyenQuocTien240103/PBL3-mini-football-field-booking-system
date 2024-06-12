using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
                _Id= value;
            }
        }
        private int _IdField;
        public int IdField
        {
            get
            {
                return _IdField;
            }
            set
            {
                _IdField = value;
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
        private string _FieldName;
        public string FieldName
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

        private string _CustomerName;
        public string CustomerName
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

        private string _CustomerPhone;
        public string CustomerPhone
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

        private DateTime _startTime;
        public DateTime startTime
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

        private DateTime _endTime;
        public DateTime endTime
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

        private string _status;
        public string status
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
        public CustomerBookingDetail(DataRow row)
        {
            this.Id = (int)row["id"];
            this.IdField = (int)row["idField"];
            this.TypeName = (string)row["TypeName"];
            this.FieldName = (string)row["FieldName"];
            this.CustomerName= (string)row["CustomerName"];
            this.CustomerPhone = (string)row["CustomerPhone"]; 
            this.startTime = (DateTime)row["startTime"];
            this.endTime = (DateTime)row["endTime"];
            this.priceBooking = Convert.ToSingle(row["priceBooking"]);
            this.status = (string)row["status"];
            this.Ngaydat = (DateTime)row["ngaydat"];

        }
        public override string ToString()
        {
            return Id + " " + IdField + " " + TypeName + " " + FieldName + " " + CustomerName + " " + CustomerPhone+ " " + startTime.ToString("HH:mm") + " " + endTime.ToString("HH:mm") + " " + priceBooking + " " +status+ " "+Ngaydat.ToString("dd/MM//yyyy") + " " ;
        }
    }
}
