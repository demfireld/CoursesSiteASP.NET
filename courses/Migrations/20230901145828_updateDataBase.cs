using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace courses.Migrations
{
    /// <inheritdoc />
    public partial class updateDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffPatronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffAge = table.Column<int>(type: "int", nullable: false),
                    StaffWorkExperience = table.Column<int>(type: "int", nullable: false),
                    StaffPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffImg = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherPatronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeacherAge = table.Column<int>(type: "int", nullable: false),
                    TeacherWorkExperience = table.Column<int>(type: "int", nullable: false),
                    TeacherPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherImg = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
