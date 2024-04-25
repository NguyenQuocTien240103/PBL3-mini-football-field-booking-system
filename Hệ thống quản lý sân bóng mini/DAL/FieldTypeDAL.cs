using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hệ_thống_quản_lý_sân_bóng_mini.DTO;

namespace Hệ_thống_quản_lý_sân_bóng_mini.DAL
{
    public class FieldTypeDAL
    {
        private static FieldTypeDAL _Instance;
        public  static FieldTypeDAL Instance
        {
            get
            {
                if(_Instance == null)
                {
                    _Instance = new FieldTypeDAL();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        private FieldTypeDAL() { }

        public List<FieldType> LoadFieldType()
        {
            List<FieldType> listFieldType = new List<FieldType>();
            string sql = "EXEC dbo.GetFieldType";
            DataTable data = DataProvider.Instance.ExcuteQuery(sql);
            foreach (DataRow row in data.Rows)
            {
                FieldType fieldType = new FieldType(row);
                listFieldType.Add(fieldType);

            }
            return listFieldType;
        }
    }
}
