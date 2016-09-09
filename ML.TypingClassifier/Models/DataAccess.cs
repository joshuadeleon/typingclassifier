using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ML.TypingClassifier.Models
{
    public static class SampleMapper
    {
        public readonly static string Insert =
            "INSERT INTO dbo.Users (Handle, Email, RawData) VALUES(@handle, @email, @rawData);";
        public readonly static string Update =
            "UPDATE dbo.Users SET Handle = @handle, RawData = @rawData;";

        public static Sample Read(SqlDataReader reader)
        {
            var sample = new Sample
            {
                Handle = reader["Handle"].ToString(),
                Email = reader["Email"].ToString(),
            };
            sample.Events = JsonConvert.DeserializeObject<KeyEvent[]>(reader["RawData"].ToString());
            return sample;
        }
    }

    public class DataAccess
    {
        private readonly string _connStr;

        public DataAccess(string connStr)
        {
            _connStr = connStr;
        }

        public void Add(Sample sample)
        {
            var conn = new SqlConnection(_connStr);
            SqlCommand cmd;
            var jsonEvents = JsonConvert.SerializeObject(sample.Events);
            var existingUser = Single(sample.Email);
            cmd = PrepareCommand(sample, jsonEvents, existingUser);
            try
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null)
                    conn.Dispose();
                if (cmd != null)
                    cmd.Dispose();
            }
        }

        public Sample Single(string email)
        {
            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand("SELECT TOP(1) Id, Handle, Email, RawData FROM dbo.Users WHERE Email = @email;", conn))
            {
                cmd.Parameters.Add(new SqlParameter("@email", email));
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return SampleMapper.Read(reader);
                    }
                }
            }
            return null;
        }

        public List<Sample> All()
        {
            using (var conn = new SqlConnection(_connStr))
            using (var cmd = new SqlCommand("SELECT Id, Handle, Email, RawData FROM dbo.Users;", conn))
            {
                conn.Open();
                var samples = new List<Sample>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        samples.Add(SampleMapper.Read(reader));
                    }
                }
                return samples;
            }
        }

        private static SqlCommand PrepareCommand(Sample sample, string jsonEvents, Sample existingUser)
        {
            SqlCommand cmd;
            if (existingUser == null)
            {
                cmd = new SqlCommand(SampleMapper.Insert);
                cmd.Parameters.AddRange(new[]
                {
                    new SqlParameter("@handle", sample.Handle),
                    new SqlParameter("@email", sample.Email),
                    new SqlParameter("@rawData", jsonEvents)
                });
            }
            else
            {
                cmd = new SqlCommand(SampleMapper.Update);
                cmd.Parameters.AddRange(new[]
                {
                    new SqlParameter("@handle", sample.Handle),
                    new SqlParameter("@rawData", jsonEvents)
                });
            }

            return cmd;
        }
    }
}