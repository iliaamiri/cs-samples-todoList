using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todoList.Migrations
{
    public partial class TodoItemsNewColumnDifficultyLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DifficultyLevel",
                table: "TodoItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DifficultyLevel",
                table: "TodoItems");
        }
    }
}
