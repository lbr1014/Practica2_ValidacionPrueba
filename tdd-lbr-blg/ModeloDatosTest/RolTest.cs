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
        string descripcion = "rol administrativo";
        bool edicionPlanDePruebas = true;
        bool ejecucionPlanDePrueba = true;
        bool ejecucionCasosPruebas = true;
        bool edicionCasosPruebas = true;
        bool gestiones = true;

        Rol r = null;

        [TestInitialize]
        public void InicializaTest()
        {
            r = new Rol(id, nombre, descripcion, edicionPlanDePruebas, ejecucionPlanDePrueba, ejecucionCasosPruebas, edicionCasosPruebas, gestiones);

        }

        [TestMethod]
        public void Constructor()
        {
            Assert.IsNotNull(r);

            Assert.AreEqual(id, r.id);
            Assert.AreEqual(nombre, r.Nombre);
            Assert.AreEqual(descripcion, r.Descripcion);
            Assert.AreEqual(edicionPlanDePruebas, r.edicionPlanDePruebas);
            Assert.AreEqual(ejecucionPlanDePrueba, r.ejecucionPlanDePrueba);
            Assert.AreEqual(ejecucionCasosPruebas, r.ejecucionCasosPruebas);
            Assert.AreEqual(edicionCasosPruebas, r.edicionCasosPruebas);
            Assert.AreEqual(gestiones, r.gestiones);
        }


    }
}
