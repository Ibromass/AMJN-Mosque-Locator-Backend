using System;
using AMJN_Mosque_Locator.AppDbContext;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMJN_Mosque_Locator.Migrations;

[DbContext(typeof(AMJNDbContext))]
[Migration("20261016123000_AddNewsItems")]
public partial class AddNewsItems : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "NewsItems",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Category = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                Title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                DisplayDate = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                Summary = table.Column<string>(type: "character varying(3000)", maxLength: 3000, nullable: false),
                Url = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                Tag = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                WatchUrl = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            },
            constraints: table => table.PrimaryKey("PK_NewsItems", x => x.Id));

        migrationBuilder.CreateIndex(
            name: "IX_NewsItems_Category_DateCreated",
            table: "NewsItems",
            columns: new[] { "Category", "DateCreated" });

        var seededAt = new DateTime(2026, 6, 19, 12, 0, 0, DateTimeKind.Utc);
        migrationBuilder.InsertData(
            table: "NewsItems",
            columns: new[] { "Id", "Category", "Title", "DisplayDate", "Summary", "Url", "Tag", "WatchUrl", "DateCreated", "DateUpdated" },
            values: new object[,]
            {
                {
                    new Guid("c2010010-0000-4000-8000-000000000001"), "Friday Sermon",
                    "The Exemplary Generosity of The Prophet (sa): Lessons from His Life",
                    "June 19, 2026",
                    "His Holiness Hazrat Mirza Masroor Ahmad (aba) continued presenting narrations regarding the Holy Prophet's (sa) unparalleled generosity. The Holy Prophet (sa) would give away all his wealth to those who asked, never turning anyone away empty-handed. He taught that one who refrains from begging and seeks independence through reliance on Allah will be granted freedom by God. This sermon reminds us that true generosity springs from complete trust in the Divine.",
                    "https://www.alislam.org/friday-sermon/", null, "https://www.mta.tv", seededAt, seededAt
                },
                {
                    new Guid("c2010010-0000-4000-8000-000000000002"), "Nigeria",
                    "Ahmadiyya Nigeria Calls for Global Peace Through Love", "March 2026",
                    "The Amir of Ahmadiyya Muslim Jama'at Nigeria, Barrister Azeez Alatoye, delivered an Eid-ul-Fitr message urging members to seek the love of Allah and uphold the dignity of mankind to achieve lasting world peace.",
                    "https://ahmadiyya.ng", "Nigeria", null, seededAt, seededAt
                },
                {
                    new Guid("c2010010-0000-4000-8000-000000000003"), "Nigeria",
                    "Jamia Ahmadiyya Nigeria — Upcoming Academic Session", "2026",
                    "Jamia Ahmadiyya Nigeria continues to train missionaries in Islamic studies, Arabic language and theology. Applications for the new academic session are being processed at the Sagamu campus.",
                    "https://jamiaahmadiyya.ng", "Education", null, seededAt.AddDays(-1), seededAt.AddDays(-1)
                },
                {
                    new Guid("c2010010-0000-4000-8000-000000000004"), "Nigeria",
                    "Majlis Khuddam-ul-Ahmadiyya Nigeria Activities", "2026",
                    "The youth auxiliary of AMJN continues its community service projects across Nigeria, including medical outreach, blood donation drives, and tree-planting campaigns.",
                    "https://www.facebook.com/khuddamnigeria/", "Community", null, seededAt.AddDays(-2), seededAt.AddDays(-2)
                },
                {
                    new Guid("c2010010-0000-4000-8000-000000000005"), "World",
                    "MTA Ghana TV Channel Launched by Khalifa", "2025",
                    "His Holiness Hazrat Mirza Masroor Ahmad (aba) officially launched MTA Ghana TV, extending the reach of Ahmadiyya broadcasting across West Africa with local-language programming.",
                    "https://www.alislam.org/press-release/head-of-ahmadiyya-muslim-community-launches-mta-ghana-tv-channel/", "Media", null, seededAt, seededAt
                },
                {
                    new Guid("c2010010-0000-4000-8000-000000000006"), "World",
                    "Ahmadiyya Community Spans 200+ Nations", "2025",
                    "The Ahmadiyya Muslim Community now operates in over 200 countries with more than 16,000 mosques, 600 schools, and 30 hospitals worldwide under the leadership of Khalifatul Masih V.",
                    "https://www.alislam.org/ahmadiyya-muslim-community/", "Global", null, seededAt.AddDays(-1), seededAt.AddDays(-1)
                },
                {
                    new Guid("c2010010-0000-4000-8000-000000000007"), "World",
                    "Alahmadiyya.org Updates — June 2026", "June 2026",
                    "Multiple Ahmadiyya publications have been updated including translations of the Holy Quran into Spanish, French and Hindi, and the latest issue of The Islamic View magazine.",
                    "https://alahmadiyya.org/updates-at-alahmadiyya-org-in-june-2026/", "Publications", null, seededAt.AddDays(-2), seededAt.AddDays(-2)
                },
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "NewsItems");
    }
}
