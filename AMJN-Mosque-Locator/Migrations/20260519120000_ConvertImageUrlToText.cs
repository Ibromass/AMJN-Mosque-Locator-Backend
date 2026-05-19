using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMJN_Mosque_Locator.Migrations
{
    /// <inheritdoc />
    public partial class ConvertImageUrlToText : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE "Mosques"
                ALTER COLUMN "ImageUrl" TYPE text
                USING "ImageUrl"::text;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE "Mosques"
                ALTER COLUMN "ImageUrl" TYPE jsonb
                USING to_jsonb("ImageUrl");
                """);
        }
    }
}
