using LibreriaClases.DAL;
using LibreriaClases.DTO;
using ServidorMedidor.Comunicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ServidorMedidor.Comunicacion
{
    public class Conexion
    {
        private Socket      socketCliente;
        private ILecturaDAL lecturaDAL;
        private IMedidorDAL medidorDAL;

        public Conexion(Socket socketCliente, ILecturaDAL lecturaDAL, IMedidorDAL medidorDAL)
        {
            this.socketCliente = socketCliente;
            this.lecturaDAL    = lecturaDAL;
            this.medidorDAL    = medidorDAL;
        }

        public void ejecutar(object obj)
        {
            int id;
            int valor;
            DateTime fecha;
            string serverMsg;
            bool pass;
            
            Thread thread      = (Thread) obj;
            Lectura lec        = new Lectura();
            ClienteCom cliente = new ClienteCom(socketCliente);

            Console.WriteLine("Cliente Conectado.");
            Console.WriteLine("Hash Socket: " + this.Check(socketCliente));
            Console.WriteLine("Hash Thread: " + thread.GetHashCode());
            
            
            //Lecturas
            List<Lectura> lista = lecturaDAL.ObtenerLecturas();
            cliente.Escribir(lista.Count.ToString());

            for(int i = 0; i < lista.Count; i++)
            {
                Lectura actual = lista[i];
                cliente.Escribir ("ID:" + actual.IdMedidor + "|" + " Fecha: " + actual.Fecha + "|" + " Valor: " + actual.Valor);
            }
            

            //ID Medidor       
            do
            {
                cliente.Escribir("1. Ingrese ID del Medidor: ");      //Escribe un mensaje al cliente
                pass = Int32.TryParse(cliente.Leer().Trim(), out id); //Verifica si el mensaje recibido es un int
            } while (!pass);
            lec.IdMedidor = Convert.ToInt32(id); //Agrega el valor retornado a la lectura

            //Fecha Medidor
            do
            {
                cliente.Escribir("2. Ingrese Fecha (Formato: dd-mm-yyyy hh-mm-ss): ");
                pass = DateTime.TryParse(cliente.Leer().Trim(), out fecha);
            } while (!pass);
            lec.Fecha = Convert.ToDateTime(fecha);

            //Valor Medidor 
            do
            {
                cliente.Escribir("3. Ingrese Valor de lectura: ");
                pass = Int32.TryParse(cliente.Leer().Trim(), out valor);
            } while (!pass);
            lec.Valor = Convert.ToInt32(valor);

            if (medidorDAL.Existe(id))
            {
                lecturaDAL.AgregarLectura(lec);
                serverMsg = (lec.IdMedidor + "|" + lec.Fecha + "|" + lec.Valor);
            }
            else
            {
                serverMsg = ("Error, el medidor no existe");
            }

            cliente.Escribir(serverMsg);
            cliente.Desconectar();
            thread.Abort();
        }

        public int Check(Socket socketCliente)
        {
            return socketCliente.GetHashCode();
        }
    }
}