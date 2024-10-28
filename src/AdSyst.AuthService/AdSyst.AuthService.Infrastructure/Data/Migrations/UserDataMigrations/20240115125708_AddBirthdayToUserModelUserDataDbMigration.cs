using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdSyst.AuthService.SqlServerMigrations.UserDataMigrations
{
    /// <inheritdoc />
    public partial class AddBirthdayToUserModelUserDataDbMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Добавляем колонку с днем рождения
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Birthday",
                table: "AspNetUsers",
                type: "datetimeoffset",
                nullable: false,
                // Ставим дефолтное значение на случай, если в таблице уже есть запииси
                defaultValue: new DateTimeOffset(2001, 1, 1, 0, 0, 0, new TimeSpan(0, 0, 0))
            );

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Birthday",
                table: "AspNetUsers",
                type: "datetimeoffset",
                nullable: false,
                // Убираем дефолтное значение колонки. День рождения должен быть указан обязательно
                defaultValue: null
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Birthday", table: "AspNetUsers");
        }
    }
}
