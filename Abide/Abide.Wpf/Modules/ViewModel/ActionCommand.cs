using System;
using System.Windows.Input;

namespace Abide.Wpf.Modules.ViewModel
{
    /// <summary>
    /// Represents a simple action command.
    /// </summary>
    public sealed class ActionCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public Action<object> ExecuteAction { get; } = null;
        public Func<object, bool> CanExecuteFunction { get; } = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCommand"/> class.
        /// </summary>
        public ActionCommand() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCommand"/> class using the specified execute action.
        /// </summary>
        /// <param name="executeAction">The action to be performed when the command is exectued.</param>
        public ActionCommand(Action<object> executeAction)
        {
            ExecuteAction = executeAction;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCommand"/> class using the specified execute action and can execute function.
        /// </summary>
        /// <param name="executeAction">The action to be performed when the command is executed.</param>
        /// <param name="canExecuteFunction">The function to be used to determine whether or not this command can execute.</param>
        public ActionCommand(Action<object> executeAction, Func<object, bool> canExecuteFunction)
        {
            ExecuteAction = executeAction;
            CanExecuteFunction = canExecuteFunction;
        }
        public void Execute(object parameter = null)
        {
            ExecuteAction?.Invoke(parameter);
        }
        public bool CanExecute(object parameter = null)
        {
            return CanExecuteFunction?.Invoke(parameter) ?? ExecuteAction != null;
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
