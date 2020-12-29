namespace BLTC.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ShoppingTransationsRelatatedDbModelsCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceiptId",
                table: "OrdersItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShoppingCartId",
                table: "OrdersItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShippingId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    CustomerId = table.Column<string>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipts_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receipts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShippingDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Address = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: false),
                    ZipCode = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdersItems_ReceiptId",
                table: "OrdersItems",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersItems_ShoppingCartId",
                table: "OrdersItems",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingId",
                table: "Orders",
                column: "ShippingId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_CustomerId",
                table: "Receipts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_IsDeleted",
                table: "Receipts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_OrderId",
                table: "Receipts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingDetails_IsDeleted",
                table: "ShippingDetails",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_IsDeleted",
                table: "ShoppingCarts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingDetails_ShippingId",
                table: "Orders",
                column: "ShippingId",
                principalTable: "ShippingDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersItems_Receipts_ReceiptId",
                table: "OrdersItems",
                column: "ReceiptId",
                principalTable: "Receipts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersItems_ShoppingCarts_ShoppingCartId",
                table: "OrdersItems",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingDetails_ShippingId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersItems_Receipts_ReceiptId",
                table: "OrdersItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersItems_ShoppingCarts_ShoppingCartId",
                table: "OrdersItems");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "ShippingDetails");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_OrdersItems_ReceiptId",
                table: "OrdersItems");

            migrationBuilder.DropIndex(
                name: "IX_OrdersItems_ShoppingCartId",
                table: "OrdersItems");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShippingId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ReceiptId",
                table: "OrdersItems");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "OrdersItems");

            migrationBuilder.DropColumn(
                name: "ShippingId",
                table: "Orders");
        }
    }
}
