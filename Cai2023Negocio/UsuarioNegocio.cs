using System;
using Cai2023Datos;
using Cai2023Entidades;
using System.Timers;

namespace Cai2023Negocio
{
    public class UsuarioNegocio
    {
        public readonly UsuarioDatos usuarioDatos = new UsuarioDatos();

        public List<Usuario> TraerUsuarios()
        {
            List<Usuario> usuarios = usuarioDatos.TraerUsuariosActivos();
            return usuarios;
        }

        public int TraerhostUsuario(string usuarioAIniciar)
        {
            int rol = -1; // inicializo en -1, suponiendo que no hay usuario

            List<Usuario> usuarios = TraerUsuarios();

            foreach (Usuario u in usuarios)
            {
                if (u.NombreUsuario == usuarioAIniciar)
                {
                    rol = u.Host;
                    break;
                }
            }
            return rol;
        }

        public bool ComparaUsuarios(string usuarioAIniciar, string contraseñaUsuario)
        {
            List<Usuario> usuarios = TraerUsuarios();
            bool usuarioEncontrado = false;

            foreach (Usuario u in usuarios)
            {
                if (u.NombreUsuario == usuarioAIniciar && u.Contraseña == contraseñaUsuario)
                {
                    DateTime fechaContraseña = u.UltimaActualizacionClave;
                    DateTime hoy = DateTime.Now;
                    TimeSpan diff = hoy - fechaContraseña;
                    double dias = diff.TotalDays;

                    if (dias >= 30)
                    {
                        Console.WriteLine("Su contraseña ha expirado");
                        string nuevaContraseña = "CAI20232";
                        Console.WriteLine("Se asignó la contraseña definitiva CAI20232");
                        // Assuming usuarioDatos.CambiarContraseña is a valid method
                        usuarioDatos.CambiarContraseña(usuarioAIniciar, contraseñaUsuario, nuevaContraseña);
                    }
                    return true;
                }
            }

            // If the loop completes and no matching user is found
            Console.WriteLine("Inicio de sesion fallido");
            return false;
        }

        /*
        if (!usuarioEncontrado)
        {
            Console.WriteLine("Inicio de sesión fallido");
            intentosFallidos++;

            if (intentosFallidos == 3)
            {
                Console.WriteLine("Demasiados intentos fallidos. El usuario ha sido inactivado");
                foreach (Usuario u in usuarios)
                {
                    if (u.NombreUsuario == usuarioAIniciar)
                    {
                        u.Estado = "INACTIVO";
                        break;
                    }
                }
            }
            return false;
        }
        return usuarioEncontrado;
        */

        public void Desactivarusuario(string usuarioAIniciar)
        {
            List<Usuario> usuarios = TraerUsuarios();

            foreach (Usuario u in usuarios)
            {
                if (u.NombreUsuario == usuarioAIniciar)
                {
                    u.Estado = "INACTIVO";
                    break;
                }
            }
        }

        public string Login(Login login)
        {
            string idusuario=usuarioDatos.Login(login);
            return idusuario;
        }

        public void AgregarUsuario(Usuario usuario)
        {
            if (UsuarioValido(usuario) && Contraseñavalida(usuario))
            {
                usuarioDatos.Agregarusuario(usuario);
                usuario.FechaAlta = DateTime.Now;
            }
            else
            {
                Console.WriteLine("Nombre usuario o contraseña invalida");
            }
        }

        public static bool UsuarioValido(Usuario usuario)
        {
            return usuario.NombreUsuario.Length >= 8 && usuario.NombreUsuario.Length <= 15
                && !usuario.Nombre.Contains(usuario.NombreUsuario)
                && !usuario.Apellido.Contains(usuario.NombreUsuario);
        }

        public static bool Contraseñavalida(Usuario usuario)
        {
            return usuario.Contraseña.Length >= 8 && usuario.Contraseña.Length <= 15
                && usuario.Contraseña.Any(char.IsUpper)
                && usuario.Contraseña.Any(char.IsDigit);
        }

        public void EliminarUsuario(string nombreusuario)
        {
            {
                List<Usuario> usuarios = TraerUsuarios();

                foreach (Usuario u in usuarios)
                {
                    if (u.NombreUsuario == nombreusuario)
                    {
                        usuarioDatos.EliminarUsuario(u.Id.ToString());
                    }
                }
            }
        }

        public void ReactivarUsuario(string nombreusuario)
        {
                
                List<Usuario> usuarios = usuarioDatos.TraerUsuariosActivos();

                foreach (Usuario u in usuarios)
                {
                    if (u.NombreUsuario == nombreusuario)
                    {
                        usuarioDatos.ReactivarUsuario(u.Id.ToString());
                    }
                }
        }
    }
}

