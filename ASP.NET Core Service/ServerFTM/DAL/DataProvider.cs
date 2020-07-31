using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ServerFTM.DAL.DataProvider
{
    class DataProvider
    {
        private static DataProvider instance;
        public static DataProvider Instance {
            get {
                if (instance == null)
                    instance = new DataProvider();
                return instance;
            }
            set => instance = value;
        }

        private DataProvider() { }

        public DataTable ExecuteQuery(string query, object[] parameter = null) {
            DataTable data = new DataTable();

            string connectionString = Startup.Configuration.GetSection("AppSetting:ConnectionString").Value;

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null) {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara) {
                        if (item.Contains('@')) {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                connection.Close();
            }

            return data;
        }

        public int ExecuteNonQuery(string query, object[] parameter = null) {
            int data = 0;

            string connectionString = Startup.Configuration.GetSection("AppSetting:ConnectionString").Value;

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null) {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara) {
                        if (item.Contains('@')) {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteNonQuery();

                connection.Close();
            }

            return data;
        }

        public bool ExecuteNonQuery_b(string query, object[] parameter = null) {
            int data = 0;

            string connectionString = Startup.Configuration.GetSection("AppSetting:ConnectionString").Value;

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null) {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara) {
                        if (item.Contains('@')) {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteNonQuery();

                connection.Close();
            }

            return data > 0;
        }

        public object ExecuteScalar(string query, object[] parameter = null) {
            object data = 0;

            string connectionString = Startup.Configuration.GetSection("AppSetting:ConnectionString").Value;

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null) {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara) {
                        if (item.Contains('@')) {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteScalar();

                connection.Close();
            }

            return data;
        }
    }
}
