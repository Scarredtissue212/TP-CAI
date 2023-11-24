using System;
using Cai2023Datos;
using Cai2023Entidades;

namespace Cai2023Negocio
{
    public class ProveedorNegocio
    {
        private readonly ProveedorDatos proveedorDatos;

        public List<Proveedor> TraerProveedores()
        {
            List<Proveedor> proveedores = proveedorDatos.TraerTodos();
            return proveedores;
        }

        public void CrearProveedor(Proveedor proveedor)
        {
            proveedorDatos.Insertar(proveedor);
            //Deberá permitir registrar la categoría de productos para cada proveedor
            //(un proveedor puede estar en más de una categoría) 
        }

        public void EliminarProveedor(int Cuit)
        {
            List<Proveedor> proveedores = proveedorDatos.TraerTodos();
            bool proveedorEncontrado = false;

            foreach (Proveedor p in proveedores)
            {
                if (p.CUIT == Cuit)
                {
                    // En caso de baja de proveedor deberá de quedar en estado INACTIVO 
                    p.Estado = "INACTIVO";
                    //p.FechaBaja = DateTime.Now;
                    proveedorDatos.EliminarProveedor(p.IdProveedor);
                    proveedorEncontrado = true;
                    break;  
                }
            }
            if (!proveedorEncontrado)
            {
                Console.WriteLine("Proveedor no encontrado");
            }
        }

        public void ActualizarProveedor(string nombre,string apellido,string mail, int Cuit)
        {
            List<Proveedor> proveedores = proveedorDatos.TraerTodos();
            bool proveedorEncontrado = false;

            foreach (Proveedor p in proveedores)
            {
                if (p.CUIT == Cuit)
                {
                    proveedorDatos.ModificarProveedor(p.IdProveedor, nombre, apellido, mail, Cuit);
                    proveedorEncontrado = true;
                    break;
                }
            }
            if (!proveedorEncontrado)
            {
                Console.WriteLine("Proveedor no encontrado");
            }
        }
        public void ReactivarProveedor(int Cuit)
        {
            List<Proveedor> proveedores = proveedorDatos.TraerTodos();
            bool proveedorEncontrado = false;

            foreach (Proveedor p in proveedores)
            {
                if (p.CUIT == Cuit)
                {
                    p.Estado = "ACTIVO";
                    proveedorDatos.ReactivarProveedor(p.IdProveedor);
                    proveedorEncontrado = true;
                    break;
                }
            }

            if (!proveedorEncontrado)
            {
                Console.WriteLine("Proveedor no encontrado");
            }
        }
    }
}

