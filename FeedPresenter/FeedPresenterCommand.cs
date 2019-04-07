using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FeedPresenter
{
    public class FeedPresenterCommand : ICommand
    {
        private readonly Action _feedPresenterAction;

        public FeedPresenterCommand(Action feedPresenterAction)
        {
            _feedPresenterAction = feedPresenterAction;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            _feedPresenterAction?.Invoke();
        }         
        
        #pragma warning disable 67
        public event EventHandler CanExecuteChanged;
        #pragma warning restore 67
    }
}
