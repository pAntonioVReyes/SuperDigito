using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLConsola
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Ingresar un numero entero");
            string numero = Console.ReadLine();
            Console.Clear();

            int resultado = BL.SuperDigito.Calcular(numero);

            Console.WriteLine("El Super digito es: " + resultado);
            Console.ReadKey();
        }
    }
}
