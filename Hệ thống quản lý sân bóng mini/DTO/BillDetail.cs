using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hệ_thống_quản_lý_sân_bóng_mini.DTO
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
        public BillDetail(string name, string phone,  string fieldType, string fieldName,  string startTime, string endTime,
            float priceBooking, string status, float totalPrice,  DateTime paymentDay)
        {
            this.Name = name;
            this.Phone = phone;
            this.FieldType = fieldType;
            this.FieldName = fieldName;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.PriceBooking = priceBooking;
            this.Status = status;
            this.TotalPrice = totalPrice;
            this.PaymentDay = paymentDay;
           
        }
        public BillDetail(DataRow row)
        {
            this.Name = (string)row["CustomerName"];
            this.Phone = (string)row["phone"];
            this.FieldType = (string)row["FieldType"];
            this.FieldName = (string)row["FieldName"];  
            this.StartTime = (string)row["startTime"];
            this.EndTime= (string)row["endTime"];
            this.PriceBooking = Convert.ToSingle(row["priceBooking"]);
            this.Status = (string)row["status"];
            this.TotalPrice = Convert.ToSingle(row["totalPrice"]);
            this._PaymentDay = (DateTime)row["paymentDay"];
        }
    }
}
