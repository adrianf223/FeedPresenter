using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Windows.Media.Imaging;

namespace FeedPresenter.Services
{

    public class FeedItemContentSelector : IFeedItemContentSelector
    {
        public Uri GetPostImageUrl(SyndicationItem item)
        {
            var imgUrl = item.Links.FirstOrDefault(
                link => link.RelationshipType.Contains("enclosure"))?.Uri;

            return imgUrl;
        }

        public BitmapImage GetPostImage(SyndicationItem item)
        {
            var imgurl = GetPostImageUrl(item);
            return new BitmapImage(imgurl) ;
        }

        public string GetSummaryPlainText(SyndicationItem item)
        {
            return FeedPresenterHelper.HtmlToPlainText(item.Summary.Text);
        }

        public string GetTitlePlainText(SyndicationItem item)
        {
            return FeedPresenterHelper.HtmlToPlainText(item.Title.Text);
        }

        public bool PostHasImage(SyndicationItem item)
        {
            return GetPostImageUrl(item) != null;
        }
    }
}
