using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SGCUCMAPI.Data;
using SGCUCMAPI.Models;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using SGCUCMAPI.Utilities;
using System.Text.Json;



namespace SGCUCMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvenioController : ControllerBase
    {
        private readonly DataContext _context;
        public ConvenioController(DataContext context)
        {
            _context = context;
        }

        


        [HttpGet("{id?}")]
        public async Task<ActionResult<List<ConvenioGetResponse>>> GetConvenios(int? id = null)
        {
            IQueryable<Convenio> conveniosQuery = _context.Convenios;

            if (id.HasValue)
            {
                conveniosQuery = conveniosQuery.Where(c => c.IdConvenio == id.Value);
            }

            var convenios = await conveniosQuery
                .Select(c => new ConvenioGetResponse
                {
                    ID_Convenio = c.IdConvenio,
                    Nombre_Convenio = c.NombreConvenio,
                    Tipo_Convenio = c.TipoConvenio,
                    Movilidad = c.Movilidad,
                    Vigencia = c.Vigencia,
                    Anio_Firma = c.AnioFirma,
                    Tipo_Firma = c.TipoFirma,
                    Cupos = c.Cupos,
                    Documentos = c.Documentos,
                    Condicion_Renovacion = c.CondicionRenovacion,
                    Estatus = c.Estatus,
                    Fecha_Inicio = c.FechaInicio.ToString("dd/MM/yyyy"),
                    Fecha_Termino = c.FechaTermino.ToString("dd/MM/yyyy"),
                    ID_Unidad_Gestora = c.IdUnidadGestora,
                })
                .ToListAsync();

            if (!convenios.Any())
            {
                return NotFound("No se encontraron convenios.");
            }

            foreach (var convenio in convenios)
            {
                var unidadGestora = await _context.UnidadesGestoras
                    .Where(ug => ug.IdUnidadGestora == convenio.ID_Unidad_Gestora)
                    .Select(ug => new
                    {
                        ID_Institucion = ug.IdInstitucion,
                        Nombre_Unidad_Gestora = ug.NombreUnidad,
                    })
                    .FirstOrDefaultAsync();

                if (unidadGestora == null)
                {
                    return NotFound($"No se encontró la unidad gestora asociada al convenio {convenio.ID_Convenio}.");
                }

                convenio.ID_Institucion = unidadGestora.ID_Institucion;
                convenio.Nombre_Unidad_Gestora = unidadGestora.Nombre_Unidad_Gestora;

                var institucion = await _context.Instituciones
                    .Where(i => i.IdInstitucion == unidadGestora.ID_Institucion)
                    .Select(i => new
                    {
                        i.NombreInstitucion,
                        i.Pais,
                        i.Alcance,
                        i.TipoInstitucion
                    })
                    .FirstOrDefaultAsync();

                if (institucion == null)
                {
                    return NotFound($"No se encontró la institución asociada al convenio {convenio.ID_Convenio}.");
                }

                convenio.Nombre_Institucion = institucion.NombreInstitucion;
                convenio.Pais = institucion.Pais;
                convenio.Alcance = institucion.Alcance;
                convenio.Tipo_Institucion = institucion.TipoInstitucion;

                var coordinadores = await _context.RelacionesConvenioCoordinador
                    .Where(rcc => rcc.IdConvenio == convenio.ID_Convenio)
                    .Join(_context.Coordinadores,
                          rcc => rcc.IdCoordinador,
                          c => c.IdCoordinador,
                          (rcc, c) => new {c.IdCoordinador, c.Tipo, c.Nombre, c.Correo })
                    .ToListAsync();

                foreach (var coordinador in coordinadores)
                {
                    if (coordinador.Tipo == "Externo")
                    {
                        convenio.ID_Coordinador_Externo = coordinador.IdCoordinador;
                        convenio.Tipo_Coordinador_Externo = coordinador.Tipo;
                        convenio.Nombre_Coordinador_Externo = coordinador.Nombre;
                        convenio.Correo_Coordinador_Externo = coordinador.Correo;
                    }
                    else if (coordinador.Tipo == "Interno")
                    {
                        convenio.ID_Coordinador_Interno = coordinador.IdCoordinador;
                        convenio.Tipo_Coordinador_Interno = coordinador.Tipo;
                        convenio.Nombre_Coordinador_Interno = coordinador.Nombre;
                        convenio.Correo_Coordinador_Interno = coordinador.Correo;
                    }
                }
            }
            return Ok(convenios);
        }

        [HttpPost]
        public async Task<ActionResult<List<Convenio>>> CreateConvenio(ConvenioPostRequest convenioRequest)
        {
            try
            {
                var convenio = new Convenio
                {
                    IdUnidadGestora = convenioRequest.IdUnidadGestora,
                    NombreConvenio = convenioRequest.NombreConv,
                    TipoConvenio = convenioRequest.TipoConv,
                    Movilidad = convenioRequest.Movilidad,
                    Vigencia = convenioRequest.Vigencia,
                    AnioFirma = convenioRequest.AnioFirma,
                    TipoFirma = convenioRequest.TipoFirma,
                    Cupos = convenioRequest.Cupos,
                    Documentos = convenioRequest.Documentos,
                    CondicionRenovacion = convenioRequest.CondicionRenovacion,
                    Estatus = convenioRequest.Estatus,
                    FechaInicio = DateTime.ParseExact(convenioRequest.FechaInicio, "dd/MM/yyyy", null),
                    FechaTermino = DateTime.ParseExact(convenioRequest.FechaTermino, "dd/MM/yyyy", null)
                };

                _context.Convenios.Add(convenio);
                await _context.SaveChangesAsync();

                var relacionCoordinadorInterno = new RelacionConvenioCoordinador
                {
                    IdConvenio = convenio.IdConvenio,
                    IdCoordinador = convenioRequest.IdCoordinadorInterno
                };
                var relacionCoordinadorExterno = new RelacionConvenioCoordinador
                {
                    IdConvenio = convenio.IdConvenio,
                    IdCoordinador = convenioRequest.IdCoordinadorExterno
                };

                _context.RelacionesConvenioCoordinador.Add(relacionCoordinadorInterno);
                _context.RelacionesConvenioCoordinador.Add(relacionCoordinadorExterno);
                await _context.SaveChangesAsync();

                return Ok(convenio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Convenio>>> UpdateConvenio(int id, ConvenioPostRequest convenioRequest)
        {
            var dbConvenio = await _context.Convenios.FindAsync(id);
            if (dbConvenio == null)
            {
                return BadRequest("Convenio no encontrado");
            }

            dbConvenio.IdUnidadGestora = convenioRequest.IdUnidadGestora;
            dbConvenio.NombreConvenio = convenioRequest.NombreConv;
            dbConvenio.TipoConvenio = convenioRequest.TipoConv;
            dbConvenio.Movilidad = convenioRequest.Movilidad;
            dbConvenio.Vigencia = convenioRequest.Vigencia;
            dbConvenio.AnioFirma = convenioRequest.AnioFirma;
            dbConvenio.TipoFirma = convenioRequest.TipoFirma;
            dbConvenio.Cupos = convenioRequest.Cupos;
            dbConvenio.Documentos = convenioRequest.Documentos;
            dbConvenio.CondicionRenovacion = convenioRequest.CondicionRenovacion;
            dbConvenio.Estatus = convenioRequest.Estatus;
            dbConvenio.FechaInicio = DateTime.ParseExact(convenioRequest.FechaInicio, "dd/MM/yyyy", null);
            dbConvenio.FechaTermino = DateTime.ParseExact(convenioRequest.FechaTermino, "dd/MM/yyyy", null);

            await _context.SaveChangesAsync();

            return Ok(dbConvenio);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Convenio>>> DeleteConvenio(int id)
        {
            var dbConvenio = await _context.Convenios.FindAsync(id);
            if (dbConvenio == null)
            {
                return BadRequest("Convenio no encontrado");
            }

            var dbRelaciones = await _context.RelacionesConvenioCoordinador
                .Where(r => r.IdConvenio == id)
                .ToListAsync();

            _context.RelacionesConvenioCoordinador.RemoveRange(dbRelaciones);
            _context.Convenios.Remove(dbConvenio);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpPost("generarInformePdf")]
        public IActionResult GenerarInformePdf([FromBody] List<Convenio> convenios)
        {

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
                {
                    new CustomDateTimeConverter()
                }
            };

            var jsonString = JsonSerializer.Serialize(convenios, options);


            var stream = new MemoryStream();
            QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.Legal.Landscape());
                    page.Margin(1, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontFamily("Calibri"));

                    page.Header().ShowOnce().Column(col1 =>
                    {
                        col1.Item().AlignCenter().Text("Informe de Convenios").SemiBold().FontSize(16);
                        col1.Item().AlignLeft().Text(txt =>
                        {
                            txt.Span("Fecha: ").SemiBold().FontSize(11);
                            txt.Span($"{DateTime.Now:dd/MM/yyyy}").FontSize(11);
                        });
                    });

                    page.Content().PaddingVertical(10).Column(col2 =>
                    {

                        col2.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();

                            });

                            tabla.Header(header =>
                            {
                                header.Cell().BorderLeft(0.5f).BorderTop(0.5f).BorderBottom(0.5f).BorderColor("#A9A9A9").Background("#E6E6E6")
                                .Padding(2).Text("Nombre");

                                header.Cell().BorderTop(0.5f).BorderBottom(0.5f).BorderColor("#A9A9A9").Background("#E6E6E6")
                               .Padding(2).Text("Tipo");

                                header.Cell().BorderTop(0.5f).BorderBottom(0.5f).BorderColor("#A9A9A9").Background("#E6E6E6")
                               .Padding(2).Text("Movilidad");

                                header.Cell().BorderTop(0.5f).BorderBottom(0.5f).BorderColor("#A9A9A9").Background("#E6E6E6")
                               .Padding(2).Text("Vigencia");

                                header.Cell().BorderTop(0.5f).BorderBottom(0.5f).BorderColor("#A9A9A9").Background("#E6E6E6")
                               .Padding(2).Text("Año Firma");

                                header.Cell().BorderTop(0.5f).BorderBottom(0.5f).BorderColor("#A9A9A9").Background("#E6E6E6")
                               .Padding(2).Text("Tipo Firma");

                                header.Cell().BorderTop(0.5f).BorderBottom(0.5f).BorderColor("#A9A9A9").Background("#E6E6E6")
                               .Padding(2).Text("Cupos");

                                header.Cell().BorderTop(0.5f).BorderBottom(0.5f).BorderColor("#A9A9A9").Background("#E6E6E6")
                               .Padding(2).Text("Renovacion");

                                header.Cell().BorderTop(0.5f).BorderBottom(0.5f).BorderColor("#A9A9A9").Background("#E6E6E6")
                               .Padding(2).Text("Estatus");

                                header.Cell().BorderTop(0.5f).BorderBottom(0.5f).BorderColor("#A9A9A9").Background("#E6E6E6")
                               .Padding(2).Text("Inicio");

                                header.Cell().BorderRight(0.5f).BorderTop(0.5f).BorderBottom(0.5f).BorderColor("#A9A9A9").Background("#E6E6E6")
                               .Padding(2).Text("Termino");
                            });

                            for (int i = 0; i < convenios.Count; i++)
                            {
                                var convenio = convenios[i];

                                tabla.Cell().Border(0.5f).BorderColor("#A9A9A9")
                                 .Padding(2).Text(convenio.NombreConvenio).FontSize(10);
                                tabla.Cell().Border(0.5f).BorderColor("#A9A9A9")
                                 .Padding(2).Text(convenio.TipoConvenio).FontSize(10);
                                tabla.Cell().Border(0.5f).BorderColor("#A9A9A9")
                                 .Padding(2).Text(convenio.Movilidad).FontSize(10);
                                tabla.Cell().Border(0.5f).BorderColor("#A9A9A9")
                                 .Padding(2).Text(convenio.Vigencia).FontSize(10);
                                tabla.Cell().Border(0.5f).BorderColor("#A9A9A9")
                                 .Padding(2).Text(convenio.AnioFirma.ToString()).FontSize(10);
                                tabla.Cell().Border(0.5f).BorderColor("#A9A9A9")
                                 .Padding(2).Text(convenio.TipoFirma).FontSize(10);
                                tabla.Cell().Border(0.5f).BorderColor("#A9A9A9")
                                 .Padding(2).Text(convenio.Cupos.ToString()).FontSize(10);
                                tabla.Cell().Border(0.5f).BorderColor("#A9A9A9")
                                 .Padding(2).Text(convenio.CondicionRenovacion).FontSize(10);
                                tabla.Cell().Border(0.5f).BorderColor("#A9A9A9")
                                 .Padding(2).Text(convenio.Estatus).FontSize(10);
                                tabla.Cell().Border(0.5f).BorderColor("#A9A9A9")
                                 .Padding(2).Text(convenio.FechaInicio.ToString("dd/MM/yyyy")).FontSize(10);
                                tabla.Cell().Border(0.5f).BorderColor("#A9A9A9")
                                 .Padding(2).Text(convenio.FechaTermino.ToString("dd/MM/yyyy")).FontSize(10);

                            }
                        });
                    });

                });
            }).GeneratePdf(stream);

            stream.Position = 0;
            var bytes = stream.ToArray();
            var base64String = Convert.ToBase64String(bytes);

            return File(stream, "application/pdf", "informe.pdf");
        }

    }


    public class ConvenioPostRequest
    {
        [JsonPropertyName("id_unidad_gestora")]
        public int IdUnidadGestora { get; set; }
        [JsonPropertyName("id_coordinador_externo")]
        public int IdCoordinadorExterno { get; set; }

        [JsonPropertyName("id_coordinador_interno")]
        public int IdCoordinadorInterno { get; set; }

        [JsonPropertyName("nombre_conv")]
        public string NombreConv { get; set; } = string.Empty;

        [JsonPropertyName("tipo_conv")]
        public string TipoConv { get; set; } = string.Empty;

        [JsonPropertyName("movilidad")]
        public string Movilidad { get; set; } = string.Empty;

        [JsonPropertyName("vigencia")]
        public string Vigencia { get; set; } = string.Empty;

        [JsonPropertyName("ano_firma")]
        public int AnioFirma { get; set; }

        [JsonPropertyName("tipo_firma")]
        public string TipoFirma { get; set; } = string.Empty;

        [JsonPropertyName("cupos")]
        public int Cupos { get; set; }

        [JsonPropertyName("documentos")]
        public string Documentos { get; set; } = string.Empty;

        [JsonPropertyName("condicion_renovacion")]
        public string CondicionRenovacion { get; set; } = string.Empty;

        [JsonPropertyName("estatus")]
        public string Estatus { get; set; } = string.Empty;

        [JsonPropertyName("fecha_inicio")]
        public string FechaInicio { get; set; } = string.Empty;

        [JsonPropertyName("fecha_termino")]
        public string FechaTermino { get; set; } = string.Empty;
    }

    public class ConvenioGetResponse
    {
        public int ID_Convenio { get; set; }
        public string Nombre_Convenio { get; set; } = string.Empty;
        public string Tipo_Convenio { get; set; } = string.Empty;
        public string Movilidad { get; set; } = string.Empty;
        public string Vigencia { get; set; } = string.Empty;
        public int Anio_Firma { get; set; }
        public string Tipo_Firma { get; set; } = string.Empty;
        public int Cupos { get; set; }
        public string Documentos { get; set; } = string.Empty;
        public string Condicion_Renovacion { get; set; } = string.Empty;
        public string Estatus { get; set; } = string.Empty;
        public string Fecha_Inicio { get; set; } = string.Empty;
        public string Fecha_Termino { get; set; } = string.Empty;
        public int ID_Institucion { get; set; }
        public string Nombre_Institucion { get; set; } = string.Empty;
        public int ID_Unidad_Gestora { get; set; }
        public string Nombre_Unidad_Gestora { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
        public string Alcance { get; set; } = string.Empty;
        public string Tipo_Institucion { get; set; } = string.Empty;
        public int ID_Coordinador_Externo { get; set; }
        public string Tipo_Coordinador_Externo { get; set; } = string.Empty;
        public string Nombre_Coordinador_Externo { get; set; } = string.Empty;
        public string Correo_Coordinador_Externo { get; set; } = string.Empty;
        public int ID_Coordinador_Interno { get; set; }
        public string Tipo_Coordinador_Interno { get; set; } = string.Empty;
        public string Nombre_Coordinador_Interno { get; set; } = string.Empty;
        public string Correo_Coordinador_Interno { get; set; } = string.Empty;
    }
}
