using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace westcoasteducation2.api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_teacherData_coursesTaughtId",
                table: "teacherData",
                column: "coursesTaughtId");

            migrationBuilder.CreateIndex(
                name: "IX_studentData_coursesTakenId",
                table: "studentData",
                column: "coursesTakenId");

            migrationBuilder.CreateIndex(
                name: "IX_coursesData_nameId",
                table: "coursesData",
                column: "nameId");

            migrationBuilder.AddForeignKey(
                name: "FK_coursesData_coursesNameData_nameId",
                table: "coursesData",
                column: "nameId",
                principalTable: "coursesNameData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_studentData_coursesNameData_coursesTakenId",
                table: "studentData",
                column: "coursesTakenId",
                principalTable: "coursesNameData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_teacherData_coursesNameData_coursesTaughtId",
                table: "teacherData",
                column: "coursesTaughtId",
                principalTable: "coursesNameData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_coursesData_coursesNameData_nameId",
                table: "coursesData");

            migrationBuilder.DropForeignKey(
                name: "FK_studentData_coursesNameData_coursesTakenId",
                table: "studentData");

            migrationBuilder.DropForeignKey(
                name: "FK_teacherData_coursesNameData_coursesTaughtId",
                table: "teacherData");

            migrationBuilder.DropIndex(
                name: "IX_teacherData_coursesTaughtId",
                table: "teacherData");

            migrationBuilder.DropIndex(
                name: "IX_studentData_coursesTakenId",
                table: "studentData");

            migrationBuilder.DropIndex(
                name: "IX_coursesData_nameId",
                table: "coursesData");
        }
    }
}
