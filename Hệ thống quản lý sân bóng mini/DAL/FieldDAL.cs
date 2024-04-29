using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hệ_thống_quản_lý_sân_bóng_mini.demo;

namespace Hệ_thống_quản_lý_sân_bóng_mini.DAL
{
    public class FieldDAL
    {
        private static FieldDAL _Instance;

        public static FieldDAL Instance {  
            get { 
                if (_Instance == null)
                {
                    _Instance = new FieldDAL();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        private FieldDAL() { }

        public List<Field> LoadFieldList()
        {
            String sql = "EXEC dbo.GetFieldList";
            List<Field> fieldList = new List<Field>();

            DataTable dataTable = DataProvider.Instance.ExcuteQuery(sql);

            foreach (DataRow row in dataTable.Rows)
            {
                Field field = new Field(row);
                fieldList.Add(field);
            }
            return fieldList;
        }
        public List<Field> GetFieldByIdFieldType(int id)
        {
            String sql = "Select * FROM dbo.FieldName Where idFieldType = "+id.ToString();
            List<Field> fieldList = new List<Field>();
            DataTable data = DataProvider.Instance.ExcuteQuery(sql);
            foreach (DataRow row in data.Rows)
            {
                Field field = new Field(row);
                fieldList.Add(field);
            }
            return fieldList;
        }

    }
}
