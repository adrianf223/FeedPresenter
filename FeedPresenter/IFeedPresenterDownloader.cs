using System;
using System.ServiceModel.Syndication;

namespace FeedPresenter
{
    public interface IFeedPresenterDownloader
    {
        SyndicationFeed GetSourceFeed(string theUrl);
        Uri GetPostImageUrl(SyndicationItem item);
        bool HasImage(SyndicationItem item);
    }
}
