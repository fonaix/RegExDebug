using System;
using System.Windows.Input;

namespace RegExDebug.ViewModel
{
    public class RelayCommand<T> : ICommand
    {
        #region 字段
        readonly Func<T, Boolean> _canExecute;
        readonly Action<T> _execute;


        #endregion

        #region 构造函数
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {

        }
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }
            _canExecute = canExecute;
            _execute = execute;
        }
        #endregion

        #region ICommand 成员
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }
            if (parameter == null && typeof(T).IsValueType)
            {
                return _canExecute(default(T));
            }
            return _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
        #endregion
    }
}
