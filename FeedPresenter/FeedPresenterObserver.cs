using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FeedPresenter
{
    public class FeedPresenterObserver :
        INotifyPropertyChanged, IFeedPresenterObserver
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public FeedPresenterObserver()
        {

        }

        private void ThisPropertyIsChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public bool Set<T>(ref T field, T value, [CallerMemberName] string member = null)
        {
            if (Equals(field, value))
            {
                return false;
            }

            field = value;
            ThisPropertyIsChanged(member);
            return true;
        }
    }
}
