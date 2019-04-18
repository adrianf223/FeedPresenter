using System;

namespace FeedPresenter
{
    public class FeedPresenterLoadedAction : IFeedPresenterLoadedAction
    {
        public Action LoadedAction { get; set; }

        public FeedPresenterLoadedAction()
        {
        }

        public FeedPresenterLoadedAction(Action action)
        {
            LoadedAction = action;
        }

        public void OnWindowLoaded()
        {
            LoadedAction?.Invoke();
        }
    }
}
