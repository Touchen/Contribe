using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContribeTest.Command
{
    class NoArgCommands:BasicCommand
    {
        private Action _Execute;
        private Func<bool> _CanExecute;

        /// <summary>
        /// This creates the Command functions for the button.
        /// </summary>
        /// <param name="aCanExecute"></param>
        /// <param name="aExecute"></param>
        public NoArgCommands(Func<bool> aCanExecute, Action aExecute)
        {
            this._Execute = aExecute;
            this._CanExecute = aCanExecute;
        }
        
        /// <summary>
        /// Checks if it can be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {
            return _CanExecute();
        }

        /// <summary>
        /// Executes the event
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            _Execute();
        }
    }
}
