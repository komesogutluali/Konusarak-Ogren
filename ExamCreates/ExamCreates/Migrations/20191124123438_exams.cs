using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamCreates.Migrations
{
    public partial class exams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loginmodel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    User = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loginmodel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Exammodel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Detail = table.Column<string>(nullable: false),
                    Dater = table.Column<string>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exammodel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Exammodel_Loginmodel_UserID",
                        column: x => x.UserID,
                        principalTable: "Loginmodel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questionsmodels",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    question = table.Column<string>(maxLength: 100, nullable: false),
                    A = table.Column<string>(maxLength: 100, nullable: false),
                    B = table.Column<string>(maxLength: 100, nullable: false),
                    C = table.Column<string>(maxLength: 100, nullable: false),
                    D = table.Column<string>(maxLength: 100, nullable: false),
                    DC = table.Column<string>(maxLength: 100, nullable: false),
                    ExamID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionsmodels", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Questionsmodels_Exammodel_ExamID",
                        column: x => x.ExamID,
                        principalTable: "Exammodel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exammodel_UserID",
                table: "Exammodel",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Questionsmodels_ExamID",
                table: "Questionsmodels",
                column: "ExamID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questionsmodels");

            migrationBuilder.DropTable(
                name: "Exammodel");

            migrationBuilder.DropTable(
                name: "Loginmodel");
        }
    }
}
