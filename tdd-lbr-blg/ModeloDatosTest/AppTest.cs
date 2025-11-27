using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModeloDatos;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDatosTest
{
    [TestClass]
    public class AppTest
    {
        int id = 1;
        string nombre = "Valkiria";
        Usuario usuario = null;
  
        List<Proyecto> proyectos = null;
        List<Rol> roles = null;
       

        Dictionary<Usuario, List<Proyecto>> usuariosYSusProyectos = null;
        Dictionary<List<Proyecto>, List<Rol>> proyectosSusRoles = null;
     

        App app = null;
        App app2 = null;
        App app3 = null;

        Proyecto p1 = null;
        Proyecto p2 = null;

        Rol r1 = null;
        Rol r2 = null;

        [TestInitialize]
        public void InicializaTest()
        {
            usuario = new Usuario(1, "Andrea", "Medina Martín", "C/ Alcalá de Enares", 28805, "andreMedina@gmail.com", "ConMasDe12Caracteres!", DateTime.Now.AddDays(365), DateTime.Now.AddDays(365), DateTime.MinValue, true);
            proyectos = new List<Proyecto>();
            roles = new List<Rol>();
          
            usuariosYSusProyectos = new Dictionary<Usuario, List<Proyecto>>();
            proyectosSusRoles = new Dictionary<List<Proyecto>, List<Rol>>();
            

            p1 = new Proyecto(1, "Valkiria", "Proyecto Valkiria: Aplicacion que registra actividades fisicas.");
            p2 = new Proyecto(1, "Nombre", "Proyecto de prueba");
            proyectos.Add(p1);
            proyectos.Add(p2);

            r1 = new Rol(1, "administrador", "rol administrador de la aplicacón", true, true, true, true, true);
            r2 = new Rol(1, "tester", "rol encargado de realizar las pruebas", true, true, true, false, false);
            roles.Add(r1);
            roles.Add(r2);

            app = new App(id, nombre, usuario);
            app2 = new App(id, nombre, usuario);
            app3 = new App(15, "nombre", usuario);

        }

        [TestMethod]
        public void Constructor()
        {
            Assert.IsNotNull(app);

            
            Assert.AreEqual(0, app.Proyectos.Count);
            Assert.AreEqual(0, app.Roles.Count);
            Assert.AreEqual(0, app.UsuariosYSusProyectos.Count);
            Assert.AreEqual(0, app.ProyectosSusRoles.Count);

            Assert.AreEqual(id, app.Id);
            Assert.AreEqual(nombre, app.Nombre);
            Assert.AreEqual(usuario, app.Usuario);
            CollectionAssert.AreEqual(new List<Proyecto>(), app.Proyectos);
            CollectionAssert.AreEqual(new List<Rol>(), app.Roles);
            CollectionAssert.AreEqual(new Dictionary<Usuario, List<Proyecto>>(),app.UsuariosYSusProyectos);
            CollectionAssert.AreEqual(new Dictionary<List<Proyecto>, List<Rol>>(), app.ProyectosSusRoles);

            Assert.IsNotNull(app.Usuario);


        }

        [TestMethod]
        public void GetYset()
        {
            app.Id = 33;
            Assert.AreEqual(33, app.Id);

            app.Nombre = "Tester";
            Assert.AreEqual("Tester", app.Nombre);

            Usuario u3 = new Usuario(1, "Fernando", "Alonso", "Asturias", 33001, "la33@gmail.com", "ConMasDe12Caracteres!", DateTime.Now.AddDays(365), DateTime.Now.AddDays(365), DateTime.MinValue, true);

            app.Usuario = u3;
            Assert.AreEqual(u3, app.Usuario);

            List<Proyecto> proyectos4 = null;
            app.Proyectos = proyectos4;
            Assert.AreEqual(proyectos4, app.Proyectos);

            List<Rol> roles4 = null;
            app.Roles = roles4;
            Assert.AreEqual(roles4, app.Roles);

            Dictionary<Usuario, List<Proyecto>> usuariosYSusProyectos4 = null;
            app.UsuariosYSusProyectos = usuariosYSusProyectos4;
            Assert.AreEqual(usuariosYSusProyectos4, app.UsuariosYSusProyectos);

            Dictionary<List<Proyecto>, List<Rol>> proyectosSusRoles4 = null;
            app.ProyectosSusRoles = proyectosSusRoles4;
            Assert.AreEqual(proyectosSusRoles4, app.ProyectosSusRoles);

            
        }

        [TestMethod]
        public void agregarProyectoTest()
        {
            Assert.AreEqual(0, app.Proyectos.Count);

            app.agregarProyecto(p1);

            Assert.IsNotNull(app.Proyectos.Count);
        }

        [TestMethod]
        public void agregarRolProyectoTest()
        {
            Assert.AreEqual(0, app.ProyectosYSusRoles(p1));

            app.agregarRolProyecto(p1, r1);
            Assert.AreEqual(1, app.ProyectosYSusRoles(p1));

            app.agregarRolProyecto(p1, r1);
            Assert.AreEqual(1, app.ProyectosYSusRoles(p1));

            app.agregarRolProyecto(p2, r1);
            Assert.AreEqual(1, app.ProyectosYSusRoles(p2));

            app.agregarRolProyecto(p1, r2);
            Assert.AreEqual(2, app.ProyectosYSusRoles(p1));

        }

        [TestMethod]
        public void asignarRolUsuarioTest()
        {
            app.UsuariosYSusProyectos = null;
            app.Roles = null;

            app.asignarRolUsuario(usuario, r1);

     
            Assert.IsNotNull(app.Roles);
            Assert.AreEqual(1, app.Roles.Count);

           
            app.UsuariosYSusProyectos = new Dictionary<Usuario, List<Proyecto>>(); 
            app.Roles = new List<Rol>();
            app.asignarRolUsuario(usuario, r1);

          
            Assert.AreEqual(1, app.Roles.Count);

           
            app.agregarProyecto(p1);
            app.agregarProyecto(p2);
            app.UsuariosYSusProyectos[usuario] = new List<Proyecto> { p1, p2 };

            app.asignarRolUsuario(usuario, r1);

            
            Assert.AreEqual(1, app.ProyectosYSusRoles(p1));
            Assert.AreEqual(1, app.ProyectosYSusRoles(p2));

           
            Assert.AreEqual(1, app.Roles.Count);

        }

        [TestMethod]
        public void listarProyectosUsuariosTest()
        {
            app.UsuariosYSusProyectos = null;
            var resultado1 = app.listarProyectosUsuario(usuario);
            Assert.IsNotNull(resultado1);
            Assert.AreEqual(0, resultado1.Count); 

            app.UsuariosYSusProyectos = new Dictionary<Usuario, List<Proyecto>>();
            
            Assert.IsNotNull(app.listarProyectosUsuario(usuario));
            Assert.AreEqual(0, app.listarProyectosUsuario(usuario).Count); 

            app.agregarProyecto(p1);
            app.agregarProyecto(p2);

            app.UsuariosYSusProyectos[usuario] = new List<Proyecto> { p1, p2 };
            app.ProyectosSusRoles = null; 

            Assert.AreEqual(2, app.listarProyectosUsuario(usuario).Count);
            Assert.AreEqual(0, app.listarProyectosUsuario(usuario)[p1].Count);
            Assert.AreEqual(0, app.listarProyectosUsuario(usuario)[p2].Count);

            app.ProyectosSusRoles = new Dictionary<List<Proyecto>, List<Rol>>();
            app.agregarRolProyecto(p1, r1);
            app.agregarRolProyecto(p2, r2);

            Assert.AreEqual(2, app.listarProyectosUsuario(usuario).Count);
            Assert.AreEqual(1, app.listarProyectosUsuario(usuario)[p1].Count);
            Assert.AreEqual(r1, app.listarProyectosUsuario(usuario)[p1][0]);
            Assert.AreEqual(1, app.listarProyectosUsuario(usuario)[p2].Count);
            Assert.AreEqual(r2, app.listarProyectosUsuario(usuario)[p2][0]);

        }

        [TestMethod]
        public void PermisosUsuarioProyectoTest()
        {
            Assert.IsNotNull(app);

            
            app.agregarProyecto(p1);
            app.agregarProyecto(p2);
            Assert.IsNotNull(app.Proyectos);

            Assert.AreEqual(0, app.ProyectosYSusRoles(p1));
            app.agregarRolProyecto(p1, r1);
            app.agregarRolProyecto(p1, r2);
            Assert.AreEqual(2, app.ProyectosYSusRoles(p1));

            List<String> permisos = app.PermisosUsuarioProyecto(usuario, p1);
            var contar = permisos.Count;

            Assert.AreEqual(5, contar);

            permisos = app.PermisosUsuarioProyecto(usuario, p2);
            contar = permisos.Count;

            Assert.AreEqual(0, contar);

            app.ProyectosSusRoles = null; 
            app.agregarProyecto(p1);

            List<string> permisosNulos = app.PermisosUsuarioProyecto(usuario, p1);

            Assert.IsNotNull(permisos); 
            Assert.AreEqual(0, permisosNulos.Count); 


        }

        [TestMethod]
        public void ToStringTest()
        {
            app.agregarProyecto(p1);
            app.agregarProyecto(p2);
            app.agregarRolProyecto(p1, r1);
            app.agregarRolProyecto(p2, r2);
            string cadena = "Usuario: " + usuario.Nombre + " " + usuario.Apellidos + "\n" + "Proyectos: \n" + p1.Nombre + ": " + r1.Nombre + "\n" + p2.Nombre + ": " + r2.Nombre;

            Assert.AreEqual(cadena, app.ToString());

            app.Usuario = null;
            Assert.AreEqual("Usuario: null\nProyectos: \n", app.ToString());

            app.Usuario = usuario;
            app.Proyectos = null;
            Assert.AreEqual("Usuario: Andrea Medina Martín\nProyectos:", app.ToString());
        }

        [TestMethod]
        public void ProyectosYSusRolesTest()
        {
            Assert.IsNotNull(app);

            
            app.agregarProyecto(p1);
            Assert.IsNotNull(app.Proyectos);

            
            Assert.AreEqual(0, app.ProyectosYSusRoles(p1));
            app.agregarRolProyecto(p1, r1);
            Assert.AreEqual(1, app.ProyectosYSusRoles(p1));

            app.agregarRolProyecto(p1, r2);
            Assert.AreEqual(2, app.ProyectosYSusRoles(p1));

            app.agregarRolProyecto(p1, r2);
            Assert.AreEqual(2, app.ProyectosYSusRoles(p1));

            app.agregarRolProyecto(p2, r2);
            Assert.AreEqual(1, app.ProyectosYSusRoles(p2));

            app.ProyectosSusRoles = null;
            Assert.AreEqual(0, app.ProyectosYSusRoles(p1));

        }

        [TestMethod()]
        public void EqualsTest()
        {
            Assert.IsTrue(app.Equals(app2));
            Assert.IsFalse(app.Equals(app3));

        }

        [TestMethod()]
        public void GetHashCodeTest()
        {

            int hash1 = app.GetHashCode();
            int hash2 = app2.GetHashCode();
            Assert.AreEqual(hash1, hash2);

            int hash3 = app3.GetHashCode();
            Assert.AreNotEqual(hash1, hash3);
        }


    }
}
