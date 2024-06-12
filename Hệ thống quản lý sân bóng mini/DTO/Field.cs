using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hệ_thống_quản_lý_sân_bóng_mini.demo
{
    public class Field
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
        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        private int _IdFieldType;
        public int IdFieldType
        {
            get
            {
                return _IdFieldType;
            }
            set
            {
                _IdFieldType = value;
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

        public Field(int Id, string Name)
        {
            this.Id = Id;   
            this.Name = Name;

        }
        public override string ToString()
        {
            return _Name;

        }

        public Field(DataRow row)
        {
            this.Id = (int)row["id"];
            this.Name = (string)row["name"];
            this.Status= (string)row["status"];
            this.IdFieldType = (int)row["idFieldType"];
        }
    }
}
