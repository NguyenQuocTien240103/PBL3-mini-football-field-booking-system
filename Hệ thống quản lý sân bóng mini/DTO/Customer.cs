﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hệ_thống_quản_lý_sân_bóng_mini.DTO
{
    public class Customer
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
        private String _Phone;
        public String Phone
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



        public Customer(int Id, string Name, string Phone)
        {
            this.Id = Id;
            this.Name = Name;
            this.Phone = Phone;

        }

        public Customer(DataRow row)
        {
            this.Id = (int)row["id"];
            this.Name = (string)row["name"];
            this.Phone = (string)row["phone"];
            
        }
    }
}
