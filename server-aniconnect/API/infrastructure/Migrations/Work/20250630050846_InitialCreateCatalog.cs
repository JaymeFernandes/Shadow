#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Migrations.Work;

/// <inheritdoc />
public partial class InitialCreateCatalog : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Works",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Code = table.Column<Guid>(type: "uuid", nullable: false),
                Status = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                Cover = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                Background = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                UsersAvaliable = table.Column<int>(type: "integer", nullable: false),
                TotalScore = table.Column<long>(type: "bigint", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Works", x => x.Id);
                table.UniqueConstraint("AK_Works_Code", x => x.Code);
            });

        migrationBuilder.CreateTable(
            name: "Author",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "text", nullable: false),
                workId = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Author", x => x.Id);
                table.ForeignKey(
                    name: "FK_Author_Works_workId",
                    column: x => x.workId,
                    principalTable: "Works",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Categories",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Code = table.Column<int>(type: "integer", nullable: false),
                WorkId = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Categories", x => x.Id);
                table.ForeignKey(
                    name: "FK_Categories_Works_WorkId",
                    column: x => x.WorkId,
                    principalTable: "Works",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Chapters",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Code = table.Column<string>(type: "text", nullable: false),
                Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                Sequence = table.Column<double>(type: "double precision", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                WorkId = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Chapters", x => x.Id);
                table.UniqueConstraint("AK_Chapters_Code", x => x.Code);
                table.ForeignKey(
                    name: "FK_Chapters_Works_WorkId",
                    column: x => x.WorkId,
                    principalTable: "Works",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Names",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                Lang = table.Column<string>(type: "text", nullable: false),
                WorkId = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Names", x => x.Id);
                table.ForeignKey(
                    name: "FK_Names_Works_WorkId",
                    column: x => x.WorkId,
                    principalTable: "Works",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Tags",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Code = table.Column<int>(type: "integer", nullable: false),
                WorkId = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tags", x => x.Id);
                table.ForeignKey(
                    name: "FK_Tags_Works_WorkId",
                    column: x => x.WorkId,
                    principalTable: "Works",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Author_workId",
            table: "Author",
            column: "workId");

        migrationBuilder.CreateIndex(
            name: "IX_Categories_Code",
            table: "Categories",
            column: "Code");

        migrationBuilder.CreateIndex(
            name: "IX_Categories_WorkId",
            table: "Categories",
            column: "WorkId");

        migrationBuilder.CreateIndex(
            name: "IX_Chapters_WorkId",
            table: "Chapters",
            column: "WorkId");

        migrationBuilder.CreateIndex(
            name: "IX_Names_Name",
            table: "Names",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Names_WorkId",
            table: "Names",
            column: "WorkId");

        migrationBuilder.CreateIndex(
            name: "IX_Tags_Code",
            table: "Tags",
            column: "Code");

        migrationBuilder.CreateIndex(
            name: "IX_Tags_WorkId",
            table: "Tags",
            column: "WorkId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Author");

        migrationBuilder.DropTable(
            name: "Categories");

        migrationBuilder.DropTable(
            name: "Chapters");

        migrationBuilder.DropTable(
            name: "Names");

        migrationBuilder.DropTable(
            name: "Tags");

        migrationBuilder.DropTable(
            name: "Works");
    }
}