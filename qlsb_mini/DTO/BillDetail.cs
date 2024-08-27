using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qlsb_mini.DTO
{
    public class BillDetail
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
                _Phone = value;
            }
        }

        private string _FieldType;
        public string FieldType
        {
            get
            {
                return _FieldType;

            }
            set
            {
                _FieldType = value;
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

        private DateTime _PaymentDay;
        public DateTime PaymentDay
        {
            get
            {
                return _PaymentDay;

            }
            set
            {
                _PaymentDay = value;
            }
        }
        public BillDetail(DataRow row)
        {
            this.Name = (string)row["CustomerName"];
            this.Phone = (string)row["phone"];
            this.FieldType = (string)row["FieldType"];
            this.FieldName = (string)row["FieldName"];  
            this.StartTime = (DateTime)row["startTime"];
            this.EndTime= (DateTime)row["endTime"];
            this.PriceBooking = Convert.ToSingle(row["priceBooking"]);
            this.Status = (string)row["status"];
            this.TotalPrice = Convert.ToSingle(row["totalPrice"]);
            this._PaymentDay = (DateTime)row["paymentDay"];
        }
    }
}
