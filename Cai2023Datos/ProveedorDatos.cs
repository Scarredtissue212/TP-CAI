using System;
using System.Collections.Specialized;
using Cai2023Entidades;
using Cai2023Datos.Utilidades;
using Newtonsoft.Json;

namespace Cai2023Datos
{
    public class ProveedorDatos
    {
        public List<Proveedor> TraerTodos()
        {
            HttpResponseMessage response = WebHelper.Get("Proveedor/TraerProveedores");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Verifique los datos ingresados");
            }
            else
            {
                var contentStream = response.Content.ReadAsStringAsync().Result;
                List<Proveedor> listadoProveedores = JsonConvert.DeserializeObject<List<Proveedor>>(contentStream);
                return listadoProveedores;
            }
        }

        public void Insertar(Proveedor proveedor)
        {
            var jsonRequest = JsonConvert.SerializeObject(proveedor);

            HttpResponseMessage response = WebHelper.Post("Proveedor/AgregarProveedor", jsonRequest);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public string ModificarProveedor(Guid idProveedor,string nombre,string apellido,string mail, int Cuit)
        {
            string idusuario = "D347CE99-DB8D-4542-AA97-FC9F3CCE6969";//hardcode de id usuario admin
            Dictionary<String, String> map = new Dictionary<String, String>();
            map.Add("id", idProveedor.ToString());
            map.Add("IdUsuario", idusuario);
            map.Add("nombre", nombre);
            map.Add("apellido", apellido);
            map.Add("mail", mail);
            map.Add("Cuit", Cuit.ToString());

            var jsonRequest = JsonConvert.SerializeObject(map);

            HttpResponseMessage response = WebHelper.Patch("Producto/ModificarProducto", jsonRequest);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Verifique los datos ingresados");
            }

            var reader = new StreamReader(response.Content.ReadAsStream());

            String respuesta = reader.ReadToEnd();

            return respuesta;
        }

        public void EliminarProveedor(Guid idProveedor)
        {
            string idusuario = "D347CE99-DB8D-4542-AA97-FC9F3CCE6969";
            Dictionary<String, String> map = new Dictionary<String, String>();
            map.Add("id", idProveedor.ToString());
            map.Add("idUsuario", idusuario);

            var jsonRequest = JsonConvert.SerializeObject(map);

            HttpResponseMessage response = WebHelper.DeleteConBody("Proveedor/BajaProveedor", jsonRequest);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Verifique los datos ingresados");
            }

        }

        public void ReactivarProveedor(Guid idProveedor)
        {
            string idusuario = "D347CE99-DB8D-4542-AA97-FC9F3CCE6969";

            Dictionary<String, String> map = new Dictionary<String, String>();
            map.Add("id", idProveedor.ToString());
            map.Add("idUsuario", idusuario);

            var jsonRequest = JsonConvert.SerializeObject(map);

            HttpResponseMessage response = WebHelper.Patch("Proveedor/Reactivar", jsonRequest);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Verifique los datos ingresados");
            }

        }
    }
}

