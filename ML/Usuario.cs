﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string UserName { get; set; }
        public string Contraseña { get; set; }
        public List<object> Usuarios { get; set; }

        public Usuario() { }

        public Usuario(int idUsuario, string userName, string Contraseña) 
        {
            this.IdUsuario = idUsuario;
            this.UserName = userName;
            this.Contraseña = Contraseña;
        }
    }
}
