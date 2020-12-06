using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinic.Migrations
{
    public partial class n2nExam_Medicnine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Examinations",
                columns: table => new
                {
                    ExaminationId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppointmentId = table.Column<int>(nullable: false),
                    Diagnose = table.Column<string>(nullable: true),
                    Symptom = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examinations", x => x.ExaminationId);
                    table.ForeignKey(
                        name: "FK_Examinations_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "AppointmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Examinations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prescriptionDetails",
                columns: table => new
                {
                    ExaminationId = table.Column<int>(nullable: false),
                    MedicineId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Instruction = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prescriptionDetails", x => new { x.ExaminationId, x.MedicineId });
                    table.ForeignKey(
                        name: "FK_prescriptionDetails_Examinations_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examinations",
                        principalColumn: "ExaminationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_prescriptionDetails_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "MedicineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_AppointmentId",
                table: "Examinations",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_EmployeeId",
                table: "Examinations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_prescriptionDetails_MedicineId",
                table: "prescriptionDetails",
                column: "MedicineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "prescriptionDetails");

            migrationBuilder.DropTable(
                name: "Examinations");
        }
    }
}
