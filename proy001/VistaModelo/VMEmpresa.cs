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
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar las empresas: " + ex.Message);
            }

            return empresas;
        }

        public void CreaEmpresa(ModEmpresa pEmpresa)
        {
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
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear la empresa: " + ex.Message);
            }   
        }

        public void EliminaEmpresa(string pCodigo)
        {
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
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar la empresa: " + ex.Message);
            }
        }
    }
}