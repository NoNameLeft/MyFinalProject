namespace BLTC.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddOverviewColumnToManufacturerDbModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Overview",
                table: "Manufacturers",
                nullable: false,
                defaultValue: string.Empty);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Overview",
                table: "Manufacturers");
        }
    }
}
