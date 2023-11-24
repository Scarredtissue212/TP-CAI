using System;
using Microsoft.Win32;
using System.Collections.Specialized;
using Cai2023Entidades;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using Cai2023Datos;
using Newtonsoft.Json;
using Cai2023Datos.Utilidades;

namespace Cai2023Datos
{
    public class ClienteDatos
    {
        public void Insertar(Cliente cliente)
        {
            var jsonRequest = JsonConvert.SerializeObject(cliente);

            HttpResponseMessage response = WebHelper.Post("Cliente/AgregarCliente", jsonRequest);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public List<Cliente> TraerTodos()
        {
            HttpResponseMessage response = WebHelper.Get("Clientes");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Verifique los datos ingresados");
            }
            else
            {
                var contentStream = response.Content.ReadAsStringAsync().Result;
                List<Cliente> listadoclientes = JsonConvert.DeserializeObject<List<Cliente>>(contentStream);
                return listadoclientes;
            }
        }

        public void ActualizarCliente(Guid idcliente,string direccion, string telefono, string mail)
        {
            Dictionary<String, String> map = new Dictionary<String, String>();
            map.Add("idusuario", idcliente.ToString());
            map.Add("direccion", direccion);
            map.Add("telefono", telefono);
            map.Add("mail", mail);

            var jsonRequest = JsonConvert.SerializeObject(map);

            HttpResponseMessage response = WebHelper.Patch("Cliente/PatchCliente" + idcliente, jsonRequest);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Verifique los datos ingresados");
            }

            var reader = new StreamReader(response.Content.ReadAsStream());

            String respuesta = reader.ReadToEnd();
        }

        public void ReactivarCliente(Cliente cliente)
        {
            var jsonRequest = JsonConvert.SerializeObject(cliente);

            HttpResponseMessage response = WebHelper.Post("Cliente/Reactivar?id="+ cliente.IdCliente, jsonRequest);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
    }
}

