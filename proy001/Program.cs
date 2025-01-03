using proy001.Modelo;


namespace proy001
{
    class Program
    {
        static void Main(string[] args){
            Console.WriteLine("Hola Mundo");
         //    Pruebaclases misclases = new Pruebaclases();
           // misclases.Pruebas();
           // ClaseDao miDao = new ClaseDao();

            //miDao.Conectar();

            VMEmpresa mvempresa  = new VMEmpresa();
            //for (int i = 180; i < 200; i++)
            //{
            //    mvempresa.EliminaEmpresa(i.ToString("D3"));
            //}
            var _empresa = new ModEmpresa();
            for (int i = 6; i < 200; i++)
            {
                _empresa.EMP_CODIGO = i.ToString("D3");       //$"0{i}";
                _empresa.EMP_DESCRI = $"EMPRESA {i}";
                _empresa.EMP_ESTADO = "S";
                mvempresa.CreaEmpresa(_empresa);
            }

            var mlista = mvempresa.ConsultarEmpresas();
            foreach (var item in mlista)
            {
                Console.WriteLine($" {item.EMP_CODIGO} == {item.EMP_DESCRI} == {item.EMP_ESTADO}");
               // Console.WriteLine(item.EMP_DESCRI);
               // Console.WriteLine(item.EMP_ESTADO);
            }
           
           

        }
    
      
    }
}   