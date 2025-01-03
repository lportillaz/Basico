using Microsoft.Identity.Client;
using proy001.Modelo;
using System;
using System.Collections.Generic;
using proy001.clases;
using Microsoft.IdentityModel.Tokens;




namespace proy001
{
    public class Program
    {
       private VMEmpresa Ovmempresa;

        public Program()
        {
            Ovmempresa = new VMEmpresa();
        }

        private void eliminarEmpresa(int pinicio, int pfin)
        {
            if (pinicio < 0 || pfin < 0 || pinicio >= pfin)
            {
                throw new ArgumentException("Rango inválido para pinicio y pfin.");
            }

            for (int i = pinicio; i < pfin; i++)
            {
                try
                {
                    Ovmempresa.EliminaEmpresa(i.ToString("D3"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error eliminando empresa con código {i.ToString("D3")}: {ex.Message}");
                }
            }
        }

        private void crearEmpresa(int pinicio, int pfin)
        {
            if (pinicio < 0 || pfin < 0 || pinicio >= pfin)
            {
                throw new ArgumentException("Rango inválido para pinicio y pfin.");
            }

            ModEmpresa _empresa = new ModEmpresa();
            for (int i = pinicio; i < pfin; i++)
            {
                _empresa.EMP_CODIGO = i.ToString("D3");
                _empresa.EMP_DESCRI = $"EMPRESA {i}";
                _empresa.EMP_ESTADO = "S";
                try
                {
                    Ovmempresa.CreaEmpresa(_empresa);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creando empresa con código {i.ToString("D3")}: {ex.Message}");
                }
            }
        }

        private void leerTabla()
        {
            try
            {
                var mlista = Ovmempresa.ConsultarEmpresas();
                foreach (var item in mlista)
                {
                    Console.WriteLine($" {item.EMP_CODIGO} == {item.EMP_DESCRI} == {item.EMP_ESTADO}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error leyendo tabla: {ex.Message}");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hola Mundo");

            Program miPrograma = new Program();
            // miPrograma.eliminarEmpresa(200, 210);
            // miPrograma.crearEmpresa(215, 220);
            //miPrograma.leerTabla();
            miPrograma.EjemploDatabaseHelper();
            miPrograma.Ovmempresa.leerTabla();
        }

            private void EjemploDatabaseHelper()
            {
                
                // Crear una instancia de DatabaseHelper
                DatabaseHelper dbHelper = new DatabaseHelper();

                // Ejemplo de inserción
                string insertCommand = "INSERT INTO EMPRESA (EMP_CODIGO, EMP_DESCRI, EMP_ESTADO) VALUES (@EMP_CODIGO, @EMP_DESCRI, @EMP_ESTADO)";
                Dictionary<string, object> insertParams = new Dictionary<string, object>
                {
                    { "@EMP_CODIGO", "301" },
                    { "@EMP_DESCRI", "Empresa de Ejemplo" },
                    { "@EMP_ESTADO", "Activo" }
                };
                bool insertResult = dbHelper.EjecutarComandoConParametros(insertCommand, insertParams);
                Console.WriteLine(insertResult ? "Registro insertado exitosamente." : "Error al insertar el registro.");

                // Ejemplo de actualización
                string updateCommand = "UPDATE EMPRESA SET EMP_DESCRI = @EMP_DESCRI, EMP_ESTADO = @EMP_ESTADO WHERE EMP_CODIGO = @EMP_CODIGO";
                Dictionary<string, object> updateParams = new Dictionary<string, object>
                {
                    { "@EMP_CODIGO", "011" },
                    { "@EMP_DESCRI", "Empresa Actualizada" },
                    { "@EMP_ESTADO", "Inactivo" }
                };
                bool updateResult = dbHelper.EjecutarComandoConParametros(updateCommand, updateParams);
                Console.WriteLine(updateResult ? "Registro actualizado exitosamente." : "Error al actualizar el registro.");

                // Ejemplo de eliminación
                string deleteCommand = "DELETE FROM EMPRESA WHERE EMP_CODIGO = @EMP_CODIGO";
                Dictionary<string, object> deleteParams = new Dictionary<string, object>
                {
                    { "@EMP_CODIGO", "013" }
                };
                bool deleteResult = dbHelper.EjecutarComandoConParametros(deleteCommand, deleteParams);
                Console.WriteLine(deleteResult ? "Registro eliminado exitosamente." : "Error al eliminar el registro.");
                
            }
        
    }
}   