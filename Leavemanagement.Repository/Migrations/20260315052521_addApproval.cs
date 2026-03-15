using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Leavemanagement.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addApproval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_leaveRequests_Employees_EmployeeId",
                table: "leaveRequests");

            migrationBuilder.AddColumn<DateTime>(
                name: "AdminApprovalDate",
                table: "leaveRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "leaveRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HRId",
                table: "leaveRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "HRapprovalDate",
                table: "leaveRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "leaveRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ManagerapprovalDate",
                table: "leaveRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Resoan",
                table: "leaveRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "leaveRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_leaveRequests_AdminId",
                table: "leaveRequests",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_leaveRequests_HRId",
                table: "leaveRequests",
                column: "HRId");

            migrationBuilder.CreateIndex(
                name: "IX_leaveRequests_ManagerId",
                table: "leaveRequests",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_leaveRequests_Employees_AdminId",
                table: "leaveRequests",
                column: "AdminId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_leaveRequests_Employees_EmployeeId",
                table: "leaveRequests",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_leaveRequests_Employees_HRId",
                table: "leaveRequests",
                column: "HRId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_leaveRequests_Employees_ManagerId",
                table: "leaveRequests",
                column: "ManagerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_leaveRequests_Employees_AdminId",
                table: "leaveRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_leaveRequests_Employees_EmployeeId",
                table: "leaveRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_leaveRequests_Employees_HRId",
                table: "leaveRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_leaveRequests_Employees_ManagerId",
                table: "leaveRequests");

            migrationBuilder.DropIndex(
                name: "IX_leaveRequests_AdminId",
                table: "leaveRequests");

            migrationBuilder.DropIndex(
                name: "IX_leaveRequests_HRId",
                table: "leaveRequests");

            migrationBuilder.DropIndex(
                name: "IX_leaveRequests_ManagerId",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "AdminApprovalDate",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "HRId",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "HRapprovalDate",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "ManagerapprovalDate",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "Resoan",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "leaveRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_leaveRequests_Employees_EmployeeId",
                table: "leaveRequests",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
