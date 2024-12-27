using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;





namespace proy001.Modelo 
{
    public class ModEmpresa : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private string _emp_codigo = string.Empty;
        private string _emp_descri = string.Empty;
        private string _emp_estado = string.Empty;
        private bool  _emp_graba;


        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
       
        public string EMP_CODIGO
        {
            get => _emp_codigo;
            set 
            {
                if (_emp_codigo!= value)
                {
                    _emp_codigo = value;
                    ValidaEmp_Codigo();
                    OnPropertyChanged();
                }
            }
        }

        public string EMP_DESCRI
        {
            get => _emp_descri;
            set
            {
                if (_emp_descri != value)
                {
                    _emp_descri = value.ToUpper();
                    ValidaEmp_Descri();
                    OnPropertyChanged();
                }
            }
        }

        public string EMP_ESTADO
        {
            get => _emp_estado;
            set
            {
                if (_emp_estado != value)
                {
                    _emp_estado = value.ToUpper();
                    ValidaEmp_Estado();
                    OnPropertyChanged();
                }
            }
        }

        public bool EMP_GRABA
        {
            get => _emp_graba;
            set
            {
                if (_emp_graba != value)
                {
                    _emp_graba = value;
                    OnPropertyChanged();
                }
            }
        }




        // Validaciones 

        private void ValidaEmp_Codigo()
        {
            ClearErrors(nameof(EMP_CODIGO));
            if (string.IsNullOrWhiteSpace(EMP_CODIGO))
            {
                AddError(nameof(EMP_CODIGO), "No puede estar vacío.");
            }
            
            else if (EMP_CODIGO.Length != 3)
            {
                AddError(nameof(EMP_CODIGO), "Debe tener 3 dígitos.");
            }
        }

        public void ValidaEmp_Descri()
        {

            ClearErrors(nameof(EMP_DESCRI));

            if (string.IsNullOrWhiteSpace(EMP_DESCRI)) 
            {
                AddError(nameof(EMP_DESCRI), "No Vacio.");
            }
          
            else if (EMP_DESCRI.Length < 3)
            {
                AddError(nameof(EMP_DESCRI), "Minimo 3 Caracteres.");
            }
 
        }        

        public void ValidaEmp_Estado()
        {
            ClearErrors(nameof(EMP_ESTADO));

            if ((string.IsNullOrWhiteSpace(EMP_ESTADO)) || string.IsNullOrEmpty(EMP_ESTADO))
            {
                AddError(nameof(EMP_ESTADO), "No debe estar vacio");
            }
            else if ((EMP_ESTADO != "S") && (EMP_ESTADO != "N"))
            {
                AddError(nameof(EMP_ESTADO), "Debe ser S o N");
            }
        }

       
        //******************************************************************************************************************

        // Implementación de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Implementación de INotifyDataErrorInfo
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool HasErrors => _errors.Count > 0;

        public IEnumerable GetErrors(string propertyName)
        {
            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
        }

        public void AddError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
            {
                _errors[propertyName] = new List<string>();
            }
            if (!_errors[propertyName].Contains(error))
            {
                _errors[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        public void ClearErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        protected void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }

}
   


//}
///* 
//    [dbo].[EMPRESA](
//	[EMP_CODIGO] [varchar](3) NOT NULL,
//	[EMP_DESCRI] [varchar](50) NOT NULL,
//	[EMP_ESTADO] [varchar](1) NOT NULL, 
// */