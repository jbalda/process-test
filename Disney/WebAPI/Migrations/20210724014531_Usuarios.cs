using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class Usuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeliculaSerie_Genero_GeneroId",
                table: "PeliculaSerie");

            migrationBuilder.DropForeignKey(
                name: "FK_PeliculaSeriePersonaje_Personaje_PersonajesId",
                table: "PeliculaSeriePersonaje");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Personaje",
                table: "Personaje");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genero",
                table: "Genero");

            migrationBuilder.RenameTable(
                name: "Personaje",
                newName: "Personajes");

            migrationBuilder.RenameTable(
                name: "Genero",
                newName: "Generos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Personajes",
                table: "Personajes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Generos",
                table: "Generos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculaSerie_Generos_GeneroId",
                table: "PeliculaSerie",
                column: "GeneroId",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculaSeriePersonaje_Personajes_PersonajesId",
                table: "PeliculaSeriePersonaje",
                column: "PersonajesId",
                principalTable: "Personajes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeliculaSerie_Generos_GeneroId",
                table: "PeliculaSerie");

            migrationBuilder.DropForeignKey(
                name: "FK_PeliculaSeriePersonaje_Personajes_PersonajesId",
                table: "PeliculaSeriePersonaje");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Personajes",
                table: "Personajes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Generos",
                table: "Generos");

            migrationBuilder.RenameTable(
                name: "Personajes",
                newName: "Personaje");

            migrationBuilder.RenameTable(
                name: "Generos",
                newName: "Genero");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Personaje",
                table: "Personaje",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genero",
                table: "Genero",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculaSerie_Genero_GeneroId",
                table: "PeliculaSerie",
                column: "GeneroId",
                principalTable: "Genero",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculaSeriePersonaje_Personaje_PersonajesId",
                table: "PeliculaSeriePersonaje",
                column: "PersonajesId",
                principalTable: "Personaje",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
