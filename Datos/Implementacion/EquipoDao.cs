using EquipoQ22.Domino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace EquipoQ22.Datos.Implementacion
{
    public class EquipoDao : IEquipoDao
    {
        public bool CrearEquipo(Equipo equipo)
        {
            return HelperDao.ObtenerInstancia().AgregarEquipo(equipo, "SP_INSERTAR_EQUIPO", "SP_INSERTAR_DETALLES_EQUIPO");
        }

        public List<Persona> ObtenerPersonas()
        {
            List<Persona> lPersona = new List<Persona>();
            DataTable tabla = HelperDao.ObtenerInstancia().ConsultarPersonas("SP_CONSULTAR_PERSONAS");
            foreach(DataRow data in tabla.Rows)
            {
                Persona persona = new Persona();
                persona.IdPersona = Convert.ToInt32(data["id_persona"].ToString());
                persona.NombreCompleto = data["nombre_completo"].ToString();
                persona.Clase = Convert.ToInt32(data["clase"].ToString());
                lPersona.Add(persona);
            }
            return lPersona;
        }
    }
}
