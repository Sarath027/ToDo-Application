using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApp.Repository.Migrations
{
    /// <inheritdoc />
    public partial class azure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Status__C8EE2063956ED592", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "TaskInfo",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TaskTabl__7C6949B1708A6B36", x => x.TaskId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CC4CD18FFE6E", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserTasks",
                columns: table => new
                {
                    UserTaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    TaskId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CompletedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserTask__4EF5961FC10484ED", x => x.UserTaskId);
                    table.ForeignKey(
                        name: "FK__UserTasks__Statu__3F466844",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId");
                    table.ForeignKey(
                        name: "FK__UserTasks__TaskI__3E52440B",
                        column: x => x.TaskId,
                        principalTable: "TaskInfo",
                        principalColumn: "TaskId");
                    table.ForeignKey(
                        name: "FK__UserTasks__UserI__3D5E1FD2",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTasks_StatusId",
                table: "UserTasks",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTasks_TaskId",
                table: "UserTasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTasks_UserId",
                table: "UserTasks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTasks");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "TaskInfo");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
