using System;

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

        public Usuario(int id, string nombre, string apellidos, string direccion, int codigoPostal, string email, string contraseña, DateTime fechaCaducidadCuenta, DateTime fechaCaducidadContraseña, DateTime ultimoAcceso, bool estado)
        {
            this.id = id;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.direccion = direccion;
            this.codigoPostal = codigoPostal;
            this.email = email;
            this.contraseña = contraseña;
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
    }
}