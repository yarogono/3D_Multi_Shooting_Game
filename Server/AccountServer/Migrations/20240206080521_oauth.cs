using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace AccountServer.Migrations
{
    /// <inheritdoc />
    public partial class oauth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredAt",
                table: "Account",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Oauth",
                columns: table => new
                {
                    OauthId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    OauthToken = table.Column<string>(type: "longtext", nullable: false),
                    OauthType = table.Column<int>(type: "int", nullable: false),
                    accountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oauth", x => x.OauthId);
                    table.ForeignKey(
                        name: "FK_Oauth_Account_accountId",
                        column: x => x.accountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Oauth_accountId",
                table: "Oauth",
                column: "accountId");

            migrationBuilder.CreateIndex(
                name: "IX_Oauth_OauthId",
                table: "Oauth",
                column: "OauthId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Oauth");

            migrationBuilder.DropColumn(
                name: "ExpiredAt",
                table: "Account");
        }
    }
}
