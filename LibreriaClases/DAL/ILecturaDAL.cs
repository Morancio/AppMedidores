using LibreriaClases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClases.DAL
{
    public interface ILecturaDAL
    {
        void AgregarLectura(Lectura lectura);

        List<Lectura> ObtenerLecturas();

        List<Lectura> LecturasByMedidorId(int id);
    }
}
