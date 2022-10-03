using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipoQ22.Domino
{
    public class Equipo
    {
        public string Pais { get; set; }
        public string DirectorTecnico { get; set; }
        public List<Jugador> Ljugadores { get; set; }
        public Equipo()
        {
            Ljugadores = new List<Jugador>();
        }
        public void Agregar(Jugador jugador)
        {
            Ljugadores.Add(jugador);
        
        }
        public void Quitar(int indice)
        {
            Ljugadores.RemoveAt(indice);
        }
    }
}
