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
        Dictionary<List<Proyecto>, List<Rol>> proyectosYSusRoles = null;

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

            // Sin particiones iniciales
            Assert.AreEqual(0, app.Proyectos.Count);
            Assert.AreEqual(0, app.Roles.Count);
            Assert.AreEqual(0, app.UsuariosYSusProyectos.Count);
            Assert.AreEqual(0, app.ProyectosYSusRoles.Count);

            Assert.AreEqual(id, app.Id);
            Assert.AreEqual(nombre, app.Nombre);
            Assert.AreEqual(usuario, app.Usuario);
            Assert.AreEqual(proyectos, app.Proyectos);
            Assert.AreEqual(roles, app.Roles);
            Assert.AreEqual(usuariosYSusProyectos, app.UsuariosYSusProyectos);
            Assert.AreEqual(proyectosYSusRoles, app.ProyectosYSusRoles);

            Assert.IsNotNull(app.Usuario);


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
            Assert.AreEqual(0, app.Roles.Count);

            app.asignarRolUsuario(usuario, r1);
            Assert.AreEqual(1, app.Roles.Count);

            app.agregarRolProyecto(p1, r2);
            Assert.AreEqual(2, app.Roles.Count);

            Assert.AreEqual(2, app.Roles.Count);

        }

        [TestMethod]
        public void listarProyectosUsuariosTest()
        {
            Assert.IsNotNull(app);

            Assert.IsNull(app.Proyectos);
            app.agregarProyecto(p1);
            Assert.IsNotNull(app.Proyectos);

            Assert.AreEqual(0, app.ProyectosYSusRoles(p1));
            app.agregarRolProyecto(p1, r1);
            Assert.AreEqual(1, app.ProyectosYSusRoles(p1));

            Dictionary<Proyecto, List<Rol>> listaProyectos = app.listarProyectosUsuario(usuario);


            Assert.IsNotNull(listaProyectos);


        }

        [TestMethod]
        public void PermisosUsuarioProyectoTest()
        {
            Assert.IsNotNull(app);

            Assert.IsNull(app.Proyectos);
            app.agregarProyecto(p1);
            app.agregarProyecto(p2);
            Assert.IsNotNull(app.Proyectos);

            Assert.AreEqual(0, app.ProyectosYSusRoles(p1));
            app.agregarRolProyecto(p1, r1);
            app.agregarRolProyecto(p1, r2);
            Assert.AreEqual(1, app.ProyectosYSusRoles(p1));

            List<String> permisos = app.PermisosUsuarioProyecto(usuario, p1);
            var contar = permisos.Count;

            Assert.AreEqual(5, contar);

            permisos = app.PermisosUsuarioProyecto(usuario, p2);
            contar = permisos.Count;

            Assert.AreEqual(0, contar);
        }

        [TestMethod]
        public void ToStringTest()
        {
            app.agregarProyecto(p1);
            app.agregarProyecto(p2);
            app.agregarRolProyecto(p1, r1);
            app.agregarRolProyecto(p2, r2);
            string cadena = "Usuario: " + usuario.Nombre + "" + usuario.Apellidos + "\n" + "Proyectos: \n" + p1.Nombre + ": " + r1.Nombre + "\n" + p2.Nombre + ": " + r2.Nombre;

            Assert.AreEqual(cadena, app.ToString());
        }

        [TestMethod]
        public void ProyectosYSusRolesTest()
        {
            Assert.IsNotNull(app);

            Assert.IsNull(app.Proyectos);
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
            Assert.AreEqual(1, app.ProyectosYSusRoles(p1));

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
