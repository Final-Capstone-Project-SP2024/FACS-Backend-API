using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using Google.Api.Gax.ResourceNames;
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

        public async Task<List<RecordInMonth>> GetMonthAnalysis()
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

        public async Task<List<RecordInWeek>> GetWeekAnalysis()
        {
            string dataConnection = _configuration["ConnectionStrings:DefaultConnection"];
            List<RecordInWeek> records = new List<RecordInWeek>();

            // Execute raw SQL query
            try
            {
                using (var conn = new NpgsqlConnection(dataConnection))
                {
                    await conn.OpenAsync();
                    string sqlQuery = @"
                SELECT 
                    DATE_TRUNC('week', ""RecordTime"") AS ""Week"",
                    COUNT(*) AS ""Count""
                FROM 
                    ""Records""
                GROUP BY 
                    DATE_TRUNC('week', ""RecordTime"")
                ORDER BY 
                    DATE_TRUNC('week', ""RecordTime"")";

                    using (var cmd = new NpgsqlCommand(sqlQuery, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            DateTime date = reader.GetDateTime(0);
                            int numRecords = reader.GetInt32(1);

                            records.Add(new RecordInWeek { Week = date, Count = numRecords });
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

        public async Task<GetLocationAnalysisData> GetLocationAnalysis()
        {
            GetLocationAnalysisData analysisData = new GetLocationAnalysisData();
            string dataConnection = _configuration["ConnectionStrings:DefaultConnection"];

            try
            {
                using (var conn = new NpgsqlConnection(dataConnection))
                {
                    await conn.OpenAsync();
                    string sqlQuery = @"
                SELECT 
                    l.""LocationName"",
                    c.""CameraName"",
                    COUNT(r.""CameraID"") AS ""RecordCount""
                FROM 
                    public.""Locations"" l
                JOIN 
                    public.""Cameras"" c ON l.""Id"" = c.""LocationID""
                LEFT JOIN 
                    public.""Records"" r ON c.""Id"" = r.""CameraID""
                GROUP BY 
                    l.""LocationName"", c.""CameraName""";

                    using (var cmd = new NpgsqlCommand(sqlQuery, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            string locationName = reader.GetString(0);
                            string cameraName = reader.GetString(1);
                            int recordCount = reader.GetInt32(2);

                            // Check if the location already exists in the dictionary
                            if (analysisData.Analysis.TryGetValue(locationName, out var cameraInLocationAnalyses))
                            {
                                cameraInLocationAnalyses.Add(new CameraInLocationAnalysis
                                {
                                    CameraName = cameraName,
                                    Count = recordCount
                                });
                            }
                            else
                            {
                                // Create a new list and add the first CameraInLocationAnalysis to it
                                var newCameraInLocationAnalyses = new List<CameraInLocationAnalysis>
                        {
                            new CameraInLocationAnalysis
                            {
                                CameraName = cameraName,
                                Count = recordCount
                            }
                        };

                                // Add the new list to the dictionary
                                analysisData.Analysis.Add(locationName, newCameraInLocationAnalyses);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return analysisData;
        }
    }
}
