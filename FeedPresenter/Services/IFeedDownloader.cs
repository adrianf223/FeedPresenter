using System.ServiceModel.Syndication;

namespace FeedPresenter.Services
{
    public interface IFeedDownloader
    {
        SyndicationFeed GetSourceFeed();
        SyndicationItem FirstPost { get; }
    }
}
