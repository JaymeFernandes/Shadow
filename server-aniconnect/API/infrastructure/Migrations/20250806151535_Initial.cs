using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    OriginalName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    HeightCm = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    ZodiacSign = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Aggregators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Avatar = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aggregators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BackGrounds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Link = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackGrounds", x => x.Id);
                });

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
                name: "Contents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    Type = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false, defaultValue: ""),
                    UsersAvaliable = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    TotalScore = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Covers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Link = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Covers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryByContent",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    ContentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryByContent", x => new { x.ContentId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_CategoryByContent_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryByContent_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    OriginalName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    HeightCm = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    ZodiacSign = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false, defaultValue: 3),
                    Url = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    Platform = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Thumbnail = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ContentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medias_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Names",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Lang = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    ContentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Names", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Names_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SeasonNumber = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    ContentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seasons_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ContentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CharacterByActor",
                columns: table => new
                {
                    ActorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterByActor", x => new { x.CharacterId, x.ActorId });
                    table.ForeignKey(
                        name: "FK_CharacterByActor_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterByActor_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Sequence = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SeasonId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episodes_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Episodes_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagByContent",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    ContentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagByContent", x => new { x.ContentId, x.TagId });
                    table.ForeignKey(
                        name: "FK_TagByContent_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagByContent_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExternalLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EpisodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    AggregatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalLinks_Aggregators_AggregatorId",
                        column: x => x.AggregatorId,
                        principalTable: "Aggregators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExternalLinks_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExternalLinks_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_CategoryByContent_CategoryId",
                table: "CategoryByContent",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterByActor_ActorId",
                table: "CharacterByActor",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_ContentId",
                table: "Characters",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_ContentId",
                table: "Episodes",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_SeasonId_Sequence",
                table: "Episodes",
                columns: new[] { "SeasonId", "Sequence" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExternalLinks_AggregatorId",
                table: "ExternalLinks",
                column: "AggregatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalLinks_ContentId",
                table: "ExternalLinks",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalLinks_EpisodeId",
                table: "ExternalLinks",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_ContentId",
                table: "Medias",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Names_ContentId",
                table: "Names",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_ContentId",
                table: "Seasons",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_TagByContent_TagId",
                table: "TagByContent",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ContentId",
                table: "Tags",
                column: "ContentId");

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
                name: "BackGrounds");

            migrationBuilder.DropTable(
                name: "CategoryByContent");

            migrationBuilder.DropTable(
                name: "CharacterByActor");

            migrationBuilder.DropTable(
                name: "Covers");

            migrationBuilder.DropTable(
                name: "ExternalLinks");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DropTable(
                name: "Names");

            migrationBuilder.DropTable(
                name: "TagByContent");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Aggregators");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "Contents");
        }
    }
}
