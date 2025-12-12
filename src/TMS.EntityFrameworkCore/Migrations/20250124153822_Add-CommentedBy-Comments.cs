using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentedByComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AbpUsers_CommentedById",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CommentedById",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CommentedById",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "CommentedBy",
                table: "Comments",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentedBy",
                table: "Comments");

            migrationBuilder.AddColumn<Guid>(
                name: "CommentedById",
                table: "Comments",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentedById",
                table: "Comments",
                column: "CommentedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AbpUsers_CommentedById",
                table: "Comments",
                column: "CommentedById",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }
    }
}
