using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteTool.Commands
{
    /// <summary>
    /// Delegate Command
    /// </summary>
    /// <remarks>Reserved</remarks>
    public class DelegateCommand : DelegateCommand<object>
    {
        public DelegateCommand(Action executeMethod) : base(o => executeMethod())
        {
        }

        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod) : base(o => executeMethod(), o => canExecuteMethod())
        {
        }
    }
}
