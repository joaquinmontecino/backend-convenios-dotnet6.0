namespace SGCUCMAPI.Models.Historial
{
    public class InstitucionHistorial
    {
        public int IdCambioInstitucion { get; set; }
        public int IdInstitucion { get; set; }
        public string NombreInstitucion { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
        public string Alcance { get; set; } = string.Empty;
        public string TipoInstitucion { get; set; } = string.Empty;
        public string Accion { get; set; } = string.Empty;
        public DateTime FechaCambio { get; set; } = DateTime.Now;
        public string UsuarioCambio { get; set; } = string.Empty;
    }
}
