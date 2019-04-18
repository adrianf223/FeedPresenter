using System.Windows;

namespace FeedPresenter
{
    public class FeedPresenterBindings
    {

        public static readonly DependencyProperty LoadedEnabled =
            DependencyProperty.RegisterAttached(
                "LoadedEnabled", 
                typeof(bool), 
                typeof(FeedPresenterBindings),
                new PropertyMetadata(false, new PropertyChangedCallback(OnLoadedEnabled)));


        public static readonly DependencyProperty LoadedAction =
            DependencyProperty.RegisterAttached(
                "LoadedAction",
                typeof(IFeedPresenterLoadedAction),
                typeof(FeedPresenterBindings),
                new PropertyMetadata(null));


        public static bool GetLoadedEnabled(DependencyObject sender)
        {
            return (bool)sender.GetValue(LoadedEnabled);
        }

        public static void SetLoadedEnabled(DependencyObject sender, bool value)
        {
            sender.SetValue(LoadedEnabled, value);
        }

        public static IFeedPresenterLoadedAction GetLoadedAction(DependencyObject sender)
        {
            return (IFeedPresenterLoadedAction)sender.GetValue(LoadedAction);
        }

        public static void SetLoadedAction(DependencyObject sender, IFeedPresenterLoadedAction value)
        {
            sender.SetValue(LoadedAction, value);
        }

        private static void OnLoadedEnabled(object sender, DependencyPropertyChangedEventArgs data)
        {
            if (sender is Window window)
            {
                var propertyBeforeChange = (bool)data.NewValue;
                var propertyAfterChange = (bool)data.OldValue;

                if (propertyAfterChange && !propertyBeforeChange)
                    window.Loaded -= FeedPresenterWindowLoaded;
                else if (!propertyAfterChange && propertyBeforeChange)
                    window.Loaded += FeedPresenterWindowLoaded;
            }
        }

        private static void FeedPresenterWindowLoaded(object sender, RoutedEventArgs data)
        {
            var loadedAction = GetLoadedAction((Window)sender);
            loadedAction?.OnWindowLoaded();
        }
    }
}
