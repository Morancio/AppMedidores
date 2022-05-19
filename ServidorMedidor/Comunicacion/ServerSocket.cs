using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServidorMedidor.Comunicacion
{
    public class ServerSocket
    {
        private int puerto;
        private String ip;
        private Socket servidor;

        public ServerSocket(int puerto, String ip)
        {
            this.puerto = puerto;
            this.ip = ip;
        }

        public bool Iniciar()
        {
            try
            {
                this.servidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.servidor.Bind(new IPEndPoint(IPAddress.Parse(ip), this.puerto));
                this.servidor.Listen(10);
                return true;
            }
            catch (SocketException ex)
            {
                return false;
            }
        }

        public Socket ObtenerCliente()
        {
            return this.servidor.Accept();
        }

        public void Cerrar()
        {
            try
            {
                this.servidor.Close();
            }
            catch (Exception)
            {

            }
        }
    }
}
