using qlsb_mini.DAL;
using qlsb_mini.demo;
using qlsb_mini.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qlsb_mini.BLL
{
    public class FieldBLL
    {
        private static FieldBLL _Instance;
        public static FieldBLL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new FieldBLL();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        private FieldBLL() { }

        public Field GetFieldById(int IdField)
        {
            return FieldDAL.Instance.getFieldByIdField(IdField);
        }
        public List<Field> GetAllField(int IdFieldType = 0)
        {
            if(IdFieldType == 0)
            {
                return FieldDAL.Instance.LoadFieldList();
            }
            else
            {
                return FieldDAL.Instance.GetFieldByIdFieldType(IdFieldType);
            }
        }
    }
}
