using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Operacion
    {
        public int IdOperacion { get; set; }
        public string Numero { get; set; }
        public int Resultado { get; set; }
        public DateTime Fecha { get; set; }
        public List<object> Operaciones { get; set; }
        public ML.Usuario Usuario { get; set; }

        public Operacion() { }

        public Operacion(int idOperacion, string Numero, int Resultado, int idUsuario, string userName, DateTime fecha) 
        {
            this.IdOperacion= idOperacion;
            this.Numero = Numero;
            this.Resultado = Resultado;
            this.Fecha= fecha;
            Usuario = new ML.Usuario();
            this.Usuario.IdUsuario = idUsuario;
            this.Usuario.UserName = userName;
            
        }
    }
}
