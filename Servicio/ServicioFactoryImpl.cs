using EquipoQ22.Servicio.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipoQ22.Servicio.Implementacion;

namespace EquipoQ22.Servicio
{
    class ServicioFactoryImpl : AbstractFactoryServicio
    {
        public override IServicio CrearServicio()
        {
            return new EquipoServicio();
        }
    }
}
