using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        Dictionary<List<Proyecto>, List<Rol>> proyectosSusRoles = null;

        public App(int id, string nombre, Usuario usuario)
        {
            this.id = id;
            this.nombre = nombre;
            this.usuario = usuario;
            proyectos = new List<Proyecto>();
            roles = new List<Rol>();
            usuariosYSusProyectos = new Dictionary<Usuario, List<Proyecto>>();
            proyectosSusRoles = new Dictionary<List<Proyecto>, List<Rol>>();

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
        public Dictionary<List<Proyecto>, List<Rol>> ProyectosSusRoles {
            get { return this.proyectosSusRoles; }
            set { this.proyectosSusRoles = value; }
        }

        public void agregarProyecto(Proyecto proyecto)
        {
            proyectos.Add(proyecto);
        }

        public void agregarRolProyecto(Proyecto proyecto,Rol rol)
        {
            List<Proyecto> listaClave = null;
            foreach (var key in proyectosSusRoles.Keys)
            {
                if (key.Contains(proyecto))
                {
                    listaClave = key;
                    break;
                }
            }
            if (listaClave == null)
            {
                listaClave = new List<Proyecto> { proyecto };
                proyectosSusRoles[listaClave] = new List<Rol>();
            }
            if (!proyectosSusRoles[listaClave].Contains(rol))
            {
                proyectosSusRoles[listaClave].Add(rol);
            }
          
            if (!roles.Contains(rol))
            {
                roles.Add(rol);
            }
        }

        public int ProyectosYSusRoles(Proyecto proyecto)
        {
            if (proyectosSusRoles == null)
                return 0;
            foreach (var kvp in proyectosSusRoles)
            {
                List<Proyecto> listaProyectos = kvp.Key;
                List<Rol> listaRoles = kvp.Value;
                if (listaProyectos.Contains(proyecto))
                {

                    return listaRoles.Count;
                }
            }
            return 0;
        }

        public void asignarRolUsuario(Usuario usuario, Rol rol)
        {
            if (roles == null)
                roles = new List<Rol>();

            if (!roles.Contains(rol))
                roles.Add(rol);

            if (usuariosYSusProyectos != null && usuariosYSusProyectos.ContainsKey(usuario))
            {
                List<Proyecto> proyectosDelUsuario = usuariosYSusProyectos[usuario];

                foreach (var proyecto in proyectosDelUsuario)
                {
                    agregarRolProyecto(proyecto, rol);
                }
            }
        }

        public Dictionary<Proyecto,List<Rol>> listarProyectosUsuario(Usuario usuario)
        {
            var resultado = new Dictionary<Proyecto, List<Rol>>();

            
            if (usuariosYSusProyectos != null && usuariosYSusProyectos.ContainsKey(usuario))
            {
                List<Proyecto> proyectosDelUsuario = usuariosYSusProyectos[usuario];

                foreach (var proyecto in proyectosDelUsuario)
                {
                    List<Rol> rolesDelProyecto = new List<Rol>();

                    
                    if (proyectosSusRoles != null)
                    {
                        foreach (var kvp in proyectosSusRoles)
                        {
                            List<Proyecto> listaProyectos = kvp.Key;
                            List<Rol> listaRoles = kvp.Value;

                           
                            if (listaProyectos.Contains(proyecto))
                            {
                                rolesDelProyecto = listaRoles;
                                break; 
                            }
                        }
                    }

                    resultado[proyecto] = rolesDelProyecto;
                }
            }

            return resultado;
        }

        public override string ToString()
        {
            if (usuario == null)
                return "Usuario: null\nProyectos: \n";

            var sb = new StringBuilder();
            sb.Append("Usuario: " + usuario.Nombre + " " + usuario.Apellidos + "\n");
            sb.Append("Proyectos: \n");

            if (proyectos == null || proyectos.Count == 0)
                return sb.ToString().TrimEnd();

            for (int i = 0; i < proyectos.Count; i++)
            {
                var proyecto = proyectos[i];
                List<Rol> rolesDelProyecto = new List<Rol>();
                if (proyectosSusRoles != null)
                {
                    foreach (var kvp in proyectosSusRoles)
                    {
                        if (kvp.Key.Contains(proyecto))
                        {
                            rolesDelProyecto = kvp.Value;
                            break;
                        }
                    }
                }
                string rolesConcatenados = string.Join(", ", rolesDelProyecto.Select(r => r.Nombre));
                sb.Append(proyecto.Nombre + ": " + rolesConcatenados);

                if (i < proyectos.Count - 1)
                    sb.Append("\n");
            }
            return sb.ToString();
        }

        public List<string> PermisosUsuarioProyecto(Usuario usuario, Proyecto proyecto)
        {
            var permisos = new HashSet<string>();

            if (proyectosSusRoles == null)
                return permisos.ToList();

            foreach (var kvp in proyectosSusRoles)
            {
                if (kvp.Key.Contains(proyecto))
                {
                    foreach (var rol in kvp.Value)
                    {
                        if (rol.EdicionPlanDePruebas) permisos.Add("edicionPlanDePruebas");
                        if (rol.EjecucionPlanDePrueba) permisos.Add("ejecucionPlanDePrueba");
                        if (rol.EjecucionCasosPruebas) permisos.Add("ejecucionCasosPruebas");
                        if (rol.EdicionCasosPruebas) permisos.Add("edicionCasosPruebas");
                        if (rol.Gestiones) permisos.Add("gestiones");
                    }
                    break;
                }
            }

            return permisos.ToList();
        }

        public override bool Equals(object obj)
        {
            return obj is App app &&
                   id == app.id &&
                   nombre == app.nombre &&
                   EqualityComparer<Usuario>.Default.Equals(usuario, app.usuario) &&
                   Id == app.Id &&
                   Nombre == app.Nombre &&
                   EqualityComparer<Usuario>.Default.Equals(Usuario, app.Usuario);
        }

        public override int GetHashCode()
        {
            int hashCode = -710342842;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(nombre);
            hashCode = hashCode * -1521134295 + EqualityComparer<Usuario>.Default.GetHashCode(usuario);
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nombre);
            hashCode = hashCode * -1521134295 + EqualityComparer<Usuario>.Default.GetHashCode(Usuario);
            return hashCode;
        }
    }
}