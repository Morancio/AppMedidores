using LibreriaClases.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClases.DAL
{
    public class LecturaArchivoDAL : ILecturaDAL
    {

        private LecturaArchivoDAL()
        {

        }

        private static LecturaArchivoDAL instance;

        public static LecturaArchivoDAL GetInstance()
        {
            if (instance == null)
            {
                instance = new LecturaArchivoDAL();
            }
            return instance;
        }

        private static string archivo = "Lecturas.txt";
        private static string ruta = Directory.GetCurrentDirectory() + "/" + archivo;

        public void AgregarLectura(Lectura lectura)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(ruta, true))
                {
                    string texto = lectura.IdMedidor + "|" + lectura.Fecha + "|" + lectura.Valor + "|";
                    sw.WriteLine(texto);
                    sw.Flush();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al escribir el archivo...");
            }
        }

        public List<Lectura> ObtenerLecturas()
        {
            List<Lectura> lecturas = new List<Lectura>();
            string texto;
            try
            {
                using (StreamReader reader = new StreamReader(ruta))
                    do
                    {
                        texto = reader.ReadLine();
                        if (texto != null)
                        {
                            string[] textoArr = texto.Trim().Split('|');

                            int idMedidor = Convert.ToInt32(textoArr[0]);
                            DateTime fecha = Convert.ToDateTime(textoArr[1]);
                            int valor = Convert.ToInt32(textoArr[2]);

                            Lectura lectura = new Lectura()
                            {
                                IdMedidor = idMedidor,
                                Fecha = fecha,
                                Valor = valor
                            };

                            lecturas.Add(lectura);
                        }
                    } while (texto != null);
            }
            catch (Exception ex)
            {

            }
            return lecturas;
        }

        public List<Lectura> LecturasByMedidorId(int id)
        {
            return ObtenerLecturas().FindAll(lecturas => lecturas.IdMedidor == id);
        }


    }
}
