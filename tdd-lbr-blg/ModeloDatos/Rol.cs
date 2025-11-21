using System.Collections.Generic;

namespace ModeloDatos
{
    public class Rol
    {
        private int id;
        private string nombre;
        private string descripcion;
        private bool edicionPlanDePruebas;
        private bool ejecucionPlanDePrueba;
        private bool ejecucionCasosPruebas;
        private bool edicionCasosPruebas;
        private bool gestiones;

        public Rol(int id, string nombre, string descripcion, bool edicionPlanDePruebas, bool ejecucionPlanDePrueba, bool ejecucionCasosPruebas, bool edicionCasosPruebas, bool gestiones)
        {
            this.id = id;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.edicionPlanDePruebas = edicionPlanDePruebas;
            this.ejecucionPlanDePrueba = ejecucionPlanDePrueba;
            this.ejecucionCasosPruebas = ejecucionCasosPruebas;
            this.edicionCasosPruebas = edicionCasosPruebas;
            this.gestiones = gestiones;
        }

        public int Id {
            get { return this.id; }
            set { this.id = value; }
        }
        public string Nombre { 
            get { return this.nombre; }
            set { this.nombre = value; }
        }
        public string Descripcion {
            get { return this.descripcion; }
            set { this.descripcion = value; }
        }
        public bool EdicionPlanDePruebas {
            get { return this.edicionPlanDePruebas; }
            set { this.edicionPlanDePruebas = value; }
        }
        public bool EjecucionPlanDePrueba {
            get { return this.ejecucionPlanDePrueba; }
            set { this.ejecucionPlanDePrueba = value; }
        }
        public bool EjecucionCasosPruebas {
            get { return this.ejecucionCasosPruebas; }
            set { this.ejecucionCasosPruebas = value; }
        }
        public bool EdicionCasosPruebas {
            get { return this.edicionCasosPruebas; }
            set { this.edicionCasosPruebas = value; }
        }
        public bool Gestiones {
            get { return this.gestiones; }
            set { this.gestiones = value; }
        }

        public override bool Equals(object obj)
        {
            return obj is Rol rol &&
                   id == rol.id &&
                   nombre == rol.nombre &&
                   descripcion == rol.descripcion &&
                   edicionPlanDePruebas == rol.edicionPlanDePruebas &&
                   ejecucionPlanDePrueba == rol.ejecucionPlanDePrueba &&
                   ejecucionCasosPruebas == rol.ejecucionCasosPruebas &&
                   edicionCasosPruebas == rol.edicionCasosPruebas &&
                   gestiones == rol.gestiones &&
                   Id == rol.Id &&
                   Nombre == rol.Nombre &&
                   Descripcion == rol.Descripcion &&
                   EdicionPlanDePruebas == rol.EdicionPlanDePruebas &&
                   EjecucionPlanDePrueba == rol.EjecucionPlanDePrueba &&
                   EjecucionCasosPruebas == rol.EjecucionCasosPruebas &&
                   EdicionCasosPruebas == rol.EdicionCasosPruebas &&
                   Gestiones == rol.Gestiones;
        }

        public override int GetHashCode()
        {
            int hashCode = -1030890594;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(nombre);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(descripcion);
            hashCode = hashCode * -1521134295 + edicionPlanDePruebas.GetHashCode();
            hashCode = hashCode * -1521134295 + ejecucionPlanDePrueba.GetHashCode();
            hashCode = hashCode * -1521134295 + ejecucionCasosPruebas.GetHashCode();
            hashCode = hashCode * -1521134295 + edicionCasosPruebas.GetHashCode();
            hashCode = hashCode * -1521134295 + gestiones.GetHashCode();
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nombre);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Descripcion);
            hashCode = hashCode * -1521134295 + EdicionPlanDePruebas.GetHashCode();
            hashCode = hashCode * -1521134295 + EjecucionPlanDePrueba.GetHashCode();
            hashCode = hashCode * -1521134295 + EjecucionCasosPruebas.GetHashCode();
            hashCode = hashCode * -1521134295 + EdicionCasosPruebas.GetHashCode();
            hashCode = hashCode * -1521134295 + Gestiones.GetHashCode();
            return hashCode;
        }
    }
}