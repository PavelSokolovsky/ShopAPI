using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Respons
{
    public partial class ResponceClient
    {
        public ResponceClient(Client client)
        {
            Name = client.name;
            Login = client.login;
            Password = client.password;
        }
        public string Name;
        public string Login;
        public string Password;
        
    }
}