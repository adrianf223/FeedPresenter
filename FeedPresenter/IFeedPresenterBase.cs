﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FeedPresenter
{
    public interface IFeedPresenterBase
    {
        event PropertyChangedEventHandler PropertyChanged;

        bool Set<T>(ref T field, T value, [CallerMemberName] string member = null);
    }
}