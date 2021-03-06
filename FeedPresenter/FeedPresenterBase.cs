﻿using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FeedPresenter
{
    public abstract class FeedPresenterBase : INotifyPropertyChanged
    {
        private readonly IServiceCollection _services;

        public ServiceProvider FeedServiceProvider { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ThisPropertyIsChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        protected bool Set<T>(ref T field, T value, [CallerMemberName] string member = null)
        {
            if (Equals(field, value))
            {
                return false;
            }

            field = value;
            ThisPropertyIsChanged(member);
            return true;
        }


        protected FeedPresenterBase()
        {
            _services = new ServiceCollection();                       
            _services.AddSingleton<IFeedPresenterDownloader>(new FeedPresenterDownloader());
            _services.AddSingleton<IFeedPresenterItem>(new FeedPresenterItem());


            FeedServiceProvider = _services.BuildServiceProvider();
        }
    }
}
