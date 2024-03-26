namespace SGCUCMAPI.Models.Historial
{
    public class ConvenioHistorial
    {
        public int IdCambioConvenio { get; set; }
        public int IdConvenio { get; set; }
        public int IdUnidadGestora { get; set; }
        public string NombreConvenio { get; set; } = string.Empty;
        public string TipoConvenio { get; set; } = string.Empty;
        public string Movilidad { get; set; } = string.Empty;
        public string Vigencia { get; set;} = string.Empty;
        public int AnioFirma { get; set; }
        public string TipoFirma { get; set; } = string.Empty;
        public int Cupos { get; set; }
        public string Documentos { get; set; } = string.Empty;
        public string CondicionRenovacion { get; set; } = string.Empty;
        public string Estatus { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public string Accion { get; set; } = string.Empty;
        public DateTime FechaCambio { get; set; } = DateTime.Now;
        public string UsuarioCambio {  get; set; } = string.Empty;
    }
}
