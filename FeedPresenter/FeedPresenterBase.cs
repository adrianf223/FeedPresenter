using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace FeedPresenter
{
    public abstract class FeedPresenterBase : INotifyPropertyChanged
    {
        private readonly IServiceCollection _services;

        public ServiceProvider FeedServiceProvider { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void ThisPropertyIsChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }  

        protected FeedPresenterBase()
        {
            _services = new ServiceCollection();
            _services.AddSingleton<IFeedPresenterDownloader>(new FeedPresenterDownloader());


            FeedServiceProvider = _services.BuildServiceProvider();
        }
    }
}
