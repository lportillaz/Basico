using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace proy001.clases
{
    public class DatabaseHelper
    {
        private static IConfiguration Configuration { get; set; }

        static DatabaseHelper()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }

        private static SqlConnection BDConectarSql()
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

        public object EjecutarEscalarConParametros(string consulta, Dictionary<string, object> parametros)
        {
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
    }
}
