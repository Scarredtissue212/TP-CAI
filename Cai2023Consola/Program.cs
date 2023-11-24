    using System;
    using Cai2023Entidades;
    using Cai2023Negocio;
    using CAI2023Consola.Utilidades;
    using System.Collections.Generic;
    using System.Net;
    using System.Collections;

namespace CAI2023Consola
{
    class Program
    {
        private static ProveedorNegocio proveedorNegocio;
        private static ProductoNegocio productoNegocio;
        private static VentaNegocio ventaNegocio;
        private static ClienteNegocio clienteNegocio;


        static void Main(string[] args)
        {
            proveedorNegocio = new ProveedorNegocio();
            ventaNegocio = new VentaNegocio();
            productoNegocio = new ProductoNegocio();
            clienteNegocio = new ClienteNegocio();


            Console.WriteLine("Bienvenido a Cai 2023 - Gestion de Stock!\n");
            Console.ReadLine();

            UsuarioGenerico.MenuPrincipal();
        }
    }

    internal class UsuarioGenerico
    {
        private static UsuarioGenerico usuario = new UsuarioGenerico();
        private static UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
        public static string usuarioIngresado;
        public static int intentosFallidos = 0;


        public static void MenuPrincipal()
        {
            Console.WriteLine("Presione - 1 - para Iniciar sesión");
            Console.WriteLine("Presione - 0 - para Salir del Sistema");

            int valor;
            valor = Validaciones.PedirInt("\nSeleccione la opcion 1 para Iniciar:", 0, 1);
            Console.Clear();
            do
            {
                switch (valor)
                {
                    case 0:
                        Console.WriteLine("Muchas gracias por usar el sistema!!\nPresiona una tecla para salir");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;
                    case 1:
                        Console.WriteLine("Presione una tecla para continuar con el login");
                        string usuariologin = Validaciones.PedirStr("Ingrese su nombre de usuario:");
                        string contraseñalogin = Validaciones.PedirStr("Ingrese su Contraseña:");
                        Login(usuariologin,contraseñalogin);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }

            }
            while (valor != 0);
        }


        public static void Login(string usuariologin,string contraseñalogin)
        {
            if (intentosFallidos < 3) 
            {
                Login login = new Login(usuariologin, contraseñalogin);

                bool existeyvalida = usuarioNegocio.ComparaUsuarios(usuariologin, contraseñalogin);

                if (existeyvalida == true)
                {
                    string usuarioingresado = usuarioNegocio.Login(login);
                    int host = usuarioNegocio.TraerhostUsuario(usuarioingresado); //devolver Host para setear perfil

                    if (host == 3) //supongo que 3 es admin
                    {
                        Administrador.MenuAdministrador();
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    else
                    if (host == 2) //supongo que 2 es supervisor
                    {
                        Supervisor.MenuSupervisor();
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    else
                    if (host == 1) //supongo que 1 es vendedor
                    {
                        Vendedor.MenuVendedor();
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("El inicio de sesion no fue exitoso vuelva a intentarlo");
                    intentosFallidos++;
                }
            }
            else
            {
                Console.WriteLine("Se cerrara el programa");
                Console.WriteLine("Se desactivara su usuario por intentos no exitosos");
                usuarioNegocio.Desactivarusuario(usuariologin);
                Environment.Exit(0);
            } 
        }
    }
    internal class Supervisor : UsuarioGenerico
    {
        private static Supervisor sup = new Supervisor();
        private static ProductoNegocio productoNegocio;
        private static VentaNegocio ventaNegocio;
        private static ClienteNegocio clientenegocio;

        public static void MenuSupervisor()
        {
            {
                Console.WriteLine("Esta en el menu Supervisor");
                Console.WriteLine("Ingrese una opcion");
                Console.WriteLine("1 - Alta de productos"); 
                Console.WriteLine("2 - Modificación de productos");
                Console.WriteLine("3 - Baja de productos");
                Console.WriteLine("4 - Devolucion "); //unica para supervisor.
                Console.WriteLine("5 - Reporte de stock critico");
                Console.WriteLine("6 - Reporte de ventas por Vendedor");
                Console.WriteLine("7 - Reporte de productos mas vendidos");

                string opcionMenuAdmin;
                opcionMenuAdmin = Validaciones.PedirStr("Ingrese la opcion requerida");

                switch (opcionMenuAdmin)
                {
                    case "1":
                        AltaProductos();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "2":
                        BajaProductos();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "3":
                        ModificacionProductos();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "4":
                        Devolucion();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "5":
                        Reportestock();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "6":
                        Reportedeventas();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "7":
                        Reporteproductosporcat();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        internal static void AltaProductos()
        {
            Console.WriteLine("Ingresar los productos a dar el Alta");
            do
            {
                int Categoria = Validaciones.PedirInt("Ingrese una categoria de producto", 1, 5);
                string Nombre = Validaciones.PedirStr("Ingrese un nombre para el producto:");
                int precio = Validaciones.PedirInt("Ingrese un precio para el producto:", 1, 1000000000);
                int stock = Validaciones.PedirInt("Ingrese un stock para el producto:", 1, 50000);
                int cuit = Validaciones.PedirInt("Ingrese el Cuit del proveedor:", 1, 10);

                productoNegocio.CrearProducto(Categoria, Nombre, precio, stock, cuit);

                // Pregunto al usuario si desea agregar otro producto.
                string respuesta = Validaciones.PedirStr("¿Desea agregar otro producto? (S/N)");

                if (respuesta.ToLower() != "s")
                {
                    break; 
                }
            } while (true);
        }

        internal static void BajaProductos()
        {
            bool confirmacion = Validaciones.ValidarSN("¿Está seguro de realizar la baja del producto? (S/N)");

            if (confirmacion)
            {
                Guid productoaeliminar = Validaciones.PedirGuid("Ingrese el Id Unico de producto a eliminar");
                productoNegocio.EliminarProducto(productoaeliminar);
                Console.WriteLine("Producto eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("Operación cancelada. Pulse una tecla para continuar");
            }
        }

        internal static void ModificacionProductos()
        {
            do
            {
                Guid idproducto = Validaciones.PedirGuid("Ingrese el ID del producto a modificar:");
                double precio = Validaciones.PedirDouble("Ingrese el nuevo precio del producto:", 1, 1000000000);
                int stock = Validaciones.PedirInt("Ingrese el nuevo stock del producto:", 1, 50000);

                productoNegocio.ActualizarProducto(idproducto, precio, stock);

                string respuesta = Validaciones.PedirStr("¿Desea modificar otro producto? (S/N)");

                if (respuesta.ToLower() != "s")
                {
                    break; 
                }
            } while (true);
        }

        private static void Devolucion()
        {
            Guid idVenta = Validaciones.PedirGuid("Ingrese el id de la venta a devolver");
            Guid idUsuario = Validaciones.PedirGuid("Ingrese el id de su usuario vendedor"); //Se podria tomar de data del login , tiene que ser supervisor
            ventaNegocio.Devolucion(idVenta, idUsuario);
        }

        internal static void Reporteproductosporcat()
        {
            Console.WriteLine("El reporte de productos por categoria es el siguiente");
            List<Producto> listaproductos = productoNegocio.TraerProductosporCat();
            Console.WriteLine(listaproductos);
        }

        internal static void Reportestock()
        {
            Console.WriteLine("El Reporte de productos es el siguiente");
            List<Producto> listaproductos = productoNegocio.TraerProductos();
            foreach (Producto p in listaproductos)
            {
                Console.WriteLine("El producto" + p.IdProducto + "tiene" + p.Stock + "unidades restantes");
            }
        }

        internal static List<Venta> Reportedeventas()
        {
            Console.WriteLine("El Reporte de ventas es el siguiente");

            List<Cliente> clientes = clientenegocio.TraerClientes();
            List<Venta> ventasclientes = new List<Venta>();

            foreach (Cliente c in clientes)
            {
                List<Venta> ventasClienteActual = ventaNegocio.TraerVenta(c.IdCliente);
                ventasclientes.AddRange(ventasClienteActual);
                Console.WriteLine($"Ventas para el cliente {c.Nombre}:");
                foreach (Venta v in ventasClienteActual)
                {
                    Console.WriteLine($"Fecha: {v.FechaAlta}");
                }
                Console.WriteLine(); 
            }
            Console.WriteLine("Resumen de todas las ventas:");
            return ventasclientes;
        }
    }

    internal class Vendedor : UsuarioGenerico
    {
        private static VentaNegocio ventaNegocio;

        public static void MenuVendedor()
        {
            Console.WriteLine("Esta en el menu Vendedor");
            Console.WriteLine("Ingrese una opcion");
            Console.WriteLine("1 - Alta de Ventas");
            Console.WriteLine("2 - Reporte de Ventas");

            string opcionMenuAdmin;
            opcionMenuAdmin = Validaciones.PedirStr("Ingrese la opcion requerida");

            switch (opcionMenuAdmin)
            {
                case "1":
                    Registrarventas();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "2":
                    //ReportedeVentasporVendedor(); no desarrollado 
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
        }
         public static void Registrarventas()
            {
                Guid idCliente = Validaciones.PedirGuid("Ingrese el identificador de Cliente");
                Guid idProducto = Validaciones.PedirGuid("Ingrese el id de producto a registrar");
                Guid idVendedor = Validaciones.PedirGuid("Ingrese el id de vendedor:");//este se podria traer del login 
                int Cantidad = Validaciones.PedirInt("Ingrese una Cantidad para la venta:", 1, 100000);

                bool ingresar = Validaciones.ValidarSN("Está a punto de cargar la venta, está de acuerdo?");

                if (ingresar)
                {
                    ventaNegocio.RegistrarVenta(idCliente, idProducto, idVendedor, Cantidad);
                    Console.WriteLine("Venta ingresada correctamente! Pulse una tecla para continuar");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Ha decidido no ingresar la venta. Pulse una tecla para continuar");
                    Console.ReadKey();
                }
            }
        }

    internal class Administrador : Supervisor
    {
        private static UsuarioNegocio usuarioNegocio;
        private static ProveedorNegocio proveedorNegocio;
        private static VentaNegocio ventaNegocio;

        public static void MenuAdministrador()
        {
            Console.WriteLine("Esta en el menu Administrador");
            Console.WriteLine("Ingrese una opcion");

            Console.WriteLine("1 - Alta de usuario");
            Console.WriteLine("2 - Modificación de usuarios");
            Console.WriteLine("3 - Baja de usuarios");
            Console.WriteLine("4 - Alta de Proveedores");
            Console.WriteLine("5 - Modificación de Proveedores");
            Console.WriteLine("6 - Baja de Proveedores");
            Console.WriteLine("7 - Alta de Productos ");
            Console.WriteLine("8 - Modificación de Productos ");
            Console.WriteLine("9 - Baja de Productos");
            Console.WriteLine("10 - Reporte de stock crítico");
            Console.WriteLine("11 - Reporte de ventas por vendedor ");
            Console.WriteLine("12 - Reporte de productos más vendido por categoría");

            string opcionMenuAdmin;
            opcionMenuAdmin = Validaciones.PedirStr("Ingrese la opcion requerida");

            switch (opcionMenuAdmin)
            {
                case "1":
                    AgregarUsuario();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "2":
                    BajaDeUsuario();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "3":
                    //ModificacionUsuario(); //nofigura en swagger pero seria un patch de usuario con datos a modificar
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "4":
                    AltaProveedor();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "5":
                    ModificacionProveedor();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "6":
                    BajaProveedor();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "7":
                    Supervisor.AltaProductos();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "8":
                    Supervisor.ModificacionProductos();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "9":
                    Supervisor.BajaProductos();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "10":
                    Supervisor.Reportestock();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "11":
                    //Reportesvtasporvendedor(); no desarrollado 
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "12":
                    Supervisor.Reporteproductosporcat();
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
        }

        private static void AgregarUsuario()
        {
            Guid idUsuario = Validaciones.PedirGuid("Ingrese el id del usuario:");
            int host = Validaciones.PedirInt("Ingrese el perfil de usuario", 1, 2);
            string nombre = Validaciones.PedirStr("Ingrese un nombre para el usuario:");
            string apellido = Validaciones.PedirStr("Ingrese un apellido para el usuario:");
            int dni = Validaciones.PedirInt("Ingrese un Dni para el usuario", 1, 99999999);
            string direccion = Validaciones.PedirStr("Ingrese una dirección para el usuario:");
            int telefono = Validaciones.PedirInt("Ingrese un numero de teléfono para el usuario:", 1, 999999999);
            string email = Validaciones.PedirEmail("Ingrese un email para el usuario");
            DateTime fechaNacimiento = Validaciones.PedirFecha("Ingrese fecha de nacimiento");
            string nombreusuario = Validaciones.PedirStr("Ingrese el nombre del usuario:");
            string contraseña = Validaciones.PedirStr("Ingrese una contraseña para el usuario");

            bool ingresar;

            Usuario usuario = new Usuario(idUsuario, host, nombre, apellido, direccion, telefono, email, fechaNacimiento, nombreusuario, dni, contraseña);
            Console.Clear();
            ingresar = Validaciones.ValidarSN("Está a punto de ingresar el usuario, está de acuerdo?");
            if (ingresar)
            {
                usuarioNegocio.AgregarUsuario(usuario);
                Console.WriteLine("Usuario ingresado correctamente! Pulse una tecla para continuar");
                Console.ReadKey();

            }
            else
            {
                Console.WriteLine("Ha decidido no ingresar el usuario. Pulse una tecla para continuar");
                Console.ReadKey();
            }
        }

        private static void BajaDeUsuario()
        {
            string nombreusuario = Validaciones.PedirStr("Ingrese el nombre del usuario a eliminar:");
            bool ingresar;

            ingresar = Validaciones.ValidarSN("Está a punto de eliminar el usuario, está de acuerdo?");

            if (ingresar)
            {
                usuarioNegocio.EliminarUsuario(nombreusuario);
                Console.WriteLine("Usuario eliminado correctamente! Pulse una tecla para continuar");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Ha decidido no eliminar el usuario. Pulse una tecla para continuar");
                Console.ReadKey();
            }
        }
        private static void AltaProveedor()
        {
            Guid id = Validaciones.PedirGuid("Ingrese el Id de Proveedor");
            string nombre = Validaciones.PedirStr("Ingrese un nombre o razon social para el Proveedor:");
            string apellido = Validaciones.PedirStr("Ingrese un apellido para el proveedor:");
            DateTime fechaAlta = DateTime.Now;
            int cuit = Validaciones.PedirInt("Ingrese un email para el proveedor", 1, 3);
            string email = Validaciones.PedirEmail("Ingrese un email para el proveedor");
            Guid idusuario = Validaciones.PedirGuid("Ingrese un id de usuario que carga el pedido");

            bool ingresar = Validaciones.ValidarSN("Está a punto de modificar el usuario, está de acuerdo?");

            if (ingresar)
            {
                Proveedor proveedor = new Proveedor(id, idusuario, nombre, fechaAlta, cuit, email, apellido);
                proveedorNegocio.CrearProveedor(proveedor);
                proveedor.FechaAlta = DateTime.Now;
                Console.WriteLine("Usuario ingresado correctamente! Pulse una tecla para continuar");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Ha decidido no ingresar el usuario. Pulse una tecla para continuar");
                Console.ReadKey();
            }
        }

        private static void BajaProveedor()
        {
            int CuitProveedor = Validaciones.PedirInt("Ingrese el cuit del proveedor a eliminar", 1, 10);
            proveedorNegocio.EliminarProveedor(CuitProveedor);
        }

        private static void ModificacionProveedor()
        {
            string nombre = Validaciones.PedirStr("Ingrese un nuevo nombre para el proveedor:");
            string apellido = Validaciones.PedirStr("Ingrese un nuevo apellido para el proveedor:");
            string email = Validaciones.PedirEmail("Ingrese un nuevo mail para el proveedor");
            int cuit = Validaciones.PedirInt("Ingrese un nuevo Cuit para el proveedor", 1, 3);
            proveedorNegocio.ActualizarProveedor(nombre, apellido, email, cuit);
        }

        /*
         void ModificacionUsuario()
         {
             Guid id = Validaciones.PedirGuid("Ingrese el id del usuario:");
             string direccion = Validaciones.PedirStr("Ingrese una dirección para el usuario:");
             int telefono = Validaciones.PedirInt("Ingrese un numero de teléfono para el usuario:", 1, 999999999);
             string email = Validaciones.PedirEmail("Ingrese un email para el usuario");

             bool ingresar;

             ingresar = Validaciones.ValidarSN("Está a punto de modificar el usuario, está de acuerdo?");

             if (ingresar)
             {
                 usuarioNegocio.
                 //Usuario usuarioamodificar = new Usuario(id, "", "", direccion, telefono, email, default, default, "", 0, 0, ""); //ver si esta bien que le pase info parcial
                 //modificarusuario
             }
             else
             {
                 Console.WriteLine("Ha decidido no modificar el usuario. Pulse una tecla para continuar");
                 Console.ReadKey();
             }
         }
        */
    }
}

