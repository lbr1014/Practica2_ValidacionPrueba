namespace ModeloDatos
{
    public class Usuario
    {
        private int id;
        private string nombre;
        private string apellidos;
        private string email;
        private string password;

        public Usuario(int id, string nombre, string apellidos, string email, string password)
        {
            this.Id = id;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.password = password;
        }

        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string DireccionPostal { get; set; }
        public int Id { get => id; set => id = value; }
    }
}