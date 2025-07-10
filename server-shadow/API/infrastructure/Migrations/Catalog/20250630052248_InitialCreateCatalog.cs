using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MigrationsCatalog;

/// <inheritdoc />
public partial class InitialCreateCatalog : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Categories",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Categories", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Tags",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tags", x => x.Id);
            });

        migrationBuilder.InsertData(
            table: "Categories",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "Action" },
                { 2, "Adventure" },
                { 3, "Comedy" },
                { 4, "Drama" },
                { 5, "Romance" },
                { 6, "Fantasy" },
                { 7, "Supernatural" },
                { 8, "Horror" },
                { 9, "Thriller" },
                { 10, "Mystery" },
                { 11, "Psychological" },
                { 12, "Slice of Life" },
                { 13, "Sci-Fi" },
                { 14, "Mecha" },
                { 15, "Military" },
                { 16, "Historical" },
                { 17, "Music" },
                { 18, "Sports" },
                { 19, "Martial Arts" },
                { 20, "School Life" },
                { 21, "Magic" },
                { 22, "Crime" },
                { 23, "Detective" },
                { 24, "Dementia" },
                { 25, "Game" },
                { 26, "Survival" },
                { 27, "Apocalypse" },
                { 28, "Post-Apocalyptic" },
                { 29, "Space" },
                { 30, "Time Travel" },
                { 31, "Isekai" },
                { 32, "Reincarnation" },
                { 33, "Parallel Worlds" },
                { 34, "Demons" },
                { 35, "Vampires" },
                { 36, "Zombies" },
                { 37, "Aliens" },
                { 38, "Mythology" },
                { 39, "Gods" },
                { 40, "Cultivation" },
                { 41, "Superpower" },
                { 42, "Cyberpunk" },
                { 43, "Steampunk" },
                { 44, "Political" },
                { 45, "Philosophy" },
                { 46, "Tragedy" },
                { 47, "Dark Fantasy" },
                { 48, "High Fantasy" },
                { 49, "Urban Fantasy" },
                { 50, "Light Novel" },
                { 51, "Shounen" },
                { 52, "Shoujo" },
                { 53, "Seinen" },
                { 54, "Josei" },
                { 55, "Harem" },
                { 56, "Reverse Harem" },
                { 57, "Ecchi" },
                { 58, "Fanservice" },
                { 59, "BL (Boys Love)" },
                { 60, "GL (Girls Love)" },
                { 61, "LGBTQIA+" },
                { 62, "Drama Family" },
                { 63, "Friendship" },
                { 64, "Revenge" },
                { 65, "Anti-Hero" },
                { 66, "Healing" },
                { 67, "Coming of Age" },
                { 68, "Satire" },
                { 69, "Parody" },
                { 70, "Post-Truth" },
                { 71, "Psychological Horror" },
                { 72, "Psychological Thriller" },
                { 73, "Noir" },
                { 74, "Biography" },
                { 75, "Documentary" },
                { 76, "Experimental" },
                { 77, "Abstract" },
                { 78, "Virtual Reality" },
                { 79, "Augmented Reality" },
                { 80, "Artificial Intelligence" },
                { 81, "Post-Human" },
                { 82, "Meta" },
                { 83, "Fourth Wall" },
                { 84, "Breaking the Fourth Wall" },
                { 85, "Memoir" },
                { 86, "Fantasy Comedy" },
                { 87, "Fantasy Romance" },
                { 88, "Romantic Comedy" },
                { 89, "Action Comedy" },
                { 90, "Action Romance" },
                { 91, "Supernatural Mystery" }
            });

        migrationBuilder.CreateIndex(
            name: "IX_Categories_Name",
            table: "Categories",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Tags_Name",
            table: "Tags",
            column: "Name",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Categories");

        migrationBuilder.DropTable(
            name: "Tags");
    }
}