using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Windows.Media.Imaging;

namespace FeedPresenter
{

    public class FeedPresenterItem : IFeedPresenterItem
    {
        private SyndicationItem _item;

        public string FeedTitle { get; private set; }
        public string FeedSummary { get; private set; }
        public BitmapImage Image { get; private set; }

        public FeedPresenterItem()
        {
            _item = new SyndicationItem();
            FeedTitle = nameof(FeedTitle);
            FeedSummary = nameof(FeedSummary);
            Image = new BitmapImage();
        }

        public FeedPresenterItem Load(SyndicationItem item)
        {
            _item = item;
            FeedTitle = FeedPresenterHelper.HtmlToPlainText(_item.Title.Text);
            FeedSummary = FeedPresenterHelper.HtmlToPlainText(_item.Summary.Text);
            Image = GetImage(_item);

            return this;
        }

        private Uri GetImageUrl()
        {
            var imgUrl = _item.Links.FirstOrDefault(
                link => link.RelationshipType.Contains("enclosure"))?.Uri;

            return imgUrl;
        }

        private static BitmapImage GetImage(SyndicationItem item)
        {
            var imgUrl = item.Links.FirstOrDefault(link => 
                link.RelationshipType.Contains("enclosure"))?.Uri;

            return new BitmapImage(imgUrl);
        }

        public bool HasImage => GetImageUrl() != null;
    }
}
