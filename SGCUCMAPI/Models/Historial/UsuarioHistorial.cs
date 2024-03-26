namespace SGCUCMAPI.Models.Historial
{
    public class UsuarioHistorial
    {
        public int IdCambioUsuario { get; set; }
        public int IdUsuario { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Vigencia { get; set; } = string.Empty;
        public string Privilegios { get; set; } = string.Empty;
        public string Accion { get; set; } = string.Empty;
        public DateTime FechaCambio { get; set; } = DateTime.Now;
        public string UsuarioCambio { get; set; } = string.Empty;
    }
}
