using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Hệ_thống_quản_lý_sân_bóng_mini.DAL
{
    public class DataProvider
    {

        private static DataProvider _Instance;
        public static DataProvider Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DataProvider();
                }
                return _Instance;
            }
            private set
            {
                
            }
        }
        private DataProvider() { }
        //private string connectionSTR =
        //        "Data Source=DESKTOP-L96UHDF\\SQLEXPRESS;Initial Catalog=quanli;Integrated Security=True";
        private string connectionSTR =
        "Data Source=DESKTOP-CVOKJNA\\TIEN;Initial Catalog=QLSB;Integrated Security=True";
        public DataTable ExcuteQuery(string query, object[] parameter =null)
        {
            // open connection to DB
            SqlConnection connection = new SqlConnection(connectionSTR);
            connection.Open();
            // execute query
            SqlCommand cmd = new SqlCommand(query, connection);
            if(parameter != null )
            {
                int i = 0;
                string[] listPara = query.Split(' ');
                foreach(string para in listPara)
                {
                    if (para.Contains("@"))
                    {
                        cmd.Parameters.AddWithValue(para, parameter[i]);
                        i++;
                    }
                
                }
            }
            // represent one table
            DataTable data = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(data);

            connection.Close();
            return data;
        }

        public int ExcuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            // open connection to DB
            SqlConnection connection = new SqlConnection(connectionSTR);
            connection.Open();
            // execute query
            SqlCommand cmd = new SqlCommand(query, connection);
            if (parameter != null)
            {
                int i = 0;
                string[] listPara = query.Split(' ');
                foreach (string para in listPara)
                {
                    if (para.Contains("@"))
                    {
                        cmd.Parameters.AddWithValue(para, parameter[i]);
                        i++;
                    }

                }
            }

            data = cmd.ExecuteNonQuery();
            connection.Close();
            return data;
        }
        public object ExcuteScaLar (string query, object[] parameter = null)
        {
            object data = 0;
            // open connection to DB
            SqlConnection connection = new SqlConnection(connectionSTR);
            connection.Open();
            // execute query
            SqlCommand cmd = new SqlCommand(query, connection);
            if (parameter != null)
            {
                int i = 0;
                string[] listPara = query.Split(' ');
                foreach (string para in listPara)
                {
                    if (para.Contains("@"))
                    {
                        cmd.Parameters.AddWithValue(para, parameter[i]);
                        i++;
                    }

                }
            }

            data = cmd.ExecuteScalar();
            connection.Close();
            return data;
        }
    }
}
