using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kodlama.io.Devs.Persistence.Migrations
{
    public partial class mig_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammingLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedName = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammingLanguages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    AuthenticatorType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammingTechnologies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedName = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProgrammingLanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammingTechnologies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgrammingTechnologies_ProgrammingLanguages_ProgrammingLanguageId",
                        column: x => x.ProgrammingLanguageId,
                        principalTable: "ProgrammingLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GithubProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GithubProfileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GithubProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GithubProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RevokedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonRevoked = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OperationClaimId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperationClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOperationClaims_OperationClaims_OperationClaimId",
                        column: x => x.OperationClaimId,
                        principalTable: "OperationClaims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOperationClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Editor" }
                });

            migrationBuilder.InsertData(
                table: "ProgrammingLanguages",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "ModifiedName", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "C#" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Python" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ruby" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "Status" },
                values: new object[,]
                {
                    { 1, 0, "tst@editor.com", "Testeditor", "editor", new byte[] { 81, 172, 79, 189, 115, 38, 7, 38, 70, 114, 198, 48, 111, 164, 95, 56, 30, 28, 255, 174, 175, 198, 201, 164, 154, 145, 133, 61, 19, 82, 38, 84, 215, 18, 100, 11, 164, 66, 246, 41, 86, 152, 111, 181, 57, 219, 187, 130, 182, 191, 0, 81, 102, 152, 163, 116, 45, 188, 190, 115, 34, 82, 104, 99 }, new byte[] { 1, 221, 5, 216, 168, 237, 156, 234, 99, 97, 220, 132, 101, 152, 246, 62, 248, 68, 45, 204, 148, 3, 232, 16, 180, 126, 169, 155, 3, 12, 132, 100, 180, 39, 68, 205, 179, 237, 92, 123, 173, 180, 160, 236, 179, 214, 24, 4, 234, 69, 183, 178, 204, 95, 213, 7, 35, 93, 164, 154, 199, 16, 135, 152, 228, 64, 23, 153, 252, 129, 90, 131, 153, 142, 133, 34, 31, 158, 40, 242, 202, 68, 150, 118, 21, 187, 187, 206, 99, 68, 233, 66, 120, 47, 222, 117, 68, 46, 174, 149, 1, 201, 190, 72, 4, 206, 53, 46, 228, 252, 165, 199, 224, 241, 84, 167, 44, 192, 32, 106, 242, 113, 240, 213, 50, 105, 35, 15 }, true },
                    { 2, 2, "tst@adm.com", "TestAdm", "admin", new byte[] { 227, 210, 9, 160, 165, 176, 53, 67, 28, 188, 186, 239, 184, 116, 28, 135, 186, 232, 122, 80, 9, 93, 152, 213, 234, 42, 36, 88, 19, 201, 2, 98, 1, 113, 45, 96, 126, 39, 30, 188, 253, 32, 231, 20, 199, 168, 157, 197, 10, 129, 21, 100, 240, 238, 186, 216, 176, 100, 237, 55, 118, 102, 106, 8 }, new byte[] { 127, 229, 112, 205, 92, 212, 254, 184, 161, 238, 82, 34, 136, 109, 204, 36, 141, 225, 28, 20, 148, 79, 126, 10, 230, 128, 42, 79, 60, 159, 119, 186, 243, 63, 144, 203, 160, 32, 152, 142, 130, 3, 87, 98, 50, 214, 240, 26, 74, 72, 238, 53, 105, 178, 203, 250, 6, 179, 10, 26, 209, 137, 104, 5, 165, 94, 26, 60, 141, 27, 94, 245, 48, 62, 43, 23, 103, 33, 200, 88, 83, 35, 48, 62, 158, 144, 69, 208, 185, 12, 230, 124, 255, 128, 14, 151, 43, 195, 74, 69, 88, 216, 51, 26, 201, 75, 26, 157, 247, 222, 152, 27, 163, 111, 52, 164, 134, 103, 12, 41, 79, 56, 214, 41, 87, 199, 193, 176 }, true }
                });

            migrationBuilder.InsertData(
                table: "GithubProfiles",
                columns: new[] { "Id", "GithubProfileUrl", "UserId" },
                values: new object[] { 1, "https://github.com/SaruGit75", 1 });

            migrationBuilder.InsertData(
                table: "ProgrammingTechnologies",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "ModifiedName", "Name", "ProgrammingLanguageId" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), ".Net Core", 1 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Django", 2 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "React.js", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GithubProfiles_UserId",
                table: "GithubProfiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammingTechnologies_ProgrammingLanguageId",
                table: "ProgrammingTechnologies",
                column: "ProgrammingLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaims_OperationClaimId",
                table: "UserOperationClaims",
                column: "OperationClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaims_UserId",
                table: "UserOperationClaims",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GithubProfiles");

            migrationBuilder.DropTable(
                name: "ProgrammingTechnologies");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "UserOperationClaims");

            migrationBuilder.DropTable(
                name: "ProgrammingLanguages");

            migrationBuilder.DropTable(
                name: "OperationClaims");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
