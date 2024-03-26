namespace SGCUCMAPI.Models
{
    public class Coordinador
    {
        public int IdCoordinador { get; set; }
        public int IdInstitucion { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;

    }
}
