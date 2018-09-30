using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Northwind.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Northwind.Data.Seed
{
    public static class DatabaseInitializer
    {
        public static void SeedData(NorthwindContext northwindContext, MasterContext masterContext)
        {
            bool databaseExists = (northwindContext.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists();

            if (!databaseExists)
            {
                string filePath = Path.Combine(AppContext.BaseDirectory, "Seed", "Northwind.sql");
                string seedSql = File.ReadAllText(filePath);

                string masterDbConnectionString = masterContext.Database.GetDbConnection().ConnectionString;

                // Overview (SMO): https://docs.microsoft.com/en-us/sql/relational-databases/server-management-objects-smo/overview-smo?view=sql-server-2017
                using (SqlConnection connection = new SqlConnection(masterDbConnectionString))
                {
                    Server server = new Server(new ServerConnection(connection));
                    //https://docs.microsoft.com/en-us/previous-versions/sql/sql-server-2014/ms199350%28v%3dsql.120%29
                    server.ConnectionContext.ExecuteNonQuery(seedSql);
                }
                
                //var batches = SplitSqlIntoBatches(databaseSql);

                //using (var masterContext = new MasterContext())
                //{
                //    for (int i = 0; i < batches.Count; i++)
                //    {
                //        masterContext.Database.ExecuteSqlCommand(batches[i]);
                //    }

                //    masterContext.SaveChanges();
                //}
            }
        }

        private static List<string> SplitSqlIntoBatches(string sqlString)
        {
            var batchesList = new List<string>();

            var batches = Regex.Split(
                Regex.Replace(
                    sqlString,
                    @"\\\r?\n",
                    string.Empty,
                    default,
                    TimeSpan.FromMilliseconds(1000.0)),
                @"^\s*(GO[ \t]+[0-9]+|GO)(?:\s+|$)",
                RegexOptions.IgnoreCase | RegexOptions.Multiline,
                TimeSpan.FromMilliseconds(1000.0));

            for (var i = 0; i < batches.Length; i++)
            {
                if (batches[i].StartsWith("GO", StringComparison.OrdinalIgnoreCase)
                    || string.IsNullOrWhiteSpace(batches[i]))
                {
                    continue;
                }

                var count = 1;

                if (i != batches.Length - 1 && batches[i + 1].StartsWith("GO", StringComparison.OrdinalIgnoreCase))
                {
                    var match = Regex.Match(
                        batches[i + 1], "([0-9]+)",
                        default,
                        TimeSpan.FromMilliseconds(1000.0));

                    if (match.Success)
                    {
                        count = int.Parse(match.Value);
                    }
                }

                for (var j = 0; j < count; j++)
                {
                    batchesList.Add(batches[i]);
                }
            }

            return batchesList;
        }

        private static string RemoveGoStatementFromSql(string sqlString)
        {
            var builder = new StringBuilder();

            var batches = Regex.Split(
                Regex.Replace(
                    sqlString,
                    @"\\\r?\n",
                    string.Empty,
                    default,
                    TimeSpan.FromMilliseconds(1000.0)),
                @"^\s*(GO[ \t]+[0-9]+|GO)(?:\s+|$)",
                RegexOptions.IgnoreCase | RegexOptions.Multiline,
                TimeSpan.FromMilliseconds(1000.0));

            for (var i = 0; i < batches.Length; i++)
            {
                if (batches[i].StartsWith("GO", StringComparison.OrdinalIgnoreCase)
                    || string.IsNullOrWhiteSpace(batches[i]))
                {
                    continue;
                }

                var count = 1;

                if (i != batches.Length - 1 && batches[i + 1].StartsWith("GO", StringComparison.OrdinalIgnoreCase))
                {
                    var match = Regex.Match(
                        batches[i + 1], "([0-9]+)",
                        default,
                        TimeSpan.FromMilliseconds(1000.0));

                    if (match.Success)
                    {
                        count = int.Parse(match.Value);
                    }
                }

                for (var j = 0; j < count; j++)
                {
                    builder.Append(batches[i]);

                    if (i == batches.Length - 1)
                    {
                        builder.AppendLine();
                    }
                }
            }

            return builder.ToString();
        }
    }
}
