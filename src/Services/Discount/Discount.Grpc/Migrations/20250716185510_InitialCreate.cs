using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Discount.Grpc.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: ["Id", "Amount", "Description", "ProductName"],
                values: new object[,]
                {
                    { 1, 150.00m, "Discount on iPhone 15 purchase.", "Apple iPhone 15" },
                    { 2, 120.00m, "Save on Samsung Galaxy S24.", "Samsung Galaxy S24" },
                    { 3, 50.00m, "Noise-canceling headphones discount.", "Sony WH-1000XM5" },
                    { 4, 200.00m, "Exclusive Dell XPS 15 discount.", "Dell XPS 15" },
                    { 5, 180.00m, "Special discount on MacBook Air M3.", "Apple MacBook Air M3" },
                    { 6, 20.00m, "Save on premium Logitech mouse.", "Logitech MX Master 3S" },
                    { 7, 250.00m, "Graphics card discount offer.", "Asus ROG Strix RTX 4080" },
                    { 8, 175.00m, "Camera purchase discount.", "Canon EOS R7" },
                    { 9, 15.00m, "Discount on smart speaker.", "Amazon Echo Dot (5th Gen)" },
                    { 10, 30.00m, "Fitness tracker discount.", "Fitbit Charge 6" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupons");
        }
    }
}
