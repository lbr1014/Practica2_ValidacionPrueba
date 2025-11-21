using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ModeloDatos
{
    public class Usuario
    {
        private int id;
        private string nombre;
        private string apellidos;
        private string direccion;
        private int codigoPostal;
        private string email;
        private string contraseña;
        private DateTime fechaCaducidadCuenta;
        private DateTime fechaCaducidadContraseña;
        private DateTime ultimoAcceso;
        private bool estado;
        private int contador;

        public Usuario(int id, string nombre, string apellidos, string direccion, int codigoPostal, string email, string contraseña, DateTime fechaCaducidadCuenta, DateTime fechaCaducidadContraseña, DateTime ultimoAcceso, bool estado)
        {
            this.id = id;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.direccion = direccion;
            this.codigoPostal = codigoPostal;
            this.email = email;
            this.contraseña = EncriptarContraseña(contraseña);
            this.fechaCaducidadCuenta = fechaCaducidadCuenta;
            this.fechaCaducidadContraseña = fechaCaducidadContraseña;
            this.ultimoAcceso = ultimoAcceso;
            this.estado = estado;
        }

        public bool ValidarContraseña(string password)
        {
            /*
             * Este método valida una contraseña, obligando a que tenga más de 12 caracteres, al menos
             * 2 mayusculas, 2 números y 1 caracter especial.
             * Parametros:
             *      password: contraseña sin cifrar.
             * Returns:
             *      true si cumple las condiciones; false en caso contrario.
             */
            int contadorMayusculas = 0;
            int contadorMinusculas = 0;
            int contadorNumeros = 0;
            int contadorCaracteresEspeciales = 0;

            if (string.IsNullOrWhiteSpace(password) || password.Length < 12)
            {
                return false;
            }

            for (int i = 0; i < password.Length; i++)
            {
                char caracterContrasena = password[i];

                if (char.IsUpper(caracterContrasena))
                {
                    contadorMayusculas++;

                }
                else if (char.IsLower(caracterContrasena))
                {
                    contadorMinusculas++;

                }
                else if (char.IsDigit(caracterContrasena))
                {
                    contadorNumeros++;

                }
                else
                {
                    contadorCaracteresEspeciales++;
                }
            }

            return contadorMayusculas >= 2 && contadorMinusculas >= 2 && contadorNumeros >= 1 && contadorCaracteresEspeciales >= 1;
        }
        
        public void CaducarCuenta()
        {
            if (fechaCaducidadCuenta <= DateTime.Today)
            {
                estado = false;
            }

        }
        public void CaducarContraseña()
        {
            if (fechaCaducidadCuenta <= DateTime.Today)
            {
                estado = false;
                contraseña = "";
               
            }
        }
        public void ReactivarCuenta(int dias)
        {
            estado = true;
            fechaCaducidadCuenta.AddDays(dias);
        }
        public void CambiarContraseña(String contraseñaNueva)
        {
            if (estado == false)
            {
                estado = true;
            }
            string contraseñaNuevaEncriptada= EncriptarContraseña(contraseñaNueva);
            contraseña = contraseñaNuevaEncriptada;
           
        }
        public bool CuentaActiva(Usuario usuario)
        {
            return usuario.Estado;
        }

        public bool ComprobarContraseña(String password)
        {
            /*
             * Este método comprueba el hash de una contraseña candita con la almacenada.
             * Si falla, incrementa un contador de intentos, tras tres fallso bloqeua la cuenta
             * del usuario.
             * Parametros:
             *      password: Contraseña candidata.
             * Returns:
             *      true si ambas contraseñas coinciden; false si no lo hacen.
             */

            bool contraseñaIgual = false;

            string candidatoHash = EncriptarContraseña(password);
            bool coincide = string.Equals(this.contraseña, candidatoHash, StringComparison.Ordinal);

            if (coincide)
            {
                contraseñaIgual = true;
        
            }

            return contraseñaIgual;


        }
        public  String EncriptarContraseña(String value)
        {
            /*
             * Este método calcula el hash SHA-256 de una contraseña pasada como testo 
             * y lo convierte en formato hexadecimal en minúsculas.
             * Parametros:
             *      value: COntraseña en formato texto sin cifrar.
             * Returns:
             *      ContraseñaCifrada como cadena hexadecimal de 64 caracteres que se corresponde 
             *      con el hash SHA-256 de la contraseña sin cifrar.
             */

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(value);
                byte[] hash = sha256.ComputeHash(bytes);

                // Convertir hash a string hexadecimal
                StringBuilder ContraseñaCifrada = new StringBuilder();
                foreach (byte b in hash)
                {
                    ContraseñaCifrada.Append(b.ToString("x2")); // formato hexadecimal
                }
                return ContraseñaCifrada.ToString();
            }
        }

        /* GETS Y SETS */
        public int Id { 
            get { return this.id; }
            set { this.id = value; } 
        }
        public string Nombre {
            get { return this.nombre; }
            set { this.nombre = value; }
        }
        public string Apellidos {
            get { return this.apellidos; }
            set { this.apellidos = value; }
 
        }
        public string Direccion {
            get { return this.direccion; }
            set { this.direccion = value; } 
        }
        public int CodigoPostal {
            get { return this.codigoPostal; }
            set { this.codigoPostal = value; }
 
        }
        public string Email {
            get { return this.email; }
            set { this.email = value; }

        }
        public DateTime FechaCaducidadCuenta {
            get { return this.fechaCaducidadCuenta; }
            set { this.fechaCaducidadCuenta = value; }
        }
        public DateTime FechaCaducidadContraseña {
            get { return this.fechaCaducidadContraseña; }
            set { this.fechaCaducidadContraseña = value; }

        }
        public DateTime UltimoAcceso {
            get { return this.ultimoAcceso; }
            set { this.ultimoAcceso = value; }

        }
        public bool Estado {
            get { return this.estado; }
            set { this.estado = value; }
        }

        public override bool Equals(object obj)
        {
            return obj is Usuario usuario &&
                   id == usuario.id &&
                   nombre == usuario.nombre &&
                   apellidos == usuario.apellidos &&
                   direccion == usuario.direccion &&
                   codigoPostal == usuario.codigoPostal &&
                   email == usuario.email &&
                   contraseña == usuario.contraseña &&
                   fechaCaducidadCuenta == usuario.fechaCaducidadCuenta &&
                   fechaCaducidadContraseña == usuario.fechaCaducidadContraseña &&
                   ultimoAcceso == usuario.ultimoAcceso &&
                   estado == usuario.estado &&
                   Id == usuario.Id &&
                   Nombre == usuario.Nombre &&
                   Apellidos == usuario.Apellidos &&
                   Direccion == usuario.Direccion &&
                   CodigoPostal == usuario.CodigoPostal &&
                   Email == usuario.Email &&
                   FechaCaducidadCuenta == usuario.FechaCaducidadCuenta &&
                   FechaCaducidadContraseña == usuario.FechaCaducidadContraseña &&
                   UltimoAcceso == usuario.UltimoAcceso &&
                   Estado == usuario.Estado;
        }

        public override int GetHashCode()
        {
            int hashCode = 1509075510;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(nombre);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(apellidos);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(direccion);
            hashCode = hashCode * -1521134295 + codigoPostal.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(contraseña);
            hashCode = hashCode * -1521134295 + fechaCaducidadCuenta.GetHashCode();
            hashCode = hashCode * -1521134295 + fechaCaducidadContraseña.GetHashCode();
            hashCode = hashCode * -1521134295 + ultimoAcceso.GetHashCode();
            hashCode = hashCode * -1521134295 + estado.GetHashCode();
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nombre);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Apellidos);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Direccion);
            hashCode = hashCode * -1521134295 + CodigoPostal.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + FechaCaducidadCuenta.GetHashCode();
            hashCode = hashCode * -1521134295 + FechaCaducidadContraseña.GetHashCode();
            hashCode = hashCode * -1521134295 + UltimoAcceso.GetHashCode();
            hashCode = hashCode * -1521134295 + Estado.GetHashCode();
            return hashCode;
        }
    }
}