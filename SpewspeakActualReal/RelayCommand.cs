using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpewspeakActualReal
{
    class RelayCommand : ICommand
    {
        #region Members
        private Func<string, string> _func;

        public event EventHandler CanExecuteChanged;
        #endregion

        #region Constructors
        public RelayCommand(Func<string, string> func)
        {
            _func = func;
        }
        #endregion

        #region Methods
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public string Execute(object parameter)
        {
            if (parameter != null)
            {
                return _func(parameter.ToString());
            }
            else
            {
                throw new ArgumentNullException("parameter");
            }
        }
        #endregion
    }
}
