using System;
namespace Cai2023Entidades
{
    public class Producto
    {
        public Guid IdProducto { get; set; }
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime Fechabaja { get; set; } 
        public double Precio { get; set; }
        public int Stock { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdProveedor { get; set; }

        public DateTime FechaBaja { get; set; } = default;


        public Producto(Guid idproducto, int idCategoria, string nombre, DateTime fechaAlta, double precio, int stock, Guid idUsuario, Guid idProveedor)
        {
            IdProducto = idproducto;
            IdCategoria = idCategoria;
            Nombre = nombre;
            FechaAlta = fechaAlta;
            Precio = precio;
            Stock = stock;
            IdUsuario = idUsuario;
            IdProveedor = idProveedor;
        }
    }
    
}

