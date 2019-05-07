using System.ServiceModel.Syndication;
using System.Windows.Media.Imaging;

namespace FeedPresenter
{

    public interface IFeedPresenterItem
    {
        string FeedTitle { get;  }
        string FeedSummary { get;  }
        BitmapImage Image { get;  }
        bool HasImage { get; }

        FeedPresenterItem Load(SyndicationItem item);
    }
}
