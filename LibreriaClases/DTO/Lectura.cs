using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClases.DTO
{
    public class Lectura
    {
        private int      idMedidor;
        private DateTime fecha;
        private int      lectura;

        public int      IdMedidor { get { return idMedidor; } set { idMedidor = value; } }
        public DateTime Fecha     { get { return fecha; } set { fecha = value; } }
        public int      Valor   { get { return lectura; } set { lectura = value; } }
            
    }
}
