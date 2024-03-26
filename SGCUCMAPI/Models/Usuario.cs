namespace SGCUCMAPI.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Vigencia { get; set; } = string.Empty;
        public string Privilegios { get; set; } = string.Empty;

    }
}
