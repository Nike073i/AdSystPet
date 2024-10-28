using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdSyst.AuthService.SqlServerMigrations.UserDataMigrations
{
    /// <inheritdoc />
    public partial class AddRolesUserDataDbMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string clientRoleId = "9c27c90c-d730-4822-bac1-06968fac690c";
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "544e9a99-77dc-44cb-95c2-7a3c50445acb", null, "System", "SYSTEM" },
                    { "6c44af13-1186-4ded-a05a-e18f9f1be6ed", null, "Moderator", "MODERATOR" },
                    { clientRoleId, null, "Client", "CLIENT" },
                    { "9f414ada-0a4b-4d90-aec5-21fce9109d8b", null, "Editor", "EDITOR" }
                }
            );

            // Установка роли "Клиент" для всех пользователей без каких-либо ролей
            migrationBuilder.Sql(
                @$"
                INSERT INTO [AspNetUserRoles] ([UserId], [RoleId])
                SELECT [Id], '{clientRoleId}'
                FROM [AspNetUsers]
                WHERE [Id] NOT IN (SELECT DISTINCT [UserId] FROM [AspNetUserRoles])
                "
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "544e9a99-77dc-44cb-95c2-7a3c50445acb"
            );

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c44af13-1186-4ded-a05a-e18f9f1be6ed"
            );

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c27c90c-d730-4822-bac1-06968fac690c"
            );

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f414ada-0a4b-4d90-aec5-21fce9109d8b"
            );
        }
    }
}
