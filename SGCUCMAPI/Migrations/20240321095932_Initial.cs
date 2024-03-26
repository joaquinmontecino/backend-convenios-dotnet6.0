using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGCUCMAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CONVENIO_HISTORIAL",
                columns: table => new
                {
                    ID_CAMBIO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_CONVENIO = table.Column<int>(type: "int", nullable: false),
                    ID_UNIDAD_GESTORA = table.Column<int>(type: "int", nullable: false),
                    NOMBRE_CONV = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TIPO_CONV = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MOVILIDAD = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    VIGENCIA = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ANO_FIRMA = table.Column<int>(type: "int", nullable: false),
                    TIPO_FIRMA = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CUPOS = table.Column<int>(type: "int", nullable: false),
                    DOCUMENTOS = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CONDICION_RENOVACION = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ESTATUS = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FECHA_INICIO = table.Column<DateTime>(type: "date", nullable: false),
                    FECHA_TERMINO = table.Column<DateTime>(type: "date", nullable: false),
                    ACCION = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    FECHA_CAMBIO = table.Column<DateTime>(type: "datetime", nullable: false),
                    USUARIO_CAMBIO = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONVENIO_HISTORIAL", x => x.ID_CAMBIO);
                });

            migrationBuilder.CreateTable(
                name: "COORDINADOR_HISTORIAL",
                columns: table => new
                {
                    ID_CAMBIO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_COORDINADOR = table.Column<int>(type: "int", nullable: false),
                    ID_INSTITUCION = table.Column<int>(type: "int", nullable: false),
                    TIPO = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NOMBRE = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CORREO = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ACCION = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    FECHA_CAMBIO = table.Column<DateTime>(type: "datetime", nullable: false),
                    USUARIO_CAMBIO = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COORDINADOR_HISTORIAL", x => x.ID_CAMBIO);
                });

            migrationBuilder.CreateTable(
                name: "INSTITUCION",
                columns: table => new
                {
                    ID_INSTITUCION = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE_INST = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PAIS = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ALCANCE = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TIPO_INSTITUCION = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INSTITUCION", x => x.ID_INSTITUCION);
                });

            migrationBuilder.CreateTable(
                name: "INSTITUCION_HISTORIAL",
                columns: table => new
                {
                    ID_CAMBIO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_INSTITUCION = table.Column<int>(type: "int", nullable: false),
                    NOMBRE_INST = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PAIS = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ALCANCE = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TIPO_INSTITUCION = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ACCION = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    FECHA_CAMBIO = table.Column<DateTime>(type: "datetime", nullable: false),
                    USUARIO_CAMBIO = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INSTITUCION_HISTORIAL", x => x.ID_CAMBIO);
                });

            migrationBuilder.CreateTable(
                name: "UNIDAD_GESTORA_HISTORIAL",
                columns: table => new
                {
                    ID_CAMBIO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_UNIDAD_GESTORA = table.Column<int>(type: "int", nullable: false),
                    ID_INSTITUCION = table.Column<int>(type: "int", nullable: false),
                    NOMBRE_UNIDAD = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ACCION = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    FECHA_CAMBIO = table.Column<DateTime>(type: "datetime", nullable: false),
                    USUARIO_CAMBIO = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UNIDAD_HISTORIAL", x => x.ID_CAMBIO);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID_USUARIO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EMAIL = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CONTRASENA = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NOMBRE = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    APELLIDO = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    VIGENCIA = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    PRIVILEGIOS = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID_USUARIO);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO_HISTORIAL",
                columns: table => new
                {
                    ID_CAMBIO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_USUARIO = table.Column<int>(type: "int", nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CONTRASENA = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NOMBRE = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    APELLIDO = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    VIGENCIA = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    PRIVILEGIOS = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    ACCION = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    FECHA_CAMBIO = table.Column<DateTime>(type: "datetime", nullable: false),
                    USUARIO_CAMBIO = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_HISTORIAL", x => x.ID_CAMBIO);
                });

            migrationBuilder.CreateTable(
                name: "COORDINADOR",
                columns: table => new
                {
                    ID_COORDINADOR = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_INSTITUCION = table.Column<int>(type: "int", nullable: false),
                    TIPO = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NOMBRE = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CORREO = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COORDINADOR", x => x.ID_COORDINADOR);
                    table.ForeignKey(
                        name: "FK_COORDINADOR_INSTITUCION",
                        column: x => x.ID_INSTITUCION,
                        principalTable: "INSTITUCION",
                        principalColumn: "ID_INSTITUCION",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UNIDAD_GESTORA",
                columns: table => new
                {
                    ID_UNIDAD_GESTORA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_INSTITUCION = table.Column<int>(type: "int", nullable: false),
                    NOMBRE_UNIDAD = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UNIDADGESTORA", x => x.ID_UNIDAD_GESTORA);
                    table.ForeignKey(
                        name: "FK_UNIDAD_INSTITUCION",
                        column: x => x.ID_INSTITUCION,
                        principalTable: "INSTITUCION",
                        principalColumn: "ID_INSTITUCION",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CONVENIO",
                columns: table => new
                {
                    ID_CONVENIO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_UNIDAD_GESTORA = table.Column<int>(type: "int", nullable: false),
                    NOMBRE_CONV = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TIPO_CONV = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MOVILIDAD = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    VIGENCIA = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ANO_FIRMA = table.Column<int>(type: "int", nullable: false),
                    TIPO_FIRMA = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CUPOS = table.Column<int>(type: "int", nullable: false),
                    DOCUMENTOS = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CONDICION_RENOVACION = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ESTATUS = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FECHA_INICIO = table.Column<DateTime>(type: "date", nullable: false),
                    FECHA_TERMINO = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONVENIO", x => x.ID_CONVENIO);
                    table.ForeignKey(
                        name: "FK_CONVENIO_UNIDADGESTORA",
                        column: x => x.ID_UNIDAD_GESTORA,
                        principalTable: "UNIDAD_GESTORA",
                        principalColumn: "ID_UNIDAD_GESTORA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RELACION_CONVENIO_COORDINADOR",
                columns: table => new
                {
                    ID_RELACION_CONV_COORD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_CONVENIO = table.Column<int>(type: "int", nullable: false),
                    ID_COORDINADOR = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RELACION", x => x.ID_RELACION_CONV_COORD);
                    table.ForeignKey(
                        name: "FK_RELACION_CONVENIO",
                        column: x => x.ID_CONVENIO,
                        principalTable: "CONVENIO",
                        principalColumn: "ID_CONVENIO");
                    table.ForeignKey(
                        name: "FK_RELACION_COORDINADOR",
                        column: x => x.ID_COORDINADOR,
                        principalTable: "COORDINADOR",
                        principalColumn: "ID_COORDINADOR");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CONVENIO_ID_UNIDAD_GESTORA",
                table: "CONVENIO",
                column: "ID_UNIDAD_GESTORA");

            migrationBuilder.CreateIndex(
                name: "IX_COORDINADOR_ID_INSTITUCION",
                table: "COORDINADOR",
                column: "ID_INSTITUCION");

            migrationBuilder.CreateIndex(
                name: "IX_RELACION_CONVENIO_COORDINADOR_ID_CONVENIO",
                table: "RELACION_CONVENIO_COORDINADOR",
                column: "ID_CONVENIO");

            migrationBuilder.CreateIndex(
                name: "IX_RELACION_CONVENIO_COORDINADOR_ID_COORDINADOR",
                table: "RELACION_CONVENIO_COORDINADOR",
                column: "ID_COORDINADOR");

            migrationBuilder.CreateIndex(
                name: "IX_UNIDAD_GESTORA_ID_INSTITUCION",
                table: "UNIDAD_GESTORA",
                column: "ID_INSTITUCION");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CONVENIO_HISTORIAL");

            migrationBuilder.DropTable(
                name: "COORDINADOR_HISTORIAL");

            migrationBuilder.DropTable(
                name: "INSTITUCION_HISTORIAL");

            migrationBuilder.DropTable(
                name: "RELACION_CONVENIO_COORDINADOR");

            migrationBuilder.DropTable(
                name: "UNIDAD_GESTORA_HISTORIAL");

            migrationBuilder.DropTable(
                name: "USUARIO");

            migrationBuilder.DropTable(
                name: "USUARIO_HISTORIAL");

            migrationBuilder.DropTable(
                name: "CONVENIO");

            migrationBuilder.DropTable(
                name: "COORDINADOR");

            migrationBuilder.DropTable(
                name: "UNIDAD_GESTORA");

            migrationBuilder.DropTable(
                name: "INSTITUCION");
        }
    }
}
