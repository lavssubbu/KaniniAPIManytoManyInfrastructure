using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIMMwithoutJunctionModel.Migrations
{
    /// <inheritdoc />
    public partial class initcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DocId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DocId);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatId);
                });

            migrationBuilder.CreateTable(
                name: "DocPatient",
                columns: table => new
                {
                    DoctorsDocId = table.Column<int>(type: "int", nullable: false),
                    PatientsPatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocPatient", x => new { x.DoctorsDocId, x.PatientsPatId });
                    table.ForeignKey(
                        name: "FK_DocPatient_Doctors_DoctorsDocId",
                        column: x => x.DoctorsDocId,
                        principalTable: "Doctors",
                        principalColumn: "DocId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocPatient_Patients_PatientsPatId",
                        column: x => x.PatientsPatId,
                        principalTable: "Patients",
                        principalColumn: "PatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "DocId", "DocName", "Specialization" },
                values: new object[] { 1, "Prem", "Cardio" });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatId", "PatName" },
                values: new object[] { 11, "Liya" });

            migrationBuilder.CreateIndex(
                name: "IX_DocPatient_PatientsPatId",
                table: "DocPatient",
                column: "PatientsPatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocPatient");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
