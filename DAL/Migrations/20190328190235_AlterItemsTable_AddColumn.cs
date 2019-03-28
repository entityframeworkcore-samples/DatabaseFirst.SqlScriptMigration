using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.JecaestevezApp.Migrations
{
    public partial class AlterItemsTable_AddColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER TABLE [dbo].[Items]  ADD IsEnable bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER TABLE [dbo].[Items]  DROP IsEnable");
        }
    }
}
