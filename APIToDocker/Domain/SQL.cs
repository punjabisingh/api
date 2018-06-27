using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace APIToDocker.Domain
{
    public class SQL
    {
        public SqlConnection sqlConnection { get; set; }
        public SqlConnectionStringBuilder builder{ get; set; }
        private string connectionString { get; set; }
        public SQL()
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = "sql_server_demo,1433";
            builder.UserID = "sa";
            builder.Password = "P@ssw0rd";
            builder.InitialCatalog = "GovTech";
        }

        public string TestConnection()
        {
            try
            {
                var conn = GetConnection();
                return "Connection OK";
            }catch(Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        private SqlConnection GetConnection()
        {
            sqlConnection = new SqlConnection(builder.ConnectionString);
            connectionString = sqlConnection.ConnectionString.ToString();
            sqlConnection.Open();
            return sqlConnection;
        }

        public IEnumerable<ApexDTO> GetApexDTO()
        {
            //sqlConnection = new SqlConnection(builder.ConnectionString);
            //Console.WriteLine("SQL Connection");
            List<ApexDTO> result = new List<ApexDTO>();
            try
            {
                //sqlConnection.Open();
                var _conn = GetConnection();
                string query = "Select * from dbo.APEX";
                using (SqlCommand cmd = new SqlCommand(query, _conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new ApexDTO()
                            {
                                AgencyID = reader.GetInt32(0),
                                ConsumerURL = reader.GetString(1),
                                HostedURL = reader.GetString(2),
                                ProductDescription = reader.GetString(3),
                                Error = "NO Error!",
                                InstanceID = Guid.NewGuid()
                            });
                        }
                    }
                }
            }catch(Exception ex)
            {
                //Console.WriteLine(ex.Message.ToString());
                result.Add(new ApexDTO() {AgencyID=0, HostedURL=connectionString, Error = ex.Message.ToString() });

            }
            return result;
        }
    }
}
