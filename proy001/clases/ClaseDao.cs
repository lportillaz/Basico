using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
namespace proy001.clases
{
    public class ClaseDao
    {
        private static IConfiguration Configuration { get; set; }

        static ClaseDao()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public static SqlConnection BDConectarSql()
        {
            try
            {
                string cadena = Configuration.GetConnectionString("DefaultConnection");
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

        public void Conectar()
        {
            string cadena = Configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection bdCadenaConexion = new SqlConnection(cadena))
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

        public bool BdEjecutar(string pRegistor)
        {
            bool lRet = true;
            try
            {
                SqlConnection BdConexion = BDConectarSql();
                BdConexion.Open();
                SqlCommand bdInstrucion = new SqlCommand(pRegistor, BdConexion);
                bdInstrucion.Connection = BdConexion;
                bdInstrucion.ExecuteNonQuery();
                bdInstrucion.Connection.Close();
                BdConexion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Error en Ejecutar Instruccion en Base de Datos");
                lRet = false;
            }
            return lRet;
        }

        public DataTable EjecutarConsulta(string consulta)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = BDConectarSql())
                {
                    using (SqlCommand cmd = new SqlCommand(consulta, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
            }
            return dt;
        }

        public DataTable EjecutarConsultaConParametros(string consulta, Dictionary<string, object> parametros)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = BDConectarSql())
                {
                    using (SqlCommand cmd = new SqlCommand(consulta, conn))
                    {
                        foreach (var param in parametros)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
            }
            return dt;
        }

        public object EjecutarEscalar(string consulta)
        {
            try
            {
                using (SqlConnection conn = BDConectarSql())
                {
                    using (SqlCommand cmd = new SqlCommand(consulta, conn))
                    {
                        return cmd.ExecuteScalar();
                    }
                }
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

        public bool EjecutarComandoConParametros(string comando, Dictionary<string, object> parametros)
        {
            try
            {
                using (SqlConnection conn = BDConectarSql())
                {
                    using (SqlCommand cmd = new SqlCommand(comando, conn))
                    {
                        foreach (var param in parametros)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                return false;
            }
        }

        public void CerrarConexion(SqlConnection conn)
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}