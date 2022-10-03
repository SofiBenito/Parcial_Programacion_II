using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EquipoQ22.Domino;

namespace EquipoQ22.Datos
{
   public class HelperDao
    {
        private static HelperDao instancia;
        private SqlConnection cnn;
        private SqlCommand cmd;

        public HelperDao()
        {
            cnn = new SqlConnection("Data Source=localhost;Initial Catalog=Qatar2022;Integrated Security=True");
            cmd = new SqlCommand();
        }
        public static HelperDao ObtenerInstancia()
        {
            if(instancia==null)
            {
                instancia = new HelperDao();
            }
            return instancia;
        }
        public DataTable ConsultarPersonas(string NombreSP)
        {
            cnn.Open();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = NombreSP;
            DataTable tabla = new DataTable();
            tabla.Load(cmd.ExecuteReader());
            cmd.Parameters.Clear();
            cnn.Close();
            return tabla;
        }
        public bool AgregarEquipo(Equipo oEquipo,string MaestroSP,string DetalleSP)
        {
            bool okey = false;
            SqlTransaction transaction = null;
            try
            {
                cnn.Open();
                transaction = cnn.BeginTransaction();
                SqlCommand cmdMaestro = new SqlCommand(MaestroSP, cnn, transaction);
                cmdMaestro.CommandType = CommandType.StoredProcedure;
                cmdMaestro.Parameters.AddWithValue("@pais", oEquipo.Pais);
                cmdMaestro.Parameters.AddWithValue("@director_tecnico", oEquipo.DirectorTecnico);

                SqlParameter pOUT = new SqlParameter();
                pOUT.ParameterName = "@id";
                pOUT.DbType = DbType.Int32;
                pOUT.Direction = ParameterDirection.Output;
                cmdMaestro.Parameters.Add(pOUT);
                cmdMaestro.ExecuteNonQuery();
                int IdEquipo = (int)pOUT.Value;

                SqlCommand cmdDetalle = null;
                foreach(Jugador jugador in oEquipo.Ljugadores)
                {
                    cmdDetalle = new SqlCommand(DetalleSP, cnn, transaction);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@id_equipo",IdEquipo);
                    cmdDetalle.Parameters.AddWithValue("@id_persona",jugador.Persona.IdPersona);
                    cmdDetalle.Parameters.AddWithValue("@camiseta",jugador.Camiseta);
                    cmdDetalle.Parameters.AddWithValue("@posicion",jugador.Posicion);
                    cmdDetalle.ExecuteNonQuery();
                }
                transaction.Commit();
                okey = true;
            }
            catch (Exception)
            {

                transaction.Rollback();
                okey = false;
            }
            finally
            {
                if(cnn!=null && cnn.State==ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return okey;

        }

    }
}
