using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SST.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Faculty = table.Column<string>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    IsMain = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Email = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(256)", nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Lectors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    AcademicStatus = table.Column<string>(nullable: false),
                    UserRef = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lectors_Users_UserRef",
                        column: x => x.UserRef,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsApproved = table.Column<bool>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UserRef = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Users_UserRef",
                        column: x => x.UserRef,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    GroupRef = table.Column<int>(nullable: true),
                    UserRef = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Groups_GroupRef",
                        column: x => x.GroupRef,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Students_Users_UserRef",
                        column: x => x.UserRef,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    LectorRef = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_Lectors_LectorRef",
                        column: x => x.LectorRef,
                        principalTable: "Lectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecondaryGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentRef = table.Column<int>(nullable: false),
                    GroupRef = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondaryGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecondaryGroups_Groups_GroupRef",
                        column: x => x.GroupRef,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecondaryGroups_Students_StudentRef",
                        column: x => x.StudentRef,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupRef = table.Column<int>(nullable: false),
                    SubjectRef = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupSubjects_Groups_GroupRef",
                        column: x => x.GroupRef,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupSubjects_Subjects_SubjectRef",
                        column: x => x.SubjectRef,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JournalColumns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    GroupSubjectRef = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalColumns_GroupSubjects_GroupSubjectRef",
                        column: x => x.GroupSubjectRef,
                        principalTable: "GroupSubjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mark = table.Column<int>(nullable: false),
                    StudentRef = table.Column<int>(nullable: false),
                    JournalColumnRef = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grades_JournalColumns_JournalColumnRef",
                        column: x => x.JournalColumnRef,
                        principalTable: "JournalColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grades_Students_StudentRef",
                        column: x => x.StudentRef,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Faculty", "IsMain", "Name", "Year" },
                values: new object[,]
                {
                    { 1, "Факультет прикладної математики та інформатики", true, "ПМІм-11", 0 },
                    { 2, "Факультет прикладної математики та інформатики", true, "ПМІм-12", 0 },
                    { 3, "Факультет прикладної математики та інформатики", true, "ПМІм-13", 0 },
                    { 4, "Факультет журналістики", true, "ЖРН-11с", 0 },
                    { 5, "Філософський факультет", true, "ФФП-42с", 0 }
                });

            migrationBuilder.InsertData(
                table: "Lectors",
                columns: new[] { "Id", "AcademicStatus", "FirstName", "LastName", "UserRef" },
                values: new object[,]
                {
                    { 1, "Доцент", "Анатолій", "Музичук", null },
                    { 2, "Асистент", "Андрій", "Глова", null },
                    { 3, "Професор", "Юрій", "Щербина", null },
                    { 4, "Доцент", "Віталій", "Горлач", null },
                    { 5, "Асистент", "Любомир", "Галамага", null },
                    { 6, "Професор", "Софія", "Грабовська", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Email", "IsAdmin", "PasswordHash" },
                values: new object[,]
                {
                    { "admin@email.com", true, "Yh+CEuxWzPTw0y2M9zgFEw1stxAwoa1mvyaoI2157nY=" },
                    { "vadimgromov1403@gmail.com", false, "X1ReXJ0j6yv7TfPCmfQ/pTeniB/AnjIif5c03K1QNEU=" }
                });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "CreationDate", "IsApproved", "UserRef" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 12, 13, 22, 1, 21, 757, DateTimeKind.Local).AddTicks(6979), true, "admin@email.com" },
                    { 2, new DateTime(2021, 12, 13, 22, 1, 21, 759, DateTimeKind.Local).AddTicks(7926), true, "vadimgromov1403@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "FirstName", "GroupRef", "LastName", "UserRef" },
                values: new object[,]
                {
                    { 5, "Іван", 1, "Іванов", null },
                    { 1, "Данило", 3, "Тимець", null },
                    { 3, "Тарас", 3, "Бобеляк", null },
                    { 4, "Віктор", 3, "Стрельников", null },
                    { 2, "Вадим", 3, "Громов", "vadimgromov1403@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "LectorRef", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Програмна інженерія" },
                    { 2, 3, "Дискретна математика" },
                    { 4, 3, "Теорія ймовірності та математична статистика" },
                    { 3, 5, "Програмування" },
                    { 5, 6, "Психологія примирення" }
                });

            migrationBuilder.InsertData(
                table: "GroupSubjects",
                columns: new[] { "Id", "GroupRef", "SubjectRef" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 2 },
                    { 4, 3, 4 },
                    { 5, 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "JournalColumns",
                columns: new[] { "Id", "Date", "GroupSubjectRef", "Note" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null },
                    { 2, new DateTime(2020, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null },
                    { 3, new DateTime(2020, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null },
                    { 4, new DateTime(2020, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null },
                    { 5, new DateTime(2020, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null }
                });

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "Id", "JournalColumnRef", "Mark", "StudentRef" },
                values: new object[,]
                {
                    { 4, 1, 14, 2 },
                    { 3, 2, 18, 3 },
                    { 2, 3, 15, 2 },
                    { 1, 4, 20, 1 },
                    { 5, 5, 20, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grades_JournalColumnRef",
                table: "Grades",
                column: "JournalColumnRef");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StudentRef",
                table: "Grades",
                column: "StudentRef");

            migrationBuilder.CreateIndex(
                name: "IX_GroupSubjects_GroupRef",
                table: "GroupSubjects",
                column: "GroupRef");

            migrationBuilder.CreateIndex(
                name: "IX_GroupSubjects_SubjectRef",
                table: "GroupSubjects",
                column: "SubjectRef");

            migrationBuilder.CreateIndex(
                name: "IX_JournalColumns_GroupSubjectRef",
                table: "JournalColumns",
                column: "GroupSubjectRef");

            migrationBuilder.CreateIndex(
                name: "IX_Lectors_UserRef",
                table: "Lectors",
                column: "UserRef",
                unique: true,
                filter: "[UserRef] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_UserRef",
                table: "Requests",
                column: "UserRef",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SecondaryGroups_GroupRef",
                table: "SecondaryGroups",
                column: "GroupRef");

            migrationBuilder.CreateIndex(
                name: "IX_SecondaryGroups_StudentRef",
                table: "SecondaryGroups",
                column: "StudentRef");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupRef",
                table: "Students",
                column: "GroupRef");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserRef",
                table: "Students",
                column: "UserRef",
                unique: true,
                filter: "[UserRef] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_LectorRef",
                table: "Subjects",
                column: "LectorRef");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "SecondaryGroups");

            migrationBuilder.DropTable(
                name: "JournalColumns");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "GroupSubjects");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Lectors");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
