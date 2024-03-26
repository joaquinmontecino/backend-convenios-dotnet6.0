namespace SGCUCMAPI.Models.Historial
{
    public class CoordinadorHistorial
    {
        public int IdCambioCoordinador { get; set; }
        public int IdCoordinador { get; set; }
        public int IdInstitucion { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Accion { get; set; } = string.Empty;
        public DateTime FechaCambio { get; set; } = DateTime.Now;
        public string UsuarioCambio { get; set; } = string.Empty;
    }
}
