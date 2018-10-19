﻿using System;
using System.Windows.Input;

namespace Project_TIIK_WPF
{
    public class RelayCommand : ICommand, IDisposable
    {
        public event EventHandler CanExecuteChanged;

        private Func<bool> canExecuteFunc;
        private Action executeAction;
        private Action<object> execute;

        public RelayCommand(Action executeAction)
            : this(executeAction, () => true)
        {
        }

        public RelayCommand(Action executeAction, Func<bool> canExecuteFunc)
        {
            this.executeAction = executeAction;
            this.canExecuteFunc = canExecuteFunc;
        }

        public RelayCommand(Action<object> execute)
        {
            this.execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecuteFunc();
        }

        public void Dispose()
        {
            RemoveAllEvents();
        }

        public void Execute(object parameter)
        {
            executeAction();
        }

        public void RemoveAllEvents()
        {
            CanExecuteChanged = null;
        }

    }
}
