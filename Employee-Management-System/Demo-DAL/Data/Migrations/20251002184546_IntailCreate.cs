using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo_DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class IntailCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "10, 10"),
                    Name = table.Column<string>(type: "varchar(20)", nullable: false),
                    Code = table.Column<string>(type: "varchar(20)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_By = table.Column<int>(type: "int", nullable: false),
                    Created_On = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Modified_By = table.Column<int>(type: "int", nullable: false),
                    Modified_On = table.Column<DateTime>(type: "datetime2", nullable: false, computedColumnSql: "getdate()"),
                    Last_Mdifed_On = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Last_Modified_By = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
