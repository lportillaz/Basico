using Microsoft.Data.SqlClient;
using proy001.clases;
using proy001.Modelo;

namespace proy001
{
    public class VMEmpresa
    {
        ModEmpresa empresa = new ModEmpresa();

        public List<ModEmpresa> ConsultarEmpresas()
        {
            List<ModEmpresa> empresas = new List<ModEmpresa>();
            try
            {
                using (SqlConnection conn = ClaseDao.BDConectarSql())
                {
                    if (conn != null)
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT EMP_CODIGO, EMP_DESCRI, EMP_ESTADO FROM EMPRESA", conn))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    ModEmpresa Oempresa = new ModEmpresa
                                    {
                                        EMP_CODIGO = reader.GetString(0),
                                        EMP_DESCRI = reader.GetString(1),
                                        EMP_ESTADO = reader.GetString(2)
                                    };

                                    empresas.Add(Oempresa);
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No se pudo establecer la conexión con la base de datos.");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error de SQL al consultar las empresas: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Operación inválida al consultar las empresas: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar las empresas: " + ex.Message);
            }

            return empresas;
        }

        public void leerTabla()
        {
            try
            {
                using (SqlConnection conn = ClaseDao.BDConectarSql())
                {
                    if (conn != null)
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT EMP_CODIGO, EMP_DESCRI, EMP_ESTADO FROM EMPRESA", conn))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine(reader.GetString(0) + " " + reader.GetString(1) + " " + reader.GetString(2));
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No se pudo establecer la conexión con la base de datos.");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error de SQL al leer la tabla: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Operación inválida al leer la tabla: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer la tabla: " + ex.Message);
            }
        }

        public void CreaEmpresa(ModEmpresa pEmpresa)
        {
            if (pEmpresa == null)
            {
                throw new ArgumentNullException(nameof(pEmpresa));
            }

            try
            {
                using (SqlConnection conn = ClaseDao.BDConectarSql())
                {
                    if (conn != null)
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO EMPRESA (EMP_CODIGO, EMP_DESCRI, EMP_ESTADO) VALUES (@EMP_CODIGO, @EMP_DESCRI, @EMP_ESTADO)", conn))
                        {
                            cmd.Parameters.AddWithValue("@EMP_CODIGO", pEmpresa.EMP_CODIGO);
                            cmd.Parameters.AddWithValue("@EMP_DESCRI", pEmpresa.EMP_DESCRI);
                            cmd.Parameters.AddWithValue("@EMP_ESTADO", pEmpresa.EMP_ESTADO);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No se pudo establecer la conexión con la base de datos.");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error de SQL al crear la empresa: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Operación inválida al crear la empresa: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear la empresa: " + ex.Message);
            }
        }

        public void EliminaEmpresa(string pCodigo)
        {
            if (string.IsNullOrWhiteSpace(pCodigo))
            {
                throw new ArgumentException("El código de la empresa no puede ser nulo o vacío.", nameof(pCodigo));
            }

            try
            {
                using (SqlConnection conn = ClaseDao.BDConectarSql())
                {
                    if (conn != null)
                    {
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM EMPRESA WHERE EMP_CODIGO = @EMP_CODIGO", conn))
                        {
                            cmd.Parameters.AddWithValue("@EMP_CODIGO", pCodigo);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No se pudo establecer la conexión con la base de datos.");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error de SQL al eliminar la empresa: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Operación inválida al eliminar la empresa: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar la empresa: " + ex.Message);
            }
        }
    }
}