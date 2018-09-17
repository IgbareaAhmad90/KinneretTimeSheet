using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KinneretCollegeTimeSheet.Data.Migrations
{
    public partial class UpdateTableReports1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_userCourse_UserCourseId",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Report");

            migrationBuilder.AlterColumn<int>(
                name: "UserCourseId",
                table: "Report",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_userCourse_UserCourseId",
                table: "Report",
                column: "UserCourseId",
                principalTable: "userCourse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_userCourse_UserCourseId",
                table: "Report");

            migrationBuilder.AlterColumn<int>(
                name: "UserCourseId",
                table: "Report",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Report",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_userCourse_UserCourseId",
                table: "Report",
                column: "UserCourseId",
                principalTable: "userCourse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
