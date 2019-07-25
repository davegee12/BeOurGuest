using Microsoft.EntityFrameworkCore.Migrations;

namespace BeOurGuest.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Characters_ChosenCharacterCharacterId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "ChosenCharacterCharacterId",
                table: "Users",
                newName: "TopCharacterCharacterId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_ChosenCharacterCharacterId",
                table: "Users",
                newName: "IX_Users_TopCharacterCharacterId");

            migrationBuilder.AddColumn<string>(
                name: "SecondCharacter",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThirdCharacter",
                table: "Users",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Characters_TopCharacterCharacterId",
                table: "Users",
                column: "TopCharacterCharacterId",
                principalTable: "Characters",
                principalColumn: "CharacterId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Characters_TopCharacterCharacterId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SecondCharacter",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ThirdCharacter",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "TopCharacterCharacterId",
                table: "Users",
                newName: "ChosenCharacterCharacterId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_TopCharacterCharacterId",
                table: "Users",
                newName: "IX_Users_ChosenCharacterCharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Characters_ChosenCharacterCharacterId",
                table: "Users",
                column: "ChosenCharacterCharacterId",
                principalTable: "Characters",
                principalColumn: "CharacterId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
