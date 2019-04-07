using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
