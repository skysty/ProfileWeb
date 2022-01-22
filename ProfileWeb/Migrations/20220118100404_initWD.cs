using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileWeb.Migrations
{
    public partial class initWD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fistname_en",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Fistname_kz",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Fistname_ru",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Fistname_tr",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Sex_ID",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Rank_ID",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Faculty_ID",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Department_ID",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Degree_ID",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Firstname_en",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Firstname_kz",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Firstname_ru",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Firstname_tr",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Topic_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TR_Topic = table.Column<string>(nullable: true),
                    KZ_Topic = table.Column<string>(nullable: true),
                    RU_Topic = table.Column<string>(nullable: true),
                    EN_Topic = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Topic_Id);
                    table.ForeignKey(
                        name: "FK_Achievements_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Degrees",
                columns: table => new
                {
                    Degree_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TIP = table.Column<byte>(nullable: false),
                    TR_AD = table.Column<string>(nullable: true),
                    KZ_AD = table.Column<string>(nullable: true),
                    RU_AD = table.Column<string>(nullable: true),
                    EN_AD = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degrees", x => x.Degree_ID);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KOD = table.Column<string>(nullable: true),
                    Tip = table.Column<string>(nullable: true),
                    TR_AD = table.Column<string>(nullable: true),
                    EN_AD = table.Column<string>(nullable: true),
                    RU_AD = table.Column<string>(nullable: true),
                    KZ_AD = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Kafedras",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KOD = table.Column<string>(nullable: true),
                    TIP = table.Column<string>(nullable: true),
                    TR_AD = table.Column<string>(nullable: true),
                    KZ_AD = table.Column<string>(nullable: true),
                    RU_AD = table.Column<string>(nullable: true),
                    EN_AD = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kafedras", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Qulifications",
                columns: table => new
                {
                    Qu_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TR_Qu = table.Column<string>(nullable: true),
                    KZ_Qu = table.Column<string>(nullable: true),
                    RU_Qu = table.Column<string>(nullable: true),
                    EN_Qu = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qulifications", x => x.Qu_Id);
                    table.ForeignKey(
                        name: "FK_Qulifications_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ranks",
                columns: table => new
                {
                    Rank_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TIP = table.Column<byte>(nullable: false),
                    TR_AD = table.Column<string>(nullable: true),
                    KZ_AD = table.Column<string>(nullable: true),
                    RU_AD = table.Column<string>(nullable: true),
                    EN_AD = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ranks", x => x.Rank_ID);
                });

            migrationBuilder.CreateTable(
                name: "Researches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Res_Id = table.Column<int>(nullable: false),
                    KZ_Title = table.Column<string>(nullable: true),
                    FileUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Researches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Researches_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sexes",
                columns: table => new
                {
                    Sex_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sex_KZ = table.Column<string>(nullable: true),
                    Sex_EN = table.Column<string>(nullable: true),
                    Sex_RU = table.Column<string>(nullable: true),
                    Sex_TR = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sexes", x => x.Sex_ID);
                });

            migrationBuilder.CreateTable(
                name: "Workways",
                columns: table => new
                {
                    Work_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TR_Work = table.Column<string>(nullable: true),
                    KZ_Work = table.Column<string>(nullable: true),
                    RU_Work = table.Column<string>(nullable: true),
                    EN_Work = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workways", x => x.Work_Id);
                    table.ForeignKey(
                        name: "FK_Workways_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Degree_ID",
                table: "AspNetUsers",
                column: "Degree_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Department_ID",
                table: "AspNetUsers",
                column: "Department_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Faculty_ID",
                table: "AspNetUsers",
                column: "Faculty_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Rank_ID",
                table: "AspNetUsers",
                column: "Rank_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Sex_ID",
                table: "AspNetUsers",
                column: "Sex_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_Id",
                table: "Achievements",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Qulifications_Id",
                table: "Qulifications",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Workways_Id",
                table: "Workways",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Degrees_Degree_ID",
                table: "AspNetUsers",
                column: "Degree_ID",
                principalTable: "Degrees",
                principalColumn: "Degree_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Kafedras_Department_ID",
                table: "AspNetUsers",
                column: "Department_ID",
                principalTable: "Kafedras",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Faculties_Faculty_ID",
                table: "AspNetUsers",
                column: "Faculty_ID",
                principalTable: "Faculties",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Ranks_Rank_ID",
                table: "AspNetUsers",
                column: "Rank_ID",
                principalTable: "Ranks",
                principalColumn: "Rank_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Sexes_Sex_ID",
                table: "AspNetUsers",
                column: "Sex_ID",
                principalTable: "Sexes",
                principalColumn: "Sex_ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Degrees_Degree_ID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Kafedras_Department_ID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Faculties_Faculty_ID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Ranks_Rank_ID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Sexes_Sex_ID",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "Degrees");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "Kafedras");

            migrationBuilder.DropTable(
                name: "Qulifications");

            migrationBuilder.DropTable(
                name: "Ranks");

            migrationBuilder.DropTable(
                name: "Researches");

            migrationBuilder.DropTable(
                name: "Sexes");

            migrationBuilder.DropTable(
                name: "Workways");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Degree_ID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Department_ID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Faculty_ID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Rank_ID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Sex_ID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Firstname_en",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Firstname_kz",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Firstname_ru",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Firstname_tr",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Sex_ID",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Rank_ID",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Faculty_ID",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Department_ID",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Degree_ID",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "Fistname_en",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fistname_kz",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fistname_ru",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fistname_tr",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
