using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModeloDatos;
using System;

namespace ModeloDatosTest
{
    
[TestClass]
    public class UsuarioTest
    {
        int id = 0;
        string nombre = "Lydia";
        string apellidos = "Blanco Ruiz";
        string direccionPostal = "C/ COndesa Mencia 120";
        string email = "lbr1014@alu.ubu.es";
        string password = "ConMasDe12Caracteres!";
        DateTime hoy = DateTime.Now;
        Usuario u = null;

        [TestInitialize]
        public void InicializaTest()
        {
            u = new Usuario(id, nombre, apellidos, email, password);

        }
        [TestMethod]
        public void Constructor()
        {
            
            Assert.IsNotNull(u);
            Assert.AreEqual(id, u.Id);
            Assert.AreEqual(nombre, u.Nombre);
            Assert.AreEqual(apellidos, u.Apellidos);
            Assert.AreEqual(direccionPostal, u.DireccionPostal);
            Assert.AreEqual(email, u.email);
            Assert.IsTrue(u.CuentaActiva);
            Assert.IsTrue(u.ComprobarContraseña(password));
            Assert.AreEqual(DateTime.MinValue, u.UltimoAceso);

            //Comprobamos las fechas
            Assert.AreEqual(hoy.AddDays(365), u.FechaCaducidadCuenta);
            Assert.AreEqual(hoy.AddDays(365), u.FechaCaducidadContraseña);



            Assert.IsTrue(u.ComprobarContraseña("ConMasDe12Caracteres!"));
            Assert.IsFalse(u.CambiarContraseña("anterior", "mueva"));
        }

        [TestMethod]
        public void GetYSetTest()
        {
            u.Id = 33;
            Assert.AreEqual(33, u.Id);
        }

    }
}
