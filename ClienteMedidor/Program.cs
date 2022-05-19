using ClienteMedidor.Comunicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClienteMedidor
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            int puerto;
            String servidor;
            bool esValido;

            //Logo
            Console.WriteLine(@"
                  __  __          _ _     _                     
                 |  \/  |        | (_)   | |                    
                 | \  / | ___  __| |_  __| | ___  _ __ ___  ___ 
                 | |\/| |/ _ \/ _` | |/ _` |/ _ \| '__/ _ \/ __|
                 | |  | |  __/ (_| | | (_| | (_) | | |  __/\__ \
                 |_|  |_|\___|\__,_|_|\__,_|\___/|_|  \___||___/ 
");
            Console.WriteLine("\nBienvenido a la aplicación para medidores.\n");

            //Obtener el servidor
            do
            {
                Console.Write("Ingrese dirección IP (127.0.0.1): ");
                servidor = Console.ReadLine().Trim();
            }
            while (servidor.Equals(string.Empty));

            //Obtener el puerto
            do
            {
                Console.Write("Ingrese puerto (2050): ");
                esValido = Int32.TryParse(Console.ReadLine().Trim(), out puerto);

            } while (!esValido);

            ClienteSocket clienteSocket = new ClienteSocket(servidor, puerto);

            Console.WriteLine("\nConectando a servidor {0} en puerto {1}...\n", servidor, puerto);

            if (clienteSocket.Conectar())
            {
                int id;
                DateTime fecha;
                int valor;
                string clienteMsg;
                string serverMsg;
                bool pass;

                Console.WriteLine("Conexión exitosa!\n");

                //Lista Lecturas
                Console.WriteLine("--- Lecturas ---");
                int count = Convert.ToInt32(clienteSocket.Leer());
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine(clienteSocket.Leer());
                }
                Console.WriteLine("--- Lecturas ---\n");

                //ID Medidor
                do
                {
                    Console.Write(clienteSocket.Leer());        //Imprime el mensaje enviado por el servidor 
                    clienteMsg = Console.ReadLine().Trim();     //Settea clienteMsg según lo escrito en consola
                    clienteSocket.Escribir(clienteMsg);         //Envia un mensaje al server
                    pass = Int32.TryParse(clienteMsg, out id);  //Verifica si el mensaje escrito es int                 
                } while (!pass);

                //Fecha Medidor
                do
                {
                    Console.Write(clienteSocket.Leer());
                    clienteMsg = Console.ReadLine().Trim();
                    clienteSocket.Escribir(clienteMsg);
                    pass = DateTime.TryParse(clienteMsg, out fecha);
                } while (!pass);

                //Valor Medidor
                do
                {
                    Console.Write(clienteSocket.Leer());
                    clienteMsg = Console.ReadLine().Trim();
                    clienteSocket.Escribir(clienteMsg);
                    pass = Int32.TryParse(clienteMsg, out valor);
                } while (!pass);

                serverMsg = clienteSocket.Leer();
                Console.WriteLine("\nEste es el mensaje del servidor: " + serverMsg);
            }
            else
            {
                Console.WriteLine("Se ha producido un error, verifique los valores ingresados.");
            }

            Console.WriteLine("\nSe ha cerrado la conexión, presione cualquier tecla para salir.");
            Console.ReadKey();
        }
    }
}
