using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using AperoBoxApi.Context;

namespace AperoBoxApi.Controllers
{
    public class DesignTimeContextFactory : IDesignTimeDbContextFactory<AperoBoxApi_dbContext>
    {
        private const string CONNECTION_STRING_CONFIG_KEY = "Connection";
        readonly string connectionString;
        public DesignTimeContextFactory()
        {
            var helper = new ConfigurationHelper(CONNECTION_STRING_CONFIG_KEY);
            connectionString = helper.GetConnectionString();
        }
        public AperoBoxApi_dbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<AperoBoxApi_dbContext> builder = new DbContextOptionsBuilder<AperoBoxApi_dbContext>();
            builder.UseSqlServer(connectionString);
            return new AperoBoxApi_dbContext(builder.Options);
        }
    }
}