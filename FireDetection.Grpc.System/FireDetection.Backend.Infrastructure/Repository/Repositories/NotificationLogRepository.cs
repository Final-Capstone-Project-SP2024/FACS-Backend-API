using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FireDetection.Backend.Infrastructure.Repository.Repositories
{
    public class NotificationLogRepository : GenericRepository<NotificationLog>, INotificationLogRepository
    {
        private readonly FireDetectionDbContext _context;
        private readonly IConfiguration _configuration;

        public NotificationLogRepository(FireDetectionDbContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<RecordInDay>> GetDayAnalysis()
        {
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            List<RecordInDay> records = new List<RecordInDay>();

            // Execute raw SQL query
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    string sqlQuery = @"
                SELECT 
                    DATE_TRUNC('day', ""RecordTime"") AS ""Date"",
                    COUNT(*) AS ""Count""
                FROM 
                    ""Records""
                GROUP BY 
                    DATE_TRUNC('day', ""RecordTime"")
                ORDER BY 
                    DATE_TRUNC('day', ""RecordTime"")";

                    using (var cmd = new NpgsqlCommand(sqlQuery, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            DateTime date = reader.GetDateTime(0);
                            int numRecords = reader.GetInt32(1);

                            records.Add(new RecordInDay { Date = date, Count = numRecords });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return records;
        }

        public async Task<List<RecordInYear>> GetYearAnalysis()
        {
            string dataConnection = _configuration["ConnectionStrings:DefaultConnection"];
            List<RecordInYear> records = new List<RecordInYear>();

            // Execute raw SQL query
            try
            {
                using (var conn = new NpgsqlConnection(dataConnection))
                {
                    await conn.OpenAsync();
                    string sqlQuery = @"
                SELECT 
                    DATE_TRUNC('year', ""RecordTime"") AS ""Year"",
                    COUNT(*) AS ""Count""
                FROM 
                    ""Records""
                GROUP BY 
                    DATE_TRUNC('year', ""RecordTime"")
                ORDER BY 
                    DATE_TRUNC('year', ""RecordTime"")";

                    using (var cmd = new NpgsqlCommand(sqlQuery, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            DateTime date = reader.GetDateTime(0);
                            int numRecords = reader.GetInt32(1);
                            Console.WriteLine(date);
                            records.Add(new RecordInYear { Year = date, Count = numRecords });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return records;
        }

        public  async Task<List<RecordInMonth>> GetMonthAnalysis()
        {
            string dataConnection = _configuration["ConnectionStrings:DefaultConnection"];
            List<RecordInMonth> records = new List<RecordInMonth>();

            // Execute raw SQL query
            try
            {
                using (var conn = new NpgsqlConnection(dataConnection))
                {
                    await conn.OpenAsync();
                    string sqlQuery = @"
                SELECT 
                    DATE_TRUNC('month', ""RecordTime"") AS ""Month"",
                    COUNT(*) AS ""Count""
                FROM 
                    ""Records""
                GROUP BY 
                    DATE_TRUNC('month', ""RecordTime"")
                ORDER BY 
                    DATE_TRUNC('month', ""RecordTime"")";

                    using (var cmd = new NpgsqlCommand(sqlQuery, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            DateTime date = reader.GetDateTime(0);
                            int numRecords = reader.GetInt32(1);

                            records.Add(new RecordInMonth { Month = date, Count = numRecords });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return records;
        }
    }
}
