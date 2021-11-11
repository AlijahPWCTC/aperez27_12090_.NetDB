using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace A11___Convert_Application_to_use_Database.Migrations
{
    public partial class InsertMovies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "SQL", @"2-InsertMovies.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from movies");
        }
    }
}
