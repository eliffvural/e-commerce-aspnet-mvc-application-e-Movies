using System;
using System.Data;
using Microsoft.AnalysisServices.AdomdClient;

namespace eTickets.Data.Services
{
    public interface IOlapService
    {
        DataTable ExecuteOlapQuery(string query);
    }

    public class OlapService : IOlapService
    {
        private readonly string _connectionString;

        public OlapService(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public DataTable ExecuteOlapQuery(string query)
        {
            using (var connection = new AdomdConnection(_connectionString))
            {
                connection.Open();
                var command = new AdomdCommand(query, connection);

                using (var reader = command.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);
                    return dataTable;
                }
            }
        }
    }
}
