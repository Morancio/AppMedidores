using LibreriaClases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaClases.DAL
{
    public interface IMedidorDAL
    {
        void AgregarMedidor(Medidor medidor);

        List<Medidor> ObtenerMedidores();

        Medidor MedidorById(int id);

        bool Existe(int id);
    }
}
