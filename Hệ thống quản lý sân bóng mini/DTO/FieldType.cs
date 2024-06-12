using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hệ_thống_quản_lý_sân_bóng_mini.DTO
{
    public class FieldType
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

        private float _NormalPrice;
        public float NormalPrice
        {
            get
            {
                return _NormalPrice;
            }
            set
            {
                _NormalPrice = value;
            }
        }
        private float _SpecialPrice;
        public float SpecialPrice
        {
            get
            {
                return _SpecialPrice;
            }
            set
            {
                _SpecialPrice = value;
            }
        }
        public override string ToString()
        {
            return _TypeName;

        }
        public FieldType(int id, string typeName)
        {
            this.Id = id;
            this.TypeName = typeName;
        }
        public FieldType(DataRow row)
        {
            this.Id = (int)row["id"];   
            this.NormalPrice = Convert.ToSingle(row["normalDayPrice"]);
            this.SpecialPrice = Convert.ToSingle(row["specialDayPrice"]);
            this.TypeName = (string)row["TypeName"];
           
        }
    }
}
