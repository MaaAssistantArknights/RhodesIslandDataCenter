using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RIDC.Database.Migrations.Sqlite.Migrations
{
    public partial class AddEnemyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enemies",
                columns: table => new
                {
                    EnemyId = table.Column<string>(type: "TEXT", nullable: false),
                    EnemyIndex = table.Column<string>(type: "TEXT", nullable: true),
                    EnemyTags = table.Column<string>(type: "TEXT", nullable: true),
                    SortId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    EnemyRace = table.Column<string>(type: "TEXT", nullable: true),
                    EnemyLevel = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    AttackType = table.Column<string>(type: "TEXT", nullable: true),
                    Endure = table.Column<string>(type: "TEXT", nullable: true),
                    Attack = table.Column<string>(type: "TEXT", nullable: true),
                    Defence = table.Column<string>(type: "TEXT", nullable: true),
                    Resistance = table.Column<string>(type: "TEXT", nullable: true),
                    Ability = table.Column<string>(type: "TEXT", nullable: true),
                    IsInvalidKilled = table.Column<string>(type: "TEXT", nullable: true),
                    HideInHandbook = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enemies", x => x.EnemyId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enemies");
        }
    }
}
