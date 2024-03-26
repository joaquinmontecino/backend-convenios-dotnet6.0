namespace SGCUCMAPI.Models
{
    public class Institucion
    {
        public int IdInstitucion { get; set; }
        public string NombreInstitucion { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
        public string Alcance { get; set; } = string.Empty;
        public string TipoInstitucion { get; set; } = string.Empty;

    }
}
