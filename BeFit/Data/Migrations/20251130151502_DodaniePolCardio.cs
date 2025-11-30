using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeFit.Data.Migrations
{
    
    public partial class DodaniePolCardio : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tempo",
                table: "CwiczeniaWSesji",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZakresTetna",
                table: "CwiczeniaWSesji",
                type: "TEXT",
                nullable: true);
        }

        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tempo",
                table: "CwiczeniaWSesji");

            migrationBuilder.DropColumn(
                name: "ZakresTetna",
                table: "CwiczeniaWSesji");
        }
    }
}
