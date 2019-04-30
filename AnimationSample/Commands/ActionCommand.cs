using AnimationSample.ViewModels;
using System;
using System.Windows.Input;

namespace AnimationSample.Commands
{
    public class ActionCommand : ViewModel<ActionCommand>, ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Func<object, bool> CanExecuteAction { get; set; }
        public bool CanExecute(object parameter) => CanExecuteAction != null ? CanExecuteAction.Invoke(parameter) : true;

        public Action<object> ExecuteAction { get; set; }
        public void Execute(object parameter)
        {
            ExecuteAction?.Invoke(parameter);
        }
    }
}
