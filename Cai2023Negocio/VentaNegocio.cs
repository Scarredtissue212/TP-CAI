using System;
using System.Runtime.ConstrainedExecution;
using Cai2023Datos;
using Cai2023Entidades;


namespace Cai2023Negocio
{
    public class VentaNegocio
    {
        private readonly VentaDatos ventaDatos;
        private readonly ProductoDatos productoDatos;
        private readonly ClienteDatos clienteDatos;


        public List<Venta> TraerVenta(Guid idVenta)
        {
            List<Venta> venta = ventaDatos.TraerVenta(idVenta);
            return venta;
        }

        public List<Venta> TraerVentaxCliente(Guid idCliente)
        {
            List<Venta> venta = ventaDatos.TraerVentaporCliente(idCliente);
            return venta;
        }

        public void RegistrarVenta(Guid idCliente, Guid idProducto, Guid idVendedor, int Cantidad)
        {

            List<Producto> productos = productoDatos.TraerTodos();
            foreach (Producto p in productos)
            {
                if (p.IdCategoria == 3) //Categoria de electro hogar
                {
                    double valorcompra = p.Precio * Cantidad;
                    if (valorcompra > 100000)
                    {
                        p.Precio = p.Precio * 0.5;
                        Console.WriteLine("Descuento aplicado: 50%");
                    }
                    else
                    {
                        Console.WriteLine("no hay desucentos aplicables por categoria");
                    }
                }
                List<Cliente> clientes = clienteDatos.TraerTodos();
                foreach (Cliente c in clientes)
                {
                    if (c.IdCliente == idCliente)
                    {
                        List<Venta> ventasporcliente = ventaDatos.TraerVentaporCliente(idCliente);
                        foreach (Venta v in ventasporcliente)
                        {
                            int cantventas = v.Cantidad;
                            if (cantventas > 0)
                            {
                                p.Precio = p.Precio * 0.5;
                                Console.WriteLine("Descuento aplicado por compras anteriores: 50%");
                            }
                        }
                        ventaDatos.AgregarVenta(idCliente, idProducto, idVendedor, Cantidad);
                    }
                }
            }
        }
    
            public void Devolucion(Guid IdVenta,Guid IdVendedor)
            {

            Guid idabuscar = IdVenta;
            Guid idVendedor = IdVendedor;

            ventaDatos.DevolverVenta(IdVenta, IdVendedor);
        }

    }
    }



