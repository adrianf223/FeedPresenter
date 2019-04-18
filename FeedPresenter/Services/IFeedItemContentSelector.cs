using System;
using System.ServiceModel.Syndication;
using System.Windows.Media.Imaging;

namespace FeedPresenter.Services
{

    public interface IFeedItemContentSelector
    {
        Uri GetPostImageUrl(SyndicationItem item);
        BitmapImage GetPostImage(SyndicationItem item);
        bool PostHasImage(SyndicationItem item);
        string GetSummaryPlainText(SyndicationItem item);
        string GetTitlePlainText(SyndicationItem item);
    }
}
