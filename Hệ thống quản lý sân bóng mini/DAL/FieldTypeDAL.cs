using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Hệ_thống_quản_lý_sân_bóng_mini.DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            //   string sql = "EXEC dbo.GetFieldType";
            string sql = "Select * FROM dbo.FieldType";
            DataTable data = DataProvider.Instance.ExecuteQuery(sql);
            foreach (DataRow row in data.Rows)
            {
                FieldType fieldType = new FieldType(row);
                listFieldType.Add(fieldType);

            }
            return listFieldType;
        }

        public FieldType getFieldTypeById(int id)
        {

            //string sql = "Select * FROM dbo.FieldType Where id = " + id.ToString();
            //DataTable data = DataProvider.Instance.ExecuteQuery(sql);
            ////mỗi TypeField chỉ có 1 id duy nhất nên chỉ cho 1 trường TypeField duy nhất vậy nên ta k cần dùng list
            //FieldType fieldType = new FieldType(data.Rows[0]);
            //return fieldType;

            // Sử dụng tham số trong câu truy vấn để tăng cường bảo mật
            string sql = "SELECT * FROM dbo.FieldType WHERE id = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                 new SqlParameter("@id", id)
            };

            // Thực thi truy vấn với tham số
            DataTable data = DataProvider.Instance.ExecuteQuery(sql, parameters);

            // Kiểm tra xem có bản ghi nào được trả về hay không
            if (data.Rows.Count > 0)
            {
                // Mỗi FieldType chỉ có một id duy nhất, nên chỉ trả về một đối tượng FieldType
                FieldType fieldType = new FieldType(data.Rows[0]);
                return fieldType;
            }
            else
            {
                return null; // Trả về null nếu không tìm thấy FieldType nào có id cụ thể
            }
        }

        public void updateFieldType(int id, string name, float normalPrice, float specialPrice)
        {
            //string sql = "UPDATE  dbo.FieldType SET TypeName ='" + name.ToString() + "'" +",normalDayPrice='"+normalPrice + "'"+",specialDayPrice =' "+specialPrice
            //    +"' where id = " + id;
            //DataProvider.Instance.ExcuteNonQuery(sql);

            string sql = "UPDATE dbo.FieldType SET TypeName = @name, normalDayPrice = @normalPrice, specialDayPrice = @specialPrice WHERE id = @id";

            // Tạo mảng các SqlParameter
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@name", name),
                new SqlParameter("@normalPrice", normalPrice),
                new SqlParameter("@specialPrice", specialPrice),
                new SqlParameter("@id", id)
            };

            // Thực thi truy vấn với tham số
            DataProvider.Instance.ExecuteNonQuery(sql, parameters);
        }   
    }   
}
