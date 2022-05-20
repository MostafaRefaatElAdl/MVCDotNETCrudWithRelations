using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Day04App.Migrations
{
    public partial class coursesDepartmen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseDepartment",
                columns: table => new
                {
                    CoursesInDeptCrsID = table.Column<int>(type: "int", nullable: false),
                    DepartmentCoursesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseDepartment", x => new { x.CoursesInDeptCrsID, x.DepartmentCoursesId });
                    table.ForeignKey(
                        name: "FK_CourseDepartment_Courses_CoursesInDeptCrsID",
                        column: x => x.CoursesInDeptCrsID,
                        principalTable: "Courses",
                        principalColumn: "CrsID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseDepartment_Departments_DepartmentCoursesId",
                        column: x => x.DepartmentCoursesId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseDepartment_DepartmentCoursesId",
                table: "CourseDepartment",
                column: "DepartmentCoursesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseDepartment");
        }
    }
}
