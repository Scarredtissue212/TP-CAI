using System;
using Cai2023Datos;
using Cai2023Entidades;

namespace Cai2023Negocio
{
    public class ClienteNegocio
    {
        private readonly ClienteDatos clienteDatos;

        public List<Cliente> TraerClientes()
        {
            List<Cliente> clientes = clienteDatos.TraerTodos();
            return clientes;
        }

        public void AgregarCliente(Cliente cliente)
        {
            clienteDatos.Insertar(cliente);
        }

        public void ActualizarCliente(string direccion, string telefono, string mail, int Dni)
        {
            List<Cliente> clientes = clienteDatos.TraerTodos();
            bool clienteEncontrado = false;

            foreach (Cliente c in clientes)
            {
                if (c.DNI == Dni)
                {
                    clienteDatos.ActualizarCliente(c.IdCliente, direccion, telefono, mail);
                    clienteEncontrado = true;
                    break;
                }
            }
            if (!clienteEncontrado)
            {
                Console.WriteLine("Proveedor no encontrado");
            }
        }
    }
}

