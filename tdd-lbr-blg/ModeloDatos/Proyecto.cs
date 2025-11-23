using System.Collections.Generic;

namespace ModeloDatos
{
    public class Proyecto
    {
        private int id;
        private string nombre;
        private string descripcion;


        public Proyecto(int id, string nombre, string descripcion)
        {
            this.id = id;
            this.nombre = nombre;
            this.descripcion = descripcion;
        }

        /*GETS Y SETS*/
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public string Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }
        public string Descripcion
        {
            get { return this.descripcion; }
            set { this.descripcion = value; }
        }

        public override bool Equals(object obj)
        {
            return obj is Proyecto proyecto &&
                   id == proyecto.id &&
                   nombre == proyecto.nombre &&
                   descripcion == proyecto.descripcion &&
                   Id == proyecto.Id &&
                   Nombre == proyecto.Nombre &&
                   Descripcion == proyecto.Descripcion;
        }

        public override int GetHashCode()
        {
            int hashCode = -915643516;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(nombre);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(descripcion);
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nombre);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Descripcion);
            return hashCode;
        }
    }
}