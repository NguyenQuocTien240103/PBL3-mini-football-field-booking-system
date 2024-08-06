using Hệ_thống_quản_lý_sân_bóng_mini.DAL;
using Hệ_thống_quản_lý_sân_bóng_mini.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hệ_thống_quản_lý_sân_bóng_mini.BLL
{
    public class FieldTypeBLL
    {
        private static FieldTypeBLL _Instance;
        public static FieldTypeBLL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new FieldTypeBLL();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        private FieldTypeBLL() { }

        public FieldType GetFieldTypeById(int IdFieldType)
        {
            return FieldTypeDAL.Instance.getFieldTypeById(IdFieldType);
        }
        public List<FieldType> GetAllFieldType()
        {
            return FieldTypeDAL.Instance.LoadFieldType();
        }
    }
}
