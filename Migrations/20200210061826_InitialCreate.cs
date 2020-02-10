using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "films",
                columns: table => new
                {
                    filmId = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    filmName = table.Column<string>(type: "varchar(100)", maxLength: 50, nullable: false),
                    duration = table.Column<int>(type: "int(5)", nullable: false),
                    category = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    language = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    director = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_films", x => x.filmId);
                });

            migrationBuilder.CreateTable(
                name: "houses",
                columns: table => new
                {
                    houseId = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    houseRow = table.Column<int>(type: "int(10)", nullable: false),
                    houseColumn = table.Column<int>(type: "int(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_houses", x => x.houseId);
                });

            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true),
                    isComplete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    password = table.Column<string>(type: "varchar(100)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "broadcasts",
                columns: table => new
                {
                    broadcastId = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    dates = table.Column<DateTime>(type: "date", nullable: false),
                    filmId = table.Column<int>(type: "int(10)", nullable: false),
                    houseId = table.Column<int>(type: "int(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_broadcasts", x => x.broadcastId);
                    table.ForeignKey(
                        name: "FK_broadcasts_films_filmId",
                        column: x => x.filmId,
                        principalTable: "films",
                        principalColumn: "filmId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_broadcasts_houses_houseId",
                        column: x => x.houseId,
                        principalTable: "houses",
                        principalColumn: "houseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    commentId = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<int>(type: "int(10)", nullable: false),
                    filmId = table.Column<int>(type: "int(10)", nullable: false),
                    comment = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.commentId);
                    table.ForeignKey(
                        name: "FK_comments_films_filmId",
                        column: x => x.filmId,
                        principalTable: "films",
                        principalColumn: "filmId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comments_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    ticketId = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    seatNo = table.Column<int>(type: "int(5)", nullable: false),
                    seatName = table.Column<string>(type: "varchar(10)", nullable: false),
                    broadcastId = table.Column<int>(type: "int(10)", nullable: false),
                    valid = table.Column<bool>(type: "boolean", nullable: false),
                    userId = table.Column<int>(type: "int(10)", nullable: false),
                    ticketType = table.Column<string>(type: "enum('student', 'adult')", nullable: false),
                    ticketFee = table.Column<int>(type: "int(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.ticketId);
                    table.ForeignKey(
                        name: "FK_tickets_broadcasts_broadcastId",
                        column: x => x.broadcastId,
                        principalTable: "broadcasts",
                        principalColumn: "broadcastId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tickets_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_broadcasts_filmId",
                table: "broadcasts",
                column: "filmId");

            migrationBuilder.CreateIndex(
                name: "IX_broadcasts_houseId",
                table: "broadcasts",
                column: "houseId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_filmId",
                table: "comments",
                column: "filmId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_userId",
                table: "comments",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_broadcastId",
                table: "tickets",
                column: "broadcastId");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_userId",
                table: "tickets",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "TodoItems");

            migrationBuilder.DropTable(
                name: "broadcasts");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "films");

            migrationBuilder.DropTable(
                name: "houses");
        }
    }
}
