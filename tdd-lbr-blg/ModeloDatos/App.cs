using System.Collections.Generic;

namespace ModeloDatos
{
    public class App
    {
        private int id;
        private string nombre;
        private Usuario usuario;

        List<Proyecto> proyectos = null;
        List<Rol> roles = null;

        Dictionary<Usuario, List<Proyecto>> usuariosYSusProyectos = null;
        Dictionary<List<Proyecto>, List<Rol>> proyectosYSusRoles = null;

        public App(int id, string nombre, Usuario usuario)
        {
            this.id = id;
            this.nombre = nombre;
            this.usuario = usuario;
        }

        public int Id { 
            get { return this.id; }
            set { this.id = value; }
        }
        public string Nombre { 
            get { return this.nombre; }
            set { this.nombre = value; }
        }
        public Usuario Usuario { 
            get { return this.usuario; }
            set { this.usuario = value; }
        }
        public List<Proyecto> Proyectos {
            get { return this.proyectos; }
            set { this.proyectos = value; }
        }
        public List<Rol> Roles { 
            get { return this.roles; }
            set { this.roles = value; }
        }
        public Dictionary<Usuario, List<Proyecto>> UsuariosYSusProyectos { 
            get { return this.usuariosYSusProyectos; }
            set { this.usuariosYSusProyectos = value; }
        }
        public Dictionary<List<Proyecto>, List<Rol>> ProyectosYSusRoles {
            get { return this.proyectosYSusRoles; }
            set { this.proyectosYSusRoles = value; }
        }
    }
}