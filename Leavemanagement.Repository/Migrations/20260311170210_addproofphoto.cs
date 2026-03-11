using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Leavemanagement.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addproofphoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProofPhoto",
                table: "ProofMappings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProofPhoto",
                table: "ProofMappings");
        }
    }
}
