using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS.Migrations
{
    /// <inheritdoc />
    public partial class SetDetailType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Comments_Detail",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "Detail",
                table: "Comments",
                type: "character varying(65000)",
                maxLength: 65000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Detail",
                table: "Comments",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(65000)",
                oldMaxLength: 65000);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Detail",
                table: "Comments",
                column: "Detail");
        }
    }
}
