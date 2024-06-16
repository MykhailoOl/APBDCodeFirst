using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task10.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "doctors",
                columns: table => new
                {
                    IdDoctor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctors", x => x.IdDoctor);
                });

            migrationBuilder.CreateTable(
                name: "medicaments",
                columns: table => new
                {
                    IdMedicament = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicaments", x => x.IdMedicament);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    IdPatient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.IdPatient);
                });

            migrationBuilder.CreateTable(
                name: "prescriptions",
                columns: table => new
                {
                    IdPrescription = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DoctorIdDoctor = table.Column<int>(type: "int", nullable: false),
                    PatientIdPatient = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prescriptions", x => x.IdPrescription);
                    table.ForeignKey(
                        name: "FK_prescriptions_doctors_DoctorIdDoctor",
                        column: x => x.DoctorIdDoctor,
                        principalTable: "doctors",
                        principalColumn: "IdDoctor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_prescriptions_patients_PatientIdPatient",
                        column: x => x.PatientIdPatient,
                        principalTable: "patients",
                        principalColumn: "IdPatient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prescription_medicaments",
                columns: table => new
                {
                    IdMedicament = table.Column<int>(type: "int", nullable: false),
                    IdPrescription = table.Column<int>(type: "int", nullable: false),
                    Dose = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prescription_medicaments", x => new { x.IdMedicament, x.IdPrescription });
                    table.ForeignKey(
                        name: "FK_prescription_medicaments_medicaments_IdMedicament",
                        column: x => x.IdMedicament,
                        principalTable: "medicaments",
                        principalColumn: "IdMedicament",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_prescription_medicaments_prescriptions_IdPrescription",
                        column: x => x.IdPrescription,
                        principalTable: "prescriptions",
                        principalColumn: "IdPrescription",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_prescription_medicaments_IdPrescription",
                table: "prescription_medicaments",
                column: "IdPrescription");

            migrationBuilder.CreateIndex(
                name: "IX_prescriptions_DoctorIdDoctor",
                table: "prescriptions",
                column: "DoctorIdDoctor");

            migrationBuilder.CreateIndex(
                name: "IX_prescriptions_PatientIdPatient",
                table: "prescriptions",
                column: "PatientIdPatient");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "prescription_medicaments");

            migrationBuilder.DropTable(
                name: "medicaments");

            migrationBuilder.DropTable(
                name: "prescriptions");

            migrationBuilder.DropTable(
                name: "doctors");

            migrationBuilder.DropTable(
                name: "patients");
        }
    }
}
