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
    public class ProyectoTest
    {
        int id = 0;
        string nombre = "Valkiria";
        string descripcion = "Proyecto Valkiria: Aplicacion que registra actividades fisicas.";

        Proyecto p = null;
        Proyecto p2 = null;
        Proyecto p3 = null;

        [TestInitialize]
        public void InicializaTest()
        {
            p = new Proyecto(id, nombre, descripcion);
            p2 = new Proyecto(id, nombre, descripcion);
            p3 = new Proyecto(1, "Nombre", "Proyecto de prueba");
        }

        [TestMethod]
        public void Constructor()
        {
            Assert.IsNotNull(p);
            Assert.AreEqual(id, p.Id);
            Assert.AreEqual(nombre, p.Nombre);
            Assert.AreEqual(descripcion, p.Descripcion);

        }

        [TestMethod]
        public void GetYSetTest()
        {
            p.Id = 33;
            Assert.AreEqual(33, p.Id);

            p.Nombre = "Proyecto";
            Assert.AreEqual("Proyecto", p.Nombre);

            p.Descripcion = "Esto es una descripcion";
            Assert.AreEqual("Esto es una descripcion", p.Descripcion);
        }

        [TestMethod]
        public void EqualsTest()
        {
            Assert.IsTrue(p.Equals(p2));
            Assert.IsFalse(p.Equals(p3));
        }
        [TestMethod]
        public void GestHashCodeTest()
        {
            int hash1 = p.GetHashCode();
            int hash2 = p2.GetHashCode();
            Assert.AreEqual(hash1, hash2);

            int hash3 = p3.GetHashCode();
            Assert.AreNotEqual(hash1, hash3);
        }

    }
}
