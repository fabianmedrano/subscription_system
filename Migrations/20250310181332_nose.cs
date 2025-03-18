using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace subscription_system.Migrations
{
    /// <inheritdoc />
    public partial class nose : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminPlanHistoryViewModel_Plan_PlanId",
                table: "AdminPlanHistoryViewModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_AspNetUsers_UserId",
                table: "Subscriptions");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Subscriptions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "AdminFeatureVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AdminPlanCreateVMId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminFeatureVM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminFeatureVM_AdminPlanCreateViewModel_AdminPlanCreateVMId",
                        column: x => x.AdminPlanCreateVMId,
                        principalTable: "AdminPlanCreateViewModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminFeatureVM_AdminPlanCreateVMId",
                table: "AdminFeatureVM",
                column: "AdminPlanCreateVMId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminPlanHistoryViewModel_AdminPlanCreateViewModel_PlanId",
                table: "AdminPlanHistoryViewModel",
                column: "PlanId",
                principalTable: "AdminPlanCreateViewModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_AspNetUsers_UserId",
                table: "Subscriptions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminPlanHistoryViewModel_AdminPlanCreateViewModel_PlanId",
                table: "AdminPlanHistoryViewModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_AspNetUsers_UserId",
                table: "Subscriptions");

            migrationBuilder.DropTable(
                name: "AdminFeatureVM");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Subscriptions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AdminPlanHistoryViewModel_Plan_PlanId",
                table: "AdminPlanHistoryViewModel",
                column: "PlanId",
                principalTable: "Plan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_AspNetUsers_UserId",
                table: "Subscriptions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
