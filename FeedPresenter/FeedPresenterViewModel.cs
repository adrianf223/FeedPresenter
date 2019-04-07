using System;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml;

namespace FeedPresenter
{
    public class FeedPresenterViewModel : FeedPresenterBase
    {
        string _theUrl = "https://entwickler.de/windowsdeveloper/feed/rss2";
        SyndicationFeed downloadFeed;
        SyndicationItem firstPost;
        Uri imgUrl;


        private FeedPresenterLoadedAction _loadAction;
        private BitmapImage _imageBackground;
        private BitmapImage _imageThumb;
        private string _feedTitle;
        private string _feedSummary;

        public BitmapImage ImageBackground
        {
            get { return _imageBackground; }
            set
            {
                _imageBackground = value;
                ThisPropertyIsChanged(nameof(ImageBackground));
            }
        }

        public BitmapImage ImageThumb
        {
            get { return _imageThumb; }
            set
            {
                _imageThumb = value;
                ThisPropertyIsChanged(nameof(ImageThumb));
            }
        }

        public String FeedTitle
        {
            get { return _feedTitle; }
            set
            {
                _feedTitle = value;
                ThisPropertyIsChanged(nameof(FeedTitle));
            }
        }

        public String FeedSummary
        {
            get { return _feedSummary; }
            set
            {
                _feedSummary = value;
                ThisPropertyIsChanged(nameof(FeedSummary));
            }
        }

        public FeedPresenterLoadedAction LoadAction => _loadAction ?? (
            _loadAction = new FeedPresenterLoadedAction(OnLoadAction())
            );

        private Action OnLoadAction()
        {
            return () =>
            {
                GetSourceFeed();
                GetFirstItemData();
                UpdateFeedDisplay();

                //MessageBox.Show("Loaded!");
            };
        }

        private void GetSourceFeed()
        {
            using (var webClientInstance = new WebClient())
            {
                webClientInstance.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                var result = webClientInstance.DownloadData(new Uri(_theUrl));
                var utf8 = new UTF8Encoding();
                var webdata = utf8.GetString(result);

                using (StringReader stringReaderInstance = new StringReader(webdata.ToString()))
                {
                    var reader = XmlReader.Create(stringReaderInstance);
                    downloadFeed = SyndicationFeed.Load(reader);
                }
            }
        }

        private void GetFirstItemData()
        {
            firstPost = downloadFeed.Items.FirstOrDefault();
            imgUrl = firstPost.Links.FirstOrDefault(link => link.RelationshipType.Contains("enclosure"))?.Uri;
        }

        private bool isImagePresent() => imgUrl != null;

        private void UpdateFeedDisplay()
        {
            FeedTitle = firstPost.Title.Text;
            FeedSummary = FeedPresenterHelper.HtmlToPlainText(firstPost.Summary.Text);

            if (isImagePresent())
            {
                ImageBackground = new BitmapImage(imgUrl);
                ImageThumb = new BitmapImage(imgUrl);
            }
        }
    }
}
