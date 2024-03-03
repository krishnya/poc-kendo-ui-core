using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IFMAMVCDemo.Migrations
{
    /// <inheritdoc />
    public partial class additionalMemberfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BloodGroup",
                table: "Members",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Members",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Members",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "Members",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MembershipId",
                table: "Members",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PanNumber",
                table: "Members",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PinCode",
                table: "Members",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Members",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodGroup",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MembershipId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "PanNumber",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "PinCode",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Members");
        }
    }
}
