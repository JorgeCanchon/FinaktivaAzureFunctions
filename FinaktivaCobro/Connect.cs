using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaktivaCobro
{
    public class Connect
    {
        private NpgsqlConnection conexion;

        public Connect()
        {
            string cadenaConexion = "Server=finaktivaprod.postgres.database.azure.com;Database=finaktivapyme_prod;Port=5432;User Id=postgres;Password=P0sGr3sql;";
            Conexion = new NpgsqlConnection(cadenaConexion);
        }

        private NpgsqlConnection Conexion
        {
            get { return conexion; }
            set { conexion = value; }
        }

        private void VerificarConexion()
        {
            if (Conexion.State != ConnectionState.Open)
                Conexion.Open();
        }
        public void EjecutarComando(string sql)
        {
            VerificarConexion();
            using (NpgsqlCommand command = new NpgsqlCommand(sql, Conexion))
            {
                command.ExecuteNonQuery();
            }

        }

        public int EjecutarSP(string nombreSP, string[] nombreParametro, string[] parametro)
        {
            VerificarConexion();
            using (NpgsqlCommand command = new NpgsqlCommand())
            {
                command.Connection = Conexion;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = nombreSP;
                for (int i = 0; i < nombreParametro.Length; i++)
                {
                    command.Parameters.AddWithValue(nombreParametro[i], parametro[i]);
                }
                return command.ExecuteNonQuery();
            }
        }
      
        private void desconectar()
        {
            if (Conexion.State == ConnectionState.Open)
                Conexion.Close();
        }
    }
}
