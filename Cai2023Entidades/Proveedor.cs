using System;
namespace Cai2023Entidades
{
    public class Proveedor
    {
        public Guid IdProveedor { get; set; }
        public Guid IdProducto { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaAlta { get; set; }
        public int CUIT { get; set; }
        public string Email { get; set; }
        public string Apellido { get; set; }
        public Guid IdUsuario { get; set; }

        public string Estado { get; set; } = "INACTIVO"; //podria no haberla creado y suado la fecha baja 


        public DateTime FechaBaja { get; set; } = default;// FechaBaja es nullable (puede ser null)

        // Constructor
        public Proveedor(Guid idproveedor, Guid Idusuario, string nombre, DateTime fechaAlta, int cuit, string email, string apellido)
        {
            IdProveedor = idproveedor;
            Nombre = nombre;
            Apellido = apellido;
            FechaAlta = fechaAlta;
            CUIT = cuit;
            Email = email;
            IdUsuario = Idusuario;
        }
        override
        public String ToString() => CUIT + " " + Nombre + " " + Apellido;
    }
}

