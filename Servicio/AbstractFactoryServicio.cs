using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipoQ22.Servicio.Interfaz;

namespace EquipoQ22.Servicio
{
   abstract class AbstractFactoryServicio
    {
        public abstract IServicio CrearServicio();
    }
}
