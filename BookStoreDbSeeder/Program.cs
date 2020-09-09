using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BookStoreDbSeeder
{
    class Program
    {
        static void Main(string[] args)
        {
            string projectPath = Directory.GetCurrentDirectory() + @"..\..\..\..\";

            var options = new DbContextOptionsBuilder<BookStoreContext>()
                .UseSqlServer(GetConnectionString(projectPath + @"..\BookStore"))
                .Options;
            using var context = new BookStoreContext(options);

            var seeder = new Seeder(context, projectPath + @"\InitialData");
            seeder.Seed();
        }

        private static string GetConnectionString(string appsettingsParentFolderPath)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(appsettingsParentFolderPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            string connectionString = builder.Build().GetConnectionString("BookStore");
            return connectionString;
        }
    }
}
