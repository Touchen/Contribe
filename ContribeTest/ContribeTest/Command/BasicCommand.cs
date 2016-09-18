using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ContribeTest.Command
{
    class BasicCommand:ICommand
    {
        /// <summary>
        /// used to see if it is able to be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public virtual bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    
        /// <summary>
        /// executes the event 
        /// </summary>
        /// <param name="parameter"></param>
        public virtual void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

    }
}
