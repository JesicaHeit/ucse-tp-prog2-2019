namespace Contratos
{
    public enum Roles
    {
        Padre = 0, Directora = 1, Docente = 2
    }

    public class UsuarioLogueado
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public Roles[] Roles { get; set; }
        
        public Roles RolSeleccionado { get; set; }
    }
}