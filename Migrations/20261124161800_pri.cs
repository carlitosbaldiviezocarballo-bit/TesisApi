using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesisApi.Migrations
{
    /// <inheritdoc />
    public partial class pri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tutor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Especialidad = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tesis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Estudiante = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IdTutor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tesis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tesis_Tutor_IdTutor",
                        column: x => x.IdTutor,
                        principalTable: "Tutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tesis_IdTutor",
                table: "Tesis",
                column: "IdTutor");

            migrationBuilder.CreateIndex(
                name: "IX_Tesis_Titulo",
                table: "Tesis",
                column: "Titulo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tutor_Correo",
                table: "Tutor",
                column: "Correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tutor_Nombre",
                table: "Tutor",
                column: "Nombre",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tesis");

            migrationBuilder.DropTable(
                name: "Tutor");
        }
    }
}
