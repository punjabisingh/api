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
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbUser = Environment.GetEnvironmentVariable("DB_USER");
            var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

            if (dbHost == null)
                dbHost = "localhost,1435";
            if(dbUser==null)
                dbUser= "sa";
            if(dbPassword==null)
                dbPassword= "P@ssw0rd";

            builder = new SqlConnectionStringBuilder();
            builder.DataSource = dbHost; //"localhost,1435"; //"10.8.1.194,1433";
            builder.UserID = dbUser; //"sa";
            builder.Password = dbPassword; //"P@ssw0rd"; //"Pass1234";
            //builder.InitialCatalog = "GovTech";
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

        private SqlConnection GetConnection(string catalog = "GovTech")
        {
            builder.InitialCatalog = catalog;

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
                    if(result.Count==0 || result==null){
                        result.Add(new ApexDTO()
                        {
                            AgencyID = 1,
                            ConsumerURL = "Hello from C# code",
                            HostedURL = "Sorry there is no records from SQL DB",
                            ProductDescription = "Yellow",
                            Error = "NO Error!",
                            InstanceID = Guid.NewGuid()
                        });
                    }
                }
            }catch(Exception ex)
            {
                //Console.WriteLine(ex.Message.ToString());
                result.Add(new ApexDTO() {AgencyID=0, HostedURL=connectionString, Error = ex.Message.ToString() });

            }
            return result;
        }

        public void InitDB()
        {
            var _conn = GetConnection("master");

            string query = @"IF NOT (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = 'GovTech' OR name = 'GovTech')))
                                BEGIN
                                    create DATABASE GovTech
                            END";

            SqlCommand cmd = new SqlCommand(query, _conn);
            cmd.ExecuteNonQuery();

            string query1 = @"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'APEX')
                            BEGIN

                            CREATE TABLE [dbo].[APEX](
                                [AgencyID] [int] IDENTITY(1,1) NOT NULL,
                                [HostedURL] [varchar](500) NULL,
                                [ConsumerURL] [varchar](500) NULL,
                                [ProductDescription] [varchar](500) NULL,
                                [Error] [varchar](500) NULL,
                                [InstanceID] [varchar](500) NULL
                            ) 
                            END";

            SqlCommand cmd1 = new SqlCommand(query1, _conn);
            cmd1.ExecuteNonQuery();

            string query2 = @"insert into dbo.APEX (HostedURL,ConsumerURL,ProductDescription,Error,InstanceID) values('http://www.google.com','http://www.google.com','Google','No Errors','Initialized')";
            SqlCommand cmd2 = new SqlCommand(query2, _conn);
            cmd2.ExecuteNonQuery();
        }
    }
}
