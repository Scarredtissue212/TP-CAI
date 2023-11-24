using System;
namespace Cai2023Entidades
{
    public class Venta
    {
        public Guid IdVenta { get; set; }
        public Guid IdCliente { get; set; }
        public Guid IdProducto { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Estado { get; set; }
        public Guid IdUsuario { get; set; }

        public Venta(Guid idventa, Guid idCliente, Guid idProducto, int cantidad, DateTime fechaAlta, string estado, Guid idUsuario)
        {
            IdVenta = idventa;
            IdCliente = idCliente;
            IdProducto = idProducto;
            Cantidad = cantidad;
            FechaAlta = fechaAlta;
            Estado = estado;
            IdUsuario = idUsuario;
        }
    }
}

