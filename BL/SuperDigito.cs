using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SuperDigito
    {
        public static int Calcular(string numero) 
        {
            char[] numeros = numero.ToCharArray();
            int resultado = 0;

            if (int.Parse(numero) < 10)
            {
                resultado = int.Parse(numero);
                return resultado;
            }

            else 
            {
                foreach (var suma in numeros)
                {
                    string nuevoNum = string.Join(",", suma);
                    resultado = int.Parse(nuevoNum) + resultado;
                }

                if(resultado < 10) return resultado;
                
                else return Calcular(resultado.ToString());
            }
        }

    }
}
