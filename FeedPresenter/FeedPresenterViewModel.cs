using FeedPresenter.Services;
using System;
using System.Windows.Media.Imaging;

namespace FeedPresenter
{
    public class FeedPresenterViewModel : FeedPresenterBase
    {
        public IFeedDownloader Downloader { get; }
        public IFeedItemContentSelector FeedItemContent { get; }

        private BitmapImage _imageBackground;
        private BitmapImage _imageThumb;
        private string _feedTitle;
        private string _feedSummary;

        private FeedPresenterLoadedAction _loadAction;

        public FeedPresenterViewModel()
        {
            Downloader = (IFeedDownloader)FeedServiceProvider.GetService(typeof(IFeedDownloader));
            FeedItemContent = (IFeedItemContentSelector)FeedServiceProvider.GetService(typeof(IFeedItemContentSelector));
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
            FeedTitle = FeedItemContent.GetTitlePlainText(Downloader.FirstPost);
            FeedSummary = FeedItemContent.GetSummaryPlainText(Downloader.FirstPost);
              
            if (FeedItemContent.PostHasImage(Downloader.FirstPost))
            {
                ImageBackground = ImageThumb = FeedItemContent.GetPostImage(Downloader.FirstPost);
            }
        }
    }
}
