using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModeladoDatos;
using System;

namespace ModeloDatosTest
{
    [TestClass]
    public class UsuarioTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            int idUsuario = 1;
            string nombre = "Lydia";
            string email = "lbr1014@alu.ubu.es";
            string password = "ConMasDe12Caracteres!";
            Usuario u = new Usuario(idUsuario, nombre, email, password);

            Assert.Equals(u.idUsuario, idUsuario);

            Assert.IsFalse(u.ComprobarContraseña("password"));
            Assert.IsTrue(u.ComprobarContraseña("ConMasDe12Caracteres!"));

            Assert.IsFalse(u.CambiarContraseña("anterior", "mueva"));
        }
    }
}
