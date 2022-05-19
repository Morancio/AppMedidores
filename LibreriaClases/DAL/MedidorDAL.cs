using LibreriaClases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClases.DAL
{
    public class MedidorDAL : IMedidorDAL
    {
        private MedidorDAL()
        {

        }

        private static MedidorDAL instance;

        public static MedidorDAL GetInstance()
        {
            if (instance == null)
            {
                instance = new MedidorDAL();
            }
            return instance;
        }


        //1. Crear lista insertar medidores
        private static List<Medidor> medidores = new List<Medidor>();

        //2. Operaciones
        public void AgregarMedidor(Medidor medidor)
        {
            medidores.Add(medidor);
        } //Agregar

        public List<Medidor> ObtenerMedidores()
        {
            return medidores;
        }     //Mostrar todos los medidores

        public Medidor MedidorById(int id)
        {     
            return medidores.Find(medidor => medidor.IdMedidor == id); ;
        }          //Mostrar un medidor según la id ingresada
        
        public bool Existe(int id)
        {
            if (medidores.Contains(MedidorById(id))){
                return true;
            }
            else
            {
                return false;
            }
        }                  //Checkea si un monitor existe
    }
}
