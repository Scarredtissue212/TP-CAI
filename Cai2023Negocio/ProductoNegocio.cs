using System;
using Cai2023Entidades;
using Cai2023Datos;

namespace Cai2023Negocio
{
    public class ProductoNegocio
    {
        private readonly ProductoDatos productoDatos;
        private readonly ProveedorDatos proveedorDatos;


        public List<Producto> TraerProductos()
        {
            List<Producto> productos = productoDatos.TraerTodos();
            return productos;
        }

        public List<Producto> TraerProductosporCat()
        {
            List<Producto> productosxCat = new List<Producto>();

            for (int i = 1; i <= 5; i++)
            {
                // Solicita productos por cada categoría
                List<Producto> productosCategoria = productoDatos.TraerporCategoria(i);

                // Concatena las listas de productos de las categorias a las listas
                productosxCat.AddRange(productosCategoria);
            }
            return productosxCat;
        }

        public void CrearProducto(int Categoria, string Nombre, double precio, int stock, int CuitProveedor)
        {
            List<Proveedor> proveedor = proveedorDatos.TraerTodos();
            foreach (Proveedor p in proveedor)
            {
                if (p.CUIT == CuitProveedor)
                {
                    Guid traeidproveedor = p.IdProveedor;
                    productoDatos.InsertarProducto(Categoria, traeidproveedor, Nombre, precio, stock);
                    break;
                }
            }
        }

        public void EliminarProducto(Guid productoaeliminar)
        {
            List<Producto> productos = productoDatos.TraerTodos();

            foreach (Producto p in productos)
            {
                if (p.IdProducto == productoaeliminar)
                {
                    productoDatos.EliminarProducto(p.IdProducto);
                    break;
                }
                else
                {
                    Console.WriteLine("Producto no encontrado");
                }
            }

        }
        public void ActualizarProducto(Guid idproducto, double precio, int stock)
        {
            List<Producto> productos = productoDatos.TraerTodos();

            foreach (Producto p in productos)
            {
                if (p.IdProducto == idproducto)
                {
                    productoDatos.ActualizarProducto(idproducto, precio, stock);
                    break;
                }
                else
                {
                    Console.WriteLine("Producto no encontrado");
                }
            }
        }

        public void ReactivarProducto(Guid idProducto)
        {
            List<Producto> productos = productoDatos.TraerTodos();

            foreach (Producto p in productos)
            {
                if (p.IdProducto == idProducto)
                {
                    productoDatos.ReactivarProducto(idProducto);
                    break;
                }
                else
                {
                    Console.WriteLine("Producto no encontrado");
                }
            }
        }

        public void ActualizarStock(Producto producto, int cantidad)
        {
            if (producto.Stock >= cantidad)
            {
                producto.Stock -= cantidad;
                Guid id = producto.IdProducto;
                double precio = producto.Precio;
                int stock = producto.Stock;

                productoDatos.ActualizarProducto(id,precio,stock);
            }
            else
            {
                throw new Exception("No hay más stock del producto que desea vender");
            }
        }
        public void MostrarProductos()
        {
            List<Producto> listaproductos = TraerProductos();
            foreach (Producto producto in listaproductos)
            {
                Console.WriteLine(producto);
            }
        }
    }
}

