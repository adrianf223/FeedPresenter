using System.ServiceModel.Syndication;
using System.Threading.Tasks;

namespace FeedPresenter
{
    public interface IFeedPresenterDownloader
    {
        Task<SyndicationItem> GetFirstPostAsync();
        Task<SyndicationFeed> GetSourceFeedAsync();
    }
}
