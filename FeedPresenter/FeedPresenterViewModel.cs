using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Windows.Media.Imaging;

namespace FeedPresenter
{
    public class FeedPresenterViewModel : FeedPresenterBase
    {
        const string TheUrl = "https://entwickler.de/windowsdeveloper/feed/rss2";
        public IFeedPresenterDownloader Downloader { get; }

        SyndicationFeed _downloadedFeed;
        SyndicationItem _firstPost;
        Uri imgUrl;

        private BitmapImage _imageBackground;
        private BitmapImage _imageThumb;
        private string _feedTitle;
        private string _feedSummary;

        private FeedPresenterLoadedAction _loadAction;

        public FeedPresenterViewModel()
        {
            Downloader = (IFeedPresenterDownloader)FeedServiceProvider.GetService(typeof(IFeedPresenterDownloader));

        }

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


        private Action OnLoadAction() => () => UpdateFeedDisplay();

        public void UpdateFeedDisplay()
        {
            _downloadedFeed = Downloader.GetSourceFeed(TheUrl);
            _firstPost = _downloadedFeed.Items.FirstOrDefault();

            FeedTitle = _firstPost.Title.Text;
            FeedSummary = FeedPresenterHelper.HtmlToPlainText(_firstPost.Summary.Text);

            if (Downloader.HasImage(_firstPost))
            {
                imgUrl = Downloader.GetPostImageUrl(_firstPost);
                ImageBackground = new BitmapImage(imgUrl);
                ImageThumb = new BitmapImage(imgUrl);
            }
        }
    }
}
