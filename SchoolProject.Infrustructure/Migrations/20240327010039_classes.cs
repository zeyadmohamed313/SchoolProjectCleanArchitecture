using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class classes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    AvailablePlaces = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GetDepartmentWithStudentsResult",
                columns: table => new
                {
                    DNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudID = table.Column<int>(type: "int", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

          

            migrationBuilder.CreateTable(
                name: "ClassInstructor",
                columns: table => new
                {
                    InstructorsInsId = table.Column<int>(type: "int", nullable: false),
                    classesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassInstructor", x => new { x.InstructorsInsId, x.classesId });
                    table.ForeignKey(
                        name: "FK_ClassInstructor_Instructor_InstructorsInsId",
                        column: x => x.InstructorsInsId,
                        principalTable: "Instructor",
                        principalColumn: "InsId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ClassInstructor_classes_classesId",
                        column: x => x.classesId,
                        principalTable: "classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ClassStudent",
                columns: table => new
                {
                    EnrolledClassesId = table.Column<int>(type: "int", nullable: false),
                    StudentsStudID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassStudent", x => new { x.EnrolledClassesId, x.StudentsStudID });
                    table.ForeignKey(
                        name: "FK_ClassStudent_Students_StudentsStudID",
                        column: x => x.StudentsStudID,
                        principalTable: "Students",
                        principalColumn: "StudID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ClassStudent_classes_EnrolledClassesId",
                        column: x => x.EnrolledClassesId,
                        principalTable: "classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassInstructor_classesId",
                table: "ClassInstructor",
                column: "classesId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassStudent_StudentsStudID",
                table: "ClassStudent",
                column: "StudentsStudID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassInstructor");

            migrationBuilder.DropTable(
                name: "ClassStudent");

            migrationBuilder.DropTable(
                name: "GetDepartmentWithStudentsResult");

            migrationBuilder.DropTable(
                name: "ViewDepartment");

            migrationBuilder.DropTable(
                name: "classes");
        }
    }
}
