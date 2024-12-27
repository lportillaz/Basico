using System;
using Microsoft.Data.SqlClient;


namespace proy001.clases
{
    public class ClaseDao
    {
        public void Conectar()
        {
            
            string cadena = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=dbaplicaciones;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False" ;
            using (SqlConnection bdCadenaConexion = new (cadena))
            {
                try
                {
                   bdCadenaConexion.Open();
                    Console.WriteLine("Conectado");
                    bdCadenaConexion.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al conectar a la base de datos: " + ex.Message);
                }
            }

        }

        public static SqlConnection BDConectarSql()
        {
            try
            {
                string cadena = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=dbaplicaciones;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
                SqlConnection bdCadenaConexion = new SqlConnection(cadena);
                bdCadenaConexion.Open();
                return bdCadenaConexion;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                return null;
            }
        }




        public bool BdEjecutar(string pRegistor)
        {
            bool lRet = true;
            try
            {
                SqlConnection BdConexion = BDConectarSql();
                BdConexion.Open();
                SqlCommand bdInstrucion = new SqlCommand(pRegistor, BdConexion);
                bdInstrucion.Connection = BdConexion; // bdCadenaConexionrSql();
                bdInstrucion.ExecuteNonQuery();
                bdInstrucion.Connection.Close();
                BdConexion.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Error en Ejecutar Instruccion en Base de Datos");
            }
            return lRet;
        }

        
    }
}