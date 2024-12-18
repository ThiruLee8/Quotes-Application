using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Quotes.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "QuoteStages",
                columns: new[] { "QuoteStageId", "IsActive", "QuoteStageName" },
                values: new object[,]
                {
                    { 1, true, "Created" },
                    { 2, true, "Approved Validation" },
                    { 3, true, "Rejected Validation" },
                    { 4, true, "Approved" },
                    { 5, true, "Rejected" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "QuoteStages",
                keyColumn: "QuoteStageId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "QuoteStages",
                keyColumn: "QuoteStageId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "QuoteStages",
                keyColumn: "QuoteStageId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "QuoteStages",
                keyColumn: "QuoteStageId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "QuoteStages",
                keyColumn: "QuoteStageId",
                keyValue: 5);
        }
    }
}
