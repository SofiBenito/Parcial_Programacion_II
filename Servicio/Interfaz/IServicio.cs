using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipoQ22.Domino;

namespace EquipoQ22.Servicio.Interfaz
{
   public interface IServicio
    {
        List<Persona> CargarPersonas();
        bool ConfirmarPresupuesto(Equipo nuevo);
    }
}
