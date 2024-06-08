using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hệ_thống_quản_lý_sân_bóng_mini.demo;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hệ_thống_quản_lý_sân_bóng_mini.DAL
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
            //String sql = "Select * FROM dbo.FieldName Where idFieldType = " + id.ToString();
            //List<Field> fieldList = new List<Field>();
            //DataTable data = DataProvider.Instance.ExcuteQuery(sql);
            //foreach (DataRow row in data.Rows)
            //{
            //    Field field = new Field(row);
            //    fieldList.Add(field);
            //}
            //return fieldList;
            // Using parameterized SQL query to enhance security
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
        public void updateFieldById(int id, String status)
        {
            // String sql = "UPDATE  dbo.FieldName SET status = " + status.ToString() + "where id = " + id.ToString();
            // string sql = "UPDATE  dbo.FieldName SET status ='" + status.ToString()+"' where id = " + id.ToString();
            //DataProvider.Instance.ExcuteNonQuery(sql);
            // Parameterized SQL query to improve security
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
            //string sql = "UPDATE dbo.FieldName SET name ='" + nameField + "', idFieldType = " + idTypeField + " WHERE id = " + id;
            //DataProvider.Instance.ExcuteNonQuery(sql);
            // Parameterized SQL query to improve security
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
        public void insertField(string name,int idFieldType)
        {
            //string status = "empty";
            //string sql = "INSERT INTO dbo.FieldName (name, status, idFieldType) " +
            //     "VALUES('" + name + "','" + status + "'," + idFieldType.ToString() + ")";
            //DataProvider.Instance.ExcuteNonQuery(sql);
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
        public void DelField(int idField)
        {
            //string sql = "Delete from dbo.FieldName where id=" + idField;
            //DataProvider.Instance.ExcuteNonQuery(sql);
            // Using a parameterized query to enhance security
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
