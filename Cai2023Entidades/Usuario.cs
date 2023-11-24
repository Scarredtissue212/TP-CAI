using System;
using System.Net;

namespace Cai2023Entidades
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public int Host { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int DNI { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public string Email { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }

        public DateTime FechaBaja { get; set; } = default;
        public string Estado { get; set; } = "INACTIVO"; //podria haber usado fecha baja solamente
        public string ClaveTemporal { get; set; } = "";// clave temporal
        public DateTime UltimaActualizacionClave { get; set; } = default; // update de contraseña

        public Usuario(Guid id, int host, string nombre, string apellido, string direccion, int telefono, string email, DateTime fechaNacimiento, string nombreusuario, int dni, string contraseña)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Direccion = direccion;
            Telefono = telefono;
            Email = email;
            FechaNacimiento = fechaNacimiento;
            NombreUsuario = nombreusuario;
            Host = host;
            DNI = dni;
            Contraseña = contraseña;
        }
    }
}

