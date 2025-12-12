using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS.Migrations
{
    /// <inheritdoc />
    public partial class SetDetailLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Detail",
                table: "Comments",
                type: "text",
                maxLength: 650000000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(65000)",
                oldMaxLength: 65000);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Detail",
                table: "Comments",
                type: "character varying(65000)",
                maxLength: 65000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldMaxLength: 650000000);
        }
    }
}
