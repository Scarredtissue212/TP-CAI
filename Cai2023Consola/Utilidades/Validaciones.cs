using System;
using System.Text.RegularExpressions;
using Cai2023Negocio;
using Cai2023Entidades;
using System.Collections.Generic;

namespace CAI2023Consola.Utilidades
{
    public class Validaciones
    {
        private const string _errorDatosNoCargados = "Los datos aun no fueron cargados. Presione una tecla para volver";
        public static string ErrorDatosNoCargados
        {
            get { return _errorDatosNoCargados; }
        }

        public static int PedirInt(string mensaje, int min, int max)
        {
            bool valido = false;
            string mensError = "Debe ingresar un valor entre " + min + " y " + max;
            int valor;
            do
            {
                Console.WriteLine(mensaje);
                if (!int.TryParse(Console.ReadLine(), out valor))
                {
                    Console.WriteLine(mensError);
                }
                else
                {
                    if (valor < min || valor > max)
                    {
                        Console.WriteLine(mensError);
                    }
                    else
                    {
                        valido = true;
                    }
                }
            } while (!valido);
            return valor;
        }
        public static string PedirStr(string mensaje)
        {
            string valor;
            do
            {
                Console.WriteLine(mensaje);
                valor = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(valor))
                {
                    Console.WriteLine("Este campo no puede ser vacio");
                }
            } while (string.IsNullOrWhiteSpace(valor));

            return valor;
        }

        public static string PedirEmail(string mensaje)
        {
            string valor;

            do
            {
                Console.WriteLine(mensaje);
                valor = Console.ReadLine();

                if (!EmailValido(valor))
                {
                    Console.WriteLine("Ingrese un email válido");
                }

            } while (!EmailValido(valor));

            return valor;
        }
        public static float PedirFloat(string mensaje, float min, float max)
        {
            bool valido = false;
            string mensError = "Debe ingresar un valor entre " + min + " y " + max;
            float valor;
            do
            {
                Console.WriteLine(mensaje);
                if (!float.TryParse(Console.ReadLine(), out valor))
                {
                    Console.WriteLine(mensError);
                }
                else
                {
                    if (valor < min || valor > max)
                    {
                        Console.WriteLine(mensError);
                    }
                    else
                    {
                        valido = true;
                    }
                }
            } while (!valido);
            return valor;
        }

        public static long PedirLong(string mensaje, long min, long max)
        {
            bool valido = false;
            string mensError = "Debe ingresar un valor entre " + min + " y " + max;
            long valor;
            do
            {
                Console.WriteLine(mensaje);
                if (!long.TryParse(Console.ReadLine(), out valor))
                {
                    Console.WriteLine(mensError);
                }
                else
                {
                    if (valor < min || valor > max)
                    {
                        Console.WriteLine(mensError);
                    }
                    else
                    {
                        valido = true;
                    }
                }
            } while (!valido);
            return valor;
        }
        public static long PedirDouble(string mensaje, double min, double max)
        {
            bool valido = false;
            string mensError = "Debe ingresar un valor entre " + min + " y " + max;
            long valor;
            do
            {
                Console.WriteLine(mensaje);
                if (!long.TryParse(Console.ReadLine(), out valor))
                {
                    Console.WriteLine(mensError);
                }
                else
                {
                    if (valor < min || valor > max)
                    {
                        Console.WriteLine(mensError);
                    }
                    else
                    {
                        valido = true;
                    }
                }
            } while (!valido);
            return valor;
        }

        public static bool ValidarSN(string mensaje)
        {
            bool valido = false;
            string valor;
            bool confirmado = false;
            string indicaciones = "Debe ingresar \"S\" para confirmar y \"N\" para rechazar";
            do
            {
                Console.WriteLine(mensaje);
                Console.WriteLine(indicaciones);
                valor = Console.ReadLine().ToUpper();
                if (string.IsNullOrWhiteSpace(valor) || (valor != "S" && valor != "N"))
                {
                    Console.WriteLine(indicaciones);
                }
                else if (valor == "S")
                {
                    confirmado = true;
                    valido = true;
                }
                else
                {
                    confirmado = false;
                    valido = true;
                }

            } while (!valido);
            return confirmado;
        }

        public static DateTime PedirFecha(string mensaje)
        {
            bool valido = false;
            DateTime fecha;

            do
            {
                string mensError = "Por favor ingrese una fecha en formato válido \"dd/mm/AAAA\"";

                Console.WriteLine(mensaje);
                if (!DateTime.TryParse(Console.ReadLine(), out fecha))
                {
                    Console.WriteLine(mensError);
                }
                else
                {
                    valido = true;
                }
            } while (!valido);

            return fecha;
        }

        public static Guid PedirGuid(string mensaje)
        {
            bool valido = false;
            Guid guid;

            do
            {
                string mensError = "Por favor ingrese una fecha en formato válido \"dd/mm/AAAA\"";

                Console.WriteLine(mensaje);
                if (!Guid.TryParse(Console.ReadLine(), out guid))
                {
                    Console.WriteLine(mensError);
                }
                else
                {
                    valido = true;
                }
            } while (!valido);

            return guid;
        }
        private static bool EmailValido(string email)
        {
            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, patron);
        }
    }
}