namespace BLTC.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ImageDbModelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Images_AvatarId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_AvatarId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "Authors");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_AuthorId",
                table: "Images",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Authors_AuthorId",
                table: "Images",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Authors_AuthorId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_AuthorId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "AvatarId",
                table: "Authors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_AvatarId",
                table: "Authors",
                column: "AvatarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Images_AvatarId",
                table: "Authors",
                column: "AvatarId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
