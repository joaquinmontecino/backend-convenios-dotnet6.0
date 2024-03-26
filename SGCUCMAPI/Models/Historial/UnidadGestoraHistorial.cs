namespace SGCUCMAPI.Models.Historial
{
    public class UnidadGestoraHistorial
    {
        public int IdCambioUnidadGestora { get; set; }
        public int IdUnidadGestora { get; set; }
        public int IdInstitucion { get; set; }
        public string NombreUnidad { get; set; } = string.Empty;
        public string Accion { get; set; } = string.Empty;
        public DateTime FechaCambio { get; set; } = DateTime.Now;
        public string UsuarioCambio { get; set; } = string.Empty;
    }
}
