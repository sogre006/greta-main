using Microsoft.Extensions.Configuration;
using PeriodTracker.Model.Entities;
using Npgsql;
using NpgsqlTypes;

namespace PeriodTracker.Model.Repositories
{
    public class SymptomRepository : BaseRepository
    {
        public SymptomRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public Symptom GetById(int id)
        {
            NpgsqlConnection dbConn = null;
            try
            {
                dbConn = new NpgsqlConnection(ConnectionString);
                var cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Symptoms WHERE symptom_id = @id";
                cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;

                var data = GetData(dbConn, cmd);
                if (data != null && data.Read())
                {
                    return new Symptom
                    {
                        SymptomId = Convert.ToInt32(data["symptom_id"]),
                        Name = data["name"].ToString(),
                        Icon = data["icon"]?.ToString()
                    };
                }
                return null;
            }
            finally
            {
                dbConn?.Close();
            }
        }

        public List<Symptom> GetAllSymptoms()
        {
            NpgsqlConnection dbConn = null;
            var symptoms = new List<Symptom>();
            try
            {
                dbConn = new NpgsqlConnection(ConnectionString);
                var cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Symptoms ORDER BY name";

                var data = GetData(dbConn, cmd);
                if (data != null)
                {
                    while (data.Read())
                    {
                        symptoms.Add(new Symptom
                        {
                            SymptomId = Convert.ToInt32(data["symptom_id"]),
                            Name = data["name"].ToString(),
                            Icon = data["icon"]?.ToString()
                        });
                    }
                }
                return symptoms;
            }
            finally
            {
                dbConn?.Close();
            }
        }

    }
}