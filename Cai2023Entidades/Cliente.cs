using System;
namespace Cai2023Entidades
{
    public class Cliente
    {
        public Guid IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public string Email { get; set; }
        public DateTime Fechadealta { get; set; }
        public DateTime Fechadebaja { get; set; } = default;
        public DateTime FechadeNacimiento { get; set; }
        public string Idusuario { get; set; }
        public int Host { get; set; }
        public int DNI { get; set; }

        public Cliente(Guid idcliente, string nombre, string apellido, string direccion, int telefono, string email, string idusuario,DateTime fechadenacimiento, DateTime fechadealta,int host,int dni)
        {
            IdCliente = idcliente;
            Nombre = nombre;
            Apellido = apellido;
            Direccion = direccion;
            Telefono = telefono;
            Email = email;
            Idusuario = idusuario;
            FechadeNacimiento = fechadenacimiento;
            Fechadealta = fechadealta;
            Host = host;
            DNI = dni;
        }
    }
}

