using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModeloDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDatosTest
{
    [TestClass]
    public class RolTest
    {
        int id = 0;
        string nombre = "administrador"; 
        string descripcion = "rol administrador de la aplicacón";
        bool edicionPlanDePruebas = true;
        bool ejecucionPlanDePrueba = true;
        bool ejecucionCasosPruebas = true;
        bool edicionCasosPruebas = true;
        bool gestiones = true;

        Rol r = null;
        Rol r2 = null;
        Rol r3 = null;

        [TestInitialize]
        public void InicializaTest()
        {
            r = new Rol(id, nombre, descripcion, edicionPlanDePruebas, ejecucionPlanDePrueba, ejecucionCasosPruebas, edicionCasosPruebas, gestiones);
            r2 = new Rol(id, nombre, descripcion, edicionPlanDePruebas, ejecucionPlanDePrueba, ejecucionCasosPruebas, edicionCasosPruebas, gestiones);
            r3 = new Rol(1, "tester", "rol encargado de realizar las pruebas", true, true, true, false, false);

        }

        [TestMethod]
        public void Constructor()
        {
            Assert.IsNotNull(r);

            Assert.AreEqual(id, r.Id);
            Assert.AreEqual(nombre, r.Nombre);
            Assert.AreEqual(descripcion, r.Descripcion);
            Assert.AreEqual(edicionPlanDePruebas, r.EdicionPlanDePruebas);
            Assert.AreEqual(ejecucionPlanDePrueba, r.EjecucionPlanDePrueba);
            Assert.AreEqual(ejecucionCasosPruebas, r.EjecucionCasosPruebas);
            Assert.AreEqual(edicionCasosPruebas, r.EdicionCasosPruebas);
            Assert.AreEqual(gestiones, r.Gestiones);
        }

        [TestMethod]
        public void GetYSetTest()
        {
            r.Id = 33;
            Assert.AreEqual(33, r.Id);

            r.Nombre = "Tester";
            Assert.AreEqual("Tester", r.Nombre);

            r.Descripcion = "rol encargado de realizar las pruebas";
            Assert.AreEqual("rol encargado de realizar las pruebas", r.Descripcion);

            r.EdicionPlanDePruebas = false;
            Assert.AreEqual(false, r.EdicionPlanDePruebas);

            r.EjecucionPlanDePrueba = false;
            Assert.AreEqual(false, r.EjecucionPlanDePrueba);

            r.EjecucionCasosPruebas = false;
            Assert.AreEqual(false, r.EjecucionCasosPruebas);

            r.EdicionCasosPruebas = false;
            Assert.AreEqual(false, r.EdicionCasosPruebas);

            r.Gestiones = false;
            Assert.AreEqual(false, r.Gestiones);

        }

        [TestMethod()]
        public void EqualsTest()
        {
            Assert.IsTrue(r.Equals(r2));
            Assert.IsFalse(r.Equals(r3));

        }

        [TestMethod()]
        public void GetHashCodeTest()
        {

            int hash1 = r.GetHashCode();
            int hash2 = r2.GetHashCode();
            Assert.AreEqual(hash1, hash2);

            int hash3 = r3.GetHashCode();
            Assert.AreNotEqual(hash1, hash3);
        }


    }
}
