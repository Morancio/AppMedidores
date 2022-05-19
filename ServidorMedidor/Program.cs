using LibreriaClases.DAL;
using LibreriaClases.DTO;
using ServidorMedidor.Comunicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServidorMedidor
{
    internal class Program
    {
        private static IMedidorDAL medidorDAL = MedidorDAL.GetInstance();
        private static ILecturaDAL lecturaDAL = LecturaArchivoDAL.GetInstance();

        static void Main(string[] args)
        {
            Medidor m1 = new Medidor();
            Medidor m2 = new Medidor();
            Medidor m3 = new Medidor();

            m1.IdMedidor = 1;
            m2.IdMedidor = 2;
            m3.IdMedidor = 3;

            medidorDAL.AgregarMedidor(m1);
            medidorDAL.AgregarMedidor(m2);
            medidorDAL.AgregarMedidor(m3);

            int puerto = 2050;
            String ip = "127.0.0.1";

            Console.WriteLine(@"
   _____                 _     __          
  / ___/___  ______   __(_)___/ /___  _____
  \__ \/ _ \/ ___/ | / / / __  / __ \/ ___/
 ___/ /  __/ /   | |/ / / /_/ / /_/ / /    
/____/\___/_/    |___/_/\__,_/\____/_/     
                                           
");

            Console.WriteLine("Iniciando Servidor en dirección {0} y puerto {1}", ip, puerto);
            ServerSocket servidor = new ServerSocket(puerto, ip);

            if (servidor.Iniciar())
            {
                Console.WriteLine("Sevidor iniciado");
                while (true)
                {                    
                    Socket socketCliente = servidor.ObtenerCliente();
                    
                    if (socketCliente.Connected)
                    {   
                        Conexion con  = new Conexion(socketCliente, lecturaDAL, medidorDAL);
                        Thread thread = new Thread(new ParameterizedThreadStart(con.ejecutar));                      
                        thread.Start(thread);                    
                    }                                       
                }
            }
            else
            {
                Console.WriteLine("Error, el puerto {0} esta en uso", puerto);
            }
        }
    }
}
