using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using qlsb_mini.demo;
using qlsb_mini.DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace qlsb_mini.DAL
{
    public class FieldDAL
    {
        private static FieldDAL _Instance;

        public static FieldDAL Instance
        {
            get
            {
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
        //    String sql = "EXEC dbo.GetFieldList";
            string sql = "Select * FROM dbo.FieldName";

            List<Field> fieldList = new List<Field>();

            DataTable dataTable = DataProvider.Instance.ExecuteQuery(sql);

            foreach (DataRow row in dataTable.Rows)
            {
                Field field = new Field(row);
                fieldList.Add(field);
            }
            return fieldList;
        }
        public List<Field> GetFieldByIdFieldType(int id)
        {
            string sql = "SELECT * FROM dbo.FieldName WHERE idFieldType = @id";
            // Create SqlParameter array
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };
            // Execute query with parameters
            DataTable data = DataProvider.Instance.ExecuteQuery(sql, parameters);
            // Initialize a list to store the fields
            List<Field> fieldList = new List<Field>();
            foreach (DataRow row in data.Rows)
            {
                Field field = new Field(row);
                fieldList.Add(field);
            }
            return fieldList;
        }
        public Field getFieldByIdField(int idField)
        {
            string sql = "SELECT * FROM dbo.FieldName WHERE id = @idField";
            SqlParameter[] parameters = new SqlParameter[]
           {
                 new SqlParameter("@idField", idField)
           };
            // Thực thi truy vấn với tham số
            DataTable data = DataProvider.Instance.ExecuteQuery(sql, parameters);
            // Kiểm tra xem có bản ghi nào được trả về hay không
            if (data.Rows.Count > 0)
            {
                // Mỗi FieldType chỉ có một id duy nhất, nên chỉ trả về một đối tượng FieldType
                Field field = new Field(data.Rows[0]);
                return field;
            }
            else
            {
                return null; // Trả về null nếu không tìm thấy FieldType nào có id cụ thể
            }
        }
        public void insertField(string name, int idFieldType)
        {
            string status = "empty";  // Assuming status is always set to "empty" when creating a new field
            string sql = "INSERT INTO dbo.FieldName (name, status, idFieldType) VALUES (@name, @status, @idFieldType)";
            // Create array of SqlParameter
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@name", name),
                new SqlParameter("@status", status),
                new SqlParameter("@idFieldType", idFieldType)
            };
            // Execute non-query with parameters
            DataProvider.Instance.ExecuteNonQuery(sql, parameters);
        }
        public void updateFieldState(int id, string status)
        {
            string sql = "UPDATE dbo.FieldName SET status = @status WHERE id = @id";
            // Create array of SqlParameter
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@status", status),
                new SqlParameter("@id", id)
            };
            // Execute non-query with parameters
            DataProvider.Instance.ExecuteNonQuery(sql, parameters);
        }
        public void updateField(int id,string nameField,int idTypeField)
        {
            string sql = "UPDATE dbo.FieldName SET name = @nameField, idFieldType = @idTypeField WHERE id = @id";
            // Create array of SqlParameter
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@nameField", nameField),
                new SqlParameter("@idTypeField", idTypeField),
                new SqlParameter("@id", id)
            };
            // Execute non-query with parameters
            DataProvider.Instance.ExecuteNonQuery(sql, parameters);
        }
        public void DelField(int idField)
        {
            string sql = "DELETE FROM dbo.FieldName WHERE id = @idField";
            // Create array of SqlParameter
            SqlParameter[] parameters = new SqlParameter[]
            {
                 new SqlParameter("@idField", idField)
            };
            // Execute non-query with parameters
            DataProvider.Instance.ExecuteNonQuery(sql, parameters);
        }
    }
}
