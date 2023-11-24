using System;
namespace Cai2023Entidades
{
    public class Login
    {
        public string Id { get; set;}
        public string Contraseña { get; set; }

    public Login(string id, string contraseña)
        {
            Id = id;
            Contraseña = contraseña;
        }
    }
}
