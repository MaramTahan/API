using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace westcoasteducation2.api.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "coursesData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    courseNumber = table.Column<string>(type: "TEXT", nullable: true),
                    nameId = table.Column<int>(type: "INTEGER", nullable: false),
                    startDate = table.Column<string>(type: "TEXT", nullable: true),
                    endDate = table.Column<string>(type: "TEXT", nullable: true),
                    teacher = table.Column<string>(type: "TEXT", nullable: true),
                    placeStudy = table.Column<string>(type: "TEXT", nullable: true),
                    status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coursesData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "coursesNameData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coursesNameData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "studentData",
                columns: table => new
                {
                    userId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    firstName = table.Column<string>(type: "TEXT", nullable: true),
                    lastName = table.Column<string>(type: "TEXT", nullable: true),
                    personNu = table.Column<string>(type: "TEXT", nullable: true),
                    email = table.Column<string>(type: "TEXT", nullable: true),
                    phoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    address = table.Column<string>(type: "TEXT", nullable: true),
                    coursesTakenId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studentData", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "teacherData",
                columns: table => new
                {
                    TUserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    firstName = table.Column<string>(type: "TEXT", nullable: true),
                    lastName = table.Column<string>(type: "TEXT", nullable: true),
                    email = table.Column<string>(type: "TEXT", nullable: true),
                    phoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    address = table.Column<string>(type: "TEXT", nullable: true),
                    coursesTaughtId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teacherData", x => x.TUserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "coursesData");

            migrationBuilder.DropTable(
                name: "coursesNameData");

            migrationBuilder.DropTable(
                name: "studentData");

            migrationBuilder.DropTable(
                name: "teacherData");
        }
    }
}
