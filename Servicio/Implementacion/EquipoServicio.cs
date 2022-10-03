using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipoQ22.Domino;
using EquipoQ22.Servicio.Interfaz;
using EquipoQ22.Datos.Implementacion;
using EquipoQ22.Datos;

namespace EquipoQ22.Servicio.Implementacion
{
    public class EquipoServicio : IServicio
    {
        private IEquipoDao dao;
        public EquipoServicio()
        {
            dao = new EquipoDao();
        }
        public List<Persona> CargarPersonas()
        {
            return dao.ObtenerPersonas();
        }

        public bool ConfirmarPresupuesto(Equipo nuevo)
        {
            return dao.CrearEquipo(nuevo);
        }
    }
}
