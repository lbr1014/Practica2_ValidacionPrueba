using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModeloDatos;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace ModeloDatosTest
{
    
[TestClass]
    public class UsuarioTest
    {
        int id = 0;
        string nombre = "Alexia";
        string apellidos = "Putellas Segura";
        string direccion = "C/ Carrer del Mig 5";
        int codigoPostal = 08970;
        string email = "balonDeOro3@gmail.com";
        string contraseña = "ConMasDe12Caracteres!";
        DateTime fechaCaducidadCuenta = DateTime.Now.AddDays(365);
        DateTime fechaCaducidadContraseña = DateTime.Now.AddDays(365);
        DateTime ultimoAcceso = DateTime.MinValue;
        bool estado = true;

        Usuario u = null;
        Usuario u2 = null; 
        Usuario u3 = null;

        [TestInitialize]
        public void InicializaTest()
        {
            u = new Usuario(id, nombre, apellidos, direccion, codigoPostal, email, contraseña, fechaCaducidadCuenta, fechaCaducidadContraseña, ultimoAcceso, estado);
            u2 = new Usuario(id, nombre, apellidos, direccion, codigoPostal, email, contraseña, fechaCaducidadCuenta, fechaCaducidadContraseña, ultimoAcceso, estado);
            u3 = new Usuario(1, "Andrea", "Medina Martín", "C/ Alcalá de Enares", 28805, "andreMedina@gmail.com", "ConMasDe12Caracteres!", fechaCaducidadCuenta, fechaCaducidadContraseña, ultimoAcceso, estado);

        }
        [TestMethod]
        public void Constructor()
        {
            
            Assert.IsNotNull(u);

            Assert.AreEqual(id, u.Id);
            Assert.AreEqual(nombre, u.Nombre);
            Assert.AreEqual(apellidos, u.Apellidos);
            Assert.AreEqual(direccion, u.Direccion);
            Assert.AreEqual(codigoPostal, u.CodigoPostal);
            Assert.AreEqual(email, u.Email);
            Assert.AreEqual(fechaCaducidadCuenta, u.FechaCaducidadCuenta);
            Assert.AreEqual(fechaCaducidadContraseña, u.FechaCaducidadContraseña);
            Assert.AreEqual(ultimoAcceso, u.UltimoAcceso);
            Assert.AreEqual(estado, u.Estado);

            Assert.IsTrue(u.CuentaActiva(u));
            Assert.IsTrue(u.ComprobarContraseña(contraseña));
            Assert.IsTrue(u.ValidarContraseña(contraseña));
            Assert.AreEqual(DateTime.MinValue.Date, u.UltimoAcceso.Date);

            //Comprobamos las fechas
            DateTime hoy = DateTime.Now;
            Assert.AreEqual(hoy.AddDays(365).Date, u.FechaCaducidadCuenta.Date);
            Assert.AreEqual(hoy.AddDays(365).Date, u.FechaCaducidadContraseña.Date);

        }

        [TestMethod]
        public void GetYSetTest()
        {
            u.Id = 33;
            Assert.AreEqual(33, u.Id);

            u.Nombre = "Lando";
            Assert.AreEqual("Lando", u.Nombre);

            u.Apellidos = "Norris";
            Assert.AreEqual("Norris", u.Apellidos);

            u.Direccion = "C/Bd Albert 1";
            Assert.AreEqual("C/Bd Albert 1", u.Direccion);

            u.CodigoPostal = 98000;
            Assert.AreEqual(98000, u.CodigoPostal);

            u.Email = "primerMundial@gmail.com";
            Assert.AreEqual("primerMundial@gmail.com", u.Email);

            DateTime nuevaFecha = DateTime.Now.AddDays(15);
            u.FechaCaducidadCuenta= nuevaFecha;
            Assert.AreEqual(nuevaFecha.Date, u.FechaCaducidadCuenta.Date);

            u.FechaCaducidadContraseña=nuevaFecha;
            Assert.AreEqual(nuevaFecha.Date, u.FechaCaducidadContraseña.Date);

            nuevaFecha = DateTime.Now.AddDays(-15);
            u.UltimoAcceso= nuevaFecha;
            Assert.AreEqual(nuevaFecha.Date, u.UltimoAcceso.Date);

            u.Estado = false;
            Assert.IsFalse(u.Estado);

        }

        public static IEnumerable<object[]> ObtenerContraseñaDesdeJson()
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, "utils", "contraseña.json");
            if (!File.Exists(filePath))
                Assert.Inconclusive($"No se encontró el archivo de datos: {filePath}");

            

            string json = File.ReadAllText(filePath, System.Text.Encoding.UTF8);
         

            JsonArray data = JsonNode.Parse(json).AsArray();

            foreach (var item in data)
            {
                string contraseña = item?["contraseña"]?.ToString() ?? "";
                int correcto = item?["correcto"]?.GetValue<int?>() ?? 0;

                yield return new object[] { contraseña, correcto == 1 };
            }
        }

        [TestMethod]
        [DynamicData(nameof(ObtenerContraseñaDesdeJson))]
        public void ContraseñaTest(string contraseña, bool esperado)
        {
            bool ok = u.ValidarContraseña(contraseña);

            Assert.AreEqual(esperado, ok, $"Contraseña probado: {contraseña}");
        }

        [TestMethod]
        public void CaducarCuentaTest()
        {
            Assert.IsTrue(u.Estado);

            DateTime fechaCaducar = DateTime.Now.AddDays(-15);
            u.FechaCaducidadCuenta = fechaCaducar;
            u.CaducarCuenta();

            Assert.IsFalse(u.Estado);
        }

        [TestMethod]
        public void CaducarContraseñaTest()
        {
            Assert.IsTrue(u.Estado);

            DateTime fechaCaducar = DateTime.Now.AddDays(-15);
            u.FechaCaducidadCuenta = fechaCaducar;
            u.CaducarContraseña();

            Assert.IsFalse(u.Estado);
            Assert.IsTrue(u.ComprobarContraseña(""));
        }

        [TestMethod]
        public void ReactivarCuentaTest()
        {
            DateTime fechaCaducar = DateTime.Now.AddDays(-65);
            u.FechaCaducidadCuenta = fechaCaducar;
            u.CaducarCuenta();

            Assert.IsFalse(u.Estado);

            u.ReactivarCuenta(365);
            Assert.IsTrue(u.Estado);
            Assert.AreEqual(DateTime.Now.AddDays(300).Date, u.FechaCaducidadCuenta.Date);

        }

        [TestMethod]
        public void CambiarContraseñaTest()
        {
            Assert.IsTrue(u.ComprobarContraseña("ConMasDe12Caracteres!"));
            u.CambiarContraseña("bZnt34T8sEb7!");
            Assert.IsFalse(u.ComprobarContraseña("ConMasDe12Caracteres!"));
            Assert.IsTrue(u.ComprobarContraseña("bZnt34T8sEb7!"));

            DateTime fechaCaducar = DateTime.Now.AddDays(-65);
            u.FechaCaducidadCuenta = fechaCaducar;
            u.CaducarContraseña();
            Assert.IsFalse(u.Estado);
            Assert.IsTrue(u.ComprobarContraseña(""));

            u.CambiarContraseña("OtraContraseñaNueva22!");
            Assert.IsTrue(u.Estado);
            Assert.IsFalse(u.ComprobarContraseña(""));
            Assert.IsTrue(u.ComprobarContraseña("OtraContraseñaNueva22!"));

        }

        [TestMethod]
        public void CuentaActivaTest()
        {
            Assert.IsTrue(u.CuentaActiva(u));

            u.Estado = false;

            Assert.IsFalse(u.CuentaActiva(u));
        }

        [TestMethod]
        public void ComprobarContraseñaTest()
        {
            Assert.IsTrue(u.ComprobarContraseña("ConMasDe12Caracteres!"));
            u.CambiarContraseña("bZnt34T8sEb7!");
            Assert.IsFalse(u.ComprobarContraseña("ConMasDe12Caracteres!"));
            Assert.IsTrue(u.ComprobarContraseña("bZnt34T8sEb7!"));

        }

        [TestMethod()]
        public void EncriptarContraseñaTest()
        {
            string contraseñaSinCifrar = "ContraseñaCorrecta1!";
            string contraseñaCifrada = u.EncriptarContraseña(contraseñaSinCifrar);


            Assert.AreNotEqual(contraseñaSinCifrar, contraseñaCifrada);

        }

        [TestMethod()]
        public void EqualsTest()
        {
            Assert.IsTrue(u.Equals(u2));
            Assert.IsFalse(u.Equals(u3));

        }

        [TestMethod()]
        public void GetHashCodeTest()
        {

            int hash1 = u.GetHashCode();
            int hash2 = u2.GetHashCode();
            Assert.AreEqual(hash1, hash2);

            int hash3 = u3.GetHashCode();
            Assert.AreNotEqual(hash1, hash3);
        }


    }
}
